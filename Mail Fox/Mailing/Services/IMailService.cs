using System.Net;
using System.Security;
using System.Threading.Tasks;

namespace Mailing.Services
{
    public interface IMailService
    {
        string? Email { get; }

        SecureString? Password { get; }

        bool Connected { get; }

        bool Authentificated { get; }

        Task<bool> ConnectAsync();

        Task DisconnectAsync();

        Task<bool> AuthorizeAsync(string email, SecureString password);
    }
}