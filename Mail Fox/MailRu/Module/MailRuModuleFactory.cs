using Ninject.Modules;

namespace MailRu.Module
{
    public static class MailRuModuleFactory
    {
        public static INinjectModule CreateModule() =>
            new MailRuServiceModule();
    }
}