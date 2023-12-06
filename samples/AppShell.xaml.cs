using samples.Pages.Counter;

namespace samples
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Items.Clear();

            ShellContent content = new()
            {
                Title = "Counter",
                Route = "Counter",
                //Content = Routes.routes.buildPage("count", new Dictionary<string, dynamic> { { "count", 0 } })
                Content = Routes.routes.home
            };
            Items.Add(content);

            ShellContent content2 = new()
            {
                Title = "Counter2",
                Route = "Counter2",
                Content = Routes.routes.buildPage("count", new Dictionary<string, dynamic> { { "count", 2 }, { "title", "Counter2" } })
            };
            Items.Add(content2);
        }
    }
}
