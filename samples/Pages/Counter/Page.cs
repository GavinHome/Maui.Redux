using Converters;
using Microsoft.Maui.Controls;
using ReactiveUI;
using System;

namespace samples.Pages.Counter;

public partial class CounterPage : Page<CounterState, Dictionary<string, dynamic>>
{
    public CounterPage() : base(
        initState: initState,
        effect: buildEffect(),
        reducer: buildReducer(),
        middlewares: new[]
        {
            Redux.Middlewares.logMiddleware<CounterState>(monitor: (state) => state.ToString(), tag: "CounterPage")
        },
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

            var subHeadline = new Label()
            {
                Text = "Welcome to &#10;.NET Multi-platform App UI",
            };
            subHeadline.SetValue(SemanticProperties.HeadingLevelProperty, SemanticHeadingLevel.Level2);
            subHeadline.SetValue(SemanticProperties.DescriptionProperty, "Welcome to dot net Multi platform App UI");

            var clickButton = new Button()
            {
                Text = "Click me",
                Command = ReactiveCommand.Create(() => dispatch(CounterActionCreator.onAddAction())),
                HorizontalOptions = LayoutOptions.Fill
            };
            clickButton.SetValue(SemanticProperties.HintProperty, "Counts the number of times you click");
            clickButton.SetBinding(
                Button.TextProperty, 
                new Binding(
                    path: "Count",
                    source: state,
                    converter: new FuncValueConverter<int, string>(count => count == 0 ? "Click me" : ($"Clicked {count} time" + (count > 1 ? "s" : string.Empty)))
                )
            );

            return new ContentPage()
            {
                Content = new ScrollView()
                {
                    Content = new VerticalStackLayout()
                    {
                        Padding = new Thickness(30, 0),
                        Spacing = 25,
                        Children = {
                            image, headline, subHeadline, clickButton
                        }
                        //Children =
                        //{
                        //    //new Image()
                        //    //{
                        //    //    Source = ImageSource.FromFile("dotnet_bot.png"),
                        //    //    HeightRequest = 185,
                        //    //    Aspect =  Aspect.AspectFit
                        //    //},
                        //    //new Label()
                        //    //{
                        //    //    Text = "Hello, World!",                                
                        //    //    //Style = App.Current?.Resources["Headline"] as Style
                        //    //},
                        //    //new Label()
                        //    //{
                        //    //    Text = "Welcome to &#10;.NET Multi-platform App UI"
                        //    //},
                        //    //new Button()
                        //    //{
                        //    //    //Text = "Click me",

                        //    //    Command = ReactiveCommand.Create(() => dispatch(CounterActionCreator.onAddAction())),
                        //    //    HorizontalOptions = LayoutOptions.Fill
                        //    //}
                        //}
                    }
                }
            };
        })
    { }

    private static CounterState initState(Dictionary<string, dynamic>? param) => new CounterState() { Count = 0 };
}
