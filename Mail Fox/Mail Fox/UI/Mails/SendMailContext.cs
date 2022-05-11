using Common.UICommand;
using MailFox.UI.Context;
using MailFox.UI.Mails.Adapters;
using MailFox.UI.Mails.Attachment;
using MailFox.UI.Responce;
using Mailing.ServiceManager;
using Mailing.Services;
using MFData.Core;
using MFData.Entities;
using MimeKit;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MailFox.UI.Mails
{
    internal sealed class SendMailContext : AppBarContext
    {
        private readonly ObservableCollection<MailServiceAdapter> mailServices;
        public ObservableCollection<MailServiceAdapter> MailServices => mailServices;

        private readonly ObservableCollection<ContactAdapter> contacts;
        public ObservableCollection<ContactAdapter> Contacts => contacts;

        private readonly ICommand openTemplates;
        public ICommand OpenTemplates => openTemplates;

        private MailServiceAdapter? selectedMailService;

        public MailServiceAdapter? SelectedMailService
        { get => selectedMailService; set => selectedMailService = value; }

        private ContactAdapter? selectedContact;

        public ContactAdapter? SelectedContact
        { get => selectedContact; set => selectedContact = value; }

        private string mailTheme;

        public string MailTheme
        { get => mailTheme; set => mailTheme = value; }

        private string mailText;

        public string MailText
        { get => mailText; set => mailText = value; }

        private readonly ICommand sendCommand;
        public ICommand SendCommand => sendCommand;

        private readonly ICommand attachmentCommand;
        public ICommand AttachmentCommand => attachmentCommand;

        private ObservableCollection<AttachmentAdapter> attachments;

        private static async void GetContacts(IMFCore mailFoxDatabase,
            ObservableCollection<ContactAdapter> contacts)
        {
            IEnumerable<Contact> userContacts = await mailFoxDatabase.GetContactsAsync();

            foreach (Contact contact in userContacts)
                contacts.Add(new(contact));
        }

        private static void GetMailServices(IMailServiceManager mailServiceManager,
            ObservableCollection<MailServiceAdapter> mailServices)
        {
            IReadOnlyList<IMailService> services = mailServiceManager.GetMailServices();

            foreach (IMailService mailService in services)
                mailServices.Add(new(mailService));
        }

        public SendMailContext()
        {
            IMailServiceManager mailServiceManager = kernel.Get<IMailServiceManager>();
            IMFCore mailFoxDatabase = kernel.Get<IMFCore>();

            mailServices = new();
            contacts = new();

            GetContacts(mailFoxDatabase, contacts);
            GetMailServices(mailServiceManager, mailServices);

            mailTheme = string.Empty;
            mailText = string.Empty;

            selectedMailService = null;
            selectedContact = null;

            attachments = new();

            sendCommand = new Command(async obj =>
            {
                if (selectedMailService != null && selectedContact != null)
                {
                    MimeMessage message = new();
                    message.Sender = new("???", selectedMailService.MailService.Email);
                    message.To.Add(InternetAddress.Parse(selectedContact.Contact.ContactEmail));
                    message.Subject = mailTheme;

                    BodyBuilder builder = new();
                    builder.TextBody = mailText;

                    foreach (AttachmentAdapter attachment in attachments)
                        builder.Attachments.Add(attachment.Attachment);

                    message.Body = builder.ToMessageBody();

                    string result = await selectedMailService.MailService.SendMessageAsync(message);

                    windowManager.ShowMessage(this, "Сообщение отправлено");
                    windowManager.CloseWindow(this);
                }
            });

            attachmentCommand = new Command(obj =>
            windowManager.ShowDialog(new AttachmentWindow(attachments)));

            openTemplates = new Command(obj =>
            {
                Tuple<bool?, object?> result = windowManager
                .ShowDialogWithResult(new ResponceWindow(true));

                if (result.Item1.HasValue && result.Item1.Value)
                {
                    if (result.Item2 is Blank blank)
                    {
                        mailText = blank.BlankText;
                        OnPropertyChanged("MailText");
                    }
                }
            });
        }
    }
}