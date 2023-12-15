namespace samples.Pages.Todos.Todo;

public partial class View : ContentView
{
    public static readonly BindableProperty OnEditCommandProperty =
          BindableProperty.Create(nameof(OnEditCommand), typeof(ICommand), typeof(View));

    public static readonly BindableProperty OnRemoveCommandProperty =
      BindableProperty.Create(nameof(OnRemoveCommand), typeof(ICommand), typeof(View));

    public static readonly BindableProperty OnDoneCommandProperty =
      BindableProperty.Create(nameof(OnDoneCommand), typeof(ICommand), typeof(View));

    public View()
    {
        InitializeComponent();
    }

    public ICommand OnEditCommand
    {
        get => (ICommand)GetValue(OnEditCommandProperty);
        set => SetValue(OnEditCommandProperty, value);
    }

    public ICommand OnRemoveCommand
    {
        get => (ICommand)GetValue(OnRemoveCommandProperty);
        set => SetValue(OnRemoveCommandProperty, value);
    }

    public ICommand OnDoneCommand
    {
        get => (ICommand)GetValue(OnDoneCommandProperty);
        set => SetValue(OnDoneCommandProperty, value);
    }
}