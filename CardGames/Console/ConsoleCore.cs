using System.Collections.Generic;

namespace CardGames.Console;

public static class ConsoleCore
{
    public static Drawer drawer;
    public static ConsoleInput input;
    public static List<string> Lines;

    public static void Initialize()
    {
        drawer = new();
        input = new();
        Lines = new();
    }

    public static void Log(string line) => Lines.Add(line);

    public static void Log(object o) => Log(o.ToString());

    public static void Update()
    {
        input.Update();
        Log($"Cursor Pos: {input.input.CursorPos}, SelectionStart: {input.input.SelectionStart}, SelectionEnd: {input.input.SelectionEnd}");
    }

    public static void Draw()
    {
        drawer.Draw();
    }
}
