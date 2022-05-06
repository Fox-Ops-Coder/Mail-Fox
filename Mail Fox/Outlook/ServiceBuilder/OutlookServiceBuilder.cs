using Common.AppService.WindowService;
using Mailing.Services;
using System;
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
            throw new NotImplementedException();
        }

        public void CreateMailService(IManagable managable, INavigator navigator)
        {
            throw new NotImplementedException();
        }
    }
}