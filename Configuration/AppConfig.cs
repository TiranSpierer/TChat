using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Configuration;

public class AppConfig
{
    private readonly string _configFilePath;

    public AppConfig(IConfiguration configuration, string configFilePath)
    {
        _configFilePath = configFilePath;
        InitProperties();
        configuration.Bind(this);
        var x = AppBehavior;
    }

    public AppBehavior          AppBehavior { get; set; }
    public SerilogConfig Serilog     { get; set; }

    public void InitProperties()
    {
        AppBehavior = new AppBehavior();
    }

    public void SaveConfiguration(AppConfig appConfig)
    {
        var json = JsonConvert.SerializeObject(appConfig);
        System.IO.File.WriteAllText(_configFilePath, json);
    }

    public AppConfig Clone()
    {
        var configJson = System.IO.File.ReadAllText(_configFilePath);
        var configApp  = JsonConvert.DeserializeObject<AppConfig>(configJson);
        return configApp ?? throw new InvalidOperationException();
    }
}