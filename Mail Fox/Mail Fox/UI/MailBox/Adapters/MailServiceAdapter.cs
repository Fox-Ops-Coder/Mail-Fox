using Mailing.Services;
using System.Windows.Input;

namespace MailFox.UI.MailBox.Adapters
{
    internal sealed class MailServiceAdapter
    {
        private readonly IMailService mailService;
        public IMailService MailService => mailService;

        private readonly ICommand logoutCommand;
        public ICommand LogoutCommand => logoutCommand;

        public MailServiceAdapter(IMailService mailService, ICommand logoutCommand)
        {
            this.mailService = mailService;
            this.logoutCommand = logoutCommand;
        }

        public override string? ToString() =>
            mailService.Email;
    }
}