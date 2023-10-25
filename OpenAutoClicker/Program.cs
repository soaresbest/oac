internal class Program
{
    private static void Main(string[] args)
    {
        if (args is null || args.Length < 2 || args.Length > 4)
        {
            PrintUsage();

            return;
        }

        switch (args[0])
        {
            case "click":
                RunClick(args);
                return;
            case "hold":
                RunHold(args);
                return;
            default:
                PrintUsage();
                return;
        }
    }

    private static void RunClick(string[] args)
    {
        int startDelay;
        int delay;

        if (
            !int.TryParse(args[1], out startDelay) ||
            !int.TryParse(args[2], out delay)
        )
        {
            PrintUsage();

            return;
        }

        var script = new Script(
            new ConsoleLogger()
        );

        if (args.Length == 4 && int.TryParse(args[3], out int custom))
        {
            script.RunClick(startDelay, delay, custom);
        }
        else
        {
            script.RunClick(startDelay, delay);
        }
    }

    private static void RunHold(string[] args)
    {
        int startDelay;

        if (!int.TryParse(args[1], out startDelay))
        {
            PrintUsage();

            return;
        }

        var script = new Script(
            new ConsoleLogger()
        );

        script.RunHold(startDelay);
    }

    private static void PrintUsage()
    {
        Console.WriteLine(
            "usage: autoc click <time to start in miliseconds> <delay between clicks in miliseconds> [click count before run custom code]\n" +
            "             hold <time to start in miliseconds>"
        );
    }
}
