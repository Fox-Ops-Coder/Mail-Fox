using MailKit;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using IMailService = Mailing.Services.IMailService;

namespace MailFox.UI.MailBox.Adapters
{
    internal sealed class MessageAdapter : INotifyPropertyChanged
    {
        private readonly IMessageSummary message;
        public IMessageSummary Message => message;

        private readonly IMailFolder folder;
        public IMailFolder Folder => folder;

        private readonly IMailService service;
        public IMailService Service => service;

        private readonly ICommand openMessageCommand;
        public ICommand OpenMessageCommand => openMessageCommand;

        private readonly ICommand deleteCommand;
        public ICommand DeleteCommand => deleteCommand;

        private BitmapImage readedIcon;
        public ImageSource ReadedIcon => readedIcon;

        private readonly bool withAttachment;

        public Visibility WithAttachment =>
            withAttachment ? Visibility.Visible : Visibility.Hidden;

        public string Summary =>
            message.Envelope.Sender.ToString() +
            " " + message.Envelope.Subject;

        public void Read()
        {
            readedIcon = new(new("pack://application:,,,/Resources/readed.png"));
            OnPropertyChanged("ReadedIcon");
        }

        public MessageAdapter(IMessageSummary message, IMailService service,
            IMailFolder folder, ICommand openMessageCommand, ICommand deleteCommand)
        {
            this.message = message;
            this.folder = folder;
            this.service = service;
            this.openMessageCommand = openMessageCommand;
            this.deleteCommand = deleteCommand;

            if (message.Flags.HasValue && message.Flags.Value == MessageFlags.Seen)
                readedIcon = new(new("pack://application:,,,/Resources/readed.png"));
            else
                readedIcon = new(new("pack://application:,,,/Resources/unread.png"));

            withAttachment = message.Attachments.Any();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}