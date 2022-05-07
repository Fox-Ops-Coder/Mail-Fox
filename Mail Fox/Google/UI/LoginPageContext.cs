using Common.AppService.WindowService;
using Common.UICommand;
using Google.Service;
using Mailing.Services;
using System.Windows.Controls;
using System.Windows.Input;

namespace Google.UI
{
    internal sealed class LoginPageContext
    {
        private string email;

        public string Email
        { get => email; set => email = value; }

        private readonly ICommand loginCommand;
        public ICommand LoginCommand => loginCommand;

        public LoginPageContext(IManagable managable, INavigator navigator, string service)
        {
            email = string.Empty;

            loginCommand = new Command(async obj =>
            {
                if (!string.IsNullOrEmpty(email) && obj is PasswordBox passwordBox)
                {
                    bool result = false;

                    IMailService mailService = new GoogleService(service);

                    result = await mailService.ConnectAsync();

                    if (result)
                    {
                        result = await mailService.AuthorizeAsync(email, passwordBox.SecurePassword);

                        if (result)
                            managable.CloseWithArg(result, mailService);
                        else
                            await mailService.DisconnectAsync();
                    }
                }
            });
        }
    }
}