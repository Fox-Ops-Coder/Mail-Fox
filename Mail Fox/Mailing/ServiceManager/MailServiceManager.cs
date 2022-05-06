using Mailing.Services;
using System.Collections.Generic;

namespace Mailing.ServiceManager
{
    internal sealed class MailServiceManager : IMailServiceManager
    {
        private readonly List<IMailService> mailServices;

        public MailServiceManager() =>
            mailServices = new List<IMailService>();

        public void AddService(IMailService mailService) =>
            mailServices.Add(mailService);

        public void RemoveService(IMailService mailService) =>
            mailServices.Remove(mailService);

        public IReadOnlyList<IMailService> GetMailServices() => mailServices;
    }
}