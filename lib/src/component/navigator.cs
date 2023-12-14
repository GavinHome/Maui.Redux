/* 
 * 
 * The navigator is not perfect, and it's a simple implementation.
 * 
 */
namespace Redux.Component;

/// [RouteSettings]
public record RouteSettings(string name = "", dynamic? arguments = null);

/// [RouteFactory]
/// Definition of a standard RouteFactory function.
public delegate dynamic RouteFactory(RouteSettings settings);

/// [NavigateFactory]
/// Definition of a standard NavigateFactory function.
public delegate dynamic RouteChanged(Route<dynamic>? route);

/// [Navigator]
public class Navigator : StatefulWidget
{
    private static readonly NavigatorState navigatorState = new();

    public static RouteChanged? onRouteChanged { get; private set; }

    public static RouteFactory? onGenerateRoute { get; private set; }

    public override NavigatorState createState() => navigatorState;

    public static NavigatorState of(dynamic? _ = null)
    {
        return navigatorState;
    }

    public static void build(AbstractRoutes routes, RouteChanged routeChanged, RouteFactory? generateRoute = null, dynamic? arguments = null)
    {
        onRouteChanged = routeChanged;
        onGenerateRoute ??= generateRoute;
        buildHome(routes, arguments);
    }

    private static void buildHome(AbstractRoutes routes, dynamic? arguments = null)
    {
        ArgumentNullException.ThrowIfNull(onRouteChanged, nameof(onRouteChanged));
        onGenerateRoute ??= settings => routes.buildPage(settings.name, settings.arguments);
        Navigator.of().push<dynamic>(routes.home, arguments);
    }
}

/// [NavigatorState]
public class NavigatorState : State<StatefulWidget>
{
    readonly Stack<_RouteEntry> _history = new();
    private _RouteEntry? _current;

    public override Widget build(dynamic context)
    {
        throw new NotImplementedException();
    }

    public async Task<Route<dynamic>> push<T>(string routeName, dynamic? arguments = null, Action<T?>? call = null) where T : class
    {
        Action<dynamic?> func = (x) => call?.Invoke((T?)Convert.ChangeType(x, typeof(T)));
        Route<dynamic> route = _routeNamed<dynamic>(routeName: routeName, arguments: arguments, func: func);
        return await push(route);
    }

    public async Task<Route<dynamic>?> pop<T>(T? result)
    {
        return await _pop(result);
    }

    Task<Route<dynamic>> push(Route<dynamic> route)
    {
        ArgumentNullException.ThrowIfNull(route);

        _RouteEntry _entry = new(route);
        _history.Push(_entry);
        _current = _history.First();
        Navigator.onRouteChanged?.Invoke(_current.Route);
        return Task.Run(() => _current.Route);
    }

    Route<T> _routeNamed<T>(string routeName, dynamic? arguments = null, Action<dynamic?>? func = null) where T : class
    {
        var content = Navigator.onGenerateRoute?.Invoke(new RouteSettings(routeName, arguments));
        Route<T> route = new(new RouteSettings(routeName, arguments), content, func);
        return route;
    }

    Task<Route<dynamic>?> _pop<T>(T? result)
    {
        _current?.Route.Func?.Invoke(result as dynamic);
        _history.Pop();
        _current = _history.FirstOrDefault();
        Navigator.onRouteChanged?.Invoke(_current?.Route);
        return Task.Run(() => _current?.Route);
    }
}

/// [Route]
public class Route<T>(RouteSettings settings, dynamic content, Action<dynamic?>? func = null) where T : class
{
    readonly RouteSettings _settings = settings;
    readonly dynamic? _content = content;
    private readonly Action<T?>? _func = func;

    public RouteSettings settings => _settings!;
    public dynamic Content => _content!;
    public Action<T?>? Func => _func;
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

