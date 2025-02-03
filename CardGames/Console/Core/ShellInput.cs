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
        if (Input.Pressed(Keys.Enter) && input.text.Length > 0)
        {
            string inputText = input.text.ToString();
            if (int.TryParse(inputText, out _)) inputText = $"do {inputText}";
            Shell.runner?.Run(inputText);
            input.Reset();
        }
    }
}
