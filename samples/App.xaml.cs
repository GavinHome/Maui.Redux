using Microsoft.Maui.Controls;
using System.Diagnostics.Metrics;

namespace samples
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ////MainPage = new AppShell();

            Navigator.onGenerateRoute = settings => Routes.Current.buildPage(settings.name, settings.arguments);
            Navigator.onRouteChanged = route => MainPage = route!.Content;
            Routes.Current.buildHome();
        }
    }
}
