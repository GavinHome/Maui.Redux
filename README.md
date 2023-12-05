# Maui.Redux

Maui.Redux is an implementation of [Redux] for the [Maui] framework, which allows you to use the Redux pattern to manage your application's state and logic.

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

## Usage

To use Maui.Redux, you need to define the following parts:

- **State**: This is a class that represents your application's state, containing some properties, such as data, user input, UI state, etc.
- **Action**: This is a class that represents the actions that your application can perform, such as adding, deleting, updating, etc. Each action has a type, used to distinguish different actions, and some optional parameters, used to pass some extra information.
- **Reducer**: This is a function that takes the current state and an action, and returns a new state. Depending on the action type, you can modify the state, or return the original state if no modification is needed.
- **Store**: This is a class that creates and manages your application's state, and dispatches and handles actions. You can create an instance of Store at the entrance of your application, passing in your defined initial state and reducer function. You can also register some listeners to the Store, to update your UI or perform some other operations when the state changes.
- **Component**: This is a class that inherits from Maui's control classes, such as Window, UserControl, etc. You can use the Store's methods in your component to dispatch actions, or use the Store's properties to get the current state. You can also use some extension methods, such as Connect, Dispatch, etc., to simplify your code.

## Example

You can find a simple example in this repository, showing how to use Maui.Redux to implement a Todo List application. You can run the following commands to clone this repository and run the example:

```bash
git clone https://github.com/GavinHome/Maui.Redux.git
cd Maui.Redux
```
You can also check the source code in the [Example] folder to understand the implementation details of the example.
