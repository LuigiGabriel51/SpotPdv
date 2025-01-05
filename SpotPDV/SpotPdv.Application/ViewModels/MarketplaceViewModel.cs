﻿using SpotPdv.Application.Services;
using AsyncAwaitBestPractices;
using SpotPdv.Application.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
namespace SpotPdv.Application.ViewModels
{
    public partial class MarketplaceViewModel : BaseViewModel
    {
        public MarketplaceViewModel
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

        protected override void InitializeViewModel()
        {
            base.InitializeViewModel();
            CategoriesView = OperationState.CurrentCategories;
            ViewModelIsInitialized = true;
        }

        public override void RefreshPage(OperationStateModel operationStateModel)
        {
            base.RefreshPage(operationStateModel);
            if (ViewModelIsInitialized)
            {

            }
            else
            {
                InitializeViewModel();
            }
        }
    }
}
