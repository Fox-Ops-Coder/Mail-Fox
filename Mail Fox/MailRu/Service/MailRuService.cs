using Mailing.Services;
using MailKit.Net.Imap;
using MailKit.Security;
using MimeKit;
using System;
using System.Net;
using System.Security;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace MailRu.Service
{
    internal sealed class MailRuService : IMailService
    {
        private readonly ImapClient imapClient;
        private readonly SmtpClient smtpClient;

        private const string ImapHost = "imap.mail.ru";
        private const int ImapPort = 993;
        private const SecureSocketOptions Pop3SecureOption = SecureSocketOptions.Auto;

        private const string SmtpHost = "smtp.mail.ru";
        private const int SmtpPort = 465;
        private const SecureSocketOptions SmtpSecureOption = SecureSocketOptions.Auto;

        private NetworkCredential? userCredentials;

        public string? Email => userCredentials?.UserName;

        private readonly string service;
        public string Service => service;

        public SecureString? Password => userCredentials?.SecurePassword;

        public bool Connected => imapClient.IsConnected && smtpClient.IsConnected;

        public bool Authentificated => imapClient.IsAuthenticated && smtpClient.IsAuthenticated;

        public MailRuService(string service)
        {
            imapClient = new();
            smtpClient = new();

            this.service = service;
        }

        public async Task<bool> ConnectAsync()
        {
            try
            {
                await imapClient.ConnectAsync(ImapHost, ImapPort, Pop3SecureOption);
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
            await imapClient.DisconnectAsync(true);
            await smtpClient.DisconnectAsync(true);
        }

        public async Task<bool> AuthorizeAsync(string email, SecureString password)
        {
            userCredentials = new(email, password);

            try
            {
                await imapClient.AuthenticateAsync(userCredentials);
                await smtpClient.AuthenticateAsync(userCredentials);
            }
            catch
            {
                userCredentials = null;

                return false;
            }

            return Authentificated;
        }

        public async Task<string> SendMessageAsync(MimeMessage mailMessage)
        {
            try
            {
                return await smtpClient.SendAsync(mailMessage);
            }
            catch (Exception err)
            {
                return err.Message;
            }
        }
    }
}