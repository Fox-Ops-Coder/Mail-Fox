using Common.AppService.WindowService;
using Mailing.Services;
using MailRu.Service;
using MailRu.UI;
using System.Security;
using System.Threading.Tasks;

namespace MailRu.ServiceBuilder
{
    internal class MailRuServiceBuilder : IMailServiceBuilder
    {
        private const string serviceName = "Mail.Ru";
        public string ServiceName => serviceName;

        public async Task<IMailService?> CreateMailService(string email, SecureString password)
        {
            IMailService mailService = new MailRuService(serviceName);

            bool result = await mailService.ConnectAsync();

            if (result)
                result = await mailService.AuthorizeAsync(email, password);
            else
                await mailService.DisconnectAsync();

            return result ? mailService : null;
        }

        public void CreateMailService(IManagable managable, INavigator navigator) =>
            navigator.Navigate(new LoginPage(managable, navigator, serviceName));
    }
}