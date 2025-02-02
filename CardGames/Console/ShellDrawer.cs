using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoPlus.Graphics;


namespace CardGames.Console;

public class ShellDrawer
{
    public Color SelectionColor = new(0, 120, 215);

    public void Draw()
    {
        if (Engine.gameWindow is null || Engine.Font is null || Shell.input is null || Shell.Lines is null) return;
        SpriteFont font = Engine.Font;

        Graphics.Clear(Color.Black);
        Graphics.Begin();
        
        Vector2 arrowOffset = font.MeasureString("> ");
        Vector2 drawPos = new(arrowOffset.X, Engine.gameWindow.ClientBounds.Height - arrowOffset.Y - 30);
        TextField textField = Shell.input.input;
        

        //selection
        if (textField.SelectionStart != textField.SelectionEnd)
        {
            Vector2 selectionDrawOffset = new Vector2(font.MeasureString(textField.text.ToString().Remove(textField.SelectionStart)).X + arrowOffset.X, 0) + drawPos;
            Graphics.DrawRect(selectionDrawOffset, selectionDrawOffset + font.MeasureString(textField.GetSelection()), SelectionColor);
        }

        //Input line
        Graphics.DrawText(font, $"> {textField.text}", drawPos, Color.White);

        //Drawing cursor
        Vector2 cursorDrawOffset = textField.CursorPos == 0 ? new(0, font.MeasureString(" ").Y) : font.MeasureString(textField.text.ToString().Remove(textField.CursorPos));
        cursorDrawOffset.X += arrowOffset.X;
        Graphics.DrawLine(new Vector2(drawPos.X + cursorDrawOffset.X, drawPos.Y), drawPos + cursorDrawOffset, Color.White);

        //Lines log
        for (int lineIndex = Shell.Lines.Count - 1 - Shell.LineOffset; lineIndex >= 0; lineIndex--)
        {
            ShellLine shellLine = Shell.Lines[lineIndex];
            drawPos.Y -= shellLine.Size.Y;
            drawPos.X = arrowOffset.X;
            foreach (ColoredString coloredStr in shellLine.Parts)
                DrawColoredString(font, coloredStr, ref drawPos, arrowOffset);
            drawPos.Y -= shellLine.Size.Y;
            if (drawPos.Y < 0) break;
        }

        Graphics.End();
    }

    protected virtual void DrawColoredString(SpriteFont font, ColoredString coloredStr, ref Vector2 drawPos, Vector2 arrowOffset)
    {
        Vector2 coloredStrSize = font.MeasureString(coloredStr.text);
        if (coloredStr.backgroundColor is not null) Graphics.DrawRect(drawPos, drawPos + coloredStrSize, (Color)coloredStr.backgroundColor);
        Graphics.DrawText(font, coloredStr.text, drawPos, coloredStr.color);
        drawPos.X += coloredStrSize.X;
        if (coloredStr.EndsWithNewline)
        {
            drawPos.X = arrowOffset.X;
            drawPos.Y += font.LineSpacing;
        }
    }
}
