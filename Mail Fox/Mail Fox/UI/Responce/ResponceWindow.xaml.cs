using System.Windows;
using System.Windows.Input;

namespace MailFox.UI.Responce
{
    /// <summary>
    /// Логика взаимодействия для ResponceWindow.xaml
    /// </summary>
    internal sealed partial class ResponceWindow : Window
    {
        public ResponceWindow(bool fill = false)
        {
            InitializeComponent();

            DataContext = new ResponceContext(fill);
        }

        private void Drag(object sender, MouseButtonEventArgs e) =>
            DragMove();
    }
}