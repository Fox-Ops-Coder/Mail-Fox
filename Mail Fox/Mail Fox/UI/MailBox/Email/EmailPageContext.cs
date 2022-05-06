using MailFox.UI.Context;
using MailFox.UI.MailBox.Email.Adapters;
using Mailing.ServiceManager;
using Mailing.Services;
using MFData.Core;
using MFData.Entities;
using Ninject;
using Security.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MailFox.UI.MailBox.Email
{
    internal sealed class EmailPageContext : KernelContext
    {
        private readonly ObservableCollection<MailServiceAdapter> mailServices;
        public ObservableCollection<MailServiceAdapter> MailServices => mailServices;

        private async void LogIn()
        {
            IMFCore mailFoxDatabase = kernel.Get<IMFCore>();
            ISecurityService securityService = kernel.Get<ISecurityService>();
            IMailServiceManager mailServiceManager = kernel.Get<IMailServiceManager>();

            IEnumerable<IMailServiceBuilder> mailServiceBuilders = kernel.GetAll<IMailServiceBuilder>();
            IEnumerable<UserEmail> userEmails = await mailFoxDatabase.GetUserEmailsAsync();

            foreach (UserEmail userEmail in userEmails)
            {
                IMailServiceBuilder mailServiceBuilder = mailServiceBuilders
                    .Where(service => service.ServiceName == userEmail.Service).First();

                IMailService? mailService = await mailServiceBuilder
                    .CreateMailService(userEmail.Email, securityService.DecodeString(userEmail.Password));

                if (mailService != null)
                    mailServiceManager.AddService(mailService);
            }
        }

        public EmailPageContext()
        {
            mailServices = new();

            IMailServiceManager mailServiceManager = kernel.Get<IMailServiceManager>();
            mailServiceManager.OnAdd += new IMailServiceManager.MailServiceHandler(service => mailServices.Add(new(service)));

            LogIn();
        }
    }
}