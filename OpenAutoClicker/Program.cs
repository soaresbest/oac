internal class Program
{
    private static void Main(string[] args)
    {
        if (args is null || args.Length < 2 || args.Length > 3)
        {
            PrintUsage();

            return;
        }

        int startDelay;
        int delay;

        if (
            !int.TryParse(args[0], out startDelay) ||
            !int.TryParse(args[1], out delay)
        )
        {
            PrintUsage();

            return;
        }

        var script = new Script(
            new ConsoleLogger()
        );

        if (args.Length == 3 && int.TryParse(args[2], out int custom))
        {
            script.Run(startDelay, delay, custom);
        }
        else
        {
            script.Run(startDelay, delay);
        }
    }

    private static void PrintUsage()
    {
        Console.WriteLine("usage: autoc <time to start in miliseconds> <delay between clicks in miliseconds> [click count before run custom code]");
    }
}
