using Common.AppService.Manager;
using Common.UICommand;
using MailFox.UI.AddressBook;
using MailFox.UI.Context;
using MailFox.UI.Login;
using Ninject;
using System.Windows.Input;

namespace MailFox.UI.MailBox
{
    internal sealed class MailBoxContext : ContextBase
    {
        private readonly ICommand loginCommand;
        public ICommand LoginCommand => loginCommand;

        private readonly ICommand openAddressBookCommand;
        public ICommand OpenAddressBookCommand => openAddressBookCommand;

        public MailBoxContext()
        {
            IWindowManager windowManager = kernel.Get<IWindowManager>();

            loginCommand = new Command(obj =>
            {
                bool? added = windowManager.ShowDialog(new LoginWindow());

                if (added != null && (bool)added)
                    windowManager.ShowMessage(this, "Почтовый ящик добавлен");
                else
                    windowManager.ShowMessage(this, "Почтовый ящик не добавлен");
            });

            openAddressBookCommand = new Command(obj =>
            windowManager.ShowWindow(new AddressBookWindow()));
        }
    }
}