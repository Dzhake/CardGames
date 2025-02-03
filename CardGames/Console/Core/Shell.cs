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
    public static ShellRunner? runner;
    public static List<ShellLine>? Lines;
    public static Dictionary<string, CommandInfo>? Commands;
    public static Dictionary<Type, Func<string, object?>>? ParamParsers;
    public static int LineOffset;
    public static Menu? CurrentMenu;
    public static float LineOffsetDelay;

    public static void Initialize()
    {
        drawer = new ShellDrawer();
        input = new ShellInput();
        runner = new ShellRunner();
        Lines = new List<ShellLine>();
        DefaultParamParsers.Initialize();
        AutoComplProviders.Initialize();
        ShellRunner.FindCommands(Assembly.GetExecutingAssembly());
        CurrentMenu = new MainMenu();
    }

    public static void Update()
    {
        input?.Update();
        CurrentMenu?.Update();
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

    public static void SetMenu(Menu menu)
    {
        if (CurrentMenu == menu) return;
        CurrentMenu = menu;
        CurrentMenu.Start();
    }


    public static void Log(object o, Color? color = null)
    {
        color ??= Color.White;
        string? line = o.ToString();
        if (line is null) throw new ArgumentException($"{o}.ToString() returns null!");
        Lines?.Add(new(line, (Color)color));
    }

    public static void Clear() => Lines?.Clear();
}
