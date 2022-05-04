using Common.AppService.Manager;

namespace Mailing.Services
{
    public interface IMailServiceBuilder
    {
        string ServiceName { get; }

        IMailService CreateMailService(IWindowManager windowManager);
    }
}