using System.Windows;
using System.Windows.Input;

namespace MailFox.UI.Responce
{
    /// <summary>
    /// Логика взаимодействия для ResponceWindow.xaml
    /// </summary>
    internal sealed partial class ResponceWindow : Window
    {
        public ResponceWindow()
        {
            InitializeComponent();
        }

        private void Drag(object sender, MouseButtonEventArgs e) =>
            DragMove();
    }
}