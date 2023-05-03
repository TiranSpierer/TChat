using System.Diagnostics;
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
        var configFilePath = "Configuration\\appsettings.json";
        containerRegistry.RegisterSingleton<IConfiguration>(c =>
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configFilePath, optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true, reloadOnChange: true);
            return configurationBuilder.Build();
        });

        containerRegistry.RegisterSingleton<AppConfig>();
        var appConfig = containerRegistry.GetContainer().Resolve<AppConfig>();

        // Services
        containerRegistry.RegisterSingleton<IChatService, LocalChatService>();
        containerRegistry.RegisterSingleton<IExportChatService, ExportChatService>();
        containerRegistry.RegisterSingleton<IUserDataService, UserDataService>();

        // Database
        containerRegistry.Register<IMongoDbContext>(c => new MongoDbContext(appConfig.AppBehavior.ConnectionString, appConfig.AppBehavior.ConnectionString));
        containerRegistry.Register(typeof(IMongoDbRepository), typeof(MongoDbRepository));
        containerRegistry.Register(typeof(IDataService), typeof(DataService));

        var x = appConfig.AppBehavior.ConnectionString;
    }

    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        base.ConfigureModuleCatalog(moduleCatalog);
        moduleCatalog.AddModule<MainModule>();
    }
}