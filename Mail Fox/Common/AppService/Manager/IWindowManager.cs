using System.Windows;

namespace Common.AppService.Manager
{
    public interface IWindowManager
    {
        void CloseWindow(IWindow window, bool? dialogResult = null);

        void ShowMessage(IWindow window, string message);

        bool? ShowDialog(Window window);

        object?[] ShowDialogWithResult(Window window);

        void ShowWindow(Window window);
    }
}