using Ninject.Modules;

namespace Common.Module
{
    public static class CommonModuleFactory
    {
        public static INinjectModule CreateModule() =>
            new WindowManagerModule();
    }
}