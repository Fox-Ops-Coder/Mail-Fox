using Common.AppService.Manager;
using Mailing.Services;
using MailRu.UI;
using System;

namespace MailRu.ServiceBuilder
{
    internal class MailRuServiceBuilder : IMailServiceBuilder
    {
        private const string serviceName = "Mail.Ru";
        public string ServiceName => serviceName;

        public IMailService? CreateMailService(IWindowManager windowManager)
        {
            LoginWindow loginWindow = new();

            if (loginWindow.DataContext is LoginWindowContext context)
                context.WindowManager = windowManager;

            Tuple<bool?, object?> results = windowManager.ShowDialogWithResult(loginWindow);

            if (results.Item1.GetValueOrDefault())
                return results.Item2 as IMailService;
            else
                return null;
        }
    }
}