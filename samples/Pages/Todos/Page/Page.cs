using DynamicData.Binding;
using ReactiveUI;
using samples.Pages.Todos.Report;
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
        view: (state, dispatch, ctx) => new View())
    ////{
    ////    ////var todos = ctx.buildComponents();
    ////    var report = ctx.buildComponent("report");
    ////    var itemsView = buildItemsView(state.ToDos!, ctx);
    {
    }

    private static PageState initState(Dictionary<string, dynamic>? param) => new() { ToDos = [] };

    ////private static ItemsControl buildItemsView<T>(ObservableCollection<T> obs, ComponentContext<PageState> ctx)
    ////{
    ////    var source = new Subject<List<Control>>();
    ////    var items = new ItemsControl()
    ////    {
    ////        [!ItemsControl.ItemsSourceProperty] = source.ToBinding()
    ////    };

    ////    obs.ToObservableChangeSet().Subscribe(x =>
    ////    {
    ////        Dispatcher.UIThread.Post(() =>
    ////        {
    ////            var todos = ctx.buildComponents();
    ////            source.OnNext(todos);
    ////        });
    ////    });

    ////    return items;
    ////}
}
