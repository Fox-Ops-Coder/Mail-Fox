using Mailing.Services;
using System.Collections.Generic;

namespace Mailing.ServiceManager
{
    public interface IMailServiceManager
    {
        void AddService(IMailService mailService);

        void RemoveService(IMailService mailService);

        IReadOnlyList<IMailService> GetMailServices();
    }
}