using Discord;
using Microsoft.Extensions.Logging;

namespace Diana.Core.Defaults
{
    public class DianaLogEntry
    {
        private LogLevel _level;
        public LogLevel Level => _level;
        private string _message;
        public string Message => _message;
        public DianaLogEntry(LogLevel level, string message)
        {
            _level = level;
            _message = message;
        }
        public static DianaLogEntry Adapt(LogMessage logMessage)
        {
            switch(logMessage.Severity)
            {
                case LogSeverity.Error:
                    return new DianaLogEntry(LogLevel.Error, logMessage.ToString());
                case LogSeverity.Debug:
                    return new DianaLogEntry(LogLevel.Debug, logMessage.ToString());
                case LogSeverity.Warning:
                    return new DianaLogEntry(LogLevel.Warning, logMessage.ToString());
                default:
                    return new DianaLogEntry(LogLevel.Information, logMessage.ToString());
            }
        }
    }
}
