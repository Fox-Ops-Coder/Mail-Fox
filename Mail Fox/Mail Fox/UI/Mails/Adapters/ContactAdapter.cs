using MFData.Entities;

namespace MailFox.UI.Mails.Adapters
{
    internal sealed class ContactAdapter
    {
        private readonly Contact contact;
        public Contact Contact => contact;

        public ContactAdapter(Contact contact) =>
            this.contact = contact;

        public override string ToString() =>
            contact.ContactName;
    }
}