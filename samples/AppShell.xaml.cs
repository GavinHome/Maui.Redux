using samples.Pages.Counter;

namespace samples
{
    public partial class AppShell : Shell
    {
        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

        public AppShell()
        {
            InitializeComponent();

            Items.Clear();

            //ShellContent counterPage = new()
            //{
            //    Title = "Counter",
            //    Route = "count",
            //    Content = Routes.Current.buildPage("count", new Dictionary<string, dynamic> { { "title", "Counter" } }),
            //};
            //Items.Add(counterPage);

            ShellContent toDoListPage = new()
            {
                Title = "ToDos",
                Route = "todo_list",
                //Content = Routes.Current.buildPage("todo_list", new Dictionary<string, dynamic> { { "title", "ToDoListPage" } })
                Content = Routes.Current.home,
            };
            Items.Add(toDoListPage);

            ShellContent toDoEditPage = new()
            {
                Title = "ToDoEdit",
                Route = "todo_edit",
                ////Content = Routes.Current.buildPage("todo_edit")
            };
            Items.Add(toDoEditPage);

            Navigator.onGenerateRoute = (RouteSettings settings) =>
            {
                var page = Routes.Current.buildPage(settings.name, settings.arguments);
                return page;
            };

            Navigator.onChange += (RouteSettings settings) =>
            {
                if(settings.name == "todo_edit")
                {
                    toDoEditPage.Content = Navigator.of().current;
                }

                AppShell.Current?.GoToAsync("//" + settings.name);
            };
        }
    }
}
