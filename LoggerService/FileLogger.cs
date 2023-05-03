using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace LoggerService;

public class FileLogger : ILogger
{
    private readonly string _filePath;
    private readonly string _categoryName;
    private readonly LogLevel _minLogLevel;
    private readonly int _maxFileSizeBytes;
    private readonly int _maxRetainedFiles;
    private readonly object _lock = new object();

    public FileLogger(string filePath, string categoryName, LogLevel minLogLevel, int maxFileSizeBytes, int maxRetainedFiles)
    {
        _filePath = filePath;
        _categoryName = categoryName;
        _minLogLevel = minLogLevel;
        _maxFileSizeBytes = maxFileSizeBytes;
        _maxRetainedFiles = maxRetainedFiles;

        // Ensure the log file directory exists
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath));
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel >= _minLogLevel;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        // Lock to ensure thread safety when writing to the log file
        lock (_lock)
        {
            // Check if the log file is too large, and if so, rotate it
            if (File.Exists(_filePath) && new FileInfo(_filePath).Length > _maxFileSizeBytes)
            {
                RotateLogFile();
            }

            // Write the log message to the file
            File.AppendAllText(_filePath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{logLevel}] [{_categoryName}] {formatter(state, exception)}");
        }
    }

    private void RotateLogFile()
    {
        // Delete the oldest log file if there are already the maximum number of retained files
        var oldestFilePath = $"{_filePath}.{_maxRetainedFiles - 1}";
        if (File.Exists(oldestFilePath))
        {
            File.Delete(oldestFilePath);
        }

        // Rename the existing log files
        for (int i = _maxRetainedFiles - 2; i >= 0; i--)
        {
            var currentFilePath = $"{_filePath}.{i}";
            var nextFilePath = $"{_filePath}.{i + 1}";
            if (File.Exists(currentFilePath))
            {
                File.Move(currentFilePath, nextFilePath);
            }
        }

        // Rename the current log file
        File.Move(_filePath, $"{_filePath}.0");
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }
}