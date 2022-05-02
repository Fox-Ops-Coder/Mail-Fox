using MFData.Context;
using MFData.Core;
using Ninject.Modules;

namespace MFData.Module
{
    public sealed class DatabaseModule : NinjectModule
    {
        private readonly string dataSource;

        public DatabaseModule(string dataSource) =>
            this.dataSource = dataSource;

        public override void Load() =>
            Bind<IMFCore>().To<MFCore>()
            .InSingletonScope().WithConstructorArgument("database", new MFDatabase(dataSource));
    }
}