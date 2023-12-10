namespace samples.Pages.Todos.Report;

internal class ReportComponent() : Component<ReportState>(
        view: (state, dispatch, _) => new View() { BindingContext = state });