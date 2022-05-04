using Google.ServiceBuilder;
using Mailing.Services;
using Ninject.Modules;

namespace Google.Module
{
    internal sealed class GoogleServiceModule : NinjectModule
    {
        public override void Load() =>
            Bind<IMailServiceBuilder>().To<GoogleServiceBuilder>().InSingletonScope();
    }
}