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

            DataContext = new AddContactContext();
        }

        public AddContactWindow(string email)
        {
            InitializeComponent();

            AddContactContext context = new();
            context.EmailAddress = email;

            DataContext = context;
        }

        private void Drag(object sender, MouseButtonEventArgs e) =>
            DragMove();

    }
}