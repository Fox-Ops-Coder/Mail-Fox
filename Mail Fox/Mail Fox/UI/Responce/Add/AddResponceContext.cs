using Common.UICommand;
using MailFox.UI.Context;
using MFData.Core;
using MFData.Entities;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MailFox.UI.Responce.Add
{
    internal class AddResponceContext : AppBarContext
    {
        private readonly ICommand saveCommand;
        public ICommand SaveCommand => saveCommand;

        private string responceText;

        public string ResponceText
        {
            get => responceText;
            set => responceText = value;
        }

        public AddResponceContext()
        {
            responceText = string.Empty;

            IMFCore mailFoxDatabase = kernel.Get<IMFCore>();

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