using System.Linq;
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
        if (Input.PressedKeys?.Contains(Keys.Enter) ?? false)
        {
            ConsoleCore.EnqueueLine(input.text.ToString());
            input.text.Clear();
        }
    }
}
