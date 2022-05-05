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

        public void CloseWindow(IWindow window, bool? dialogResult = null)
        {
            Window? target = FoundWindow(window);

            target?.Dispatcher.Invoke(() =>
            {
                target.DialogResult = dialogResult;
                target.Close();
            });
        }

        public void ShowMessage(IWindow window, string message)
        {
            Window? target = FoundWindow(window);

            target?.Dispatcher.Invoke(() => MessageBox.Show(message));
        }

        public bool? ShowDialog(Window window) => window.ShowDialog();

        public Tuple<bool?, object?> ShowDialogWithResult(Window window)
        {
            bool? result = window.ShowDialog();
            object? value = null;

            if (window.DataContext is IResult context)
                value = context.Result;

            return new(result, value);
        }

        public void ShowWindow(Window window) => window.Show();

        public void HideWindow(IWindow window)
        {
            Window? target = FoundWindow(window);

            target?.Dispatcher.Invoke(() => target?.Hide());
        }

        public void ShowWindow(IWindow window)
        {
            Window? target = FoundWindow(window);

            target?.Dispatcher.Invoke(() => target?.Show());
        }
    }
}