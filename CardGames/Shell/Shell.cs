using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoPlus.Input;
using MonoPlus.Time;

namespace CardGames.Console;

public static class Shell
{
    public static ShellDrawer? drawer;
    public static ShellInput? input;
    public static List<ShellLine>? Lines;
    public static Dictionary<string, CommandInfo>? Commands;
    public static int LineOffset;
    private static float LineOffsetDelay;

    public static void Initialize()
    {
        drawer = new();
        input = new();
        Lines = new();
        AutoComplProviders.Initialize();
        FindCommands(Assembly.GetExecutingAssembly());
    }

    public static void Update()
    {
        input?.Update();
        LineOffsetDelay -= Time.DeltaTime;
        if (Lines is null || LineOffsetDelay > 0) return;

        if (Input.Down(Keys.Up))
            LineOffset++;
        else if (Input.Down(Keys.Down))
            LineOffset--;
        else
            return;
        LineOffsetDelay = 0.05f;
        LineOffset = Math.Clamp(LineOffset, 0, Lines.Count - 1);
    }

    public static void Draw()
    {
        drawer?.Draw();
    }

    public static void FindCommands(Assembly assembly)
    {
        Commands = new();
        //haha linq goes brrrr
        IEnumerable<MethodInfo> methods = from type in assembly.GetTypes()
            from method in type.GetMethods()
            where method.IsDefined(typeof(CommandAttribute), true)
            select method;

        foreach (MethodInfo method in methods)
        {
            if (!method.IsStatic)
            {
                Log($"Method {method.Name} in type {method.DeclaringType?.FullName ?? "<unknown>"} is not static!", Color.Red);
                return;
            }

            Commands.Add(method.Name.ToLower(), new CommandInfo(method));
        }
    }


    public static void Run(string commandInput)
    {
        Log($"> {commandInput}", Color.LightGreen);
        if (Commands is null)
        {
            Log("The Commands is null!", Color.Red);
            return;
        }
        string[] commandString = commandInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (commandString.Length == 0) return;
        string commandName = commandString[0];

        if (!Commands.TryGetValue(commandName, out CommandInfo? command))
        {
            Log($"Command not found: {commandName} {(commandName == commandInput ? "" : $"({commandInput})")}", Color.Red);
            return;
        }

        command.Execute();
    }


    public static void Log(object o, Color color = default)
    {
        string? line = o.ToString();
        if (line is null) throw new ArgumentException($"{o}.ToString() returns null!");
        Lines?.Add(new(line, color));
    }

    public static void Clear() => Lines?.Clear();
}
