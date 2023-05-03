using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Configuration;

public class AppBehavior
{
    public string  ConnectionString { get; set; }
    public string  DatabaseName     { get; set; }
}