using AsyncAwaitBestPractices;
using SpotPdv.Application.Models;
using SpotPdv.Application.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Application.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel
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

        protected override void InitializeViewModel()
        {
            base.InitializeViewModel();
            CheckOperatorLogin();
        }

        private async void CheckOperatorLogin()
        {
            LoadingMessage = message_init;
            await Task.Delay(2000);
            if(OperationState.Adm == null)
            {
                await NavigationService.PushModalNavigateAsync<AdministrationLoginViewModel, OperationStateModel>(OperationState);
                return;
            }

            if (CheckDateTimeLogin())
            {
                AutoCompleteIcons();
                await NavigationService.PopRootNavigateAsync(OperationState);
            }
            else
            {
                await NavigationService.PushModalNavigateAsync<OperatorLoginViewModel, OperationStateModel>(OperationState);
            }
        }

        private bool CheckDateTimeLogin()
        {
            if (OperationState.LastConection < DateTime.UtcNow.AddHours(-24))
            {
                return false;
            }
            OperationState.LastConection = DateTime.UtcNow;
            return true;
        }

        private async void AutoCompleteIcons()
        {
            if (OperationState == null) return;

            OperationState.IconsExamples = new ObservableCollection<string>()
            {
                "refeicao.png", "arroz.png", "bebida.png", "bolo.png", "cafe.png", "cerveja.png", "cha.png", "chips.png",
                "copo.png", "hamburger.png", "lanche.png", "pizza.png", "sorvete.png", "sushi.png", "torta.png"
            };
        }
    }
}