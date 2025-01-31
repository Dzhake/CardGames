using System.Collections.Generic;
using System.Reflection;

namespace CardGames.Console;

public class CommandInfo
{
    public int ArgsCount;
    public CommandParamInfo[]? ParamInfos;
    public MethodInfo Method;

    public CommandInfo(MethodInfo method)
    {
        Method = method;
        ParameterInfo[] paramInfos = Method.GetParameters();
        ArgsCount = paramInfos.Length;
        ParamInfos = new CommandParamInfo[ArgsCount];
        for (int i = 0; i < paramInfos.Length; i++)
        {
            ParameterInfo param = paramInfos[i];
            ParamInfos[i] = new(param);
        }
    }

    public void Execute()
    {
        object[] args = new object[ArgsCount];
        Method.Invoke(null, args);
    }

    public void UpdateDynamicAutoCompls(int param)
    {
        if (ParamInfos is null || ParamInfos.Length < param) return;
        ParamInfos[param].UpdateAutoCompls();
    }

    public List<string>? GetAutoCompls(int param)
    {
        if (ParamInfos is null || ParamInfos.Length < param) return null;
        return ParamInfos[param].AutoCompls;
    }
}
