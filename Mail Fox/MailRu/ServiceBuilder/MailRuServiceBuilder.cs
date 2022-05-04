using Common.AppService.Manager;
using Mailing.Services;
using System;

namespace MailRu.ServiceBuilder
{
    internal class MailRuServiceBuilder : IMailServiceBuilder
    {
        private const string serviceName = "Mail.Ru";
        public string ServiceName => serviceName;

        public IMailService CreateMailService(IWindowManager windowManager)
        {
            throw new NotImplementedException();
        }
    }
}