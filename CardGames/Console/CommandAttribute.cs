using System;

namespace CardGames.Console;

[AttributeUsage(AttributeTargets.Class)]
public class CommandAttribute(params string[] names) : Attribute
{
    public string[] names = names;
}
