using Common.AppService.Manager;
using Common.UICommand;
using MailFox.UI.Context;
using MailFox.UI.Responce.Add;
using MFData.Core;
using MFData.Entities;
using Ninject;
using System.Windows.Input;

namespace MailFox.UI.Responce
{
    internal sealed class ResponceContext : AppBarContext
    {
        private readonly ICommand addTemplateCommand;
        public ICommand AddTemplateCommand => addTemplateCommand;

        public ResponceContext()
        {
            addTemplateCommand = new Command(obj =>
            windowManager.ShowDialog(new AddResponce()));
        }
    }
}