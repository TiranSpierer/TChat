using Prism.Ioc;
using Prism.Unity;
using System;
using System.Windows;
using Core.Interfaces;
using ExportService;

namespace TChat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            var w = Container.Resolve<MainWindow>();
            return w;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<Core.Interfaces.IExportChatService, ExportChatService>();

        }
    }
}
