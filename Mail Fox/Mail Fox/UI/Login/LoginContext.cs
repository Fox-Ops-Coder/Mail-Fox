using Common.AppService.Manager;
using Common.UICommand;
using MailFox.UI.Context;
using MailFox.UI.Login.Adapter;
using Mailing.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace MailFox.UI.Login
{
    internal sealed class LoginContext : ContextBase
    {
        private List<ServiceAdapter> mailServices;
        public List<ServiceAdapter> MailServices => mailServices;

        private ServiceAdapter selectedService;
        public ServiceAdapter SelectedService
        {
            get => selectedService;
            set => selectedService = value;
        }

        private readonly ICommand loginCommand;
        public ICommand LoginCommand => loginCommand;

        public LoginContext()
        {
            IEnumerable<IMailServiceBuilder> services = kernel.GetAll<IMailServiceBuilder>();

            mailServices = new();
            foreach (IMailServiceBuilder service in services)
                mailServices.Add(new(service));

            selectedService = mailServices.First();

            IWindowManager windowManager = kernel.Get<IWindowManager>();

            loginCommand = new Command(obj =>
            {
                try
                {
                    selectedService.MailServiceBuilder.CreateMailService(windowManager);
                }
                catch (Exception ex)
                {
                    windowManager.ShowMessage(this, ex.Message);
                }
            });
        }
    }
}