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
        }
    }
}
