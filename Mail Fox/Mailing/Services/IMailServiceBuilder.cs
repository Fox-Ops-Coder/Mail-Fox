using Common.AppService.WindowService;

namespace Mailing.Services
{
    public interface IMailServiceBuilder
    {
        string ServiceName { get; }

        void CreateMailService(IManagable managable, INavigator navigator);
    }
}