using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Application.Models
{
    public partial class CashierModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<CashierMovimentation> _CashierMoviments;

        [ObservableProperty]
        private float _totalValue;
    }
}
