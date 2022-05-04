using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Mailing.Services
{
    public interface IMailService
    {
        string EmailAddress { get; }

        Task AuthorizeAsync(string emailAddress);

        Task SendMessageAsync(MailMessage message);

        Task RemoveMessage(MailMessage message);

        Task<IEnumerable<MailMessage>> GetMessagesAsync();
    }
}