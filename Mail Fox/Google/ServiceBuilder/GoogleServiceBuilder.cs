using Common.AppService.WindowService;
using Google.Service;
using Google.UI;
using Mailing.Services;
using System;
using System.Security;
using System.Threading.Tasks;

namespace Google.ServiceBuilder
{
    internal sealed class GoogleServiceBuilder : IMailServiceBuilder
    {
        private const string serviceName = "Google";
        public string ServiceName => serviceName;

        public async Task<IMailService?> CreateMailService(string email, SecureString password)
        {
            IMailService mailService = new GoogleService(serviceName);

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