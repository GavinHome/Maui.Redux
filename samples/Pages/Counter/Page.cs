namespace samples.Pages.Counter;

public partial class CounterPage() : Page<CounterState, Dictionary<string, dynamic>>(
        initState: initState,
        effect: buildEffect(),
        reducer: buildReducer(),
        middlewares:
        [
            Redux.Middlewares.logMiddleware<CounterState>(monitor: (state) => state.ToString(), tag: "CounterPage")
        ],
        view: (state, dispatch, _) =>
        {
            var image = new Image()
            {
                Source = ImageSource.FromFile("dotnet_bot.png"),
                HeightRequest = 185,
                Aspect = Aspect.AspectFit
            };
            image.SetValue(SemanticProperties.DescriptionProperty, "dot net bot in a race car number eight");

            var headline = new Label()
            {
                Text = "Hello, World!",
            };
            headline.SetValue(SemanticProperties.HeadingLevelProperty, SemanticHeadingLevel.Level1);
            if (App.Current!.Resources.TryGetValue("Headline", out object HeadlineStyle))
            {
                headline.Style = HeadlineStyle as Style;
            }

            var subHeadline = new Label()
            {
                Text = "Welcome to \r\n .NET Multi-platform App UI",
            };
            subHeadline.SetValue(SemanticProperties.HeadingLevelProperty, SemanticHeadingLevel.Level2);
            subHeadline.SetValue(SemanticProperties.DescriptionProperty, "Welcome to dot net Multi platform App UI");
            if (App.Current!.Resources.TryGetValue("Headline", out object SubHeadlineStyle))
            {
                subHeadline.Style = SubHeadlineStyle as Style;
            }

            var clickButton = new Button()
            {
                Text = "Click me",
                Command = ReactiveUI.ReactiveCommand.Create(() => dispatch(CounterActionCreator.onAddAction())),
                HorizontalOptions = LayoutOptions.Fill
            };
            clickButton.SetValue(SemanticProperties.HintProperty, "Counts the number of times you click");
            clickButton.SetBinding(
                Button.TextProperty,
                new Binding(
                    path: nameof(state.Count),
                    source: state,
                    converter: new FuncConverter<int, string>(count => count == 0 ? "Click me" : ($"Clicked {count} time" + (count > 1 ? "s" : string.Empty)))
                )
            );

            var toDosButton = new Button()
            {
                Text = "ToDos",
                Command = ReactiveUI.ReactiveCommand.Create(async () =>
                {
                    await Navigator.of().push<dynamic>("todo_list", arguments: null);
                }),
                HorizontalOptions = LayoutOptions.Fill
            };
            toDosButton.SetValue(SemanticProperties.HintProperty, "Go to ToDosPage");

            return new ContentPage()
            {
                Title = state.Title,
                Content = new ScrollView()
                {
                    Content = new VerticalStackLayout()
                    {
                        Padding = new Thickness(30, 0),
                        Spacing = 25,
                        Children =
                        {
                            image, headline, subHeadline, clickButton, toDosButton
                        }
                    }
                }
            };
        })
{
    private static CounterState initState(Dictionary<string, dynamic>? param) => new() { Count = param?.GetValueOrDefault("count") ?? 0, Title = param?.GetValueOrDefault("title") ?? string.Empty };
}
