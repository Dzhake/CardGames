using System;
using System.Collections.Generic;
using System.Reflection;

namespace CardGames.Console;

public class CommandInfo
{
    public int ParamsCount;
    public CommandParamInfo[] ParamInfos;
    public MethodInfo Method;

    public CommandInfo(MethodInfo method)
    {
        Method = method;
        ParameterInfo[] paramInfos = Method.GetParameters();
        ParamsCount = paramInfos.Length;
        ParamInfos = new CommandParamInfo[ParamsCount];
        for (int i = 0; i < paramInfos.Length; i++)
        {
            ParameterInfo param = paramInfos[i];
            ParamInfos[i] = new(param);
        }
    }

    public void Execute(string commandInput)
    {
        if (Shell.ParamParsers is null) return;
        object?[] paramValues = new object[ParamsCount];
        string[] commandParams = commandInput.Split(' ');
        for (int i = 0; i < ParamInfos.Length; i++)
        {
            if (commandParams.Length + 1 < i) break;
            CommandParamInfo paramInfo = ParamInfos[i];
            if (!Shell.ParamParsers.TryGetValue(paramInfo.type, out Func<string, object?>? paramParser))
                throw new InvalidOperationException($"Tried to convert command param to {paramInfo.type.FullName}, but parser for it was not found!");
            if (paramParser is null) throw new InvalidOperationException($"Tried to convert command param to {paramInfo.type.FullName}, but parser for it is null!");
            paramValues[i] = paramParser(commandParams[i + 1]);
        }
        Method.Invoke(null, paramValues);
    }

    public void UpdateDynamicAutoCompls(int param)
    {
        if (ParamInfos.Length < param) return;
        ParamInfos[param].UpdateAutoCompls();
    }

    public List<string>? GetAutoCompls(int param)
    {
        return ParamInfos.Length < param ? null : ParamInfos[param].AutoCompls;
    }
}
