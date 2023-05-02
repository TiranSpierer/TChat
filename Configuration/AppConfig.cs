using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration.Json;

namespace Configuration;

public class AppConfig
{
    private readonly string _configFilePath = "appsettings.json";
    private readonly IConfigurationRoot _configuration;


    public AppConfig()
    {
        _configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile(_configFilePath, optional: false, reloadOnChange: true)
                        .Build();
        _configuration.Bind(this);
    }

    public void SaveConfiguration(AppConfig appConfig)
    {
        var json = JsonConvert.SerializeObject(appConfig);
        File.WriteAllText(_configFilePath, json);
        _configuration.Reload();
    }

    public AppConfig Clone()
    {
        var configJson = File.ReadAllText(_configFilePath);
        var configApp  = JsonConvert.DeserializeObject<AppConfig>(configJson);
        return configApp ?? throw new InvalidOperationException();
    }

    public string MongoDbConnectionString { get; set; }
    public string MongoDbName { get; set; }
}