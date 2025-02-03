namespace CardGames.Console;

public static class StartCommand
{
    [Command]
    public static void Start() => Shell.CurrentMenu?.Start();
}
