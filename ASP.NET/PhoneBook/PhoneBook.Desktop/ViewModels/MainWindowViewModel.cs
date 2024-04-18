using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Desktop.Commands;
using PhoneBook.Desktop.Services;
using PhoneBook.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneBook.Desktop.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly AccountService _accountService;
        private readonly IServiceProvider _serviceProvider;
        private readonly MessageBoxService _messageBoxService;

        public MainWindowViewModel(AccountService accountService, IServiceProvider serviceProvider, MessageBoxService messageBoxService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _messageBoxService = messageBoxService ?? throw new ArgumentNullException(nameof(messageBoxService));
            _selectedViewModel = _serviceProvider.GetRequiredService<AnonymMainViewModel>();

            LoginCommand = new LambdaCommandAsync(Login, CanLogin);
            LogoutCommand = new LambdaCommandAsync(Logout, CanLogout);
            RegisterCommand = new LambdaCommandAsync(Register, CanRegister);
            UpdateCommand = new LambdaCommandAsync(Update, CanUpdate);
        }


        private BaseViewModel _selectedViewModel;

        public BaseViewModel SelectedViewModel
        {
            get => _selectedViewModel;
            set => Set(ref _selectedViewModel, value);
        }


        private string _userName = string.Empty;

        public string UserName
        {
            get => _userName;
            set => Set(ref _userName, value);
        }


        public ICommand LoginCommand { get; }

        public ICommand LogoutCommand { get; }

        public ICommand RegisterCommand { get; }

        public ICommand UpdateCommand { get; }


        private string? _email;
        [EmailAddress]
        public string? Email
        {
            get => _email;
            set => Set(ref _email, value);
        }


        private bool _commandExecuting = false;
        public bool CommandExecuting
        {
            get => _commandExecuting;
            set => Set(ref _commandExecuting, value);
        }


        private bool CanLogin(object? parameter) =>
            !string.IsNullOrWhiteSpace(Email)
            && parameter is not null
            && parameter is PasswordBox;

        private async Task Login(object? parameter)
        {
            bool result = false;
            PasswordBox passwordBox = (parameter as PasswordBox)!;
            try
            {
                CommandExecuting = true;
                result = await _accountService.Login(Email!, passwordBox);
            } catch (ServerNotResponseException)
            {
                _messageBoxService.ShowError(
                    "Сервер не отвечает",
                    "Статус входа");
                return;
            }
            finally
            {
                passwordBox.Password = string.Empty;
                Email = string.Empty;
                CommandExecuting = false;
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


        private bool CanLogout(object? p) => true;
        private async Task Logout(object? p)
        {
            _accountService.Logout();
            await UpdateWindow();
        }

        private bool CanRegister(object? p) =>
            !string.IsNullOrWhiteSpace(Email)
            && p is not null
            && p is PasswordBox
            ;

        private async Task Register(object? p)
        {
            PasswordBox passwordBox = (p as PasswordBox)!;
            if (passwordBox.Password.Length < 8)
            {
                _messageBoxService.ShowError("Пароль должен быть не менее 8 символов", "Статус регистрации");
                return;
            }

            bool result = false;
            try
            {
                CommandExecuting = true;
                result = await _accountService.Register(Email!, passwordBox);
            } catch (ServerNotResponseException)
            {
                _messageBoxService.ShowError(
                    "Сервер не отвечает",
                    "Статус регистрации");
                return;
            }
            finally
            {
                passwordBox.Password = string.Empty;
                Email = string.Empty;
                CommandExecuting = false;
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


        private async Task UpdateWindow()
        {
            UserName = _accountService.GetUserName();
            switch (_accountService.GetUserRole())
            {
                case UserRoles.Admin:
                    var adminModel = _serviceProvider.GetRequiredService<AdminMainViewModel>();
                    await adminModel.Update();
                    SelectedViewModel = adminModel;
                    break;
                case UserRoles.User:
                    var userModel = _serviceProvider.GetRequiredService<UserMainViewModel>();
                    await userModel.Update();
                    SelectedViewModel = userModel;
                    break;
                default:
                    var anonModel = _serviceProvider.GetRequiredService<AnonymMainViewModel>();
                    await anonModel.Update();
                    SelectedViewModel = anonModel;
                    break;
            }
        }

        private bool CanUpdate(object? p) => true;
        private async Task Update(object? p)
        {
            CommandExecuting = true;
            Email = string.Empty;
            await UpdateWindow();
            CommandExecuting = false;
        }
    }
}
