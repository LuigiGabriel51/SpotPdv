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
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Application.ViewModels
{
    public partial class ManageProductViewModel : BaseViewModel
    {
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
        private bool _ColorExpaderVisible;

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
            ColorExpaderVisible = false;
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
        private decimal _productPrice;
        partial void OnProductPriceChanged(decimal value)
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

        #endregion
        partial void OnProductTypeSelectedChanged(string value)
        {
            ChangeViewMode(value);
        }

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

        protected override void InitializeViewModel()
        {
            base.InitializeViewModel();
            if (OperationState.CurrentCategories == null || OperationState.CurrentProducts == null)
            {
                if(OperationState.CurrentCategories == null)  OperationState.CurrentCategories = new ObservableCollection<Category>();
                if(OperationState.CurrentProducts == null)  OperationState.CurrentProducts = new ObservableCollection<Product>();
                OperationStateService.SaveOperationState(OperationState).SafeFireAndForget();
            }
            CategoriesView = OperationState.CurrentCategories;
            ProductsView = OperationState.CurrentProducts;
            Categories = CategoriesView;
            Products = ProductsView;
            ViewModelIsInitialized = true;
        }

        public override void RefreshPage(OperationStateModel operationStateModel)
        {
            base.RefreshPage(operationStateModel);
            if(ViewModelIsInitialized)
            {
                CategoriesView = OperationState.CurrentCategories;
                ProductsView = OperationState.CurrentProducts;
            }
            else
            {
                InitializeViewModel();
            }
        }

        [RelayCommand]
        private void OpenNewProduct()
        {
            CardNewProduct = !CardNewProduct;
            if (ModeProductSelected)
            {

            }
            else
            {
                NewCategories = new Category();
                NameCategory = string.Empty;
                ImageSelected = null;
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
                IconChoose = ImageSelected == null ? true : false
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
                Price = ProductPrice,
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
            if (string.IsNullOrEmpty(NameProduct) || ProductPrice == 0 ) return false;
            if (CategorySelected == null || string.IsNullOrEmpty(UnitSelected)) return false;
            return true;
        }
    }
}
