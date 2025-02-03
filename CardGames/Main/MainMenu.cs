namespace CardGames.Console;

public class MainMenu : Menu
{
    public readonly string Title =
        """
          ___   __   ____  ____     ___   __   _  _  ____  ____     ___  __   __    __    ____  ___  ____  __  __   __ _ 
         / __) / _\ (  _ \(    \   / __) / _\ ( \/ )(  __)/ ___)   / __)/  \ (  )  (  )  (  __)/ __)(_  _)(  )/  \ (  ( \
        ( (__ /    \ )   / ) D (  ( (_ \/    \/ \/ \ ) _) \___ \  ( (__(  O )/ (_/\/ (_/\ ) _)( (__   )(   )((  O )/    /
         \___)\_/\_/(__\_)(____/   \___/\_/\_/\_)(_/(____)(____/   \___)\__/ \____/\____/(____)\___) (__) (__)\__/ \_)__)


        1 - Games selection
        2 - Settings
        3 - Online
        4 - Help&About
        5 - Exit
        """;

    public override void Start()
    {
        Shell.Log(Title);
    }

    public override void Do(int action) => Shell.Log(action + 10);
}
