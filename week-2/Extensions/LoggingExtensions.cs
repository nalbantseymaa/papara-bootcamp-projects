using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

public static class LoggingExtensions
{
    // Path to the log file
    private const string LogFilePath = "logs/log.txt";

    public static async Task LogActionAsync(this HttpContext context, string message)
    {
        var logMessage = $"{message} at {context.Request.Path} at {DateTime.UtcNow}";

        // Writing to the log file
        using (var writer = new StreamWriter(LogFilePath, true))
        {
            await writer.WriteLineAsync(logMessage);
        }
    }
}
