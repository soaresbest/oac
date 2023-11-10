using OpenAutoClicker.Mouse;
using System.Diagnostics;

public class Script
{
    private readonly ILogger _logger;

    public Script(ILogger logger)
    {
        _logger = logger;
    }

    public void RunClick(
        int startDelay,
        int delay,
        int? custom = null,
        int? wheelsCustom = null
    )
    {
        _logger.Info($"waiting {startDelay}ms to start...");

        Thread.Sleep(startDelay);

        _logger.Info("clicking");

        int clicks = 0;
        int wheels = 0;
        int slotCount = 1;

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
                for (int i = 0; i < slotCount; i++)
                {
                    MouseCommand.WheelUp();
                    Thread.Sleep(500);
                }

                MouseCommand.RightDown();
                Thread.Sleep(5000);
                MouseCommand.RightUp();
                Thread.Sleep(500);

                for (int i = 0; i < slotCount; i++)
                {
                    MouseCommand.WheelDown();
                    Thread.Sleep(500);
                }

                clicks = 0;

                if (wheels < int.MaxValue)
                {
                    wheels++;
                }
                else
                {
                    wheels = 0;
                }

                Thread.Sleep(delay);

                Console.Write($"-{wheels}-");

                if (wheelsCustom.HasValue && (wheels % wheelsCustom.Value) == 0)
                {
                    slotCount++;

                    if (slotCount == 9)
                    {
                        slotCount = 1;
                    }
                }
            }
        }
    }

    internal void RunHold(
        int startDelay,
        int? stopDelay = null
    )
    {
        _logger.Info($"waiting {startDelay}ms to start...");

        Thread.Sleep(startDelay);

        Stopwatch stopwatch = Stopwatch.StartNew();

        _logger.Info("holding");

        MousePoint startMousePoint = MouseCommand.GetCursorPosition();

        MouseCommand.LeftDown();

        while (true)
        {
            Thread.Sleep(100);

            MousePoint mousePoint = MouseCommand.GetCursorPosition();

            if (startMousePoint != mousePoint)
            {
                MouseCommand.LeftUp();

                _logger.Info("released by mouse move");

                break;
            }

            if (stopDelay.HasValue && stopwatch.ElapsedMilliseconds > stopDelay.Value)
            {
                MouseCommand.LeftUp();

                _logger.Info("released by stop delay");

                break;
            }
        }
    }
}
