using Common.Module;
using MFData.Module;
using Ninject;
using System;

namespace MailFox.Kernel
{
    internal static class AppKernel
    {
        private static IKernel? kernel;
        public static IKernel GetKernel()
        {
            if (kernel == null)
            {
                string dataSource = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                dataSource += "mailfox.dat";

                kernel = new StandardKernel();
                kernel.Load(new WindowManagerModule(), new DatabaseModule(dataSource));
            }

            return kernel;
        }
    }
}