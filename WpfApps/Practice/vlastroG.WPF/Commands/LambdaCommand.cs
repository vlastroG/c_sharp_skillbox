namespace vlastroG.WPF.Commands {
    public class LambdaCommand : BaseCommand {
        private readonly Action<object> _execute;
        private readonly Func<object, bool>? _canExecute;

        /// <summary>
        /// Создает класс команды
        /// </summary>
        /// <param name="execute">Действие команды</param>
        /// <param name="canExecute">Проверка возможности запуска команды</param>
        /// <exception cref="ArgumentNullException"></exception>
        public LambdaCommand(Action<object> execute, Func<object, bool>? canExecute = null) : base() {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }


        public override bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter!) ?? true;

        public override void Execute(object? parameter) {
            if (!CanExecute(parameter))
                return;
            _execute(parameter!);
        }
    }
}
