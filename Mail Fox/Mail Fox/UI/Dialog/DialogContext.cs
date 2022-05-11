using Common.UICommand;
using MailFox.UI.Context;
using System.Windows.Input;

namespace MailFox.UI.Dialog
{
    internal class DialogContext : AppBarContext
    {
        private readonly ICommand acceptCommand;
        public ICommand AcceptCommand => acceptCommand;

        private readonly ICommand rejectCommand;
        public ICommand RejectCommand => rejectCommand;

        public DialogContext()
        {
            acceptCommand = new Command(obj => windowManager.CloseWindow(this, true));
            rejectCommand = new Command(obj => windowManager.CloseWindow(this, false));
        }
    }
}