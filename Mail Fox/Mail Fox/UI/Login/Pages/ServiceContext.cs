using Common.AppService;
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

namespace MailFox.UI.Login.Pages
{
    internal sealed class ServiceContext : KernelContext, IWindow
    {
        private readonly Guid guid;
        public Guid Guid => throw new NotImplementedException();

        private readonly List<ServiceAdapter> mailServices;
        public List<ServiceAdapter> MailServices => mailServices;

        private ServiceAdapter selectedService;
        public ServiceAdapter SelectedService
        {
            get => selectedService;
            set => selectedService = value;
        }

        private readonly ICommand loginCommand;
        public ICommand LoginCommand => loginCommand;

        public ServiceContext(Guid guid)
        {
            this.guid = guid;

            IEnumerable<IMailServiceBuilder> services = kernel.GetAll<IMailServiceBuilder>();

            mailServices = new();
            foreach (IMailServiceBuilder service in services)
                mailServices.Add(new(service));

            selectedService = mailServices.First();

            IWindowManager windowManager = kernel.Get<IWindowManager>();

            loginCommand = new Command(obj =>
            {
                /*windowManager.HideWindow(this);

                createService = selectedService.MailServiceBuilder.CreateMailService(windowManager);

                windowManager.ShowWindow(this);

                if (createService != null)
                    windowManager.CloseWindow(this, true);
                else
                    windowManager.CloseWindow(this, false);*/
            });

        }
    }
}