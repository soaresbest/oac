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
        bool rightButton,
        int? custom = null,
        int? wheelsCustom = null,
        int? flight = null
    )
    {
        _logger.Info($"waiting {startDelay}ms to start...");

        Thread.Sleep(startDelay);

        _logger.Info("clicking");

        if (flight.HasValue)
        {
            int fireworks = 64;

            for (int s = 0; s < flight.Value; s++)
            {
                int len = s == 0 ? fireworks - 1 : fireworks;

                for (int i = 0; i < len; i++)
                {
                    MouseCommand.Down(rightButton);
                    Thread.Sleep(100);
                    MouseCommand.Up(rightButton);
                    Thread.Sleep(i == len - 1 ? delay / 2 : delay);
                }

                if (s < flight.Value - 1)
                {
                    MouseCommand.WheelDown();
                    Thread.Sleep(delay / 2);
                }
            }

            for (int i = 0; i < 301; i++)
            {
                MouseCommand.MoveY(1);
                Thread.Sleep(10);
            }

            return;
        }

        int clicks = 0;
        int wheels = 0;
        int slotCount = 1;

        while (true)
        {
            MouseCommand.Down(rightButton);
            Thread.Sleep(100);
            MouseCommand.Up(rightButton);

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
        int? stopDelay = null,
        int? wheelsCustom = null
    )
    {
        _logger.Info($"waiting {startDelay}ms to start...");

        int wheels = 1;

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

            if (
                !wheelsCustom.HasValue &&
                stopDelay.HasValue &&
                stopwatch.ElapsedMilliseconds > stopDelay.Value
            )
            {
                MouseCommand.LeftUp();

                _logger.Info("released by stop delay");

                break;
            }

            if (
                wheelsCustom.HasValue &&
                stopDelay.HasValue &&
                stopwatch.ElapsedMilliseconds > stopDelay.Value
            )
            {
                MouseCommand.LeftUp();
                Thread.Sleep(500);

                if (wheels > wheelsCustom.Value)
                {
                    for (int i = 0; i < wheels; i++)
                    {
                        MouseCommand.WheelUp();
                        Thread.Sleep(500);
                    }

                    MouseCommand.RightDown();
                    Thread.Sleep(5000);
                    MouseCommand.RightUp();
                    Thread.Sleep(500);

                    _logger.Info("released by wheel count");

                    break;
                }

                _logger.Info(".", false);

                for (int i = 0; i < wheels; i++)
                {
                    MouseCommand.WheelUp();
                    Thread.Sleep(500);
                }

                wheels++;

                MouseCommand.RightDown();
                Thread.Sleep(5000);
                MouseCommand.RightUp();
                Thread.Sleep(500);

                for (int i = 0; i < wheels; i++)
                {
                    MouseCommand.WheelDown();
                    Thread.Sleep(500);
                }

                Thread.Sleep(500);
                MouseCommand.LeftDown();

                stopwatch = Stopwatch.StartNew();
            }
        }
    }
}
