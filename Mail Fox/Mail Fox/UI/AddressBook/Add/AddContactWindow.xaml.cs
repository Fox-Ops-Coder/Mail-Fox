using System.Windows;
using System.Windows.Input;

namespace MailFox.UI.AddressBook.Add
{
    /// <summary>
    /// Логика взаимодействия для AddContactWindow.xaml
    /// </summary>
    internal sealed partial class AddContactWindow : Window
    {
        public AddContactWindow()
        {
            InitializeComponent();
        }

        private void Drag(object sender, MouseButtonEventArgs e) =>
            DragMove();

    }
}