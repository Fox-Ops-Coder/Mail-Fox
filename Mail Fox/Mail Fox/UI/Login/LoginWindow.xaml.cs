using System.Windows;
using System.Windows.Input;

namespace MailFox.UI.Login
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    internal sealed partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginContext(loginFrame.NavigationService);
        }

        private void Drag(object sender, MouseButtonEventArgs e) =>
            DragMove();
    }
}