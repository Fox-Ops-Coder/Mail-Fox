using Common.AppService.Manager;
using Common.UICommand;
using MailFox.UI.AddressBook;
using MailFox.UI.Context;
using MailFox.UI.Login;
using MailFox.UI.Mails;
using Ninject;
using System;
using System.Windows.Input;

namespace MailFox.UI.MailBox
{
    internal sealed class MailBoxContext : ContextBase
    {
        private readonly ICommand loginCommand;
        public ICommand LoginCommand => loginCommand;

        private readonly ICommand writeMail;
        public ICommand WriteMail => writeMail;

        private readonly ICommand openAddressBookCommand;
        public ICommand OpenAddressBookCommand => openAddressBookCommand;

        public MailBoxContext()
        {
            IWindowManager windowManager = kernel.Get<IWindowManager>();

            loginCommand = new Command(obj =>
            {
                Tuple<bool?, object?> added = windowManager.ShowDialogWithResult(new LoginWindow());

                if (added.Item1 != null && (bool)added.Item1)
                {
                    windowManager.ShowMessage(this, "Почтовый ящик добавлен");

                }
                else
                {
                    windowManager.ShowMessage(this, "Почтовый ящик не добавлен");
                }
            });

            writeMail = new Command(obj =>
            windowManager.ShowDialog(new SendMailWindow()));

            openAddressBookCommand = new Command(obj =>
            windowManager.ShowWindow(new AddressBookWindow()));
        }
    }
}