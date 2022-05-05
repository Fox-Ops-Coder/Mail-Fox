using Common.AppService.WindowService;
using Mailing.Services;
using System;

namespace Outlook.ServiceBuilder
{
    internal sealed class OutlookServiceBuilder : IMailServiceBuilder
    {
        private const string serviceName = "Outlook";
        public string ServiceName => serviceName;

        public void CreateMailService(IManagable managable, INavigator navigator)
        {
            throw new NotImplementedException();
        }
    }
}