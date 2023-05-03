

using Microsoft.Extensions.Logging;

namespace Configuration;

public class AppBehavior
{
    public LogLevel LogLevel { get; set; }
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}