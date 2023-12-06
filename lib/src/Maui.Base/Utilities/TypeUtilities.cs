using System.Runtime.CompilerServices;

namespace Maui.Utilities;

/// <summary>
/// Provides utilities for working with types at runtime.
/// </summary>
public static class TypeUtilities
{
    /// <summary>
    /// Returns a value indicating whether value can be casted to the specified type.
    /// If value is null, checks if instances of that type can be null.
    /// </summary>
    /// <typeparam name="T">The type to cast to</typeparam>
    /// <param name="value">The value to check if cast possible</param>
    /// <returns>True if the cast is possible, otherwise false.</returns>
    public static bool CanCast<T>(object? value)
    {
        return value is T || value is null && AcceptsNull<T>();
    }

    /// <summary>
    /// Returns a value indicating whether null can be assigned to the specified type.
    /// </summary>
    /// <typeparam name="T">The type</typeparam>
    /// <returns>True if the type accepts null values; otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AcceptsNull<T>()
    {
        return default(T) is null;
    }
}
