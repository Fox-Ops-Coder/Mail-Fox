using Common.AppService.WindowService;
using System.Windows.Controls;

namespace Outlook.UI
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
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