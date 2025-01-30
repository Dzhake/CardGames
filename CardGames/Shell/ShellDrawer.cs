using Microsoft.Xna.Framework;
using MonoPlus.Graphics;


namespace CardGames.Console;

public class ShellDrawer
{
    public Color SelectionColor = new(0, 120, 215);
    public void Draw()
    {
        if (Engine.gameWindow is null || Engine.Font is null || Shell.input is null || Shell.Lines is null) return;

        Graphics.Clear(Color.Black);
        Graphics.Begin();

        Vector2 drawPos = new(30, Engine.gameWindow.ClientBounds.Height - 50);
        TextField textField = Shell.input.input;
        float arrowOffset = Engine.Font.MeasureString("> ").X;
        
        //selection
        if (textField.SelectionStart != textField.SelectionEnd)
        {
            Vector2 selectionDrawOffset = new Vector2(Engine.Font.MeasureString(textField.text.ToString().Remove(textField.SelectionStart)).X + arrowOffset, 0) + drawPos;
            Graphics.DrawRect(selectionDrawOffset, selectionDrawOffset + Engine.Font.MeasureString(textField.GetSelection()), SelectionColor);
        }

        //Input line
        Graphics.DrawText(Engine.Font, $"> {textField.text}", drawPos, Color.White);

        //Drawing cursor
        Vector2 cursorDrawOffset = textField.CursorPos == 0 ? new(0, Engine.Font.MeasureString(" ").Y) : Engine.Font.MeasureString(textField.text.ToString().Remove(textField.CursorPos));
        cursorDrawOffset.X += arrowOffset;
        Graphics.DrawLine(new Vector2(drawPos.X + cursorDrawOffset.X, drawPos.Y), drawPos + cursorDrawOffset, Color.White);

        //Lines log
        for (int itemIndex = Shell.Lines.Count - 1; itemIndex >= 0; itemIndex--)
        {
            ShellLine line = Shell.Lines[itemIndex];
            Vector2 lineSize = Engine.Font.MeasureString(line.Text);
            drawPos.Y -= (int)lineSize.Y;
            if (drawPos.Y < 0) break;
            Graphics.DrawText(Engine.Font, line.Text, drawPos, line.color);
        }

        Graphics.End();
    }
}
