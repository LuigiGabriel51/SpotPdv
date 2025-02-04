using CommunityToolkit.Mvvm.ComponentModel;
using SpotPdv.Application.Enums;

namespace SpotPdv.Application.Models
{
    public partial class Product : ObservableObject
    {
        [ObservableProperty]
        private Guid _productId;

        [ObservableProperty]
        private Guid _categoryId;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _reducedName;

        [ObservableProperty]
        private string _category;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private string _barcode;

        [ObservableProperty]
        private UnitiType _unitType;

        [ObservableProperty]
        private decimal _price;

        [ObservableProperty]
        private int _discount;

        [ObservableProperty]
        private int _stockQuantity;

        [ObservableProperty]
        private bool _isAvailable;

        [ObservableProperty]
        private ImageModel _image;

        [ObservableProperty]
        private BackgroundCategoryModel _Color;

        [ObservableProperty]
        private DateTime _dateTimeCreate;

        public Product Clone(Product original)
        {
            if (original == null)
            {
                return new Product();
            }

            return new Product
            {
                ProductId = original.ProductId,
                CategoryId = original.CategoryId,
                Name = original.Name,
                ReducedName = original.ReducedName,
                Category = original.Category,
                Description = original.Description,
                Barcode = original.Barcode,
                UnitType = original.UnitType,
                Price = original.Price,
                Discount = original.Discount,
                StockQuantity = original.StockQuantity,
                IsAvailable = original.IsAvailable,
                Image = original.Image,
                Color = original.Color,
                DateTimeCreate = original.DateTimeCreate
            };
        }
    }
}
