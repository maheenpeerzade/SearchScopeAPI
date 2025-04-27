using Serilog;

namespace SearchScopeAPI.SerachScope.API
{
    public class CustomLogger
    {
        private readonly Serilog.ILogger _logger;

        public CustomLogger()
        {
            // Configure Serilog to log both to the console and to a file
            var loggerConfig = new LoggerConfiguration();

            loggerConfig.WriteTo.Console(); // Log to file.

            loggerConfig.WriteTo.File(path: "./Logs/SearchScopeAPI.logs", rollingInterval: RollingInterval.Day);

            _logger = loggerConfig.CreateLogger();
        }

        public void LogInformation(string message) => _logger.Information(message);
        public void LogError(string message, Exception ex = null) => _logger.Error(ex, message);
        public void LogWarning(string message) => _logger.Warning(message);
        public void LogDebug(string message) => _logger.Debug(message);
        public void LogCritical(string message) => _logger.Fatal(message);
    }
}
