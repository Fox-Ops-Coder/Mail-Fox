using System.Windows;

namespace Common.AppService.Manager
{
    public interface IWindowManager
    {
        void CloseWindow(IWindow window);

        void ShowMessage(IWindow window, string message);

        bool? ShowDialog(Window window);

        void ShowWindow(Window window);
    }
}