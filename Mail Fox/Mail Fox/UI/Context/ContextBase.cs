using Common.AppService.WindowService;
using System;

namespace MailFox.UI.Context
{
    internal class ContextBase : KernelContext, IWindow
    {
        private readonly Guid guid;
        public Guid Guid => guid;

        public ContextBase() =>
            guid = Guid.NewGuid();
    }
}