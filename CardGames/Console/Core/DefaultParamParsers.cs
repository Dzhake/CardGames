namespace CardGames.Console;

public class DefaultParamParsers
{
    public static void Initialize()
    {
        Shell.ParamParsers = new()
        {
            {typeof(int), Int}
        };
    }

    public static object? Int(string param) => int.TryParse(param, out int value) ? value : null;

    public static object? Float(string param) => float.TryParse(param, out float value) ? value : null;

    public static object? String(string param) => param;


}
