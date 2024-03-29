﻿using Common.UICommand;
using MailFox.UI.AddressBook.Add;
using MailFox.UI.Context;
using MailFox.UI.ReadMails.Attachment;
using MFData.Core;
using MimeKit;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MailFox.UI.ReadMails
{
    internal class ReadMailContext : AppBarContext
    {
        private readonly ICommand addContactCommand;
        public ICommand AddContactCommand => addContactCommand;

        private readonly ICommand openAttachments;
        public ICommand OpenAttachmentsCommand => openAttachments;

        private bool addContactVisibility;

        public Visibility AddCommandVisibility =>
            addContactVisibility ? Visibility.Visible : Visibility.Hidden;

        private readonly string from;
        public string From => from;

        private readonly string to;
        public string To => to;

        private readonly string mailTheme;
        public string MailTheme => mailTheme;

        private readonly string mailText;
        public string MailText => mailText;

        private readonly IEnumerable<MimeEntity> attachments;

        private async Task IsUserContact(IMFCore mailFoxDatabase, string email)
        {
            addContactVisibility = !(await mailFoxDatabase.GetContactsAsync())
                .Any(contact => contact.ContactEmail == email);

            OnPropertyChanged("AddCommandVisibility");
        }

        public ReadMailContext(MimeMessage message)
        {
            from = message.From.ToString();
            to = message.To.ToString();
            mailTheme = message.Subject;
            mailText = message.GetTextBody(MimeKit.Text.TextFormat.Text);
            attachments = message.Attachments;

            IMFCore mailFoxDatabase = kernel.Get<IMFCore>();

            addContactCommand = new Command(obj =>
            {
                bool? result = windowManager.ShowDialog(new AddContactWindow(from));

                if (result.HasValue && result.Value)
                {
                    addContactVisibility = false;
                    OnPropertyChanged("AddCommandVisibility");
                }
            });

            openAttachments = new Command(obj =>
            {
                windowManager.ShowDialog(new AttachmentWindow(message.Attachments));
            });

            IsUserContact(mailFoxDatabase, from).Wait();
        }
    }
}