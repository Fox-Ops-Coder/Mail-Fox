using Common.AppService.WindowService;
using System.Windows.Controls;

namespace MailRu.UI
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    internal sealed partial class LoginPage : Page
    {
        public LoginPage(IManagable managable, INavigator navigator, string service)
        {
            InitializeComponent();

            DataContext = new LoginPageContext(managable, navigator, service);
        }
    }
}