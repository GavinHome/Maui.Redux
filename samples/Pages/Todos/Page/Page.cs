using DynamicData.Binding;
using ReactiveUI;
using samples.Pages.Todos.Report;
using samples.Pages.Todos.Todo;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reactive.Subjects;

namespace samples.Pages.Todos.Page;
using Action = Redux.Action;

public partial class ToDoListPage : Page<PageState, Dictionary<string, dynamic>>
{
    public ToDoListPage() : base(
        initState: initState,
        effect: buildEffect(),
        reducer: buildReducer(),
        middlewares:
        [
            Redux.Middlewares.logMiddleware<PageState>(monitor: (state) => state.ToString(), tag: "ToDoListPage")
        ],
        dependencies: new Dependencies<PageState>(
            adapter: new NoneConn<PageState>() + new PageAdapter(),
            slots: new Dictionary<String, Dependent<PageState>>
            {
                { "report", new ReportConnector() + new ReportComponent() },
            }
        ),
        view: (state, dispatch, ctx) =>
        {
            var report = ctx.buildComponent("report");
            var itemsView = buildItemsView(state.ToDos!, ctx);
            ////return new View();
            var todos = ctx.buildComponents();
            var content = new FlexLayout()
            {
                Children = {
                     new ScrollView()
                     {
                         Content = new VerticalStackLayout()
                         {   
                             Padding = new Thickness(10,0,10,100),
                             Spacing = 25,
                             Children =
                             {
                                itemsView,
                                todos[0],
                                todos[1],
                                todos[2],
                             }
                         }
                     }
                }
            };

            content.Grow(1f);

            var addButton = new AddButton()
            {
                ZIndex = 100,
                OnAddTodo = ReactiveCommand.Create(() => dispatch(new Action("onAdd")))
            };

            return new ContentPage()
            {
                Title = "ToDoList",
                Content = new FlexLayout()
                {
                    Direction = Microsoft.Maui.Layouts.FlexDirection.Column,
                    Children =
                    {
                        content,
                        addButton,
                        report
                    }
                }
            };
        })
    {
    }

    private static PageState initState(Dictionary<string, dynamic>? param) => new() { ToDos = [] };

    private static CollectionView buildItemsView(ObservableCollection<Todo.ToDoState> obs, ComponentContext<PageState> ctx)
    {
        var source = new Subject<List<Widget>>();
        var items = new CollectionView(); 
        items.BindingContext = source.BindTo<IEnumerable, CollectionView, IEnumerable>(items, items => items.ItemsSource);
        _ = obs.ToObservableChangeSet().Subscribe(x =>
        {
            Application.Current?.Dispatcher.Dispatch(() =>
            {
                var todos = ctx.buildComponents();
                source.OnNext(todos);
            });
        });

        return items;
    }
}
