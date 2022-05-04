using Ninject.Modules;

namespace MailRu.Module
{
    public static class MailRuModuleBuilder
    {
        public static INinjectModule CreateModule() =>
            new MailRuServiceModule();
    }
}