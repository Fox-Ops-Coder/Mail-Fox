using MFData.Entities;
using System.Windows;
using System.Windows.Input;

namespace MailFox.UI.Responce.Add
{
    /// <summary>
    /// Логика взаимодействия для AddResponce.xaml
    /// </summary>
    public partial class AddResponce : Window
    {
        public AddResponce(bool isCreate = true, Blank? blank = null)
        {
            InitializeComponent();

            DataContext = new AddResponceContext(isCreate, blank);
        }

        private void Drag(object sender, MouseButtonEventArgs e) =>
            DragMove();
    }
}