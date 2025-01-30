using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CardGames.Console;

public class CommandParamInfo
{
    protected List<AutoCompl>? DynamicAutoCompls;
    protected List<string>? StaticAutoCompls;
    public Type type;
    public List<string>? AutoCompls;

    public CommandParamInfo(ParameterInfo info)
    {
        type = info.ParameterType;
        DynamicAutoCompls = new();
        StaticAutoCompls = new();
        foreach (AutoCompl attr in info.GetCustomAttributes<AutoCompl>())
            if (attr.Dynamic) DynamicAutoCompls.Add(attr);
            else StaticAutoCompls.AddRange(attr.Get());
    }

    public void UpdateAutoCompls()
    {
        AutoCompls = new();
        AutoCompls.Clear();
        if (StaticAutoCompls is not null)
            AutoCompls.AddRange(StaticAutoCompls);

        if (DynamicAutoCompls is null) return;

        AutoCompls.AddRange(DynamicAutoCompls.SelectMany(ac => ac.Get()));
    }
}
