using Mailing.Services;
using MailRu.ServiceBuilder;
using Ninject.Modules;

namespace MailRu.Module
{
    internal sealed class MailRuServiceModule : NinjectModule
    {
        public override void Load() =>
            Bind<IMailServiceBuilder>().To<MailRuServiceBuilder>().InSingletonScope();
    }
}