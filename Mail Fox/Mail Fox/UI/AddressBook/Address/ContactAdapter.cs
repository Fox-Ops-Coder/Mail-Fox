using MFData.Entities;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MailFox.UI.AddressBook.Address
{
    internal sealed class ContactAdapter : INotifyPropertyChanged
    {
        private readonly Contact contact;
        public Contact Contact => contact;

        public string Name => $"{contact.ContactName} {contact.ContactEmail}";

        private readonly ICommand editCommand;
        public ICommand EditCommand => editCommand;

        private readonly ICommand removeCommand;
        public ICommand RemoveCommand => removeCommand;

        public void TextChanged() =>
            OnPropertyChanged("Name");

        public ContactAdapter(Contact contact,
            ICommand editCommand, ICommand removeCommand)
        {
            this.contact = contact;
            this.editCommand = editCommand;
            this.removeCommand = removeCommand;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    }
}