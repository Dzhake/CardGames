using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Chasm.Formatting;

namespace CardGames.Console;

public class ColoredStringParser
{
    public Color defaultColor;
    public Color? currentColor;
    public Color color => currentColor ?? defaultColor;

    public List<ColoredString> Parse(string text)
    {
        List<ColoredString> result = new();
        currentColor = defaultColor;
        SpanParser parser = new(text);

        while (parser.CanRead())
        {
            ReadOnlySpan<char> fragment = parser.ReadUntil('{');

            result.Add(new(fragment.ToString(), color));

            if (parser.Skip('{'))
                ParseSpecial(ref parser);
        }

        return result;
    }

    protected void ParseSpecial(ref SpanParser parser)
    {
        if (parser.Skip('#'))
            currentColor = Util.ParseColor(parser.ReadUntil('}').ToString());

        if (!parser.Skip('}')) throw new FormatException();
    }
}
