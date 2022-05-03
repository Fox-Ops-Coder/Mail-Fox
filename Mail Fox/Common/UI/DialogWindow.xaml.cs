using Common.AppService.Manager;
using System.Windows;

namespace Common.UI
{
    /// <summary>
    /// Логика взаимодействия для DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        public DialogWindow(string question, IWindowManager windowManager)
        {
            InitializeComponent();

            DataContext = new DialogContext(question, windowManager);
        }
    }
}