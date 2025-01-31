using System;

namespace CardGames.Console;

[AttributeUsage(AttributeTargets.Method)]
public class CommandAttribute(params string[] aliases) : Attribute
{
    public string[] aliases = aliases;
}
