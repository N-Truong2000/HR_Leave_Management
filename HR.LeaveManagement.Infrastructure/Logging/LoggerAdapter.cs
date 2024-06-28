using HR.LeaveManagement.Application.Logging;
using HR.LeaveManagement.Infrastructure.Logging.Enums;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;

using ConsoleColor = HR.LeaveManagement.Infrastructure.Logging.Enums.ConsoleColor;


namespace HR.LeaveManagement.Infrastructure.Logging
{
    public class LoggerAdapter<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;

        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }
        public void LogInformation(string message, params object[] args)
        {
            Log(ConsoleColor.GreenBackground, message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            Log(ConsoleColor.YellowBackground, message, args);
        }

        public void LogError(string message, params object[] args)
        {
            Log(ConsoleColor.RedBackground, message, args);
        }

        private void Log(ConsoleColor color, string message, params object[] args)
        {
            var serializedArgs = args.Select(arg => JsonConvert.SerializeObject(arg)).ToArray();
            var formattedMessage = ColorConsoleFormatter.Colorize(message, color, ConsoleType.Bold);
            var callerMemberName = GetCallerMemberName();
            var concatenatedMessage = $"{formattedMessage} | Data: {string.Join(", ", serializedArgs)}";
            if (callerMemberName == nameof(LogInformation))
            {
                _logger.LogInformation(concatenatedMessage);
            }
            else if (callerMemberName == nameof(LogWarning))
            {
                _logger.LogWarning(concatenatedMessage);
            }
            else if (callerMemberName == nameof(LogError))
            {
                _logger.LogError(concatenatedMessage);
            }
        }
        private string GetCallerMemberName()
        {
            var stackTrace = new StackTrace();
            var callerFrame = stackTrace?.GetFrame(2);
            var callerMethod = callerFrame?.GetMethod();
            return callerMethod?.Name;
        }
    }
    public static class ColorConsoleFormatter
    {
        public static string Colorize(string message, ConsoleColor color, ConsoleType stype)
        {
            return $"\u001b[{(int)color};{(int)stype}m{message}\u001b[0m";
        }
    }

}
