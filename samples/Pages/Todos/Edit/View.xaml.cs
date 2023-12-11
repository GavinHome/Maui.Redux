using System.Windows.Input;

namespace samples.Pages.Todos.Edit;

public partial class View : ContentPage
{
    public static readonly BindableProperty OnDoneCommandProperty =
          BindableProperty.Create(nameof(OnDoneCommand), typeof(ICommand), typeof(View));

    public ICommand OnDoneCommand
    {
        get => (ICommand)GetValue(OnDoneCommandProperty);
        set => SetValue(OnDoneCommandProperty, value);
    }

    public View()
	{
		InitializeComponent();
	}
}