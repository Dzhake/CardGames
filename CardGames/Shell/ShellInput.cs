using Microsoft.Xna.Framework.Input;
using MonoPlus.Input;

namespace CardGames.Console;

public class ShellInput
{
    public TextField input;

    public ShellInput()
    {
        input = new TextField();
        Input.FocusedTextField = input;
    }

    public void Update()
    {
        input.Update();
        if (Input.Pressed(Keys.Enter))
        {
            Shell.Run(input.text.ToString());
            input.Reset();
        }
    }
}
