using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;
using Microsoft.Xna.Framework;

namespace CardGames.Console;

public class ShellRunner
{
    public static void FindCommands(Assembly assembly)
    {
        Shell.Commands = new();
        //haha linq goes brrrr
        IEnumerable<MethodInfo> methods = from type in assembly.GetTypes()
            from method in type.GetMethods()
            where method.IsDefined(typeof(CommandAttribute), true)
            select method;

        foreach (MethodInfo method in methods)
        {
            if (!method.IsStatic)
            {
                Shell.Log($"Method {method.Name} in type {method.DeclaringType?.FullName ?? "<unknown>"} is not static!", Color.Red);
                return;
            }

            Shell.Commands.Add(method.Name.ToLower(), new CommandInfo(method));
        }
    }


    public void Run(string commandInput)
    {
        Shell.Log($"> {commandInput}", Color.LightGreen);
        if (Shell.Commands is null)
        {
            Shell.Log("Commands is null!", Color.Red);
            return;
        }

        string[] commandString = commandInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (commandString.Length == 0) return;
        string commandName = commandString[0];

        if (!Shell.Commands.TryGetValue(commandName, out CommandInfo? command))
        {
            Shell.Log($"Command not found: {commandName} {(commandName == commandInput ? "" : $"({commandInput})")}", Color.Red);
            return;
        }

        command.Execute(commandInput);
    }
}
