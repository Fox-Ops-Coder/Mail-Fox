using Mailing.Services;
using MailKit.Net.Pop3;
using MailKit.Security;
using System.Net;
using System.Security;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace MailRu.Service
{
    internal sealed class MailRuService : IMailService
    {
        private readonly Pop3Client pop3Client;
        private readonly SmtpClient smtpClient;

        private const string Pop3Host = "pop.mail.ru";
        private const int Pop3Port = 995;
        private const SecureSocketOptions Pop3SecureOption = SecureSocketOptions.SslOnConnect;

        private const string SmtpHost = "smtp.mail.ru";
        private const int SmtpPort = 465;
        private const SecureSocketOptions SmtpSecureOption = SecureSocketOptions.SslOnConnect;

        private NetworkCredential? userCredentials;

        public string? Email => userCredentials?.UserName;

        public SecureString? Password => userCredentials?.SecurePassword;

        public bool Connected => pop3Client.IsConnected && smtpClient.IsConnected;

        public bool Authentificated => pop3Client.IsAuthenticated && smtpClient.IsAuthenticated;

        public MailRuService()
        {
            pop3Client = new();
            smtpClient = new();
        }

        public async Task<bool> ConnectAsync()
        {
            try
            {
                await pop3Client.ConnectAsync(Pop3Host, Pop3Port, Pop3SecureOption);
                await smtpClient.ConnectAsync(SmtpHost, SmtpPort, SmtpSecureOption);
            }
            catch
            {
                return false;
            }

            return Connected;
        }

        public async Task DisconnectAsync()
        {
            await pop3Client.DisconnectAsync(true);
            await smtpClient.DisconnectAsync(true);
        }

        public async Task<bool> AuthorizeAsync(string email, SecureString password)
        {
            userCredentials = new(email, password);

            try
            {
                await pop3Client.AuthenticateAsync(userCredentials);
                await smtpClient.AuthenticateAsync(userCredentials);
            }
            catch
            {
                userCredentials = null;

                return false;
            }

            return Authentificated;
        }
    }
}