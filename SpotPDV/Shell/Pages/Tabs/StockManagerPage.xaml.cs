using CommunityToolkit.Mvvm.Messaging;
using SpotPdv.Application.MessengerModel;
using SpotPdv.Application.ViewModels;

namespace SpotPdv.Shell.Pages;

public partial class StockManagerPage : ContentPage
{
	public StockManagerPage(StockManagerViewModel stockManagerViewModel)
	{
		InitializeComponent();

        BindingContext = stockManagerViewModel;

        WeakReferenceMessenger.Default.Register<ParameterMessage>(this, (r, m) =>
        {
            var viewModel = BindingContext as StockManagerViewModel;
            if (viewModel == null) return;
            viewModel.ReceiveQueryAttributes(m.OperationState);
        });

        WeakReferenceMessenger.Default.Register<FastParameterMessage>(this, (r, m) =>
        {
            var viewModel = BindingContext as StockManagerViewModel;
            if (viewModel == null) return;
            if (m.OperationState == null) return;
            viewModel.RefreshPage(m.OperationState);
        });
    }
}