using System;
using System.Windows.Controls;

namespace MailFox.UI.Login.Pages
{
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    internal sealed partial class ServicePage : Page
    {
        public ServicePage(Guid guid)
        {
            InitializeComponent();

            DataContext = new ServiceContext(guid);
        }
    }
}