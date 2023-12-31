namespace samples.Pages.Counter;

public class CounterState : ReactiveObject
{
    [Reactive]
    public int Count { get; set; }

    public string? Title { get; set; }

    public override string ToString()
    {
        return $"Count: {Count}";
    }
}