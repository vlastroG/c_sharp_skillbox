using System.Windows.Input;

namespace vlastroG.WPF.Commands {
    public abstract class BaseCommand : ICommand {
        protected BaseCommand() { }


        public event EventHandler? CanExecuteChanged {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Определяет, можно ли выполнить команду
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public abstract bool CanExecute(object? parameter);

        /// <summary>
        /// Выполняет действие команды
        /// </summary>
        /// <param name="parameter"></param>
        public abstract void Execute(object? parameter);
    }
}
