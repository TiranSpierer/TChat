

using Core.DataModels;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace TChat.ViewModels;

public abstract class BaseViewModel : BindableBase, INavigationAware
{
    public abstract bool IsNavigationTarget(NavigationContext navigationContext);

    public abstract void OnNavigatedFrom(NavigationContext navigationContext);

    public abstract void OnNavigatedTo(NavigationContext navigationContext);
}