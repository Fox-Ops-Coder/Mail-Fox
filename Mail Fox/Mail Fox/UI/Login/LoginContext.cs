using Common.AppService.Manager;
using Common.AppService.WindowService;
using MailFox.UI.Context;
using MailFox.UI.Login.Pages;
using Ninject;
using System.Windows.Navigation;

namespace MailFox.UI.Login
{
    internal sealed class LoginContext : ContextBase, IManagable, INavigator, IResult
    {
        private readonly NavigationService navigationService;
        private readonly IWindowManager windowManager;

        public object? closeArg;
        public object? Result => closeArg;

        public LoginContext(NavigationService navigationService)
        {
            this.navigationService = navigationService;
            windowManager = kernel.Get<IWindowManager>();

            navigationService.Navigate(new ServicePage(this, this));
        }

        public void Navigate(object content) =>
            navigationService.Navigate(content);

        public void Close(bool? result = null) =>
            windowManager.CloseWindow(this, result);

        public void CloseWithArg(bool? result, object? arg)
        {
            closeArg = arg;
            windowManager.CloseWindow(this, result);
        }
    }
}