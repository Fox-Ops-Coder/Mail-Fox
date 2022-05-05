using Common.AppService.Manager;
using Common.AppService.WindowService;
using Common.UICommand;
using System;
using System.Windows.Input;

namespace Common.UI
{
    internal sealed class DialogContext : IWindow
    {
        private readonly Guid guid;
        public Guid Guid => guid;

        private readonly string question;
        public string Question => question;

        private readonly ICommand acceptCommand;
        public ICommand AcceptCommand => acceptCommand;

        private readonly ICommand rejectCommand;
        public ICommand RejectCommand => rejectCommand;

        public DialogContext(string question, IWindowManager windowManager)
        {
            guid = Guid.NewGuid();

            this.question = question;

            acceptCommand = new Command(obj => windowManager.CloseWindow(this, true));
            rejectCommand = new Command(obj => windowManager.CloseWindow(this, false));
        }
    }
}