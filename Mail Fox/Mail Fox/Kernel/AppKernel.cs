using Common.Module;
using Google.Module;
using Mailing.Module;
using MailRu.Module;
using MFData.Core;
using MFData.Module;
using Ninject;
using Ninject.Modules;
using Outlook.Module;
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
                kernel.Load(new INinjectModule[]
                {
                    CommonModuleFactory.CreateModule(),
                    MailingModuleFactory.CreateModule(),
                    MailRuModuleFactory.CreateModule(),
                    GoogleModuleFactory.CreateModule(),
                    OutlookModuleFactory.CreateModule(),
                    MFDataModuleFactory.CreateModule(dataSource)
                });

                IMFCore mailFoxDatabase = kernel.Get<IMFCore>();
                mailFoxDatabase.EnsureCreated();
            }

            return kernel;
        }
    }
}