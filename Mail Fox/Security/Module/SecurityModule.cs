using Ninject.Modules;
using Security.Service;

namespace Security.Module
{
    internal sealed class SecurityModule : NinjectModule
    {
        public override void Load() =>
            Bind<ISecurityService>().To<SecurityService>().InSingletonScope();
    }
}