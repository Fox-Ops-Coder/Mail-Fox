using Mailing.ServiceManager;
using Ninject.Modules;

namespace Mailing.Module
{
    public sealed class MailingServiceModule : NinjectModule
    {
        public override void Load() =>
            Bind<IMailServiceManager>().To<MailServiceManager>().InSingletonScope();
    }
}