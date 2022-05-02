using Common.AppService.Manager;
using Common.UICommand;
using MailFox.Kernel;
using MailFox.UI.Context;
using MailFox.UI.Login.Services;
using Ninject;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MailFox.UI.Login
{
    internal sealed class LoginContext : ContextBase
    {
        private readonly ObservableCollection<ServiceAdapter> services;
        public ObservableCollection<ServiceAdapter> Services => services;

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
            services = new();

            foreach (ServiceType serviceType in Enum.GetValues(typeof(ServiceType)))
                services.Add(new(serviceType));

            selectedService = services.First();

            IKernel kernel = AppKernel.GetKernel();
            IWindowManager windowManager = kernel.Get<IWindowManager>();

            loginCommand = new Command(obj =>
            {
                switch (selectedService.ServiceType)
                {
                    case ServiceType.MailRu:
                        windowManager.ShowMessage(this, $"Сервис {selectedService.TypeName} не доступен");
                        break;

                    case ServiceType.Google:
                        windowManager.ShowMessage(this, $"Сервис {selectedService.TypeName} не доступен");
                        break;

                    case ServiceType.Outlook:
                        windowManager.ShowMessage(this, $"Сервис {selectedService.TypeName} не доступен");
                        break;
                }
            });
        }
    }
}