using Common.UICommand;
using MailFox.UI.Context;
using MFData.Core;
using MFData.Entities;
using Ninject;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MailFox.UI.Responce.Add
{
    internal class AddResponceContext : AppBarContext
    {
        private readonly ICommand saveCommand;
        public ICommand SaveCommand => saveCommand;

        private readonly string windowTitle;
        public string WindowTitle => windowTitle;

        private string responceText;

        public string ResponceText
        {
            get => responceText;
            set => responceText = value;
        }

        private async Task Operation(bool isCreate, Blank? blank)
        {
            IMFCore mailFoxDatabase = kernel.Get<IMFCore>();

            switch (isCreate)
            {
                case true:
                    Blank newBlank = new() { BlankText = responceText };
                    await mailFoxDatabase.AddBlankAsync(newBlank);
                    break;

                case false:
                    if (blank != null)
                    {
                        blank.BlankText = responceText;
                        await mailFoxDatabase.UpdateBlankAsync(blank);
                    }
                    break;
            }
        }

        public AddResponceContext(bool isCreate, Blank? blank)
        {
            if (isCreate)
                windowTitle = "Новый шаблон";
            else
                windowTitle = "Изменить шаблон";

            responceText = blank == null ? string.Empty : blank.BlankText;

            saveCommand = new Command(async obj =>
            {
                switch (string.IsNullOrEmpty(responceText))
                {
                    case true:
                        windowManager.ShowMessage(this, "Шаблон сообщения не можут быть пустым");
                        break;

                    case false:
                        await Operation(isCreate, blank);
                        windowManager.ShowMessage(this, "Шаблон сообщения сохранён");
                        windowManager.CloseWindow(this, true);
                        break;
                }
            });
        }
    }
}