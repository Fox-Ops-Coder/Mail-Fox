using Ninject.Modules;

namespace Security.Module
{
    public static class SecurityModuleFactory
    {
        public static INinjectModule CreateModule() => 
            new SecurityModule();
    }
}