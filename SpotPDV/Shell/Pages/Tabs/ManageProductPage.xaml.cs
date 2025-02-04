using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using SpotPdv.Application.MessengerModel;
using SpotPdv.Application.ViewModels;
using SpotPdv.Controls;

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

    private async void Button_Clicked(object sender, EventArgs e)
    {
        //var viewModel = BindingContext as ManageProductViewModel;
        //var button = (Button)sender;


        //var options = new List<string> { "De A à Z", "Data de criação"};
        //var popup = new ContextMenu(options);
        //popup.Anchor = button;

        //var result = await this.ShowPopupAsync(popup);

        //viewModel.RefreshPage(viewModel.OperationState, (string)result == "De A à Z" ? true : false);   
    }

    private async void DeleteCategoryClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as ManageProductViewModel;

        bool answer = await DisplayAlert("Deletar categoria?", "A categoria será excluída do PDV, continuar?", "Sim", "Não");
        if(answer) viewModel.DeleteCategoryCommand.Execute(null);
    }

    private async void SaveCategoryClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as ManageProductViewModel;

        bool answer = await DisplayAlert("Salvar categoria?", "As informações atuais sobrescreverão a categoria editada, continuar?", "Sim", "Não");
        if(answer) viewModel.SaveCategoryCommand.Execute(null);
    }
    private async void DeleteProductClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as ManageProductViewModel;

        bool answer = await DisplayAlert("Deletar produto?", "O produto será excluída do PDV, continuar?", "Sim", "Não");
        if(answer) viewModel.DeleteProductCommand.Execute(null);
    }

    private async void SaveProductClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as ManageProductViewModel;

        bool answer = await DisplayAlert("Salvar categoria?", "As informações atuais sobrescreverão o produto editado, continuar?", "Sim", "Não");
        if(answer) viewModel.SaveProductCommand.Execute(null);
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var viewModel = BindingContext as ManageProductViewModel;
        viewModel.ExpanderVisible = false;
    }
}