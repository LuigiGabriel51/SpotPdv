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
    }
}
