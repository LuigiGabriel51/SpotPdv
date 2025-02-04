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

        [ObservableProperty]
        private DateTime _dateTimeCreate;

        public Category Clone(Category original)
        {
            if (original == null)
            {
                return new Category();
            }

            return new Category
            {
                CategoryId = original.CategoryId,
                Name = original.Name,
                Image = original.Image, 
                Color = original.Color,
                Icon = original.Icon,
                IconChoose = original.IconChoose,
                DateTimeCreate = original.DateTimeCreate
            };
        }
    }
}
