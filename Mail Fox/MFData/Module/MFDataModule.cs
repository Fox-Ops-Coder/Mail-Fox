using Ninject.Modules;

namespace MFData.Module
{
    public static class MFDataModule
    {
        public static INinjectModule CreateModule(string dataSource) =>
            new DatabaseModule(dataSource);
    }
}