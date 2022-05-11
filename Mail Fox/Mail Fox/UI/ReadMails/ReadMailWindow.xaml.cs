using MimeKit;
using System.Windows;
using System.Windows.Input;

namespace MailFox.UI.ReadMails
{
    /// <summary>
    /// Логика взаимодействия для ReadMailWindow.xaml
    /// </summary>
    public partial class ReadMailWindow : Window
    {
        public ReadMailWindow(MimeMessage message)
        {
            InitializeComponent();

            DataContext = new ReadMailContext(message);
        }

        private void Drag(object sender, MouseButtonEventArgs e) =>
            DragMove();
    }
}