using Mailing.Services;
using System.Collections.Generic;
using static Mailing.ServiceManager.IMailServiceManager;

namespace Mailing.ServiceManager
{
    internal sealed class MailServiceManager : IMailServiceManager
    {
        private readonly List<IMailService> mailServices;

        public event MailServiceHandler? OnAdd;

        public event MailServiceHandler? OnRemove;

        public MailServiceManager() =>
            mailServices = new List<IMailService>();

        public void AddService(IMailService mailService)
        {
            mailServices.Add(mailService);
            OnAdd?.Invoke(mailService);
        }

        public void RemoveService(IMailService mailService)
        {
            if (mailServices.Remove(mailService))
                OnRemove?.Invoke(mailService);
        }

        public IReadOnlyList<IMailService> GetMailServices() => mailServices;
    }
}