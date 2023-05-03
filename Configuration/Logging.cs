// Created by Tiran Spierer
// Created at 03/05/2023
// Class purpose:

using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Configuration;

public class Logging
{
    public LogLevel LogLevel { get; set; }
    public File     File    { get; set; }
}

public class LogLevel
{
    public Microsoft.Extensions.Logging.LogLevel Default { get; set; }
}

public class File
{
    public string Path                   { get; set; }
    public string FileName               { get; set; }
    public long   FileSizeLimitBytes     { get; set; }
    public int    RetainedFileCountLimit { get; set; }
}