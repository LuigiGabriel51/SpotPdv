using Microsoft.Extensions.Logging;
using SpotPdv.Shell.Pages;
using SpotPdv.Application.ViewModels;
using SpotPdv.Infrastructure;
using CommunityToolkit.Maui;

namespace SpotPdv
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("FontAwesomeDuotone-Solid-900.otf", "AwesomeDuotone");
                })
                .RegisterViewAndViewModel()
                .ResgisterService();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
        public static MauiAppBuilder RegisterViewAndViewModel(this MauiAppBuilder appBuilder)
        {

            appBuilder.Services.AddTransient<MainPage>();
            appBuilder.Services.AddTransient<OperatorLoginPage>();
            appBuilder.Services.AddTransient<MarketplacePage>();
            appBuilder.Services.AddTransient<ManageProductPage>();
            appBuilder.Services.AddTransient<CashierPage>();
            appBuilder.Services.AddTransient<StockManagerPage>();
            appBuilder.Services.AddTransient<SalesLogPage>();
            appBuilder.Services.AddTransient<AdministrationLoginPage>();

            appBuilder.Services.AddTransient<MainViewModel>();
            appBuilder.Services.AddTransient<OperatorLoginViewModel>();
            appBuilder.Services.AddTransient<MarketplaceViewModel>();
            appBuilder.Services.AddTransient<ManageProductViewModel>();
            appBuilder.Services.AddTransient<CashierViewModel>();
            appBuilder.Services.AddTransient<StockManagerViewModel>();
            appBuilder.Services.AddTransient<SalesLogViewModel>();
            appBuilder.Services.AddTransient<ShellViewModel>();
            appBuilder.Services.AddTransient<BaseViewModel>();
            appBuilder.Services.AddTransient<AdministrationLoginViewModel>();

            return appBuilder;
        }
    }
}