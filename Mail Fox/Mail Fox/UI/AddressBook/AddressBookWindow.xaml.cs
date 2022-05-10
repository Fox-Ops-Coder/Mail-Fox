using System.Windows;
using System.Windows.Input;

namespace MailFox.UI.AddressBook
{
    /// <summary>
    /// Логика взаимодействия для AddressBookWindow.xaml
    /// </summary>
    internal sealed partial class AddressBookWindow : Window
    {
        public AddressBookWindow()
        {
            InitializeComponent();
        }

        private void Drag(object sender, MouseButtonEventArgs e) =>
            DragMove();
    }
}