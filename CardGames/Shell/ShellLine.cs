using Microsoft.Xna.Framework;

namespace CardGames.Console;

public class ShellLine
{
    public string Text;
    public Color color;

    public ShellLine(string text, Color color)
    {
        Text = text;
        this.color = color;
    }
}
