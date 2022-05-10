using Common.UICommand;
using MailFox.UI.Context;
using MailFox.UI.Mails.Adapters;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MailFox.UI.Mails.Attachment
{
    internal class AttachmentContext : AppBarContext
    {
        private readonly ObservableCollection<AttachmentAdapter> attachments;
        public ObservableCollection<AttachmentAdapter> Attachments => attachments;

        private readonly ICommand addCommand;
        public ICommand AddCommand => addCommand;

        private readonly ICommand removeCommand;
        public ICommand RemoveCommand => removeCommand;

        public AttachmentContext(ObservableCollection<AttachmentAdapter> attachments)
        {
            this.attachments = attachments;

            removeCommand = new Command(obj =>
            {
                if (obj is AttachmentAdapter attachment)
                    attachments.Remove(attachment);
            });

            addCommand = new Command(obj =>
            {
                OpenFileDialog fileDialog = new()
                {
                    Multiselect = true,
                };

                if (fileDialog.ShowDialog() == true)
                {
                    foreach(string files in fileDialog.FileNames)
                        attachments.Add(new AttachmentAdapter(files, removeCommand));
                }
            });
        }
    }
}
