using Ninject.Modules;

namespace Mailing.Module
{
    public static class MailingModuleFactory
    {
        public static INinjectModule CreateModule() =>
            new MailingServiceModule();
    }
}