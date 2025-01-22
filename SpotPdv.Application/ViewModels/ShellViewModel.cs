
using SpotPdv.Application.Models;
using SpotPdv.Application.Services;
using System.Buffers;
using System.Collections.ObjectModel;

namespace SpotPdv.Application.ViewModels
{
    public class ShellViewModel : BaseViewModel
    {
        public ShellViewModel
            (
            INavigationService navigationService,
            IOperationStateService operationStateService
            )
            :
            base
            (
                navigationService, operationStateService
            )
        {
        }
        public async void IntializeApplication()
        {
            OperationState = await OperationStateService.LoadOperationState();
            if (OperationState == null)
            {
                OperationState = new OperationStateModel();
                OperationState.CashierModel = new CashierModel() 
                {
                    TotalValue = 0, 
                    CashierMoviments = new ObservableCollection<CashierMovimentation>() 
                };
                await OperationStateService.SaveOperationState(OperationState);
            }
            await NavigationService.PushModalNavigateAsync<MainViewModel, OperationStateModel>(OperationState);
        }
    }
}
