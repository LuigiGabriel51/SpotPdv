using SpotPdv.Application.ViewModels;
using SpotPdv.Shell.Pages;

namespace SpotPdv.Shell
{
    public partial class AppShell : Microsoft.Maui.Controls.Shell
    {
        public AppShell(ShellViewModel shellViewModel)
        {
            InitializeComponent();
            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("AdministrationLoginPage", typeof(AdministrationLoginPage));
            Routing.RegisterRoute("OperatorLoginPage", typeof(OperatorLoginPage));
            Routing.RegisterRoute("MarketplacePage", typeof(MarketplacePage));
            Routing.RegisterRoute("CashierPage", typeof(CashierPage));
            Routing.RegisterRoute("StockManagerPage", typeof(StockManagerPage));
            Routing.RegisterRoute("SalesLogPage", typeof(SalesLogPage));

            BindingContext = shellViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var shellViewModel = BindingContext as ShellViewModel;
            shellViewModel.IntializeApplication();
        }
    }
}
