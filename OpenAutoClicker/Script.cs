using OpenAutoClicker.Mouse;

public class Script
{
    private readonly ILogger _logger;

    public Script(ILogger logger)
    {
        _logger = logger;
    }

    public void Run(int startDelay, int delay, int? custom = null)
    {
        _logger.Info($"waiting {startDelay}ms to start...");

        Thread.Sleep(startDelay);

        _logger.Info("clicking");

        int clicks = 0;

        while (true)
        {
            MouseCommand.LeftDown();
            Thread.Sleep(100);
            MouseCommand.LeftUp();

            clicks++;

            Thread.Sleep(delay);

            _logger.Info(".", false);

            if (custom.HasValue && clicks >= custom.Value)
            {
                MouseCommand.WheelUp();
                Thread.Sleep(500);
                MouseCommand.RightDown();
                Thread.Sleep(5000);
                MouseCommand.RightUp();
                Thread.Sleep(500);
                MouseCommand.WheelDown();
                Thread.Sleep(500);

                clicks = 0;

                Thread.Sleep(delay);

                Console.Write("-");
            }
        }
    }
}
