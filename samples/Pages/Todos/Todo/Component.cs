namespace samples.Pages.Todos.Todo;
using Action = Redux.Action;

internal partial class TodoComponent : Component<ToDoState>
{
    public TodoComponent() : base(
        effect: buildEffect(),
        reducer: buildReducer(),
        view: (state, dispatch, _) => new View())
    {
    }
}