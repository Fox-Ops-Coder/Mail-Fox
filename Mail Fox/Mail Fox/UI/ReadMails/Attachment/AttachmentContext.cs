using Common.UICommand;
using MailFox.UI.Context;
using MailFox.UI.ReadMails.Adapters;
using Microsoft.Win32;
using MimeKit;
using System.Collections.Generic;
using System.Windows.Input;

namespace MailFox.UI.ReadMails.Attachment
{
    internal class AttachmentContext : AppBarContext
    {
        private List<AttachmentAdapter> attachments;
        public IEnumerable<AttachmentAdapter> Attachments => attachments;

        public AttachmentContext(IEnumerable<MimeEntity> attachments)
        {
            ICommand dowloadCommand = new Command(obj =>
            {
                if (obj is AttachmentAdapter attachmentAdapter)
                {
                    SaveFileDialog fileDialog = new();

                    if (fileDialog.ShowDialog() == true)
                    {
                        attachmentAdapter.Attachment.Content.DecodeTo(fileDialog.OpenFile());
                    }
                }
            });

            this.attachments = new List<AttachmentAdapter>();

            foreach (MimeEntity attachment in attachments)
            {
                if (attachment is MimePart file)
                    this.attachments.Add(new(file, dowloadCommand));
            }
        }
    }
}