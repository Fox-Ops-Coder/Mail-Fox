using Common.AppService;
using Common.AppService.Manager;
using Common.AppService.WindowService;
using Common.UICommand;
using MailFox.UI.Context;
using MFData.Core;
using MFData.Entities;
using Ninject;
using System.Windows.Input;

namespace MailFox.UI.AddressBook.Add
{
    internal sealed class AddContactContext : ContextBase, IResult
    {
        private readonly ICommand saveCommand;
        public ICommand SaveCommand => saveCommand;

        private string emailAddress;
        public string EmailAddress { get => emailAddress; set => emailAddress = value; }

        private string contactName;
        public string ContactName { get => contactName; set => contactName = value; }

        private Contact? newContact;
        public object? Result => newContact;

        public AddContactContext()
        {
            emailAddress = string.Empty;
            contactName = string.Empty;

            IWindowManager windowManager = kernel.Get<IWindowManager>();
            IMFCore mailFoxDatabase = kernel.Get<IMFCore>();

            saveCommand = new Command(async obj =>
            {
                bool emailAddressFilled = !string.IsNullOrEmpty(emailAddress);
                bool contactNameFilled = !string.IsNullOrEmpty(contactName);

                if (emailAddressFilled && contactNameFilled)
                {
                    Contact contact = new()
                    {
                        ContactEmail = emailAddress,
                        ContactName = contactName
                    };

                    await mailFoxDatabase.AddContactAsync(contact);

                    newContact = contact;

                    windowManager.CloseWindow(this, true);
                }
            });
        }
    }
}