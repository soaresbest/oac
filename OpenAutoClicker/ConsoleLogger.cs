public class ConsoleLogger : ILogger
{
    public void Info(string message, bool breakline = true)
    {
        if (breakline)
        {
            Console.WriteLine(message);
        }
        else
        {
            Console.Write(message);
        }
    }
}
