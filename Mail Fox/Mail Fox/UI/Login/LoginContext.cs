using Common.AppService;
using MailFox.UI.Login.Pages;
using System;
using System.Windows.Navigation;

namespace MailFox.UI.Login
{
    internal sealed class LoginContext : IWindow
    {
        private readonly NavigationService navigationService;

        private readonly Guid guid;
        public Guid Guid => guid;

        public LoginContext(NavigationService navigationService)
        {
            this.navigationService = navigationService;

            guid = Guid.NewGuid();

            navigationService.Navigate(new ServicePage(guid));
        }
    }
}