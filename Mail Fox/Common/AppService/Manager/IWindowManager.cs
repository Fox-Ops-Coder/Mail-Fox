namespace Common.AppService.Manager
{
    public interface IWindowManager
    {
        void CloseWindow(IWindow window);

        void ShowMessage(IWindow window, string message);
    }
}