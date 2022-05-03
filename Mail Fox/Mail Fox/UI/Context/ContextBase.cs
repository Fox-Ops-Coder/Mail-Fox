using Common.AppService;
using System;

namespace MailFox.UI.Context
{
    internal class ContextBase : KernelContext, IWindow
    {
        private readonly Guid guid;
        public Guid Guid => guid;

        protected ContextBase() =>
            guid = Guid.NewGuid();
    }
}