using Common.AppService.Manager;
using Mailing.Services;
using MailRu.UI;

namespace MailRu.ServiceBuilder
{
    internal class MailRuServiceBuilder : IMailServiceBuilder
    {
        private const string serviceName = "Mail.Ru";
        public string ServiceName => serviceName;

        public IMailService CreateMailService(IWindowManager windowManager)
        {
            LoginWindow loginWindow = new();

            if (loginWindow.DataContext is LoginWindowContext context)
                context.WindowManager = windowManager;

            object?[] results = windowManager.ShowDialogWithResult(loginWindow);

            if (results[0] is bool result && result && results[1] is IMailService mailService)
                return mailService;
            else
                return null;
        }
    }
}