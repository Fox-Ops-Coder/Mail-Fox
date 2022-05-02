using Common.AppService.Manager;
using Common.UICommand;
using MailFox.Kernel;
using MailFox.UI.Context;
using MFData.Core;
using MFData.Entities;
using Ninject;
using System.Windows.Input;

namespace MailFox.UI.Responce
{
    internal sealed class ResponceContext : ContextBase
    {
        private readonly ICommand saveCommand;
        public ICommand SaveCommand => saveCommand;

        private string responceText;
        public string ResponceText 
        {
            get => responceText;
            set => responceText = value;
        }

        public ResponceContext()
        {
            responceText = string.Empty;

            IKernel kernel = AppKernel.GetKernel();

            IMFCore mailFoxDatabase = kernel.Get<IMFCore>();
            IWindowManager windowManager = kernel.Get<IWindowManager>();

            saveCommand = new Command(async obj =>
            {
                switch (string.IsNullOrEmpty(responceText))
                {
                    case true:
                        windowManager.ShowMessage(this, "Шаблон сообщения не можут быть пустым");
                        break;

                    case false:
                        Blank newBlank = new() { BlankText = responceText };
                        await mailFoxDatabase.AddBlankAsync(newBlank);
                        windowManager.ShowMessage(this, "Заготовка сообщения сохранена");
                        windowManager.CloseWindow(this);
                        break;
                }
            });
        }
    }
}