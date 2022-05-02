using System;
using System.Windows;

namespace Common.AppService.Manager
{
    internal sealed class WindowManager : IWindowManager
    {
        public void CloseWindow(IWindow window)
        {
            bool closed;
            int index;

            WindowCollection windowCollection = Application.Current.Windows;

            for (index = 0, closed = false; index < windowCollection.Count && !closed; ++index)
            {
                if (new Guid(windowCollection[index].Uid) == window.Guid)
                {
                    windowCollection[index].Dispatcher.Invoke(() => windowCollection[index].Close());
                    closed = true;
                }
            }
        }
    }
}