using Common.AppService.Manager;
using Common.AppService.WindowService;
using Common.UICommand;
using MailFox.UI.Context;
using MFData.Core;
using MFData.Entities;
using Ninject;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MailFox.UI.AddressBook.Add
{
    internal sealed class AddContactContext : AppBarContext, IResult
    {
        private readonly ICommand saveCommand;
        public ICommand SaveCommand => saveCommand;

        private readonly string windowTitle;
        public string WindowTitle => windowTitle;

        private string emailAddress;
        public string EmailAddress { get => emailAddress; set => emailAddress = value; }

        private string contactName;
        public string ContactName { get => contactName; set => contactName = value; }

        private Contact? newContact;
        public object? Result => newContact;

        private async Task Operation(bool isCreate, Contact? contact, IMFCore mailFoxDatabase)
        {
            switch(isCreate)
            {
                case true:
                    newContact = new()
                    {
                        ContactEmail = emailAddress,
                        ContactName = contactName
                    };
                    await mailFoxDatabase.AddContactAsync(newContact);
                    break;

                case false:
                    if (contact != null)
                    {
                        contact.ContactEmail = emailAddress;
                        contact.ContactName = contactName;

                        await mailFoxDatabase.UpdateContactAsync(contact);
                    }
                    break;
            }
        }

        public AddContactContext(bool isCreate, Contact? contact)
        {
            switch (isCreate)
            {
                case true:
                    windowTitle = "Новый контакт";
                    break;

                case false:
                    windowTitle = "Изменить контакт";
                    break;
            }

            if (contact != null)
            {
                emailAddress = contact.ContactEmail;
                contactName = contact.ContactName;
            }
            else
            {
                emailAddress = string.Empty;
                contactName = string.Empty;
            }


            IMFCore mailFoxDatabase = kernel.Get<IMFCore>();

            saveCommand = new Command(async obj =>
            {
                bool emailAddressFilled = !string.IsNullOrEmpty(emailAddress);
                bool contactNameFilled = !string.IsNullOrEmpty(contactName);

                if (emailAddressFilled && contactNameFilled)
                {
                    await Operation(isCreate, contact, mailFoxDatabase);
                    windowManager.CloseWindow(this, true);
                }
            });
        }
    }
}