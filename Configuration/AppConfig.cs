using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Configuration;

public class AppConfig
{
    public static string _configFilePath = "Configuration\\appsettings.json";

    public AppConfig(IConfiguration configuration)
    {
        InitProperties();
        configuration.Bind(this);
        configuration.GetSection(nameof(AppBehavior)).Bind(AppBehavior);

    }

    public AppBehavior AppBehavior { get; set; }

    public void InitProperties()
    {
        AppBehavior = new AppBehavior();
    }

    public void SaveConfiguration(AppConfig appConfig)
    {
        var json = JsonConvert.SerializeObject(appConfig);
        File.WriteAllText(_configFilePath, json);
    }

    public AppConfig Clone()
    {
        var configJson = File.ReadAllText(_configFilePath);
        var configApp  = JsonConvert.DeserializeObject<AppConfig>(configJson);
        return configApp ?? throw new InvalidOperationException();
    }
}