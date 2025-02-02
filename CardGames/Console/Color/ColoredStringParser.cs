using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Chasm.Formatting;
using MonoPlus.Graphics;

namespace CardGames.Console;

public static class ColoredStringParser
{
    private static Color DefaultColor;
    private static Color? currentColor;
    private static Color color => currentColor ?? DefaultColor;

    public static List<ColoredString> Parse(string text, Color? defaultColor = null)
    {
        if (defaultColor is not null) DefaultColor = (Color)defaultColor;
        if (Engine.Font != null) text = Engine.Font.WrapText(text, Engine.WindowWidth - 30);
        List<ColoredString> result = new();
        SpanParser parser = new(text);

        while (parser.CanRead())
        {
            ReadOnlySpan<char> fragment = parser.ReadUntil(ch => ch is '\n' or '{');

            result.Add(new ColoredString
            {
                text = fragment.ToString(),
                color = color,
                EndsWithNewline = parser.Skip('\n')
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
    }

    private static void ParseSpecial(ref SpanParser parser)
    {
        if (parser.Skip('#'))
            currentColor = GraphicUtils.ParseColor(parser.ReadUntil('}').ToString());

        if (!parser.Skip('}')) throw new FormatException();
    }
}
