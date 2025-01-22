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
    public partial class AdministrationLoginViewModel : BaseViewModel
    {
        public AdministrationLoginViewModel(INavigationService navigationService, IOperationStateService operationStateService) : base(navigationService, operationStateService)
        {
        }

        [ObservableProperty]
        private AdmLoginModel _newAdmLoginModel = new AdmLoginModel();

        [ObservableProperty]
        private string _repeatePassword;

        [ObservableProperty]
        private bool _erroVisible = true;

        [ObservableProperty]
        private string _erroMessage;


        public void CheckEntrys()
        {
            if (string.IsNullOrEmpty(NewAdmLoginModel.Name))
            {
                ErroVisible = true;
                ErroMessage = "Usuário não pode ficar em branco";
                return;
            }
            if (NewAdmLoginModel.Name.Length < 8)
            {
                ErroVisible = true;
                ErroMessage = "Usuário deve ter pelo menos 8 caracter";
                return;
            }
            if (string.IsNullOrEmpty(NewAdmLoginModel.Password))
            {
                ErroVisible = true;
                ErroMessage = "Senha não pode ficar em branco";
                return;
            }
            if (NewAdmLoginModel.Password.Length < 4)
            {
                ErroVisible = true;
                ErroMessage = "Senha deve ter pelo menos 4 caracter";
                return;
            }
            if (NewAdmLoginModel.Password != RepeatePassword)
            {
                ErroVisible = true;
                ErroMessage = "Senhas não coincidem";
                return;
            }
            ErroVisible = false;
        }

        [RelayCommand]
        private async void CreateAdmin()
        {
            if (ErroVisible) return;
            OperationState.Adm = NewAdmLoginModel;
            OperationState.Operators = new List<OperatorLoginModel>();
            await OperationStateService.SaveOperationState(OperationState);
            await NavigationService.PushModalNavigateAsync<OperatorLoginViewModel, OperationStateModel>(OperationState);
        }
    }
}
