using DynamicData.Binding;
using Microsoft.Maui.Controls.Shapes;
using ReactiveUI;
using samples.Pages.Todos.Report;
using System.Collections.Generic;
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
            ////var itemsView = buildItemsView(state.ToDos!, ctx);
            //return new View();
            var todos = ctx.buildComponents();
            var d = new ListView(ListViewCachingStrategy.RecycleElement);
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
                                 new Label { Text = "Label 1", BackgroundColor = Colors.Red,WidthRequest = 100, HeightRequest = 100  },
                                 new ListView()
                                 {
                                     ItemsSource = todos,
                                 }
                                 //itemsView
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

    ////private static CollectionView buildItemsView<T>(ObservableCollection<T> obs, ComponentContext<PageState> ctx)
    ////{
    ////    ////var source = new Subject<List<Widget>>();
    ////    var items = new CollectionView()
    ////    {
    ////        ////[!ItemsControl.ItemsSourceProperty] = source.ToBinding()
    ////    };
    ////    var todos = ctx.buildComponents();
    ////    items.ItemsSource = todos;
    ////    //items.SetValue(CollectionView.ItemsSourceProperty, source);
    ////    //_ = obs.ToObservableChangeSet().Subscribe(x =>
    ////    //{
    ////    //    MainThread.InvokeOnMainThreadAsync(() =>
    ////    //    {
    ////    //        var todos = ctx.buildComponents();
    ////    //        ////source.OnNext(todos);
    ////    //        items.ClearValue(CollectionView.ItemsSourceProperty);
    ////    //        items.ItemsSource = todos;
    ////    //    });
    ////    //});

    ////    return items;
    ////}
}
