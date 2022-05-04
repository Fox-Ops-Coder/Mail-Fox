using Common.Module;
using Mailing.Module;
using MFData.Core;
using MFData.Module;
using Ninject;
using System;
using System.IO;

namespace MailFox.Kernel
{
    internal static class AppKernel
    {
        private static IKernel? kernel;

        public static IKernel GetKernel()
        {
            if (kernel == null)
            {
                string userFolden = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string appFolden = userFolden + "\\Documents\\Mail Fox";
                string dataSource = appFolden + "\\mailfox.dat";

                if (!Directory.Exists(appFolden))
                    Directory.CreateDirectory(appFolden);

                kernel = new StandardKernel();
                kernel.Load(new WindowManagerModule(),
                    new DatabaseModule(dataSource),
                    new MailingServiceModule());

                IMFCore mailFoxDatabase = kernel.Get<IMFCore>();
                mailFoxDatabase.EnsureCreated();
            }

            return kernel;
        }
    }
}