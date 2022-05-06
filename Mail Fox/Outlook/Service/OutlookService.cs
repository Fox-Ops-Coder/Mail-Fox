﻿using Mailing.Services;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Net;
using System.Security;
using System.Threading.Tasks;

namespace Outlook.Service
{
    internal sealed class OutlookService : IMailService
    {
        private readonly ImapClient imapClient;
        private readonly SmtpClient smtpClient;

        private const string ImapHost = "outlook.office365.com";
        private const int ImapPort = 993;
        private const SecureSocketOptions Pop3SecureOption = SecureSocketOptions.SslOnConnect;

        private const string SmtpHost = "smtp-mail.outlook.com";
        private const int SmtpPort = 587;
        private const SecureSocketOptions SmtpSecureOption = SecureSocketOptions.StartTls;

        private NetworkCredential? userCredentials;

        public string? Email => userCredentials?.UserName;

        public SecureString? Password => userCredentials?.SecurePassword;

        private string serviceName;
        public string Service => serviceName;

        public bool Connected => imapClient.IsConnected && smtpClient.IsConnected;

        public bool Authentificated => imapClient.IsAuthenticated && smtpClient.IsAuthenticated;

        public OutlookService(string serviceName)
        {
            imapClient = new();
            smtpClient = new();

            this.serviceName = serviceName;
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
    }
}