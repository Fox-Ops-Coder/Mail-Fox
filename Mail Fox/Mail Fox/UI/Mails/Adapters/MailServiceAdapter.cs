using Mailing.Services;

namespace MailFox.UI.Mails.Adapters
{
    internal sealed class MailServiceAdapter
    {
        private readonly IMailService mailService;
        public IMailService MailService => mailService;

        public MailServiceAdapter(IMailService mailService) =>
            this.mailService = mailService;

        public override string ToString() =>
            !string.IsNullOrEmpty(mailService.Email) ? mailService.Email : string.Empty;
    }
}