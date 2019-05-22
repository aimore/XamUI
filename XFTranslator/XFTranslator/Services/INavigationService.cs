using System;
using System.Threading.Tasks;
using XFTranslator.ViewModels;

namespace XFTranslator.Services
{
    public interface INavigationService
    {
        ViewModels.BaseViewModel PreviousPageViewModel { get; }

        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModels.BaseViewModel;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;

        Task RemoveLastFromBackStackAsync();

        Task RemoveBackStackAsync();
    }
}
