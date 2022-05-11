using MimeKit;
using System;
using System.Collections.Generic;
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
