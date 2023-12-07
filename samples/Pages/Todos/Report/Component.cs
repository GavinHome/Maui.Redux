namespace samples.Pages.Todos.Report;

internal class ReportComponent : Component<ReportState>
{
    public ReportComponent() : base(
        view: (state, dispatch, _) => new View() { BindingContext = state })
    {
    }
}