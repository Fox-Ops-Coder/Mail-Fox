using System.Windows;
using System.Windows.Input;

namespace MailFox.UI.MailBox
{
    /// <summary>
    /// Логика взаимодействия для MailBoxWindow.xaml
    /// </summary>
    internal sealed partial class MailBoxWindow : Window
    {
        public MailBoxWindow()
        {
            InitializeComponent();
        }

        private void Drag(object sender, MouseButtonEventArgs e) =>
            DragMove();
    }
}