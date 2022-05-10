using MailKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailFox.UI.MailBox.Adapters
{
    internal class MailFolderAdapter
    {
        private readonly IMailFolder folder;
        public IMailFolder Folder => folder;

        public string FolderName => folder.Name;

        public MailFolderAdapter(IMailFolder folder) =>
            this.folder = folder;
    }
}
