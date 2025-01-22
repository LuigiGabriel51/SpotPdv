using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Application.Models
{
    public partial class OperationStateModel : ObservableObject
    {
        [ObservableProperty]
        private CashierModel _cashierModel;

        [ObservableProperty]
        private ObservableCollection<Category> _currentCategories;

        [ObservableProperty]
        private ObservableCollection<Product> _currentProducts;

        [ObservableProperty]
        private AdmLoginModel _adm;

        [ObservableProperty]
        private List<OperatorLoginModel> _operators;

        [ObservableProperty]
        private OperatorLoginModel _lastOperator;

        [ObservableProperty]
        private DateTime _lastConection;

        [ObservableProperty]
        private ObservableCollection<string> _iconsExamples;

    }
}
