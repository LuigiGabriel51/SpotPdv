using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Application.Models
{
    public partial class Category: ObservableObject
    {
        [ObservableProperty]
        private Guid _categoryId;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private ImageModel _image;

        [ObservableProperty]
        private BackgroundCategoryModel _Color;

        [ObservableProperty]
        private string _icon;

        [ObservableProperty]
        private bool _iconChoose;
    }
}
