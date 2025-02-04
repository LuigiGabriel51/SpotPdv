using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Application.Models
{
    public partial class DataContainer : ObservableObject
    {
        [ObservableProperty]
        private Category _category;

        [ObservableProperty]
        private ObservableCollection<Product> _products;
    }
}
