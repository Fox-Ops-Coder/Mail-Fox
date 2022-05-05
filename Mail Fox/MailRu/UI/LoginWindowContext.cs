using Common.AppService;
using Common.AppService.Manager;
using Common.UICommand;
using Mailing.Services;
using MailRu.Service;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace MailRu.UI
{
    internal sealed class LoginWindowContext : IWindow, IResult
    {
        private IWindowManager? windowManager;
        public IWindowManager? WindowManager
        { get => windowManager; set => windowManager = value; }

        private string email;
        public string Email
        { get => email; set => email = value; }

        private readonly ICommand loginCommand;
        public ICommand LoginCommand => loginCommand;

        private readonly Guid guid;
        public Guid Guid => guid;

        private IMailService? createdMailService;
        public object? Result => createdMailService;

        public LoginWindowContext()
        {
            guid = Guid.NewGuid();
            email = string.Empty;

            loginCommand = new Command(async obj =>
            {
                if (!string.IsNullOrEmpty(email) && obj is PasswordBox passwordBox)
                {
                    bool result = false;

                    IMailService mailService = new MailRuService();

                    result = await mailService.ConnectAsync();

                    if (result)
                    {
                        result = await mailService.AuthorizeAsync(email, passwordBox.SecurePassword);

                        if (result)
                            createdMailService = mailService;
                        else
                            await mailService.DisconnectAsync();
                    }

                    windowManager.CloseWindow(this, result);
                }
            });
        }
    }
}