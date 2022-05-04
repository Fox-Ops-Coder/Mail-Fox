using Mailing.Services;
using Ninject.Modules;
using Outlook.ServiceBuilder;

namespace Outlook.Module
{
    internal sealed class OutlookServiceModule : NinjectModule
    {
        public override void Load() =>
            Bind<IMailServiceBuilder>().To<OutlookServiceBuilder>().InSingletonScope();
    }
}