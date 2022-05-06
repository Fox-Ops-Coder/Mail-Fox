using Common.AppService.WindowService;
using System.Security;
using System.Threading.Tasks;

namespace Mailing.Services
{
    public interface IMailServiceBuilder
    {
        string ServiceName { get; }

        Task<IMailService?> CreateMailService(string email, SecureString password);

        void CreateMailService(IManagable managable, INavigator navigator);
    }
}