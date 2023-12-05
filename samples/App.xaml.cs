namespace samples
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = Routes.routes.home;
        }
    }
}
