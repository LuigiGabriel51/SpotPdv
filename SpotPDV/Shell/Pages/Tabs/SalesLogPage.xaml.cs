using CommunityToolkit.Mvvm.Messaging;
using SpotPdv.Application.MessengerModel;
using SpotPdv.Application.ViewModels;

namespace SpotPdv.Shell.Pages;

public partial class SalesLogPage : ContentPage
{
	public SalesLogPage(SalesLogViewModel salesLogViewModel)
	{
		InitializeComponent();

        BindingContext = salesLogViewModel;

        WeakReferenceMessenger.Default.Register<ParameterMessage>(this, (r, m) =>
        {
            var viewModel = BindingContext as SalesLogViewModel;
            if (viewModel == null) return;
            viewModel.ReceiveQueryAttributes(m.OperationState);
        });

        WeakReferenceMessenger.Default.Register<FastParameterMessage>(this, (r, m) =>
        {
            var viewModel = BindingContext as SalesLogViewModel;
            if (viewModel == null) return;
            if (m.OperationState == null) return;
            viewModel.RefreshPage(m.OperationState);
        });
    }
}