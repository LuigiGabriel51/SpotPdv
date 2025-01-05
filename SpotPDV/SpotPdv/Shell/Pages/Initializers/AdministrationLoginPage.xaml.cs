using SpotPdv.Application.ViewModels;

namespace SpotPdv.Shell.Pages;

public partial class AdministrationLoginPage : ContentPage
{
	public AdministrationLoginPage()
	{
		InitializeComponent();
	}

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        var viewModel = BindingContext as AdministrationLoginViewModel;
        viewModel.CheckEntrys();
    }
}