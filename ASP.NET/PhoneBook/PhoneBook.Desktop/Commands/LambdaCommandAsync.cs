using PhoneBook.Desktop.ViewModels;
using System.Windows.Input;

namespace PhoneBook.Desktop.Commands
{
    public class LambdaCommandAsync : BaseViewModel, ICommand
    {
        private Func<object?, Task> _execute;
        private Func<object?, bool>? _canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }


        public LambdaCommandAsync(Func<object?, Task> execute, Func<object?, bool>? canExecute = default)
        {
            _execute = execute;
            _canExecute = canExecute;
        }


        private bool _isExecuting;
        public bool IsExecuting
        {
            get => _isExecuting;
            set => Set(ref _isExecuting, value);
        }


        public bool CanExecute(object? parameter)
        {
            return !IsExecuting && (_canExecute is null || _canExecute(parameter));
        }

        public async void Execute(object? parameter)
        {
            await ExecuteAsync(parameter);
        }

        public async Task ExecuteAsync(object? parameter)
        {
            IsExecuting = true;
            try
            {
                await _execute(parameter);
            }
            finally
            {
                IsExecuting = false;
            }
        }
    }
}
