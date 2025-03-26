using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private const string LogFilePath = "logs/log.txt"; // Path to the log file

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;

        // Create the log file directory
        Directory.CreateDirectory(Path.GetDirectoryName(LogFilePath));
    }

    public async Task Invoke(HttpContext context)
    {
        var logMessage = $"Entering action: {context.Request.Path} at {DateTime.UtcNow}";
        await WriteLogAsync(logMessage);

        // Continue processing the request
        await _next(context);

        logMessage = $"Exiting action: {context.Request.Path} at {DateTime.UtcNow}";
        await WriteLogAsync(logMessage);
    }

    private async Task WriteLogAsync(string message)
    {
        // Writing to the log file
        using (var writer = new StreamWriter(LogFilePath, true))
        {
            await writer.WriteLineAsync(message);
        }
    }
}
