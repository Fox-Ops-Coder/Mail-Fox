using MailKit;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MailFox.UI.MailBox.Adapters
{
    internal sealed class MessageAdapter
    {
        private readonly IMessageSummary message;
        public IMessageSummary Message => message;

        private BitmapImage readedIcon;
        public ImageSource ReadedIcon => readedIcon;

        private readonly bool withAttachment;
        public Visibility WithAttachment => 
            withAttachment ? Visibility.Visible : Visibility.Hidden;

        public string Summary =>
            message.Envelope.Sender.ToString() +
            " " + message.Envelope.Subject;

        public MessageAdapter(IMessageSummary message)
        {
            this.message = message;

            if (message.Flags.HasValue && message.Flags.Value == MessageFlags.Seen)
                readedIcon = new(new("pack://application:,,,/Resources/readed.png"));
            else
                readedIcon = new(new("pack://application:,,,/Resources/unread.png"));

            withAttachment = message.Attachments.Any();
        }
    }
}
