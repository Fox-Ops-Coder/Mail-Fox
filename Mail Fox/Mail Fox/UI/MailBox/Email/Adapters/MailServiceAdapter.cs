using Mailing.Services;

namespace MailFox.UI.MailBox.Email.Adapters
{
    internal sealed class MailServiceAdapter
    {
        private readonly IMailService mailService;
        public IMailService MailService => mailService;

        public MailServiceAdapter(IMailService mailService) =>
            this.mailService = mailService;

        public override string? ToString() =>
            mailService.Email;
    }
}