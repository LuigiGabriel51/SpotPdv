using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpotPdv.Application.Models;
using SpotPdv.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Application.ViewModels
{
    public partial class OperatorLoginViewModel : BaseViewModel
    {
        public OperatorLoginViewModel
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

        [ObservableProperty]
        private OperatorLoginModel _newOperatorLoginModel = new OperatorLoginModel();

        [ObservableProperty]
        private bool _addFistOperadorCard;

        [ObservableProperty]
        private bool _loginCard;

        [ObservableProperty]
        private bool _erroVisible = true;

        [ObservableProperty]
        private string _erroMessage;

        protected override void InitializeViewModel()
        {
            base.InitializeViewModel();
            CheckOperators();
        }
        private void CheckOperators()
        {
            if(OperationState.Operators == null || !OperationState.Operators.Any())
            {
                AddFistOperadorCard = true;
                return;
            }

            LoginCard = true;
        }

        public void CheckEntrys()
        {
            if (string.IsNullOrEmpty(NewOperatorLoginModel.Name))
            {
                ErroVisible = true;
                ErroMessage = "Usuário não pode ficar em branco";
                return;
            }
            if (NewOperatorLoginModel.Name.Length < 4)
            {
                ErroVisible = true;
                ErroMessage = "Usuário deve ter pelo menos 4 caracter";
                return;
            }
            if (string.IsNullOrEmpty(NewOperatorLoginModel.Password))
            {
                ErroVisible = true;
                ErroMessage = "Senha não pode ficar em branco";
                return;
            }
            if (NewOperatorLoginModel.Password.Length < 4)
            {
                ErroVisible = true;
                ErroMessage = "Senha deve ter pelo menos 4 caracter";
                return;
            }

            ErroVisible = false;
        }

        [RelayCommand]
        private async void CreateOperator()
        {
            if (ErroVisible) return;
            OperationState.Operators.Add(NewOperatorLoginModel);
            OperationState.LastConection = DateTime.UtcNow;
            await OperationStateService.SaveOperationState(OperationState);
            await NavigationService.PopRootNavigateAsync(OperationState);
        }
    }
}
