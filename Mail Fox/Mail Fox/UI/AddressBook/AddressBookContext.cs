﻿using Common.AppService.Manager;
using Common.UI;
using Common.UICommand;
using MailFox.Kernel;
using MailFox.UI.AddressBook.Add;
using MailFox.UI.AddressBook.Address;
using MailFox.UI.Context;
using MFData.Core;
using MFData.Entities;
using Ninject;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MailFox.UI.AddressBook
{
    internal sealed class AddressBookContext : ContextBase
    {
        private readonly ObservableCollection<ContactAdapter> contacts;
        public ObservableCollection<ContactAdapter> Contacts => contacts;

        private readonly ICommand addContactCommand;
        public ICommand AddContactCommand => addContactCommand;

        private readonly ICommand removeContactCommand;
        public ICommand RemoveContactCommand => removeContactCommand;

        private static async Task GetContacts(IMFCore mailFoxDatabase,
            ObservableCollection<ContactAdapter> contactsCollection,
            ICommand removeCommand)
        {
            IEnumerable<Contact> contacts = await mailFoxDatabase.GetContactsAsync();

            foreach (Contact contact in contacts)
                contactsCollection.Add(new(contact, removeCommand));
        }

        public AddressBookContext()
        {
            IKernel kernel = AppKernel.GetKernel();

            IWindowManager windowManager = kernel.Get<IWindowManager>();
            IMFCore mailFoxDatabase = kernel.Get<IMFCore>();

            contacts = new ObservableCollection<ContactAdapter>();

            removeContactCommand = new Command(async obj =>
            {
                if (obj is ContactAdapter contact && contact != null)
                {
                    bool? accepted = windowManager.ShowDialog(new DialogWindow("Вы точно хотите удалить этот контакт?", windowManager));

                    if (accepted != null && (bool)accepted)
                    {
                        await mailFoxDatabase.RemoveContactAsync(contact.Contact);
                        windowManager.ShowMessage(this, "Контакт удалён");

                        contacts.Remove(contact);
                    }
                }
            });

            addContactCommand = new Command(obj =>
            {
                object?[] results = windowManager.ShowDialogWithResult(new AddContactWindow());
                bool? added = results[0] as bool?;

                if (added != null && (bool)added && results[1] is Contact newContact)
                    contacts.Add(new(newContact, removeContactCommand));
            });


            GetContacts(mailFoxDatabase, contacts, removeContactCommand).Wait();
        }
    }
}