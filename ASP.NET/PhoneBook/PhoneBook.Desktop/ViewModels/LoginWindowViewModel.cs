using PhoneBook.Desktop.Commands;
using PhoneBook.Desktop.Services;
using PhoneBook.Exceptions;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneBook.Desktop.ViewModels
{
    public class LoginWindowViewModel : BaseViewModel
    {
        private readonly AccountService _accountService;
        private readonly MessageBoxService _messageBoxService;


        public LoginWindowViewModel(AccountService accountService, MessageBoxService messageBoxService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _messageBoxService = messageBoxService ?? throw new ArgumentNullException(nameof(messageBoxService));

            LoginCommand = new LambdaCommandAsync(Login, CanLogin);
        }


        public ICommand LoginCommand { get; }


        private string? _email;
        public string? Email
        {
            get => _email;
            set => Set(ref _email, value);
        }


        private async Task Login(object? parameter)
        {
            bool result = false;
            try
            {
                result = await _accountService.Login(Email!, (parameter as PasswordBox)!);
            } catch (ServerNotResponseException)
            {
                _messageBoxService.ShowError(
                    "Сервер не отвечает",
                    "Статус входа");
                return;
            }

            if (result)
            {
                _messageBoxService.ShowInfo(
                    "Вход успешно выполнен, можете закрыть окно",
                    "Статус входа");
            } else
            {
                _messageBoxService.ShowError(
                    "Неверный логин пользователя или пароль",
                    "Статус входа");
            }
        }

        private bool CanLogin(object? parameter) =>
            !string.IsNullOrWhiteSpace(Email)
            && parameter is not null
            && parameter is PasswordBox;
    }
}
