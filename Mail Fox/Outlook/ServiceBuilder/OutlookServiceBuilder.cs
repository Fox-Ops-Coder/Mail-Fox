using Common.AppService.WindowService;
using Mailing.Services;
using Outlook.Service;
using Outlook.UI;
using System.Security;
using System.Threading.Tasks;

namespace Outlook.ServiceBuilder
{
    internal sealed class OutlookServiceBuilder : IMailServiceBuilder
    {
        private const string serviceName = "Outlook";
        public string ServiceName => serviceName;

        public async Task<IMailService?> CreateMailService(string email, SecureString password)
        {
            IMailService mailService = new OutlookService(serviceName);

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