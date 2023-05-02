using Prism.Ioc;
using Prism.Unity;
using System.Windows;
using Core.Interfaces;
using ExportService;
using TChat.Views;
using ChatService;
using Prism.Modularity;
using DataService.Services;
using Core.Interfaces.DataServices;
using DataService.Context;
using DataService.Repository;

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
        containerRegistry.Register(typeof(IDataService), typeof(DataService.Services.DataService));
    }

    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        base.ConfigureModuleCatalog(moduleCatalog);
        moduleCatalog.AddModule<MainModule>();
    }
}
