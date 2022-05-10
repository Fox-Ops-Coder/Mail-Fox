using Mailing.Services;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security;
using System.Threading.Tasks;
using IMailService = Mailing.Services.IMailService;

namespace Google.Service
{
    internal sealed class GoogleService : IMailService
    {
        private readonly ImapClient imapClient;
        private readonly SmtpClient smtpClient;

        private const string ImapHost = "imap.gmail.com";
        private const int ImapPort = 993;
        private const SecureSocketOptions Pop3SecureOption = SecureSocketOptions.Auto;

        private const string SmtpHost = "smtp.gmail.com";
        private const int SmtpPort = 587;
        private const SecureSocketOptions SmtpSecureOption = SecureSocketOptions.Auto;

        private NetworkCredential? userCredentials;

        public string? Email => userCredentials?.UserName;

        private string service;
        public string Service => service;

        public SecureString? Password => userCredentials?.SecurePassword;

        public bool Connected => imapClient.IsConnected && smtpClient.IsConnected;

        public bool Authentificated => imapClient.IsAuthenticated && smtpClient.IsAuthenticated;

        public GoogleService(string serviceName)
        {
            imapClient = new();
            smtpClient = new();

            service = serviceName;
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

        public async Task<IEnumerable<IMessageSummary>?> GetMessagesAsync(IMailFolder folder)
        {
            IEnumerable<IMessageSummary>? summaries = null;

            try
            {
                int count = 15;

                await folder.OpenAsync(FolderAccess.ReadOnly);

                List<int> ids = new();

                for (int i = folder.Count - 1; i >= 0 && ids.Count < count; --i)
                    ids.Add(i);

                summaries = await folder.FetchAsync(ids, MessageSummaryItems.BodyStructure |
                    MessageSummaryItems.Envelope | MessageSummaryItems.Flags);
            }
            finally
            {
                await folder.CloseAsync();
            }

            return summaries;
        }

        public async Task<IEnumerable<IEnumerable<IMailFolder>>> GetFoldersAsync()
        {
            List<IEnumerable<IMailFolder>> folders = new();

            foreach (FolderNamespace @namespace in imapClient.PersonalNamespaces)
                folders.Add(await imapClient.GetFoldersAsync(@namespace));

            return folders;
        }

    }
}