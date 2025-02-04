using AsyncAwaitBestPractices;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpotPdv.Application.Helpers;
using SpotPdv.Application.Models;
using SpotPdv.Application.Services;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Application.ViewModels
{
    public partial class ManageProductViewModel : BaseViewModel
    {
        private bool isOpenExpander;

        [ObservableProperty]
        private bool _currentProductsOpened;

        [ObservableProperty]
        private string _titleCurrentCategoryOpened;

        [ObservableProperty]
        private bool _expanderVisible;

        public ManageProductViewModel
            (
                INavigationService navigationService, IOperationStateService operationStateService
            )
            :
            base
            (
                navigationService, operationStateService
            )
        {
            ProductsFiltered = [];
            BackgroundCategory = Converters.GenerateRandomColors(100).ToObservableCollection();
            FoodIconCodes = IconsCategory;
        }

        #region Categorias

        [ObservableProperty]
        private Category _newCategories;

        [ObservableProperty]
        private ObservableCollection<Category> _categories = new ObservableCollection<Category>();

        [ObservableProperty]
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();

        [ObservableProperty]
        private bool _modeProductSelected;

        [ObservableProperty]
        private ObservableCollection<string> _chooseProductType = ["Categorias", "Produtos"];

        [ObservableProperty]
        private string _productTypeSelected;

        [ObservableProperty]
        private string _nameCategory;
        partial void OnNameCategoryChanged(string value)
        {
            AddNewCategoryCommand?.NotifyCanExecuteChanged();
        }

        [ObservableProperty]
        private BackgroundCategoryModel _colorCategorySelected;
        partial void OnColorCategorySelectedChanged(BackgroundCategoryModel value)
        {
            ExpanderVisible = false;
            AddNewCategoryCommand?.NotifyCanExecuteChanged();
        }

        [ObservableProperty]
        private bool _cardNewProduct;

        [ObservableProperty]
        private ImageModel _imageSelected;
        partial void OnImageSelectedChanged(ImageModel value)
        {
            AddNewCategoryCommand?.NotifyCanExecuteChanged();
        }

        [ObservableProperty]
        private string _iconSelected;
        partial void OnIconSelectedChanged(string value)
        {
            if (!string.IsNullOrEmpty(value)) ImageSelected = null;
            AddNewCategoryCommand?.NotifyCanExecuteChanged();
        }

        [ObservableProperty]
        private ObservableCollection<string> _foodIconCodes;

        [ObservableProperty]
        private ObservableCollection<BackgroundCategoryModel> _backgroundCategory;

        [ObservableProperty]
        private bool _visibleCategoryEdit;
        
        [ObservableProperty]
        private Category _categoryBeingEdited;
        #endregion 

        #region Products

        [ObservableProperty]
        private Product _newProduct;

        [ObservableProperty]
        private string _nameProduct;
        partial void OnNameProductChanged(string value)
        {
            AddNewProductCommand?.NotifyCanExecuteChanged();
        }

        [ObservableProperty]
        private string _reducedNameProduct;

        [ObservableProperty]
        private string _productDetails;

        [ObservableProperty]
        private string _barCode;

        [ObservableProperty]
        private Category _categorySelected;
        partial void OnCategorySelectedChanged(Category value)
        {
            AddNewProductCommand?.NotifyCanExecuteChanged();
        }

        [ObservableProperty]
        private ObservableCollection<string> _units = new ObservableCollection<string>()
        {
            "Grama",
            "Kilograma",
            "Unidade",
            "Pacote",
            "Litro"
        };

        [ObservableProperty]
        private string _unitSelected;
        partial void OnUnitSelectedChanged(string value)
        {
            AddNewProductCommand?.NotifyCanExecuteChanged();
        }

        [ObservableProperty]
        private string _productPrice;
        partial void OnProductPriceChanged(string value)
        {
            AddNewProductCommand?.NotifyCanExecuteChanged();
        }

        [ObservableProperty]
        private int _productDiscount;

        [ObservableProperty]
        private int _qtdStock;

        [ObservableProperty]
        private bool _isAvailable;

        [ObservableProperty]
        private ImageModel _productImageSelected;

        [ObservableProperty]
        private BackgroundCategoryModel _productColorSelected;

        [ObservableProperty]
        private bool _visibleProductEdit;

        [ObservableProperty]
        private Product _productsBeingEdited;
        partial void OnProductTypeSelectedChanged(string value)
        {
            ChangeViewMode(value);
        }

        #endregion

        #region Incialização e reinicialização
        protected override void InitializeViewModel()
        {
            base.InitializeViewModel();
            Task.Run(() =>
            {
                if (OperationState.CurrentCategories == null || OperationState.CurrentProducts == null)
                {
                    if (OperationState.CurrentCategories == null) OperationState.CurrentCategories = new ObservableCollection<Category>();
                    if (OperationState.CurrentProducts == null) OperationState.CurrentProducts = new ObservableCollection<Product>();
                    OperationStateService.SaveOperationState(OperationState).SafeFireAndForget();
                }
                ConteinerView = new ObservableCollection<DataContainer>();

                foreach (var category in OperationState.CurrentCategories)
                {
                    var productsCurrentCategory = new List<Product>();
                    foreach (var product in OperationState.CurrentProducts)
                    {
                        if (product.CategoryId == category.CategoryId)
                        {
                            productsCurrentCategory.Add(product);
                        }
                    }
                    ConteinerView.Add(new DataContainer() { Category = category, Products = productsCurrentCategory.ToObservableCollection() });
                }

                Categories = OperationState.CurrentCategories;
                Products = OperationState.CurrentProducts;
                ViewModelIsInitialized = true;
            });         
        }

        public override void RefreshPage(OperationStateModel operationStateModel, bool filterForAZ = false)
        {
            base.RefreshPage(operationStateModel, filterForAZ);

            if (ViewModelIsInitialized)
            {
                ConteinerView.Clear();
                var conteinerView = new List<DataContainer>();

                foreach (var category in OperationState.CurrentCategories)
                {
                    var productsCurrentCategory = new List<Product>();
                    foreach (var product in OperationState.CurrentProducts)
                    {
                        if (product.CategoryId == category.CategoryId)
                        {
                            productsCurrentCategory.Add(product);
                        }
                    }
                    conteinerView.Add(new DataContainer() { Category = category, Products = productsCurrentCategory.ToObservableCollection() });
                }

                if (filterForAZ)
                {
                    ConteinerView = conteinerView.OrderBy(x => x.Category.Name).ToObservableCollection();
                }
                else
                {
                    ConteinerView = conteinerView.OrderBy(x => x.Category.DateTimeCreate).ToObservableCollection();
                }

                Categories = OperationState.CurrentCategories;
                Products = OperationState.CurrentProducts;
            }
            else
            {
                InitializeViewModel();
            }

            MainThread.BeginInvokeOnMainThread(() => IsLoading = false);
        }
        #endregion

        private void ChangeViewMode(string value)
        {
            if (value == null) return;
            if (value.Contains("Categorias"))
            {
                ModeProductSelected = false;
            }
            else if (value.Contains("Produtos"))
            {
                ModeProductSelected = true;
            }
        }

        #region Filtro de produtos

        private CancellationTokenSource _debounceCts;

        [ObservableProperty]
        private string _productSearchText;
        partial void OnProductSearchTextChanged(string value)
        {
            OnNameFilteredProduct(value);
        }

        [ObservableProperty]
        private bool _filterProducts;

        [ObservableProperty]
        private ObservableCollection<Product> _productsFiltered;

        private void OnNameFilteredProduct(string value)
        {
            _debounceCts?.Cancel();
            _debounceCts = new CancellationTokenSource();

            if (string.IsNullOrEmpty(value))
            {
                FilterProducts = false;
                return;
            }

            _ = DebouncedFilterProducts(_debounceCts.Token);
        }

        private async Task DebouncedFilterProducts(CancellationToken token)
        {
            try
            {
                await Task.Delay(700, token);

                token.ThrowIfCancellationRequested();

                ExecuteFilterProducts();
            }
            catch (Exception ex)
            {
                Console.WriteLine($" ### ERROR ### {GetType().Name}: {ex}");
            }
        }

        private void ExecuteFilterProducts()
        {
            ProductsFiltered.Clear();
            if (!string.IsNullOrEmpty(ProductSearchText?.Trim()))
            {
                var products = OperationState.CurrentProducts
                    .Where(x => x.Name.ToUpper().Contains(ProductSearchText.ToUpper()))
                    .ToList();

                products.ForEach(ProductsFiltered.Add);
                IsLoading = false;
                if (ProductsFiltered.Count > 0) FilterProducts = true;
                else FilterProducts = false;
            }
            else
            {
                FilterProducts = false;
            }
        }

        [RelayCommand]
        private void CloseFilter()
        {
            FilterProducts = false;
            ProductSearchText = string.Empty;
        }

        #endregion

        #region Commandos de abertura

        [RelayCommand]
        private void OpenNewProduct()
        {
            CardNewProduct = !CardNewProduct;
            ModeProductSelected = true;
        }

        [RelayCommand]
        private void OpenNewCategory()
        {
            CardNewProduct = !CardNewProduct;
            ModeProductSelected = false;
            NewCategories = new Category();
            NameCategory = string.Empty;
            ImageSelected = null;
        }

        [RelayCommand]
        private void OpenCurrentProduct(DataContainer dataContainer)
        {
            CurrentProductsOpened = !CurrentProductsOpened;
            if (CurrentProductsOpened)
            {
                TitleCurrentCategoryOpened = dataContainer.Category.Name;
                Products.Clear();
                foreach (var item in dataContainer.Products)
                {
                    Products.Add(item);
                }
            }
        }

        [RelayCommand]
        private async Task OpenFile()
        {
            if (ModeProductSelected)
            {
                PickOptions options = new()
                {
                    PickerTitle = "Selecione uma imagem",
                    FileTypes = FilePickerFileType.Images
                };

                ProductImageSelected = await Converters.PickAndShow(options);
            }
            else
            {
                PickOptions options = new()
                {
                    PickerTitle = "Selecione uma imagem",
                    FileTypes = FilePickerFileType.Images
                };

                ImageSelected = await Converters.PickAndShow(options);
                if (ImageSelected != null) IconSelected = string.Empty;
            }        
        }

        #endregion

        #region Modo de edição

        [ObservableProperty]
        private Category _categorySelectedCategoryForProduct;

        [RelayCommand]
        private void OpenEditCategory(DataContainer dataContainer)
        {
            VisibleCategoryEdit = !VisibleCategoryEdit;
            if (VisibleCategoryEdit)
            {
                CategoryBeingEdited = dataContainer.Category.Clone(dataContainer.Category);
            }
        }

        [RelayCommand]
        private void OpenEditProduct(Product product)
        {
            VisibleProductEdit = !VisibleProductEdit;
            if (VisibleProductEdit)
            {
                ProductsBeingEdited = product.Clone(product);
            }
        }

        [RelayCommand]
        private async Task SaveCategory()
        {
            var updateCategory = OperationState.CurrentCategories.Where(x => x.CategoryId == CategoryBeingEdited.CategoryId).FirstOrDefault();
            updateCategory.CategoryId = CategoryBeingEdited.CategoryId;
            updateCategory.Color = CategoryBeingEdited.Color;
            updateCategory.DateTimeCreate = CategoryBeingEdited.DateTimeCreate;
            updateCategory.Icon = CategoryBeingEdited.Icon;
            updateCategory.Name = CategoryBeingEdited.Name;
            await OperationStateService.SaveOperationState(OperationState);
            VisibleCategoryEdit = false;
            RefreshPage(OperationState);
        }

        [RelayCommand]
        private async Task DeleteCategory()
        {
            var updateCategory = OperationState.CurrentCategories.Where(x => x.CategoryId == CategoryBeingEdited.CategoryId).FirstOrDefault();
            OperationState.CurrentCategories.Remove(updateCategory);
            VisibleCategoryEdit = false;
            await OperationStateService.SaveOperationState(OperationState);
            RefreshPage(OperationState);
        }

        [RelayCommand]
        private async Task SaveProduct()
        {
            var updateProduct = OperationState.CurrentProducts.Where(x => x.ProductId == ProductsBeingEdited.ProductId).FirstOrDefault();
            updateProduct.Name = ProductsBeingEdited.Name;
            updateProduct.ReducedName = ProductsBeingEdited.ReducedName;
            updateProduct.Category = CategorySelectedCategoryForProduct != null ? CategorySelectedCategoryForProduct.Name: updateProduct.Category;
            updateProduct.Description = ProductsBeingEdited.Description;
            updateProduct.Barcode = ProductsBeingEdited.Barcode;
            updateProduct.Price = ProductsBeingEdited.Price;
            updateProduct.CategoryId = CategorySelectedCategoryForProduct != null ? CategorySelectedCategoryForProduct.CategoryId: updateProduct.CategoryId;
            updateProduct.Discount = ProductsBeingEdited.Discount;
            updateProduct.StockQuantity = ProductsBeingEdited.StockQuantity;
            updateProduct.IsAvailable = ProductsBeingEdited.IsAvailable;
            updateProduct.Image = ProductsBeingEdited.Image;
            updateProduct.Color = ProductsBeingEdited.Color;
            await OperationStateService.SaveOperationState(OperationState);
            VisibleProductEdit = false;
            CurrentProductsOpened = false;
            RefreshPage(OperationState);
        }

        [RelayCommand]
        private async Task DeleteProduct()
        {
            var deleteProduct = OperationState.CurrentProducts.Where(x => x.ProductId == ProductsBeingEdited.ProductId).FirstOrDefault();
            OperationState.CurrentProducts.Remove(deleteProduct);
            VisibleProductEdit = false;
            VisibleCategoryEdit = false;
            await OperationStateService.SaveOperationState(OperationState);
            RefreshPage(OperationState);
        }

        #endregion

        #region Modo de criação

        [RelayCommand(CanExecute = nameof(CheckNewCategory))]
        private async Task AddNewCategory()
        {
            NewCategories = new Category()
            {
                CategoryId = Guid.NewGuid(),
                Name = NameCategory,
                Color = ColorCategorySelected,
                Icon = IconSelected,
                Image = ImageSelected,
                IconChoose = ImageSelected == null ? true : false,
                DateTimeCreate = DateTime.UtcNow
            };

            OperationState.CurrentCategories.Add(NewCategories);
            await OperationStateService.SaveOperationState(OperationState);
            RefreshPage(OperationState);
            CardNewProduct = false;
        }

        [RelayCommand(CanExecute = nameof(CheckNewProduct))]
        private async Task AddNewProduct()
        {
            NewProduct = new Product()
            {
                ProductId = Guid.NewGuid(),
                CategoryId = CategorySelected.CategoryId,
                Name = NameProduct,
                ReducedName = ReducedNameProduct,
                Description = ProductDetails,
                Barcode = BarCode,
                Price = Convert.ToDecimal(ProductPrice),
                Discount = ProductDiscount,
                IsAvailable = IsAvailable,
                UnitType = Converters.UnityTypeConverter(UnitSelected),
                Image = ProductImageSelected,
                Color = ProductColorSelected
            };

            OperationState.CurrentProducts.Add(NewProduct);
            await OperationStateService.SaveOperationState(OperationState);
            RefreshPage(OperationState);
            CardNewProduct = false;
        }

        private bool CheckNewCategory()
        {
            if (string.IsNullOrEmpty(NameCategory)) return false;
            if (ColorCategorySelected == null || string.IsNullOrEmpty(ColorCategorySelected.NameColor)) return false;
            if (ImageSelected == null && string.IsNullOrEmpty(IconSelected)) return false;
            return true;
        }

        private bool CheckNewProduct()
        {
            if (string.IsNullOrEmpty(NameProduct)) return false;
            if (CategorySelected == null || string.IsNullOrEmpty(UnitSelected)) return false;
            return true;
        }

        #endregion
    }
}
