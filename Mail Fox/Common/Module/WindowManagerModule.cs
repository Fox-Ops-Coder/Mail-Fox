using Common.AppService.Manager;
using Ninject.Modules;

namespace Common.Module
{
    public sealed class WindowManagerModule : NinjectModule
    {
        public override void Load() => 
            Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
    }
}