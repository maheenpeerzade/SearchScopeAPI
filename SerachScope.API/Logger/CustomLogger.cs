using Serilog;
using Serilog.Events;
using System.Runtime.CompilerServices;

namespace SearchScopeAPI.SerachScope.API.Logger
{
    /// <summary>
    /// Our own custom logger based on Serilog.
    /// </summary>
    public class CustomLogger
    {
        private readonly Serilog.ILogger _logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        public CustomLogger()
        {
            // Configure Serilog to log both to the console and to a file
            var loggerConfig = new LoggerConfiguration();

            loggerConfig.WriteTo.Console(); // Log to Console.
            loggerConfig.WriteTo.File(path: "./Logs/SearchScopeAPI.logs", rollingInterval: RollingInterval.Day); // Log to File.

            _logger = loggerConfig.CreateLogger();
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Information"/> level.
        /// </summary>
        /// <param name="message">Message template describing the event.</param>
        public void LogInformation(string message) => _logger.Information(message);

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Error"/> level and associated exception.
        /// </summary>
        /// <param name="ex">Specify Exception.</param>
        /// <param name="message">Message template describing the event.</param>
        /// <param name="memberName">Specify member name.</param>
        /// <param name="filePath">Specify filePath.</param>
        /// <param name="lineNumber">Specify lineNumber.</param>
        public void LogError(Exception ex, string message = "An error occurred with exception details", [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            // Log the exception along with its details
            _logger.Error("{Message}. Exception Details: {ExceptionDetails}. Function: {Function}, File: {File}, Line: {Line}", message, ex.ToString(), memberName, filePath, lineNumber
            );
        }

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Warning"/> level.
        /// </summary>
        /// <param name="message">Message template describing the event.</param>
        public void LogWarning(string message) => _logger.Warning(message);

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Debug"/> level.
        /// </summary>
        /// <param name="message">Message template describing the event.</param>
        public void LogDebug(string message) => _logger.Debug(message);

        /// <summary>
        /// Write a log event with the <see cref="LogEventLevel.Fatal"/> level.
        /// </summary>
        /// <param name="message">Message template describing the event.</param>
        public void LogCritical(string message) => _logger.Fatal(message);
    }
}
