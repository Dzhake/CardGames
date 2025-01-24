using static CardGames.Engine;
using Microsoft.Xna.Framework;


namespace CardGames.Console;

public class Drawer
{
    public void Draw()
    {
        graphics.Clear(Color.Black);
        spriteBatch.Begin();

        spriteBatch.DrawString(Font, $"{ConsoleCore.input.input.text}", new Vector2(0, gameWindow.ClientBounds.Height - 20), Color.White);
        spriteBatch.End();
    }
}
