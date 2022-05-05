using Common.AppService.WindowService;
using Mailing.Services;
using System;

namespace Google.ServiceBuilder
{
    internal sealed class GoogleServiceBuilder : IMailServiceBuilder
    {
        private const string serviceName = "Google";
        public string ServiceName => serviceName;

        public void CreateMailService(IManagable managable, INavigator navigator)
        {
            throw new NotImplementedException();
        }
    }
}