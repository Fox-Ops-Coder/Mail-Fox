using Common.AppService.WindowService;
using Common.UICommand;
using MailFox.UI.Context;
using MailFox.UI.Responce.Adapters;
using MailFox.UI.Responce.Add;
using MFData.Core;
using MFData.Entities;
using Ninject;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MailFox.UI.Responce
{
    internal sealed class ResponceContext : AppBarContext, IResult
    {
        private object? result;
        public object? Result => result;

        private readonly ICommand addTemplateCommand;
        public ICommand AddTemplateCommand => addTemplateCommand;

        private readonly ObservableCollection<ResponceAdapter> responces;
        public ObservableCollection<ResponceAdapter> Responces => responces;

        private readonly ICommand fillTemplate;
        private readonly ICommand removeTemplateCommand;
        private readonly ICommand editTemplateCommand;

        public async void LoadResponces(IMFCore mailFoxDatabase, bool canFill)
        {
            IEnumerable<Blank> blanks = await mailFoxDatabase.GetBlankAsync();

            foreach (Blank blan in blanks)
                responces.Add(new(blan, canFill, fillTemplate,
                    editTemplateCommand, removeTemplateCommand));
        }

        public ResponceContext(bool canFill)
        {
            addTemplateCommand = new Command(obj =>
            windowManager.ShowDialog(new AddResponce()));

            IMFCore mailFoxDatabase = kernel.Get<IMFCore>();

            responces = new();

            fillTemplate = new Command(obj =>
            {
                if (obj is ResponceAdapter responce)
                {
                    result = responce.Responce;
                    windowManager.CloseWindow(this, true);
                }
            });

            editTemplateCommand = new Command(obj =>
            {
                if (obj is ResponceAdapter responce)
                {
                    windowManager.ShowDialog(new AddResponce(false, responce.Responce));
                    responce.TextChanged();
                }
            });

            removeTemplateCommand = new Command(async obj =>
            {
                if (obj is ResponceAdapter responce)
                {
                    await mailFoxDatabase.RemoveBlankAsync(responce.Responce);
                    responces.Remove(responce);
                }
            });

            LoadResponces(mailFoxDatabase, canFill);
        }
    }
}