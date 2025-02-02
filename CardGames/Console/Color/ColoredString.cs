using Microsoft.Xna.Framework;

namespace CardGames.Console;

public class ColoredString
{
    public string text;
    public Color color;
    public bool EndsWithNewline;

    public ColoredString(string text, Color color, bool endsWithNewline)
    {
        this.text = text;
        this.color = color;
        EndsWithNewline = endsWithNewline;
    }
}
