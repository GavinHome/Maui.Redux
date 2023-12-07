using System.Security.Cryptography;
using System.Windows.Input;

namespace samples.Pages.Todos.Page;

public partial class AddButton : ContentView
{
    public static readonly BindableProperty OnAddTodoProperty =
              BindableProperty.Create(nameof(OnAddTodo), typeof(ICommand), typeof(AddButton));

    public ICommand OnAddTodo
    {
        get => (ICommand)GetValue(OnAddTodoProperty);
        set => SetValue(OnAddTodoProperty, value);
    }

    public AddButton()
    {
        InitializeComponent();
        this.BindingContext = this;
    }
}