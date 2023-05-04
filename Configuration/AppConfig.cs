using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Configuration;

public class AppConfig
{
    private readonly IConfiguration _configuration;
    private readonly string         _configFilePath;

    public AppConfig(IConfiguration configuration, string configFilePath)
    {
        _configuration  = configuration;
        _configFilePath = configFilePath;
        _configuration.Bind(this);
    }

    public AppBehavior AppBehavior { get; set; } = null!;
    public Serilog     Serilog     { get; set; } = null!;

    public void Save(AppConfig appConfig)
    {
        var json = JsonSerializer.Serialize(appConfig, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_configFilePath, json);
    }
}