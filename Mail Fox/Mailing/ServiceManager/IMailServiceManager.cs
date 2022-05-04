using Mailing.Services;
using System.Collections.Generic;

namespace Mailing.ServiceManager
{
    public interface IMailServiceManager
    {
        void AddServiceAsync(IMailService mailService);

        void RemoveServiceAsync(IMailService mailService);

        IReadOnlyList<IMailService> GetMailServices();
    }
}