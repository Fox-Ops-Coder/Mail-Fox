using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MailFox.UI.Mails.Adapters
{
    internal class AttachmentAdapter
    {
        private readonly string attachment;
        public string Attachment => attachment;

        private readonly ICommand removeCommand;
        public ICommand RemoveCommand => removeCommand;

        public AttachmentAdapter(string attachment, ICommand removeCommand)
        {
            this.attachment = attachment;
            this.removeCommand = removeCommand;
        }
    }
}
