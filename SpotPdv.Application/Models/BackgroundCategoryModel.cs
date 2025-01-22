using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Application.Models
{
    public partial class BackgroundCategoryModel : ObservableObject
    {
        [ObservableProperty]
        private string _color;

        [ObservableProperty]
        private string _nameColor;
    }
}