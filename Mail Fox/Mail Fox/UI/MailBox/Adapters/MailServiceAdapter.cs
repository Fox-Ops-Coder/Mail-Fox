using Common.UICommand;
using MailKit;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using IMailService = Mailing.Services.IMailService;

namespace MailFox.UI.MailBox.Adapters
{
    internal sealed class MailServiceAdapter : INotifyPropertyChanged
    {
        private readonly IMailService mailService;
        public IMailService MailService => mailService;

        private readonly ICommand logoutCommand;
        public ICommand LogoutCommand => logoutCommand;

        private readonly ICommand openFolders;
        public ICommand OpenFoldersCommand => openFolders;

        private BitmapImage folderImage;
        public ImageSource FolderImage => folderImage;

        private readonly ObservableCollection<MailFolderAdapter> folders;
        public ObservableCollection<MailFolderAdapter> Folders => folders;

        private readonly ObservableCollection<MessageAdapter> messagesCollection;
        private readonly ICommand openMessageCommand;

        private MailFolderAdapter selectedFolder;
        public MailFolderAdapter SelectedFolder
        {
            get => selectedFolder;
            set
            {
                selectedFolder = value;
                LoadMessages();
            }
        }

        private bool foldersVisible;
        public Visibility FoldersVisible =>
            foldersVisible ? Visibility.Visible : Visibility.Collapsed;

        private async void LoadMessages()
        {
            messagesCollection.Clear();

            IEnumerable<IMessageSummary>? messages = await mailService.GetMessagesAsync(selectedFolder.Folder);

            if (messages != null)
            {
                foreach(IMessageSummary summary in messages)
                    messagesCollection.Add(new(summary, mailService, selectedFolder.Folder, openMessageCommand));
            }
        }

        private async void GetFolders()
        {
            IEnumerable<IEnumerable<IMailFolder>> userFolders =
                await MailService.GetFoldersAsync();

            foreach (IEnumerable<IMailFolder> folder in userFolders)
                foreach(IMailFolder f in folder)
                    folders.Add(new(f));
        }

        public MailServiceAdapter(IMailService mailService, ICommand openMessageCommand,
            ICommand logoutCommand, ObservableCollection<MessageAdapter> messagesCollection)
        {
            this.messagesCollection = messagesCollection;
            this.openMessageCommand = openMessageCommand;
            this.mailService = mailService;
            this.logoutCommand = logoutCommand;

            folders = new();

            foldersVisible = false;

            folderImage = new(new("pack://application:,,,/Resources/arrow-down-sign-to-navigate.png"));

            openFolders = new Command(obj =>
            {
                foldersVisible = !foldersVisible;

                if (foldersVisible)
                    folderImage = new(new("pack://application:,,,/Resources/navigate-up-arrow.png"));
                else
                    folderImage = new(new("pack://application:,,,/Resources/arrow-down-sign-to-navigate.png"));

                OnPropertyChanged("FoldersVisible");
                OnPropertyChanged("FolderImage");
            });

            GetFolders();
        }

        public override string? ToString() =>
            mailService.Email;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}