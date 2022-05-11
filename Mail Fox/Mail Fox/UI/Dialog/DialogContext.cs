using Common.AppService.Manager;
using Common.UICommand;
using MailFox.UI.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
