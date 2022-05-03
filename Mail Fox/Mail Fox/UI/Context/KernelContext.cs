using MailFox.Kernel;
using Ninject;

namespace MailFox.UI.Context
{
    internal class KernelContext
    {
        protected readonly IKernel kernel;

        protected KernelContext() =>
            kernel = AppKernel.GetKernel();
    }
}