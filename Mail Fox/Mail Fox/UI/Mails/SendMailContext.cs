using Common.AppService.Manager;
using Common.UICommand;
using MailFox.UI.Context;
using MailFox.UI.Mails.Adapters;
using Mailing.ServiceManager;
using Mailing.Services;
using MFData.Core;
using MFData.Entities;
using Ninject;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Mail;
using System.Windows.Input;

namespace MailFox.UI.Mails
{
    internal sealed class SendMailContext : ContextBase
    {
        private readonly ObservableCollection<MailServiceAdapter> mailServices;
        public ObservableCollection<MailServiceAdapter> MailServices => mailServices;

        private readonly ObservableCollection<ContactAdapter> contacts;
        public ObservableCollection<ContactAdapter> Contacts => contacts;

        private MailServiceAdapter? selectedMailService;

        public MailServiceAdapter? SelectedMailService
        { get => selectedMailService; set => selectedMailService = value; }

        private ContactAdapter? selectedContact;

        public ContactAdapter? SelectedContact
        { get => selectedContact; set => selectedContact = value; }

        private string mailTheme;
        public string MailTheme => mailTheme;

        private string mailText;
        public string MailText => mailText;

        private readonly ICommand sendCommand;
        public ICommand SendCommand => sendCommand;

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
            IWindowManager windowManager = kernel.Get<IWindowManager>();
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

            sendCommand = new Command(async obj =>
            {
                if (selectedMailService != null && selectedContact != null)
                {
                    MailAddressCollection targets = new();
                    targets.Add(new(selectedContact.Contact.ContactEmail));

                    await selectedMailService.MailService.SendMessageAsync(new(
                        selectedMailService.MailService.EmailAddress,
                        selectedContact.Contact.ContactEmail,
                        mailTheme, mailText));

                    windowManager.ShowMessage(this, "Сообщение отправлено");
                    windowManager.CloseWindow(this);
                }
            });
        }
    }
}