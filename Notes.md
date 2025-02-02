# Styling guide

Text in console supports some fancy styling, i'll write it down here to not forget.

Style commands are "meta info" for text. They're not rendered, but they affect how text looks.  
Every style command is inside curly braces ("{" and "}").  
All style commands only affect text after them.  

Style commands are wiped when line is logged to console.

## Value types

Style command accept various values. But here're most common ones:

- <string> — Any value without newlines and curly braces.
- <int> — Integer decimal value.
- <float> — Non-int decimal value.
- <color> — RGB hex code for color, supports 6 or 8 symbols. (RRGGBB or RRGGBBAA).

## Existing styles

- {#<color>} — Sets text's foreground color
- {##<color>} — Sets text's background color
- {#} — Resets text's foreground color to default value for this line
- {##} — Resets text's background color to default value (Black)