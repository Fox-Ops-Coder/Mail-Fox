using MailFox.UI.Mails.Adapters;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace MailFox.UI.Mails.Attachment
{
    /// <summary>
    /// Логика взаимодействия для AttachmentWindow.xaml
    /// </summary>
    internal partial class AttachmentWindow : Window
    {
        public AttachmentWindow(ObservableCollection<AttachmentAdapter> attachments)
        {
            InitializeComponent();
            DataContext = new AttachmentContext(attachments);
        }

        private void Drag(object sender, MouseButtonEventArgs e) =>
            DragMove();
    }
}