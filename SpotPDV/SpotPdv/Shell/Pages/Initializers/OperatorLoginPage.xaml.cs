using SpotPdv.Application.ViewModels;

namespace SpotPdv.Shell.Pages;

public partial class OperatorLoginPage : ContentPage
{
	public OperatorLoginPage()
	{
		InitializeComponent();
	}
    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        var viewModel = BindingContext as OperatorLoginViewModel;
        viewModel.CheckEntrys();
    }
}