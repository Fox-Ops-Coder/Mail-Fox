using Mailing.Services;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Net;
using System.Security;
using System.Threading.Tasks;

namespace Google.Service
{
    internal sealed class GoogleService : IMailService
    {
        private readonly Pop3Client pop3Client;
        private readonly SmtpClient smtpClient;

        private const string Pop3Host = "pop.gmail.com";
        private const int Pop3Port = 995;
        private const SecureSocketOptions Pop3SecureOption = SecureSocketOptions.SslOnConnect;

        private const string SmtpHost = "smtp.gmail.com";
        private const int SmtpPort = 587;
        private const SecureSocketOptions SmtpSecureOption = SecureSocketOptions.StartTls;

        private NetworkCredential? userCredentials;

        public string? Email => userCredentials?.UserName;

        private string service;
        public string Service => service;

        public SecureString? Password => userCredentials?.SecurePassword;

        public bool Connected => pop3Client.IsConnected && smtpClient.IsConnected;

        public bool Authentificated => pop3Client.IsAuthenticated && smtpClient.IsAuthenticated;

        public GoogleService(string serviceName)
        {
            pop3Client = new();
            smtpClient = new();

            service = serviceName;
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
