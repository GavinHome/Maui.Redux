﻿namespace samples.Pages.Todos.Todo;
using Action = Redux.Action;

internal partial class TodoComponent() : Component<ToDoState>(
        effect: buildEffect(),
        reducer: buildReducer(),
        view: (state, dispatch, _) => new View()
        {
            BindingContext = state,
            OnDoneCommand = ReactiveCommand.Create(() => dispatch(ToDoActionCreator.doneAction(state.UniqueId))),
            OnEditCommand = ReactiveCommand.Create(() => dispatch(ToDoActionCreator.onEditAction(state.UniqueId))),
            OnRemoveCommand = ReactiveCommand.Create(() => dispatch(ToDoActionCreator.onRemoveAction(state.UniqueId)))
        });