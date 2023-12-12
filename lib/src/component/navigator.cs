/* 
 * [ToDo]
 * The navigator is not perfect, but I have to a simple implementation for route.
 * 
 */
using Microsoft.Maui.Controls;

namespace Redux.Component;
using Widget = ContentPage;

/// [RouteSettings]
public record RouteSettings(string name = "", dynamic? arguments = null);

/// [RouteFactory]
/// Definition of a standard RouteFactory function.
public delegate dynamic RouteFactory(RouteSettings settings);

/// [Route]
public class Route<T>(RouteSettings? settings, dynamic content) where T : class
{
    readonly RouteSettings _settings = settings ?? new RouteSettings();
    readonly dynamic? _content = content;

    public dynamic Content => _content!;
    public RouteSettings settings => _settings!;

    public Action<T>? Func { get; internal set; }
}

/// [_RouteEntry]
class _RouteEntry(Route<dynamic> route)
{
    readonly Route<dynamic>? _route = route;

    public Route<dynamic> Route => _route!;

    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

/// [Navigator]
public class Navigator : StatefulWidget
{
#pragma warning disable CA2211 // 非常量字段应当不可见
    public static System.Action<RouteSettings>? onChange;
#pragma warning restore CA2211 // 非常量字段应当不可见
    private static readonly NavigatorState navigatorState = new();

    public static RouteFactory? onGenerateRoute { get; set; }

    public static NavigatorState of(dynamic? _ = null)
    {
        return navigatorState;
    }

    public override NavigatorState createState() => navigatorState;
}

/// [NavigatorState]
public class NavigatorState : State<StatefulWidget>
{
    readonly Stack<_RouteEntry> _history = new();
    private _RouteEntry? _current;

    public Widget current => _current!.Route.Content;
    public Route<dynamic> route => _current!.Route;

    public override Widget build(dynamic context)
    {
        throw new NotImplementedException();
    }

    public async Task<Route<dynamic>> pushNamed<T>(string routeName, dynamic? arguments, Action<T>? call = null) where T : class
    {
        Route<dynamic> route = _routeNamed<dynamic>(routeName: routeName, arguments: arguments);
        route.Func = (x) => call?.Invoke((T)Convert.ChangeType(x, typeof(T)));
        return await push(route);
    }

    public async Task<Route<dynamic>> pop<T>(T? result)
    {
        return await _pop(result);
    }

    public Task<Route<dynamic>> push(Route<dynamic> route)
    {
        ArgumentNullException.ThrowIfNull(route);

        _pushEntry(new _RouteEntry(route));
        return Task.Run(() => _history.Pop().Route);
    }

    Route<T>? _routeNamed<T>(string routeName, dynamic? arguments) where T : class
    {
        var content = Navigator.onGenerateRoute?.Invoke(new RouteSettings(routeName, arguments));
        Route<T>? route = new(new RouteSettings(routeName, arguments), content);
        return route;
    }

    void _pushEntry(_RouteEntry entry)
    {
        if (_current != null && !_current.Equals(entry))
        {
            _history.Push(_current!);
        }

        _current = entry;
        _history.Push(entry);
        Navigator.onChange?.Invoke(_current.Route.settings);
    }

    Task<Route<dynamic>> _pop<T>(T? result)
    {
        _current?.Route.Func?.Invoke(result as dynamic);
        var entry = _history.Pop();
        _current = entry;
        Navigator.onChange?.Invoke(_current.Route.settings);
        return Task.Run(() => _current.Route);
    } 
}
