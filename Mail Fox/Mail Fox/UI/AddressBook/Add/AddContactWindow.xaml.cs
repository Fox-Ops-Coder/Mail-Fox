using MFData.Entities;
using System.Windows;
using System.Windows.Input;

namespace MailFox.UI.AddressBook.Add
{
    /// <summary>
    /// Логика взаимодействия для AddContactWindow.xaml
    /// </summary>
    internal sealed partial class AddContactWindow : Window
    {
        public AddContactWindow(bool isCreate = true, Contact? contact = null)
        {
            InitializeComponent();

            DataContext = new AddContactContext(isCreate, contact);
        }

        public AddContactWindow(string email)
        {
            InitializeComponent();

            AddContactContext context = new(true, null);
            context.EmailAddress = email;

            DataContext = context;
        }

        private void Drag(object sender, MouseButtonEventArgs e) =>
            DragMove();
    }
}