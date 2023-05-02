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
        containerRegistry.RegisterSingleton<IChatService, LocalChatService>();
        containerRegistry.RegisterSingleton<IExportChatService, ExportChatService>();
        containerRegistry.RegisterSingleton<IUserDataService, UserDataService>();

        // Database
        containerRegistry.Register<IMongoDbContext>(c => new MongoDbContext("mongodb://localhost:27017", "mySecondDatabase"));
        containerRegistry.Register(typeof(IMongoDbRepository), typeof(MongoDbRepository));
        containerRegistry.Register(typeof(IDataService), typeof(DataService));

        var originalConfig = new AppConfig();
        var tempConfig         = originalConfig.Clone();

        var x      = tempConfig.MongoDbName;
        //tempConfig.MongoDbName = "changed";

        //originalConfig.SaveConfiguration(tempConfig);
    }

    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        base.ConfigureModuleCatalog(moduleCatalog);
        moduleCatalog.AddModule<MainModule>();
    }
}
