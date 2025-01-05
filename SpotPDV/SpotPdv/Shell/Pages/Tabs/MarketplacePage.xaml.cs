using CommunityToolkit.Mvvm.Messaging;
using SpotPdv.Application.MessengerModel;
using SpotPdv.Application.ViewModels;

namespace SpotPdv.Shell.Pages;

public partial class MarketplacePage : ContentPage
{
    public bool viewModelConfigured;
	public MarketplacePage(MarketplaceViewModel marketplaceViewModel)
	{
		InitializeComponent();
		BindingContext = marketplaceViewModel;

        WeakReferenceMessenger.Default.Register<ParameterMessage>(this, (r, m) =>
        {
            var viewModel = BindingContext as MarketplaceViewModel;
            if (viewModel == null) return;
            viewModel.ReceiveQueryAttributes(m.OperationState);
        });

        WeakReferenceMessenger.Default.Register<FastParameterMessage>(this, (r, m) =>
        {
            var viewModel = BindingContext as MarketplaceViewModel;
            if (viewModel == null) return;
            if (m.OperationState == null) return;
            viewModel.RefreshPage(m.OperationState);
        });
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
        var viewModel = BindingContext as MarketplaceViewModel;
        if (viewModel == null) return;

        WeakReferenceMessenger.Default.Send(new FastParameterMessage(viewModel.OperationState, nameof(MarketplacePage)));
    }
}