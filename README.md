# Maui.Redux

Maui Redux is an assembled cross-platform application framework based on Redux state management for MVU pattern,  using C# and Maui. It allows you to manage your application state and logic based on Redux implemention of MVU. MVU is an implementation idea based on one-way data flow, as shown in the figure below:

<p><img src="./assets/mvu.png" alt="redux-data-flow"></p>

- **1. When the application start, and state is initialized to the current state.**
- **2. When the state changes, and UI rendering to be rendered will be triggered.**
- **3. When interaction occurs, such as the user clicks on the event, an action is sent to the Reducer.**
- **4. Executing the Update action will create an instance of the updated state.**
- **5. The new state replaces the current state, and returns to step [2].**


## Installation(Coming soon)

You can use the NuGet package manager to install Maui.Redux, just run the following command in your project:

```bash
dotnet add package Maui.Redux
```

Or, you can also add the following dependency in your project file:

```xml
<ItemGroup>
  <PackageReference Include="Maui.Redux" Version="0.1.0" />
</ItemGroup>
```

## Design Principles

<p><img src="./assets/maui-redux.png" alt="Maui-redux-framework"></p>

The Maui Redux framework mainly contains the following parts:

- **Action Creators**: Create executable actions that represent pages or components. Each action has a type to distinguish different actions, and some optional parameters to pass some additional information.
- **Store**: Create and manage application state, dispatch and process actions. When each Page is initialized, a Store instance is created, the initial state and Reducer function are passed in, and some listeners can also be registered with the Store to update the UI or perform some other operations when the state changes.
- **Reducers**: This is a pure function that receives the current state and an action and returns a new state.
- **Effect**: Extensions that handle application side effects, such as jump routing, request data, etc.
- **Component**: Represents a stateless component, which is combined into a part of the Page through Adapter Connector or Slot Connector.
- **Page**: Represents a stateful page component, inherited from Component, which contains the following three parts.
   - InitState: initialization state function, unique to Page component
   - Middleware: middleware, such as printing logs in listening functions, etc.
   - CombineReducers: Integrate all Reducers of page components and sub-components together


## Usage

To use Maui.Redux, you need to define the following parts:

- **State**: This is a class that represents data and contains some properties, etc.
- **Action**: This is a class or enumeration that represents the actions that the application can perform, such as add, delete, update, etc. Each action has a type to distinguish different actions, and some optional parameters to pass some additional information.
- **Component**: (Optional) This is a class that represents a stateless view component. Use the method (Dispatch) in Store to dispatch actions, or use the properties of Store to obtain the current state.
- **Page**: This is a class that represents a stateful page component, including the following 6 parts, of which initState, Reducer, and View must be set.
   - **InitState**: initialization state function
   - **Reducer**: This is a pure function. According to the action type, the corresponding function is executed to modify the state.
   - **View**: The UI part of the component, which can be built using XAML or C#
   - **Effect**: (optional) An extension to Reducer, which mainly handles side effects and executes corresponding functions according to the action type.
   - **Middleware**: (optional) middleware, such as listening functions for printing logs, etc.
   - **Dependencies**: (Optional) If a complex page is composed of multiple sub-components, you can set Adapter Connector and Slot Connector. The former is suitable for dynamic collection components, and the latter is suitable for single components.


There are five steps to use the counter as an example:

> 1. Import Maui redux package
> 2. Create State
> 3. Define Action and ActionCreator
> 4. Create Reducer that modifies state
> 5. Create Page or Component
 
<p><img src="./assets/counter-code-segment.png" alt="counter-code-segment"></p>

<!--
```cs
using Redux;
using Redux.Component;
using Action = Redux.Action;
namespace samples.Pages.Counter;

/// [State]
public class CounterState : ReactiveObject
{
    [Reactive]
    public int Count { get; set; }

    public override string ToString()
    {
        return $"Count: {Count}";
    }
}

/// [Action]
enum CounterAction { add, onAdd }

/// [ActionCreator]
internal static class CounterActionCreator
{
    internal static Action addAction(int payload)
    {
        return new Action(CounterAction.add, payload);
    }

    internal static Action onAddAction()
    {
        return new Action(CounterAction.onAdd);
    }
}

/// [Reducer]
public partial class CounterPage
{
    private static Reducer<CounterState> buildReducer() => ReducerConverter.AsReducers(new Dictionary<object, Reducer<CounterState>>
    {
        {
            CounterAction.add, _add
        }
    });

    private static CounterState _add(CounterState state, Action action)
    {
        state.Count += action.Payload;
        return state;
    }
}

/// [Effect]
public partial class CounterPage
{
    private static Effect<CounterState>? buildEffect() => EffectConverter.CombineEffects(new Dictionary<object, SubEffect<CounterState>>
    {
        {
            CounterAction.onAdd, _onAdd
        }
    });

    private static async Task _onAdd(Action action, ComponentContext<CounterState> ctx)
    {
        ctx.Dispatch(CounterActionCreator.addAction(1));
        await Task.CompletedTask;
    }
}

/// [Page]
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
    /// [InitState]
    private static CounterState initState(Dictionary<string, dynamic>? param) => new() { Count = param?.GetValueOrDefault("count") ?? 0, Title = param?.GetValueOrDefault("title") ?? string.Empty };
}

```
-->

## Example

You can find a simple example in this repository, showing how to use Maui.Redux to implement a Todo List application. You can run the following commands to clone this repository and run the example:

```bash
git clone https://github.com/GavinHome/Maui.Redux.git
cd Maui.Redux
```

This example is to divide a large Page into different sub-components (Components), and finally assemble it into a complete application in the Page, as shown below:

<p><img src="./assets/todo-list-page-example-v2.png" alt="todo-list-page-example"></p>

You can also check the source code in the [Example] folder to understand the implementation details of the example.
