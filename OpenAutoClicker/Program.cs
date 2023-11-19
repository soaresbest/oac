using System.CommandLine;

var rootCommand = new RootCommand("OAC - Open Auto Clicker by @soaresbest");

SetupClickCommand(rootCommand);

SetupHoldCommand(rootCommand);

await rootCommand.InvokeAsync(args);

static void SetupClickCommand(RootCommand rootCommand)
{
    var clickCommand = new Command("click", "Sends a mouse click");

    var startDelayOption = new Option<int>(new[] { "-s", "--start" }, "time to start in miliseconds") { IsRequired = true };

    clickCommand.AddOption(startDelayOption);

    var delayOption = new Option<int>(new[] { "-d", "--delay" }, "delay between clicks in miliseconds") { IsRequired = true };

    clickCommand.AddOption(delayOption);

    var customOption = new Option<int?>(new[] { "-c", "--click-count" }, () => null, "click count before run custom code") { IsRequired = false };

    clickCommand.AddOption(customOption);

    var wheelsCustomOption = new Option<int?>(new[] { "-w", "--wheels-count" }, () => null, "wheels count before run custom code") { IsRequired = false };

    clickCommand.AddOption(wheelsCustomOption);

    clickCommand.SetHandler((startDelay, delay, custom, wheelsCustom) =>
    {
        var script = new Script(new ConsoleLogger());

        script.RunClick(startDelay, delay, custom, wheelsCustom);
    }, startDelayOption, delayOption, customOption, wheelsCustomOption);

    rootCommand.Add(clickCommand);
}

static void SetupHoldCommand(RootCommand rootCommand)
{
    var holdCommand = new Command("hold", "Hold");

    var startDelayOption = new Option<int>(new[] { "-s", "--start" }, "time to start in miliseconds") { IsRequired = true };

    holdCommand.AddOption(startDelayOption);

    var stopOption = new Option<int?>(new[] { "-t", "--stop" }, () => null, "time to stop in miliseconds") { IsRequired = false };

    holdCommand.AddOption(stopOption);

    var wheelsCustomOption = new Option<int?>(new[] { "-w", "--wheels-count" }, () => null, "wheels count before run custom code") { IsRequired = false };

    holdCommand.AddOption(wheelsCustomOption);

    holdCommand.SetHandler((startDelay, stopDelay, wheelsCustom) =>
    {
        var script = new Script(new ConsoleLogger());

        script.RunHold(startDelay, stopDelay, wheelsCustom);
    }, startDelayOption, stopOption, wheelsCustomOption);

    rootCommand.Add(holdCommand);
}
