using Common.AppService.Manager;
using Common.UICommand;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MailFox.UI.Context
{
    internal class AppBarContext : ContextBase, INotifyPropertyChanged
    {
        protected IWindowManager windowManager;

        private readonly ICommand hideCommand;
        public ICommand HideCommand => hideCommand;

        private readonly ICommand fullscreenCommand;
        public ICommand FullscreenCommand => fullscreenCommand;

        protected ICommand closeCommand;
        public ICommand CloseCommand => closeCommand;

        private BitmapImage fullscreenIcon;
        public ImageSource FullScreenIcon => fullscreenIcon;

        private WindowState windowState;
        public WindowState WindowState => windowState;

        protected AppBarContext()
        {
            windowManager = kernel.Get<IWindowManager>();

            hideCommand = new Command(obj => windowManager.HideWindow(this));
            fullscreenCommand = new Command(obj =>
            {
                switch (windowState)
                {
                    case WindowState.Normal:
                        fullscreenIcon = new(new("pack://application:,,,/Resources/minimize.png"));
                        windowState = WindowState.Maximized;
                        break;

                    case WindowState.Maximized:
                        fullscreenIcon = new(new("pack://application:,,,/Resources/maximize.png"));
                        windowState = WindowState.Normal;
                        break;
                }

                OnPropertyChanged("WindowState");
                OnPropertyChanged("FullScreenIcon");
            });
            closeCommand = new Command(obj => windowManager.CloseWindow(this));

            fullscreenIcon = new(new("pack://application:,,,/Resources/maximize.png"));
            windowState = WindowState.Normal;

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}