using SpotPdv.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Application.ViewModels
{
    public class StockManagerViewModel : BaseViewModel
    {
        public StockManagerViewModel
            (
                INavigationService navigationService,
                IOperationStateService operationStateService
            )
            :
            base
            (
                navigationService,
                operationStateService
            )
        {

        }
    }
}
