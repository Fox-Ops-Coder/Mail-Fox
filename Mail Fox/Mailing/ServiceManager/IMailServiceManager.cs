using Mailing.Services;
using System.Collections.Generic;

namespace Mailing.ServiceManager
{
    public interface IMailServiceManager
    {
        void AddService(IMailService mailService);

        void RemoveService(IMailService mailService);

        delegate void MailServiceHandler(IMailService mailService);

        event MailServiceHandler OnAdd;

        event MailServiceHandler OnRemove;

        IReadOnlyList<IMailService> GetMailServices();
    }
}