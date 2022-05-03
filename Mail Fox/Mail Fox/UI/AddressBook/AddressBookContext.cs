using Common.AppService.Manager;
using Common.UI;
using Common.UICommand;
using MailFox.Kernel;
using MailFox.UI.AddressBook.Add;
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
        private readonly ObservableCollection<Contact> contacts;
        public ObservableCollection<Contact> Contacts => contacts;

        private readonly ICommand addContactCommand;
        public ICommand AddContactCommand => addContactCommand;

        private readonly ICommand removeContactCommand;
        public ICommand RemoveContactCommand => removeContactCommand;

        private static async Task GetContacts(IMFCore mailFoxDatabase,
            ObservableCollection<Contact> contactsCollection)
        {
            IEnumerable<Contact> contacts = await mailFoxDatabase.GetContactsAsync();

            foreach(Contact contact in contacts)
                contactsCollection.Add(contact);
        }

        public AddressBookContext()
        {
            IKernel kernel = AppKernel.GetKernel();

            IWindowManager windowManager = kernel.Get<IWindowManager>();
            IMFCore mailFoxDatabase = kernel.Get<IMFCore>();

            contacts = new ObservableCollection<Contact>();
            GetContacts(mailFoxDatabase, contacts).Wait();

            addContactCommand = new Command(async obj =>
            {
                bool? added = windowManager.ShowDialog(new AddContactWindow());

                if (added != null && (bool)added)
                {
                    contacts.Clear();
                    await GetContacts(mailFoxDatabase, contacts);
                }
            });

            removeContactCommand = new Command(async obj =>
            {
                if (obj is Contact contact)
                {
                    bool? accepted = windowManager.ShowDialog(new DialogWindow("Вы точно хотите удалить этот контакт?", windowManager));

                    if (accepted != null && (bool)accepted)
                    {
                        await mailFoxDatabase.RemoveContactAsync(contact);
                        windowManager.ShowMessage(this, "Контакт удалён");

                        await GetContacts(mailFoxDatabase, contacts);
                    }
                }
            });
        }
    }
}