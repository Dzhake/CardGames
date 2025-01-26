using Microsoft.Xna.Framework.Input;
using MonoPlus.Input;

namespace CardGames.Console;

public class ConsoleInput
{
    public TextField input;

    public ConsoleInput()
    {
        input = new TextField();
        Input.FocusedTextField = input;
    }

    public void Update()
    {
        input.Update();
        if (Input.Pressed(Keys.Enter))
        {
            ConsoleCore.Log(input.text.ToString());
            input.Reset();
        }
    }
}
