using static CardGames.Engine;
using Microsoft.Xna.Framework;


namespace CardGames.Console;

public class Drawer
{
    public void Draw()
    {
        graphics.Clear(Color.Black);
        spriteBatch.Begin();

        Vector2 drawPos = new(30, gameWindow.ClientBounds.Height - 50);

        TextField textField = ConsoleCore.input.input;
        spriteBatch.DrawString(Font, $"> {textField.text}", drawPos, Color.White);


        for (int itemIndex = ConsoleCore.Lines.Count - 1; itemIndex >= 0; itemIndex--)
        {
            string line = ConsoleCore.Lines[itemIndex];
            Vector2 lineSize = Font.MeasureString(line);
            drawPos.Y -= (int)lineSize.Y;
            if (drawPos.Y < 0) return;
            spriteBatch.DrawString(Font, line, drawPos, Color.White);
        }

        spriteBatch.End();
    }
}
