using Microsoft.Xna.Framework;

namespace CardGames.Console;

public class ColoredString
{
    public required string text;
    public Color color;
    public Color? backgroundColor;
    public bool EndsWithNewline;
}
