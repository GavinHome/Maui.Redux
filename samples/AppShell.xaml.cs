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
                Content = Routes.Current.home
            };
            Items.Add(content);

            ShellContent content2 = new()
            {
                Title = "ToDos",
                Route = "ToDos",
                //Content = Routes.Current.buildPage("count", new Dictionary<string, dynamic> { { "count", 2 }, { "title", "Counter2" } })
                Content = Routes.Current.buildPage("todo_list", new Dictionary<string, dynamic> { })
            };
            Items.Add(content2);
        }
    }
}
