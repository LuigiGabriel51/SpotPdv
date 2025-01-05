
using SpotPdv.Application.Services;
using SpotPdv.Infrastructure.Services;

namespace SpotPdv.Infrastructure
{
    public static class InsfrastructureModule
    {
        public static MauiAppBuilder ResgisterService(this MauiAppBuilder appBuilder)
        {

            appBuilder.Services.AddTransient<IDataBaseService, DataBaseService>();
            appBuilder.Services.AddTransient<INavigationService, NavigationService>();
            appBuilder.Services.AddTransient<IOperationStateService, OperationStateService>();
            return appBuilder;
        }
    }
}
