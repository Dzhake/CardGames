namespace CardGames.Console;

public static class DoCommand
{
    [Command]
    public static void Do(int action) => Shell.CurrentMenu?.Do(action);
}
