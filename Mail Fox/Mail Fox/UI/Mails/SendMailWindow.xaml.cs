using System.Windows;
using System.Windows.Input;

namespace MailFox.UI.Mails
{
    /// <summary>
    /// Логика взаимодействия для SendMailWindow.xaml
    /// </summary>
    internal sealed partial class SendMailWindow : Window
    {
        public SendMailWindow()
        {
            InitializeComponent();
        }

        private void Drag(object sender, MouseButtonEventArgs e) =>
            DragMove();
    }
}