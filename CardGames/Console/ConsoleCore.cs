using System.Collections.Generic;

namespace CardGames.Console;

public static class ConsoleCore
{
    public static Drawer drawer;
    public static ConsoleInput input;
    public static Queue<string> Lines;

    public static void Initialize()
    {
        drawer = new();
        input = new();
        Lines = new();
    }

    public static void EnqueueLine(string line)
    {
        Lines.Enqueue(line);
    }

    public static void Update()
    {
        input.Update();
    }

    public static void Draw()
    {
        drawer.Draw();
    }
}
