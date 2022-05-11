using MFData.Entities;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace MailFox.UI.Responce.Adapters
{
    internal class ResponceAdapter : INotifyPropertyChanged
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

        public void TextChanged() =>
            OnPropertyChanged("Text");

        public ResponceAdapter(Blank responce, bool canFill,
            ICommand fillCommand, ICommand editCommand, ICommand deleteCommand)
        {
            this.responce = responce;
            this.fillCommand = fillCommand;
            this.editCommand = editCommand;
            this.deleteCommand = deleteCommand;

            fillVisibility = canFill ? Visibility.Visible : Visibility.Hidden;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}