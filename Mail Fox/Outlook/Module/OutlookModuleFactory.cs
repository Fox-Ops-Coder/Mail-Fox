using Ninject.Modules;

namespace Outlook.Module
{
    public static class OutlookModuleFactory
    {
        public static INinjectModule CreateModule() =>
            new OutlookServiceModule();
    }
}