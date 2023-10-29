using Microsoft.Extensions.Logging;

namespace Planner.Infrastructure;

public class CustomFileLoggerProvider : ILoggerProvider
{
    private readonly StreamWriter _logFileWriter;
    private const string LogFilePath = "console_log.txt";
    private bool disposed = false;

    public CustomFileLoggerProvider()
    {
        _logFileWriter = new StreamWriter(LogFilePath, append: true);
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new CustomFileLogger(categoryName, _logFileWriter);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _logFileWriter.Flush();
            }
            disposed = true;
        }
    }
}