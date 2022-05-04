using Mailing.Services;

namespace MailFox.UI.Login.Adapter
{
    internal sealed class ServiceAdapter
    {
        private readonly IMailServiceBuilder mailServiceBuilder;
        public IMailServiceBuilder MailServiceBuilder => mailServiceBuilder;

        public ServiceAdapter(IMailServiceBuilder mailServiceBuilder) =>
            this.mailServiceBuilder = mailServiceBuilder;

        public override string ToString() =>
            mailServiceBuilder.ServiceName;
    }
}