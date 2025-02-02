using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CardGames.Console;

public class ShellLine
{
    public List<ColoredString> Parts;
    public Vector2 Size;
    public bool EndsWithNewline;

    public ShellLine(string text, Color color)
    {
        if (Engine.Font is null) throw new InvalidOperationException($"Tried to create {GetType().FullName ?? "..full type name is null.."} but Engine.Font is null! ");
        Size = Engine.Font.MeasureString(text);
        Parts = ColoredStringParser.Parse(text, color);
    }
}
