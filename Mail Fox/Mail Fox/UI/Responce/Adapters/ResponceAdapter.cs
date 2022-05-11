using MFData.Core;
using MFData.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MailFox.UI.Responce.Adapters
{
    internal class ResponceAdapter
    {
        private readonly Blank responce;
        public Blank Responce => responce;

        public string Text => responce.BlankText;

        private readonly ICommand fillCommand;
        public ICommand FillCommand => fillCommand;

        private readonly ICommand editCommand;
        public ICommand EditCommand => editCommand;

        private readonly ICommand deleteCommand;
        public ICommand DeleteCommand => deleteCommand;

        private readonly Visibility fillVisibility;
        public Visibility FillVisibility => fillVisibility;

        public ResponceAdapter(Blank responce, bool canFill,
            ICommand fillCommand, ICommand editCommand, ICommand deleteCommand)
        {
            this.responce = responce;
            this.fillCommand = fillCommand;
            this.editCommand = editCommand;
            this.deleteCommand = deleteCommand;

            fillVisibility = canFill ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
