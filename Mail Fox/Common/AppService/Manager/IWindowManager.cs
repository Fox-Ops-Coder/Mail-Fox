using Common.AppService.WindowService;
using System;
using System.Windows;

namespace Common.AppService.Manager
{
    public interface IWindowManager
    {
        void CloseWindow(IWindow window, bool? dialogResult = null);

        void ShowMessage(IWindow window, string message);

        bool? ShowDialog(Window window);

        Tuple<bool?, object?> ShowDialogWithResult(Window window);

        void ShowWindow(Window window);

        void HideWindow(IWindow window);

        void ChangeWindowState(IWindow window, bool fullscreen);
    }
}