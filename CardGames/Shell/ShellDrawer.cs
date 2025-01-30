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

        Vector2 drawPos = new(30, Engine.gameWindow.ClientBounds.Height - 50);
        TextField textField = Shell.input.input;
        float arrowOffset = font.MeasureString("> ").X;
        
        //selection
        if (textField.SelectionStart != textField.SelectionEnd)
        {
            Vector2 selectionDrawOffset = new Vector2(font.MeasureString(textField.text.ToString().Remove(textField.SelectionStart)).X + arrowOffset, 0) + drawPos;
            Graphics.DrawRect(selectionDrawOffset, selectionDrawOffset + font.MeasureString(textField.GetSelection()), SelectionColor);
        }

        //Input line
        Graphics.DrawText(font, $"> {textField.text}", drawPos, Color.White);

        //Drawing cursor
        Vector2 cursorDrawOffset = textField.CursorPos == 0 ? new(0, font.MeasureString(" ").Y) : font.MeasureString(textField.text.ToString().Remove(textField.CursorPos));
        cursorDrawOffset.X += arrowOffset;
        Graphics.DrawLine(new Vector2(drawPos.X + cursorDrawOffset.X, drawPos.Y), drawPos + cursorDrawOffset, Color.White);

        //Lines log
        for (int itemIndex = Shell.Lines.Count - 1; itemIndex >= 0; itemIndex--)
        {
            ShellLine line = Shell.Lines[itemIndex];
            string lineText = font.WrapText(line.Text, Engine.WindowWidth);
            Vector2 lineSize = font.MeasureString(lineText);
            drawPos.Y -= (int)lineSize.Y;
            if (drawPos.Y + lineSize.Y < 0) break;
            Graphics.DrawText(font, lineText, drawPos, line.color);
        }

        Graphics.End();
    }
}
