using MFData.Entities;
using System.Windows.Input;

namespace MailFox.UI.AddressBook.Address
{
    internal sealed class ContactAdapter
    {
        private readonly Contact contact;
        public Contact Contact => contact;

        private readonly ICommand removeCommand;
        public ICommand RemoveCommand => removeCommand;

        public ContactAdapter(Contact contact, ICommand removeCommand)
        {
            this.contact = contact;
            this.removeCommand = removeCommand;
        }
    }
}