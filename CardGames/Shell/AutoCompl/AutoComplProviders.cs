using System.Collections.Generic;
using System;

namespace CardGames.Console;

public class AutoComplProviders
{
    public static Dictionary<Type, Func<string, string[]>>? TypeAutoCompls;
    public static event Action? OnTypeAutoComplsCreated;

    public static void Initialize()
    {
        CreateTypeAutoCompls();
    }

    private static void CreateTypeAutoCompls()
    {
        TypeAutoCompls = new();
        OnTypeAutoComplsCreated?.Invoke();
    }
}
