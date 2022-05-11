using MimeKit;
using System.Windows.Input;

namespace MailFox.UI.ReadMails.Adapters
{
    internal class AttachmentAdapter
    {
        private readonly MimePart attachment;
        public MimePart Attachment => attachment;

        private readonly string name;
        public string Name => name;

        private readonly ICommand dowloadCommand;
        public ICommand DowloadCommand => dowloadCommand;

        public AttachmentAdapter(MimePart attachment, ICommand dowloadCommand)
        {
            this.attachment = attachment;
            this.dowloadCommand = dowloadCommand;

            name = attachment.FileName;
        }
    }
}