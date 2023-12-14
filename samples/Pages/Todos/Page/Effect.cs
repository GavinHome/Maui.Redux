using samples.Pages.Todos.Todo;

namespace samples.Pages.Todos.Page;
using Action = Redux.Action;

public partial class ToDoListPage
{
    private static Effect<PageState>? buildEffect() => EffectConverter.CombineEffects(new Dictionary<object, SubEffect<PageState>>
    {
        { Lifecycle.initState, _onInit },
        { "onAdd", _onAdd }
    });

    private static async Task _onInit(Action action, ComponentContext<PageState> ctx)
    {
        List<ToDoState> initToDos =
        [
            new ToDoState(uniqueId: "0", title: "Hello World", desc: "Learn how to program.", isDone: true),
            new ToDoState(uniqueId: "1", title: "Hello Maui", desc: "Learn how to build an Maui app.", isDone: true),
            new ToDoState(uniqueId: "2", title: "Hello Maui Redux", desc: "Learn how to use Maui Redux in an Maui app."),
        ];

        ctx.Dispatch(new Action("initToDos", payload: initToDos));
        await Task.CompletedTask;
    }

    private static async Task _onAdd(Action action, ComponentContext<PageState> ctx)
    {
        await Navigator.of(ctx)
            .push<ToDoState>("todo_edit", arguments: null, (toDo) =>
            {
                if (toDo != null && !string.IsNullOrEmpty(toDo.Title) && !string.IsNullOrEmpty(toDo.Desc))
                {
                    ctx.Dispatch(new Action("add", payload: new ToDoState
                    {
                        Title = $"{toDo.Title}",
                        Desc = $"{toDo.Desc}",
                    }));
                }
            });

        await Task.CompletedTask;
    }
}
