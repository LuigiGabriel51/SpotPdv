using CommunityToolkit.Mvvm.Messaging;
using SpotPdv.Application.MessengerModel;
using SpotPdv.Application.ViewModels;

namespace SpotPdv.Shell.Pages;

public partial class ManageProductPage : ContentPage
{
	public ManageProductPage(ManageProductViewModel manageProductViewModel)
	{
		InitializeComponent();
		BindingContext = manageProductViewModel;

        WeakReferenceMessenger.Default.Register<ParameterMessage>(this, (r, m) =>
        {
            var viewModel = BindingContext as ManageProductViewModel;
            if (viewModel == null) return;
            viewModel.ReceiveQueryAttributes(m.OperationState);
        });

        WeakReferenceMessenger.Default.Register<FastParameterMessage>(this, (r, m) =>
        {
            var viewModel = BindingContext as ManageProductViewModel;
            if (viewModel == null) return;
            if (m.OperationState == null) return;
            viewModel.RefreshPage(m.OperationState);
        });
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
        var viewModel = BindingContext as ManageProductViewModel;
        if (viewModel == null) return;

        WeakReferenceMessenger.Default.Send(new FastParameterMessage(viewModel.OperationState, nameof(MarketplacePage)));
    }

    private void picker_BindingContextChanged(object sender, EventArgs e)
    {
        var viewModel = BindingContext as ManageProductViewModel;
        if (viewModel == null) return;
        viewModel.ProductTypeSelected = "Categorias";
    }
}