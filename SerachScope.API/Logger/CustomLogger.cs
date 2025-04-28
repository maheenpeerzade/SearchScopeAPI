using Serilog;
using System.Runtime.CompilerServices;

namespace SearchScopeAPI.SerachScope.API.Logger
{
    public class CustomLogger
    {
        private readonly Serilog.ILogger _logger;

        public CustomLogger()
        {
            // Configure Serilog to log both to the console and to a file
            var loggerConfig = new LoggerConfiguration();

            loggerConfig.WriteTo.Console(); // Log to Console.
            loggerConfig.WriteTo.File(path: "./Logs/SearchScopeAPI.logs", rollingInterval: RollingInterval.Day); // Log to File.

            _logger = loggerConfig.CreateLogger();
        }

        public void LogInformation(string message) => _logger.Information(message);
        public void LogError(Exception ex, string message = "An error occurred with exception details", [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            // Log the exception along with its details
            _logger.Error("{Message}. Exception Details: {ExceptionDetails}. Function: {Function}, File: {File}, Line: {Line}", message, ex.ToString(), memberName, filePath, lineNumber
            );
        }
        public void LogWarning(string message) => _logger.Warning(message);
        public void LogDebug(string message) => _logger.Debug(message);
        public void LogCritical(string message) => _logger.Fatal(message);
    }
}
