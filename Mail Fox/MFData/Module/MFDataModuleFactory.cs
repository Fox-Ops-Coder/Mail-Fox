using Ninject.Modules;

namespace MFData.Module
{
    public static class MFDataModuleFactory
    {
        public static INinjectModule CreateModule(string dataSource) =>
            new DatabaseModule(dataSource);
    }
}