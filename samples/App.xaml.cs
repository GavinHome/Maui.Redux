using Microsoft.Maui.Controls;
using System.Diagnostics.Metrics;

namespace samples
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            ////AppShell.Current.GoToAsync("//Counter2");

            ////AppShell shell = new();
            ////shell.Items.Clear();

            ////ShellContent content = new()
            ////{
            ////    Title = "Counter",
            ////    Route = "Counter",
            ////    Content = Routes.routes.buildPage("count", new Dictionary<string, int> { { "count", 0 } })
            ////};
            ////shell.Items.Add(content);

            ////ShellContent content2 = new()
            ////{
            ////    Title = "Counter2",
            ////    Route = "Counter2",
            ////    Content = Routes.routes.buildPage("count", new Dictionary<string, int> { { "count", 2 } })
            ////};
            ////shell.Items.Add(content2);

            ////MainPage = shell;

            ////shell.GoToAsync("//Counter2");
        }
    }
}
