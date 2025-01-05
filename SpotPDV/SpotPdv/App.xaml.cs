using SpotPdv.Application.ViewModels;
using SpotPdv.Shell;

namespace SpotPdv
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        private readonly ShellViewModel _shellViewModel;
        public App(ShellViewModel shellViewModel)
        {
            InitializeComponent();
            _shellViewModel = shellViewModel;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell(_shellViewModel));
        }
    }
}