using System;
using System.IO;

namespace Logger
{
    interface ILogger
    {
        public void WriteError(string message);
    }

    class ConsoleLogWritter : ILogger
    {
        ILogger _logger;

        public ConsoleLogWritter(ILogger logger = null)
        {
            _logger = logger;
        }

        public void WriteError(string message)
        {
            Console.WriteLine(message);

            if(_logger != null)
                _logger.WriteError(message);
        }
    }

    class FileLogWritter : ILogger
    {
        ILogger _logger;

        public FileLogWritter(ILogger logger = null)
        {
            _logger = logger;
        }

        public virtual void WriteError(string message)
        {
            File.WriteAllText("log.txt", message);

            if (_logger != null)
                _logger.WriteError(message);
        }
    }

    class SecureConsoleLogWritter : ILogger
    {
        ILogger _logger;

        public SecureConsoleLogWritter(ILogger logger)
        {
            _logger = logger;
        }

        public void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                _logger.WriteError(message);
            }
        }
    }

    class Pathfinder : ILogger
    {
        ILogger _logger;

        public Pathfinder(ILogger logger)
        {
            _logger = logger;
        }
         
        public void Find()
        {
            
        }

        public void WriteError(string message)
        {
            _logger.WriteError(message);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Pathfinder pathfinder = new Pathfinder(new ConsoleLogWritter());
            Pathfinder pathfinder1 = new Pathfinder(new FileLogWritter());
            Pathfinder pathfinder2 = new Pathfinder(new SecureConsoleLogWritter(new ConsoleLogWritter()));
            Pathfinder pathfinder3 = new Pathfinder(new SecureConsoleLogWritter(new FileLogWritter()));
            Pathfinder pathfinder4 = new Pathfinder(new ConsoleLogWritter(new SecureConsoleLogWritter(new FileLogWritter())));
            pathfinder.WriteError("adsda");
            pathfinder1.WriteError("hjm");
            pathfinder2.WriteError("www");
            pathfinder3.WriteError("aaa");
            pathfinder4.WriteError("zzz");
        }
    }
}

