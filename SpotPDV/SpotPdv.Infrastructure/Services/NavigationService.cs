using CommunityToolkit.Mvvm.Messaging;
using SpotPdv.Application.MessengerModel;
using SpotPdv.Application.Models;
using SpotPdv.Application.Services;
using SpotPdv.Application.ViewModels;
using System.Reflection;

namespace SpotPdv.Infrastructure.Services
{
    public class NavigationService(IServiceProvider provider) : INavigationService
    {
        public async Task PopModalNavigateAsync(OperationStateModel parameter)
        {
            try
            {
                WeakReferenceMessenger.Default.Send(new ParameterMessage(parameter));

                await Shell.Current.Navigation.PopModalAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine($"##Error {GetType().Name}: {e}##");
            }
        }

        public async Task PopNavigateAsync(OperationStateModel parameter)
        {
            try
            {
                WeakReferenceMessenger.Default.Send(new ParameterMessage(parameter));

                await Shell.Current.Navigation.PopAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"##Error {GetType().Name}: {e}##");
            }
        }     

        public async Task PushModalNavigateAsync<TViewModel, TParameter>(OperationStateModel parameter)
        {
            var viewModelType = typeof(TViewModel);
            var viewType = GetViewTypeForViewModel(viewModelType);

            try
            {
                var pageInstance = provider.GetService(viewType) as Page;
                var viewModelInstance = provider.GetService(viewModelType) as BaseViewModel;
                pageInstance.BindingContext = viewModelInstance;
                viewModelInstance.ReceiveQueryAttributes(parameter);
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await Microsoft.Maui.Controls.Shell.Current.Navigation.PushModalAsync(pageInstance);
                });
            }
            catch (Exception e)
            {
                Console.WriteLine($"##Error {GetType().Name}: {e}##");
            }
        }

        public async Task PushNavigateAsync<TViewModel, TParameter>(OperationStateModel parameter)
        {
            var viewModelType = typeof(TViewModel);
            var viewType = GetViewTypeForViewModel(viewModelType);

            try
            {
                var navigationParameter = new Dictionary<string, object>();
                navigationParameter.Add(nameof(parameter), parameter);

                await MainThread.InvokeOnMainThreadAsync( async () =>
                {
                    await Microsoft.Maui.Controls.Shell.Current.GoToAsync($"{viewType.Name}", navigationParameter);
                });
            }
            catch (Exception e)
            {
                Console.WriteLine($"##Error {GetType().Name}: {e}##");
            }
        }

        public async Task PopRootNavigateAsync(OperationStateModel parameter)
        {
            try
            {
                WeakReferenceMessenger.Default.Send(new ParameterMessage(parameter));

                await Shell.Current.Navigation.PopToRootAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"##Error {GetType().Name}: {e}##");
            }
        }

        private Type GetViewTypeForViewModel(Type viewModelType)
        {
            var viewName = "";

            var entryPointAssembly = Assembly.Load("SpotPdv");

            viewName = viewModelType
                .FullName.Replace("Application.ViewModels", "Shell.Pages")
                .Replace("ViewModel", "Page");

            var viewType = entryPointAssembly.GetType(viewName ?? string.Empty);

            if (viewType == null)
            {
                throw new InvalidOperationException(
                    $"No view found for view model {viewModelType.FullName}"
                );
            }

            return viewType;
        }
    }
}
