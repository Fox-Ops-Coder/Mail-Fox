using MimeKit;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace MailFox.UI.ReadMails.Attachment
{
    /// <summary>
    /// Логика взаимодействия для AttachmentWindow.xaml
    /// </summary>
    public partial class AttachmentWindow : Window
    {
        public AttachmentWindow(IEnumerable<MimeEntity> attachments)
        {
            InitializeComponent();

            DataContext = new AttachmentContext(attachments);
        }

        private void Drag(object sender, MouseButtonEventArgs e) =>
            DragMove();
    }
}