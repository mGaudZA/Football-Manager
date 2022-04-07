using Football_Manager.Interfaces;

namespace Football_Manager.Providers
{
    public class ConsoleLoggingProvider : ICustomLogger
    {
        public void LogError(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
