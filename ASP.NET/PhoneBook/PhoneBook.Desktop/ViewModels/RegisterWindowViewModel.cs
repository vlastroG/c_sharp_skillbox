using PhoneBook.Desktop.Commands;
using PhoneBook.Desktop.Services;
using PhoneBook.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneBook.Desktop.ViewModels
{
    public class RegisterWindowViewModel : BaseViewModel
    {
        private readonly AccountService _accountService;
        private readonly MessageBoxService _messageBoxService;


        public RegisterWindowViewModel(AccountService accountService, MessageBoxService messageBoxService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _messageBoxService = messageBoxService ?? throw new ArgumentNullException(nameof(messageBoxService));

            RegisterCommand = new LambdaCommandAsync(Register, CanRegister);
        }


        public ICommand RegisterCommand { get; }


        private string? _email;
        [EmailAddress]
        public string? Email
        {
            get => _email;
            set => Set(ref _email, value);
        }


        private async Task Register(object? parameter)
        {
            if ((parameter as PasswordBox)!.Password.Length < 8)
            {
                _messageBoxService.ShowError("Пароль должен быть не менее 8 символов", "Статус регистрации");
                return;
            }
            bool result = false;
            try
            {
                result = await _accountService.Register(Email!, (parameter as PasswordBox)!);
            } catch (ServerNotResponseException)
            {
                _messageBoxService.ShowError(
                    "Сервер не отвечает",
                    "Статус регистрации");
                return;
            }

            if (result)
            {
                _messageBoxService.ShowInfo(
                    $"Пользователь {Email} успешно зарегистрирован, можете закрыть окно",
                    "Статус регистрации");
            } else
            {
                _messageBoxService.ShowError(
                    $"Не удалось зарегистрировать пользователя {Email}",
                    "Статус регистрации");
            }
        }

        private bool CanRegister(object? parameter) =>
            !string.IsNullOrWhiteSpace(Email)
            && parameter is not null
            && parameter is PasswordBox
            ;
    }
}
