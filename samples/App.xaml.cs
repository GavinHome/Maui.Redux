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

            Navigator.build(
                routes: Routes.routes,
                routeChanged: route => MainPage = route!.Content
            );

            //// [Specify how to generating the route page]
            ////Navigator.build(
            ////    routes: Routes.routes,
            ////    routeChanged: route => MainPage = route!.Content,
            ////    generateRoute: settings => Routes.routes.buildPage(settings.name, settings.arguments)
            ////);
        }
    }
}
