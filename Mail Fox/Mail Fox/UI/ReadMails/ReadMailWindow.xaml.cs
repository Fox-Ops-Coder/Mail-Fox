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

namespace MailFox.UI.ReadMails
{
    /// <summary>
    /// Логика взаимодействия для ReadMailWindow.xaml
    /// </summary>
    public partial class ReadMailWindow : Window
    {
        public ReadMailWindow()
        {
            InitializeComponent();
        }

        private void Drag(object sender, MouseButtonEventArgs e) =>
            DragMove();

    }
}
