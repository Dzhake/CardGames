using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Chasm.Formatting;
using Microsoft.Xna.Framework.Graphics;
using MonoPlus.Graphics;

namespace CardGames.Console;

public static class ColoredStringParser
{
    private static Color DefaultColor;
    private static Color? currentColor;
    private static Color? backgroundColor;
    private static Color color => currentColor ?? DefaultColor;

    public static List<ColoredString> Parse(string text, out Vector2 totalSize, Color? defaultColor = null)
    {
        SpriteFont? font = Engine.Font;
        if (font is null) throw new InvalidOperationException("Tried to parse colored string, but Engine.Font is null!");
        totalSize = Vector2.Zero;
        if (defaultColor is not null) DefaultColor = (Color)defaultColor;
        text = font.WrapText(text, Engine.WindowWidth - 30);
        List<ColoredString> result = new();
        SpanParser parser = new(text);

        while (parser.CanRead())
        {
            ReadOnlySpan<char> fragment = parser.ReadUntil(ch => ch is '\n' or '{');

            string fragmentString = fragment.ToString();
            float fragmentWidth = font.MeasureString(fragmentString).X;
            if (fragmentWidth > totalSize.X) totalSize.X = fragmentWidth;

            bool newline = parser.Skip('\n') || !parser.CanRead();
            if (newline)
                totalSize.Y += font.LineSpacing;

            result.Add(new ColoredString
            {
                text = fragmentString,
                color = color,
                backgroundColor = backgroundColor,
                EndsWithNewline = newline
            });

            if (parser.Skip('{'))
                ParseSpecial(ref parser);
        }

        Reset();
        return result;
    }

    public static void Reset()
    {
        currentColor = null;
        DefaultColor = Color.White;
        backgroundColor = null;
    }

    private static void ParseSpecial(ref SpanParser parser)
    {
        if (parser.Skip('#'))
        {
            if (parser.Skip('}'))
            {
                currentColor = null;
                return;
            }

            if (parser.Skip('#')) //background color
            {
                if (parser.Skip('}'))
                {
                    backgroundColor = null;
                    return;
                }
                backgroundColor = GraphicUtils.ParseColor(parser.ReadUntil('}').ToString());
            }
            else
                currentColor = GraphicUtils.ParseColor(parser.ReadUntil('}').ToString());
        }

        if (!parser.Skip('}')) throw new FormatException();
    }
}
