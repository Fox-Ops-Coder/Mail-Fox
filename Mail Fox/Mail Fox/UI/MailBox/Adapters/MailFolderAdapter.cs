using MailKit;

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