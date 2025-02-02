using System.Collections.Generic;
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
        for (int itemIndex = Shell.Lines.Count - 1 - Shell.LineOffset; itemIndex >= 0; itemIndex--)
        {
            ShellLine line = Shell.Lines[itemIndex];
            drawPos.Y -= line.Size.Y;
            foreach (ColoredString str in line.Parts)
            {
                Graphics.DrawText(font, str.text, drawPos, str.color);
                if (str.EndsWithNewline)
                {
                    drawPos.X = arrowOffset.X;
                    drawPos.Y += font.LineSpacing;
                }
            }
            if (drawPos.Y < 0) break;
        }

        Graphics.End();
    }
}
