using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using SpotPdv.Application.Enums;
using SpotPdv.Application.MessengerModel;
using SpotPdv.Application.Models;
using SpotPdv.Application.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Application.ViewModels
{
    public partial class BaseViewModel : ObservableObject, IQueryAttributable
    {
        public readonly INavigationService NavigationService;
        public readonly IOperationStateService OperationStateService;

        public string message_init = "Atualizando o sistema... quase pronto!";
        public string message_get_products = "Buscando produtos...";
        public string message_verify_cashier = "Verificando o caixa...";
        public string message_verify_stock = "Verificando o estoque...";
        public string message_get_historic_sales = "Buscando histórico de vendas...";
        public string message_after_sale = "Atualizando o sistema... quase pronto para a próxima venda!";
        public string message_cashier_total = "Calculando os totais... deixe-nos organizar tudo!";
        public string message_check_login = "Checando login...";

        public BaseViewModel
            (
                INavigationService navigationService,
                IOperationStateService operationStateService
            )
        {
            NavigationService = navigationService;
            OperationStateService = operationStateService;       
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query == null || !query.Any()) return;

            OperationState = query["parameter"] as OperationStateModel;
            InitializeViewModel();
        }

        public void ReceiveQueryAttributes(OperationStateModel operationStateModel)
        {
            OperationState = operationStateModel;
            InitializeViewModel();
        }

        protected virtual void InitializeViewModel()
        {

        }

        public virtual void RefreshPage(OperationStateModel operationStateModel)
        {
            OperationState = operationStateModel;
        }
   

        [ObservableProperty]
        private OperationStateModel _operationState;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private string _loadingMessage;

        [ObservableProperty]
        private ObservableCollection<Category> _categoriesView;

        [ObservableProperty]
        private ObservableCollection<Product> _productsView;

        public ObservableCollection<BackgroundCategoryModel> Colors = new ObservableCollection<BackgroundCategoryModel>
        {
            new BackgroundCategoryModel() { Color = "#FF5733", NameColor = "Vermelho" },
            new BackgroundCategoryModel() { Color = "#33FF57", NameColor = "Verde" },
            new BackgroundCategoryModel() { Color = "#3357FF", NameColor = "Azul" },
            new BackgroundCategoryModel() { Color = "#F4A300", NameColor = "Laranja" },
            new BackgroundCategoryModel() { Color = "#FF33F6", NameColor = "Rosa" },
            new BackgroundCategoryModel() { Color = "#6A5ACD", NameColor = "Slate Blue" },
            new BackgroundCategoryModel() { Color = "#FFD700", NameColor = "Amarelo" },
            new BackgroundCategoryModel() { Color = "#ADFF2F", NameColor = "Green Yellow" },
            new BackgroundCategoryModel() { Color = "#8A2BE2", NameColor = "Azul Violeta" },
            new BackgroundCategoryModel() { Color = "#FF6347", NameColor = "Tomate" },
            new BackgroundCategoryModel() { Color = "#7FFF00", NameColor = "Chartreuse" },
            new BackgroundCategoryModel() { Color = "#D2691E", NameColor = "Chocolate" },
            new BackgroundCategoryModel() { Color = "#FF1493", NameColor = "Deep Pink" },
            new BackgroundCategoryModel() { Color = "#20B2AA", NameColor = "Light Sea Green" },
            new BackgroundCategoryModel() { Color = "#FF8C00", NameColor = "Dark Orange" }
        };

        public ObservableCollection<string> IconsCategory = new ObservableCollection<string>
        {
            "\uf805", // Hamburger
            "\uf817", // Pizza
            "\ue3e5", // Cake
            "\uf810", // Ice Cream
            "\ue0b3", // Beer
            "\uf5d1", // Apple
            "\ue2eb", // Rice
            "\ue43b", // Plate
            "\uf824", // Steak
            "\ue1f5", // Tea
            "\uf094", // Lemon
            "\uf819"  // Corn
        };

        public bool ViewModelIsInitialized = false;        
    }
}
