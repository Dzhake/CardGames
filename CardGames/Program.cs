using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CardGames;

public static class Program
{
    public static string ErrorLogPath = $"{AppContext.BaseDirectory}errorlog.txt";
    public static Engine? game;

    public static void Main(string[] args)
    {
        //Do not include main crash catcher when debugging so that VS's error catcher is used instead.
#if !DEBUG
        try
        {
#endif
        if (args.Contains("--crash")) throw new Exception("Testing how Main crash catcher works!");
        Run();
#if !DEBUG
        }
        catch (Exception ex)
        {
            var stream = File.CreateText(ErrorLogPath);
            stream.Write(ex.ToString());
            stream.Close();
            Process.Start(new ProcessStartInfo(ErrorLogPath) { UseShellExecute = true });
        }
#endif
    }

    private static void Run()
    {
        (game = new Engine()).Run();
    }

    public static void ReRun()
    {
        if (game is not null) game.Exit();
        Run();
    }
}
