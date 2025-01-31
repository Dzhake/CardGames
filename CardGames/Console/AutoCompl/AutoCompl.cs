using System;
using System.Collections.Generic;

namespace CardGames.Console;

[AttributeUsage(AttributeTargets.Parameter)]
public abstract class AutoCompl : Attribute
{
    public bool Dynamic = false;
    public abstract List<string> Get();
}
