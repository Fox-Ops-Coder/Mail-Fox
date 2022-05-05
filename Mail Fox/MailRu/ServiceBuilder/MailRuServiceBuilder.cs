using Common.AppService.WindowService;
using Mailing.Services;
using MailRu.UI;

namespace MailRu.ServiceBuilder
{
    internal class MailRuServiceBuilder : IMailServiceBuilder
    {
        private const string serviceName = "Mail.Ru";
        public string ServiceName => serviceName;

        public void CreateMailService(IManagable managable, INavigator navigator) =>
            navigator.Navigate(new LoginPage(managable, navigator));
    }
}