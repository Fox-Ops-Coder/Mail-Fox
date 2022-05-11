using Common.AppService.Manager;
using Common.UI;
using Common.UICommand;
using MailFox.Kernel;
using MailFox.UI.AddressBook.Add;
using MailFox.UI.AddressBook.Address;
using MailFox.UI.Context;
using MFData.Core;
using MFData.Entities;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MailFox.UI.AddressBook
{
    internal sealed class AddressBookContext : AppBarContext
    {
        private readonly ObservableCollection<ContactAdapter> contacts;
        public ObservableCollection<ContactAdapter> Contacts => contacts;

        private readonly ICommand addContactCommand;
        public ICommand AddContactCommand => addContactCommand;

        private readonly ICommand editCommand;
        public ICommand EditCommand => editCommand;

        private readonly ICommand removeContactCommand;
        public ICommand RemoveContactCommand => removeContactCommand;

        private static async void GetContacts(IMFCore mailFoxDatabase,
            ObservableCollection<ContactAdapter> contactsCollection,
            ICommand removeCommand, ICommand editCommand)
        {
            IEnumerable<Contact> contacts = await mailFoxDatabase.GetContactsAsync();

            foreach (Contact contact in contacts)
                contactsCollection.Add(new(contact, editCommand, removeCommand));
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

            editCommand = new Command(obj =>
            {
                if (obj is ContactAdapter contact)
                {
                    windowManager.ShowDialog(new AddContactWindow(false, contact.Contact));
                    contact.TextChanged();
                }
            });

            addContactCommand = new Command(obj =>
            {
                Tuple<bool?, object?> results = windowManager.ShowDialogWithResult(new AddContactWindow());

                if (results.Item1.GetValueOrDefault() && results.Item2 is Contact contact)
                    contacts.Add(new(contact, editCommand, removeContactCommand));
            });

            GetContacts(mailFoxDatabase, contacts, removeContactCommand, editCommand);
        }
    }
}