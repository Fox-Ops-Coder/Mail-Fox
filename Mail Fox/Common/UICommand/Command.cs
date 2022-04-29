using System;
using System.Windows.Input;

namespace Common.UICommand
{
    public sealed class Command : ICommand
    {
        private readonly Action<object?> performAction;
        private readonly Func<object?, bool>? isExecutable;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public Command(Action<object?> performAction, Func<object?, bool>? isExecutable = null)
        {
            this.performAction = performAction;
            this.isExecutable = isExecutable;
        }

        public bool CanExecute(object? param) => isExecutable == null || isExecutable(param);

        public void Execute(object? param) => performAction(param);
    }
}