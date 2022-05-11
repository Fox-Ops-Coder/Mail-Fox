using MFData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
