using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CardGames.Console;

public class ColoredString
{
    public string text = "";
    public Color color;

    public ColoredString(string text, Color color)
    {
        this.text = text;
        this.color = color;
    }
}
