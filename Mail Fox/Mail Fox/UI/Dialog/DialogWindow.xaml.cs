using System.Windows;
using System.Windows.Input;

namespace MailFox.UI.Dialog
{
    /// <summary>
    /// Логика взаимодействия для DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        public DialogWindow()
        {
            InitializeComponent();
        }

        private void Drag(object sender, MouseButtonEventArgs e) =>
            DragMove();
    }
}