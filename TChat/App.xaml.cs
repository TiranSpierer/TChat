using Prism.Ioc;
using Prism.Unity;
using System.Windows;
using Core.Interfaces;
using ExportService;
using TChat.Views;
using ChatService;
using Prism.Modularity;
using DataAccess.Services;
using Core.Interfaces.DataServices;
using DataAccess.Context;
using DataAccess.Repository;
using Configuration;
using Unity;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using Serilog;
using ILogger = Serilog.ILogger;
using Core.StateMachine;

namespace TChat;

public partial class App : PrismApplication
{
    protected override Window CreateShell()
    {
        var w = Container.Resolve<MainWindow>();
        return w;
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        RegisterConfiguration(containerRegistry);
        RegisterServices(containerRegistry);
        RegisterDatabase(containerRegistry, containerRegistry.GetContainer().Resolve<AppConfig>());
        RegisterLogger(containerRegistry, containerRegistry.GetContainer().Resolve<IConfiguration>());
    }

    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        base.ConfigureModuleCatalog(moduleCatalog);
        moduleCatalog.AddModule<MainModule>();
    }

    private void RegisterConfiguration(IContainerRegistry containerRegistry)
    {
        var configFileNamePrefix = Path.Combine("Configuration", "appsettings");
        var fileExtension        = "json";
        var environment          = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "production";

        var configFilePath      = $"{configFileNamePrefix }.{fileExtension}";
        var environmentFilePath = $"{configFileNamePrefix }.{environment}.{fileExtension}";

        containerRegistry.RegisterSingleton<IConfiguration>(c =>
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configFilePath,      optional: false, reloadOnChange: true)
                .AddJsonFile(environmentFilePath, optional: true,  reloadOnChange: true);
            return configurationBuilder.Build();
        });

        
        var config    = containerRegistry.GetContainer().Resolve<IConfiguration>();
        containerRegistry.RegisterSingleton<AppConfig>(c => new AppConfig(config, configFilePath));
    }

    private void RegisterServices(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterSingleton<IChatService, LocalChatService>();
        containerRegistry.RegisterSingleton<IExportChatService, ExportChatService>();
        containerRegistry.RegisterSingleton<IUserDataService, UserDataService>();
        containerRegistry.RegisterSingleton<MyStateMachine>();
    }

    private void RegisterDatabase(IContainerRegistry containerRegistry, AppConfig appConfig)
    {
        containerRegistry.RegisterSingleton<IMongoDbContext>(c => new MongoDbContext(appConfig.AppBehavior.ConnectionString, appConfig.AppBehavior.ConnectionString));
        containerRegistry.RegisterSingleton(typeof(IMongoDbRepository), typeof(MongoDbRepository));
        containerRegistry.RegisterSingleton(typeof(IDataService), typeof(DataService));
    }

    private void RegisterLogger(IContainerRegistry containerRegistry, IConfiguration config)
    {
        var loggerConfiguration = new LoggerConfiguration()
                                 .ReadFrom.Configuration(config)
                                 .CreateLogger();

        containerRegistry.RegisterInstance<ILogger>(loggerConfiguration);
    }
}