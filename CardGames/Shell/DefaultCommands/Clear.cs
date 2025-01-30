namespace CardGames.Console;

public static class ClearCommand
{
    [Command]
    public static void Clear() => Shell.Clear();
}
