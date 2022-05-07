using Common.AppService.Manager;
using Common.UICommand;
using MailFox.UI.AddressBook;
using MailFox.UI.Context;
using MailFox.UI.Login;
using MailFox.UI.Mails;
using Mailing.ServiceManager;
using Mailing.Services;
using MFData.Core;
using MFData.Entities;
using Ninject;
using Security.Service;
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
            ISecurityService securityService = kernel.Get<ISecurityService>();
            IMFCore mailFoxDatabase = kernel.Get<IMFCore>();
            IMailServiceManager serviceManager = kernel.Get<IMailServiceManager>();

            loginCommand = new Command(async obj =>
            {
                Tuple<bool?, object?> added = windowManager.ShowDialogWithResult(new LoginWindow());

                if (added.Item1 != null && (bool)added.Item1)
                {
                    IMailService? mailService = added.Item2 as IMailService;

#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
#pragma warning disable CS8601 // Возможно, назначение-ссылка, допускающее значение NULL.
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                    UserEmail userEmail = new()
                    {
                        Email = mailService.Email,
                        Service = mailService.Service,
                        Password = securityService.EncodeString(mailService.Password),
                    };
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
#pragma warning restore CS8601 // Возможно, назначение-ссылка, допускающее значение NULL.
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.

                    await mailFoxDatabase.AddUserEmailAsync(userEmail);
                    serviceManager.AddService(mailService);

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