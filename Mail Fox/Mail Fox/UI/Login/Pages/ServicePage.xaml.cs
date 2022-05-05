using Common.AppService;
using Common.AppService.WindowService;
using System;
using System.Windows.Controls;

namespace MailFox.UI.Login.Pages
{
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    internal sealed partial class ServicePage : Page
    {
        public ServicePage(IManagable managable, INavigator navigator)
        {
            InitializeComponent();

            DataContext = new ServiceContext(managable, navigator);
        }
    }
}