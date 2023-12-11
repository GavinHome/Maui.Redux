namespace Redux.Component;

/// Define a basic behavior of routes.
public interface AbstractRoutes
{
    Widget home { get; }

    Widget buildPage(string? path, dynamic? arguments = null);
}

/// Each page has a unique store.
public class PageRoutes(Map? pages, string? initialRoute = null) : AbstractRoutes
{
    readonly Map pages = pages ?? [];
    readonly string? initialRoute = initialRoute;

    string? initialRoutePath => initialRoute ?? pages.Keys.FirstOrDefault();

    public Widget home => buildHome(initialRoutePath);

    public Widget buildPage(string? path, dynamic? arguments = null) => pages[path!].buildPage(arguments);

    private Widget buildHome(string? path, dynamic? arguments = null)
    {
        var content = pages[path!].buildPage(arguments);
        Navigator.of().push(new Route<dynamic>(new RouteSettings(path, arguments), content));
        return content;
    }
}
