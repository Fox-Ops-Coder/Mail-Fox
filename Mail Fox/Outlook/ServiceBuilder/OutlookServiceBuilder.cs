using Common.AppService.Manager;
using Mailing.Services;
using System;

namespace Outlook.ServiceBuilder
{
    internal sealed class OutlookServiceBuilder : IMailServiceBuilder
    {
        private const string serviceName = "Outlook";
        public string ServiceName => serviceName;

        public IMailService? CreateMailService(IWindowManager windowManager)
        {
            throw new NotImplementedException();
        }
    }
}