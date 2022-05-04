using Ninject.Modules;

namespace Google.Module
{
    public static class GoogleModuleFactory
    {
        public static INinjectModule CreateModule() =>
            new GoogleServiceModule();
    }
}