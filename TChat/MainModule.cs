using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using TChat.ViewModels;
using TChat.Views;

namespace TChat;

public class MainModule : IModule
{
    public void OnInitialized(IContainerProvider containerProvider)
    {
        var region = containerProvider.Resolve<IRegionManager>();

        region.RegisterViewWithRegion("ToolBarContent", typeof(ToolBarView));

        region.RegisterViewWithRegion("MainContent", typeof(LoginView));
        region.RegisterViewWithRegion("MainContent", typeof(ContactsView));
        region.RegisterViewWithRegion("MainContent", typeof(ChatsListView));
        region.RegisterViewWithRegion("MainContent", typeof(ChatView));
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        //containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>();
        //containerRegistry.RegisterForNavigation<ChatView, ChatViewModel>();
    }
}