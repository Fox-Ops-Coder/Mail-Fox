﻿using Common.AppService.Manager;
using Common.UICommand;
using MailFox.UI.AddressBook;
using MailFox.UI.Context;
using MailFox.UI.Login;
using MailFox.UI.MailBox.Adapters;
using MailFox.UI.Mails;
using MailFox.UI.ReadMails;
using MailFox.UI.Responce;
using Mailing.ServiceManager;
using Mailing.Services;
using MailKit;
using MFData.Core;
using MFData.Entities;
using MimeKit;
using Ninject;
using Security.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using IMailService = Mailing.Services.IMailService;

namespace MailFox.UI.MailBox
{
    internal sealed class MailBoxContext : AppBarContext 
    {
        private readonly ICommand loginCommand;
        public ICommand LoginCommand => loginCommand;

        private readonly ICommand logoutCommand;
        public ICommand LogoutCommand => logoutCommand;

        private readonly ICommand writeEmailCommand;
        public ICommand WriteEmailCommand => writeEmailCommand;

        private readonly ICommand openAddressBookCommand;
        public ICommand OpenAddressBookCommand => openAddressBookCommand;

        private readonly ICommand openTemplates;
        public ICommand OpenTemplates => openTemplates;

        private readonly ObservableCollection<MailServiceAdapter> mailServices;
        public ObservableCollection<MailServiceAdapter> MailServices => mailServices;

        private readonly ObservableCollection<MessageAdapter> messages;
        public ObservableCollection<MessageAdapter> Messages => messages;

        private async void LogIn()
        {
            IMFCore mailFoxDatabase = kernel.Get<IMFCore>();
            ISecurityService securityService = kernel.Get<ISecurityService>();
            IMailServiceManager mailServiceManager = kernel.Get<IMailServiceManager>();

            IEnumerable<IMailServiceBuilder> mailServiceBuilders = kernel.GetAll<IMailServiceBuilder>();
            IEnumerable<UserEmail> userEmails = await mailFoxDatabase.GetUserEmailsAsync();

            foreach (UserEmail userEmail in userEmails)
            {
                IMailServiceBuilder mailServiceBuilder = mailServiceBuilders
                    .Where(service => service.ServiceName == userEmail.Service).First();

                IMailService? mailService = await mailServiceBuilder
                    .CreateMailService(userEmail.Email, securityService.DecodeString(userEmail.Password));

                if (mailService != null)
                    mailServiceManager.AddService(mailService);
            }
        }

        public MailBoxContext()
        {
            ISecurityService securityService = kernel.Get<ISecurityService>();
            IMFCore mailFoxDatabase = kernel.Get<IMFCore>();
            IMailServiceManager serviceManager = kernel.Get<IMailServiceManager>();

            IMailServiceManager mailServiceManager = kernel.Get<IMailServiceManager>();

            loginCommand = new Command(async obj =>
            {
                Tuple<bool?, object?> added = windowManager.ShowDialogWithResult(new LoginWindow());

                if (added.Item1 != null && (bool)added.Item1)
                {
                    IMailService? mailService = added.Item2 as IMailService;

                    UserEmail userEmail = new()
                    {
                        Email = mailService.Email,
                        Service = mailService.Service,
                        Password = securityService.EncodeString(mailService.Password),
                    };

                    await mailFoxDatabase.AddUserEmailAsync(userEmail);
                    serviceManager.AddService(mailService);

                    windowManager.ShowMessage(this, "Почтовый ящик добавлен");
                }
                else
                {
                    windowManager.ShowMessage(this, "Почтовый ящик не добавлен");
                }
            });

            logoutCommand = new Command(async obj =>
            {
                if (obj is MailServiceAdapter mailServiceAdapter)
                {
                    UserEmail userEmail = (await mailFoxDatabase.GetUserEmailsAsync())
                    .Where(email => email.Email == mailServiceAdapter.MailService.Email).First();

                    await mailServiceAdapter.MailService.DisconnectAsync();
                    await mailFoxDatabase.RemoveUserEmailAsync(userEmail);

                    mailServices.Remove(mailServiceAdapter);
                }
            });

            ICommand openCommand = new Command(async obj =>
            {
                if (obj is MessageAdapter messageAdapter)
                {
                    IMailFolder folder = messageAdapter.Folder;
                    IMailService service = messageAdapter.Service;

                    MimeMessage? message = await service.GetMessageAsync(folder, messageAdapter.Message);

                    if (message != null)
                    {
                        messageAdapter.Read();
                        windowManager.ShowWindow(new ReadMailWindow(message));
                    }
                }
            });

            messages = new();
            mailServices = new();
            mailServiceManager.OnAdd += new IMailServiceManager
                .MailServiceHandler(service => mailServices.Add(new(service, openCommand, logoutCommand, messages)));

            writeEmailCommand = new Command(obj =>
            windowManager.ShowWindow(new SendMailWindow()));

            openAddressBookCommand = new Command(obj =>
            windowManager.ShowWindow(new AddressBookWindow()));

            openTemplates = new Command(obj =>
            windowManager.ShowWindow(new ResponceWindow()));

            LogIn();
        }
    }
}