using samples.Pages.Todos.Todo;
namespace samples.Pages.Todos.Edit;

public partial class TodoEditPage() : Page<TodoEditState, ToDoState>(
        initState: initState,
        effect: buildEffect(),
        middlewares:
        [
            Redux.Middlewares.logMiddleware<TodoEditState>(tag: "TodoEditPage")
        ],
        view: (state, dispatch, ctx) => new View()
        {
            BindingContext = state,
            OnDoneCommand = ReactiveCommand.Create(() => dispatch(ToDoEditActionCreator.onDone()))
        })

{
    private static TodoEditState initState(ToDoState? arg) => new() { toDo = arg?.Clone() ?? new() };
}
