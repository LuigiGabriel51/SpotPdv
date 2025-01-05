using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Application.Models
{
    public partial class Sale : ObservableObject
    {
        [ObservableProperty]
        private Guid _id;

        [ObservableProperty]
        private DateTime _dateTime;

        [ObservableProperty]
        private IEnumerable<Product> _products; 
    }
}
