using System;
using System.Windows;

namespace Common.AppService.Manager
{
    internal sealed class WindowManager : IWindowManager
    {
        private static Window? FoundWindow(IWindow window)
        {
            Window? founded;
            int index;

            WindowCollection windowCollection = Application.Current.Windows;

            for (index = 0, founded = null; index < windowCollection.Count; ++index)
                if (!string.IsNullOrEmpty(windowCollection[index].Uid) &&
                    new Guid(windowCollection[index].Uid) == window.Guid)
                    founded = windowCollection[index];

            return founded;
        }

        public void CloseWindow(IWindow window)
        {
            Window? target = FoundWindow(window);

            if (target != null)
                target.Dispatcher.Invoke(() => target.Close());
        }

        public void ShowMessage(IWindow window, string message)
        {
            Window? target = FoundWindow(window);

            if (target != null)
                target.Dispatcher.Invoke(() => MessageBox.Show(message));
        }

        public bool? ShowDialog(Window window) => window.ShowDialog();

        public void ShowWindow(Window window) => window.Show();
    }
}