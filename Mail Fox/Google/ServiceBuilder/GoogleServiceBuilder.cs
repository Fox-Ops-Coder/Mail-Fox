using Common.AppService.Manager;
using Mailing.Services;
using System;

namespace Google.ServiceBuilder
{
    internal sealed class GoogleServiceBuilder : IMailServiceBuilder
    {
        private const string serviceName = "Google";
        public string ServiceName => serviceName;

        public IMailService? CreateMailService(IWindowManager windowManager)
        {
            throw new NotImplementedException();
        }
    }
}