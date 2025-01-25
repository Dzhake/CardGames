using System;
using Microsoft.Xna.Framework;
using MonoPlus.Graphics;


namespace CardGames.Console;

public class Drawer
{
    public Color SelectionColor = new(0, 120, 215);
    public void Draw()
    {
        Graphics.Clear(Color.Black);
        Graphics.Begin();

        Vector2 drawPos = new(30, Engine.gameWindow.ClientBounds.Height - 50);
        TextField textField = ConsoleCore.input.input;
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
        Vector2 cursorDrawOffset;
        if (textField.CursorPos == 0) cursorDrawOffset = new(0, Engine.Font.MeasureString(" ").Y);
        else cursorDrawOffset = Engine.Font.MeasureString(textField.text.ToString().Remove(textField.CursorPos));
        cursorDrawOffset.X += arrowOffset;
        Graphics.DrawLine(new Vector2(drawPos.X + cursorDrawOffset.X, drawPos.Y), drawPos + cursorDrawOffset, Color.White);

        //Lines log
        for (int itemIndex = ConsoleCore.Lines.Count - 1; itemIndex >= 0; itemIndex--)
        {
            string line = ConsoleCore.Lines[itemIndex];
            Vector2 lineSize = Engine.Font.MeasureString(line);
            drawPos.Y -= (int)lineSize.Y;
            if (drawPos.Y < 0) break;
            Graphics.DrawText(Engine.Font, line, drawPos, Color.White);
        }

        Graphics.End();
    }
}
