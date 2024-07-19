namespace Logger
{
    public interface ILogger
    {
        public void WriteError(string message);
    }

    public class ConsoleLogWritter : ILogger
    {
        public void WriteError(string message) => Console.WriteLine(message);
    }

    public class FileLogWritter : ILogger
    {
        private const string LogFileName = "log.txt";
        public void WriteError(string message) => File.WriteAllText(LogFileName, message);
    }

    public class SecureLogWritter : ILogger
    {
        private readonly ILogger _logger;

        public SecureLogWritter(ILogger logger)
        {
            _logger = logger;
        }

        public void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                _logger.WriteError(message);
        }
    }

    public class Pathfinder
    {
        private readonly ILogger[] _loggers;

        public Pathfinder(params ILogger[] loggers)
        {
            _loggers = loggers;
        }

        public void Find(string message)
        {
            foreach (var logger in _loggers) 
                logger.WriteError(message);
        }
    }

    public class Program
    {
        private static void Main(string[] args)
        {
            Pathfinder pathfinder = new Pathfinder(new ConsoleLogWritter());
            Pathfinder pathfinder1 = new Pathfinder(new FileLogWritter());
            Pathfinder pathfinder2 = new Pathfinder(new SecureLogWritter(new ConsoleLogWritter()));
            Pathfinder pathfinder3 = new Pathfinder(new SecureLogWritter(new FileLogWritter()));
            Pathfinder pathfinder4 = new Pathfinder(new ConsoleLogWritter(), new SecureLogWritter(new FileLogWritter()));
            pathfinder.Find("adsda");
            pathfinder1.Find("hjm");
            pathfinder2.Find("www");
            pathfinder3.Find("aaa");
            pathfinder4.Find("zzz");
        }
    }
}