using MailFox.UI.Mails.Adapters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
