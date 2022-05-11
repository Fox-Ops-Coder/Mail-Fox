using MailKit;
using MimeKit;
using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;

namespace Mailing.Services
{
    public interface IMailService
    {
        string? Email { get; }

        string Service { get; }

        SecureString? Password { get; }

        bool Connected { get; }

        bool Authentificated { get; }

        Task<bool> ConnectAsync();

        Task DisconnectAsync();

        Task<bool> AuthorizeAsync(string email, SecureString password);

        Task<string> SendMessageAsync(MimeMessage mailMessage);

        Task<IEnumerable<IMessageSummary>?> GetMessagesAsync(IMailFolder folder);

        Task<IEnumerable<IMessageSummary>?> GetMessagesAsync(IMailFolder folder, bool attachments, string text);

        Task<IEnumerable<IEnumerable<IMailFolder>>> GetFoldersAsync();

        Task<MimeMessage?> GetMessageAsync(IMailFolder folder,
            IMessageSummary summary);

        Task DeleteMessageAsync(IMailFolder folder,
            IMessageSummary summary);
    }
}