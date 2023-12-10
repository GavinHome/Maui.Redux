using DynamicData;
using DynamicData.Binding;
using Microsoft.Maui.Controls.Shapes;
using ReactiveUI;
using samples.Pages.Todos.Report;
using System.Collections.ObjectModel;
namespace samples.Pages.Todos.Page;

using Action = Redux.Action;

public partial class ToDoListPage() : Page<PageState, Dictionary<string, dynamic>>(
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
            var layout = buildItemsView(state.ToDos!, ctx);
            var addBtnView = buildAddBtnView(dispatch);
            var report = ctx.buildComponent("report");
            return new ContentPage()
            {
                Title = state.Title,
                Content = new FlexLayout()
                {
                    Direction = Microsoft.Maui.Layouts.FlexDirection.Column,
                    Children =
                    {
                        layout,
                        addBtnView,
                        report
                    }
                }
            };
        })
{  

    private static PageState initState(Dictionary<string, dynamic>? param) => new()
    {
        ToDos = [],
        Title = param?.GetValueOrDefault("title") ?? "ToDoList"
    };

    private static Grid buildAddBtnView(Dispatch dispatch)
    {
        return new Grid()
        {
            ZIndex = 100,
            Children =
            {
                new Border()
                {
                    HorizontalOptions = new LayoutOptions(LayoutAlignment.End, true),
                    VerticalOptions = new LayoutOptions(LayoutAlignment.End, true),
                    HeightRequest = 50,
                    WidthRequest = 50,
                    Padding = new Thickness(8,5,12,8),
                    Background = new SolidColorBrush(Color.Parse("#bbe9d4ff")),
                    StrokeThickness = 0,
                    Margin = new Thickness(0,0,15,-35),
                    StrokeShape = new RoundRectangle()
                    {
                        CornerRadius = 15,
                    },
                    Content = new Microsoft.Maui.Controls.Shapes.Path()
                    {
                        HorizontalOptions= LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Fill = new SolidColorBrush(Colors.Black),
                        Data = new PathGeometryConverter().ConvertFromString("M19,13H13V19H11V13H5V11H11V5H13V11H19V13Z") as PathGeometry,
                    },
                    GestureRecognizers =
                    {
                        new TapGestureRecognizer()
                        {
                            Command = ReactiveCommand.Create(() => dispatch(new Action("onAdd")))
                        }
                    }
                }
            }
        };
    }

    private static FlexLayout buildItemsView(ObservableCollection<Todo.ToDoState> obs, ComponentContext<PageState> ctx)
    {
        var layout = new VerticalStackLayout() { Spacing = 25 }.Padding(new Thickness(10, 0, 10, 100));
        var content = new FlexLayout().Grow(1f);
        content.Children.Add(new ScrollView() { Content = layout });

        _ = obs.ToObservableChangeSet().Subscribe(x =>
        {
            Application.Current?.Dispatcher.Dispatch(() =>
            {
                var todos = ctx.buildComponents();
                layout.Children.Clear();
                layout.Children.AddRange(todos);
            });
        });

        return content;
    }
}
