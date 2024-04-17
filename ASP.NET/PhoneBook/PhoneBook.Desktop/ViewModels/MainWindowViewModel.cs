using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Desktop.Commands;
using PhoneBook.Desktop.Services;
using PhoneBook.Desktop.Views;
using System.Windows.Input;

namespace PhoneBook.Desktop.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly AccountService _accountService;
        private readonly IServiceProvider _serviceProvider;

        public MainWindowViewModel(AccountService accountService, IServiceProvider serviceProvider)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            _selectedViewModel = _serviceProvider.GetRequiredService<AnonymMainViewModel>();

            LoginCommand = new LambdaCommand(Login, CanLogin);
            LogoutCommand = new LambdaCommand(Logout, CanLogout);
            RegisterCommand = new LambdaCommand(Register, CanRegister);
            UpdateCommand = new LambdaCommand(Update, CanUpdate);
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



        private bool CanLogin(object p) => true;

        private void Login(object p)
        {
            var window = _serviceProvider.GetRequiredService<LoginWindow>();
            window.ShowDialog();

            UpdateWindow();
        }


        private bool CanLogout(object p) => true;
        private void Logout(object p)
        {
            _accountService.Logout();
            UpdateWindow();
        }

        private bool CanRegister(object p) => true;
        private void Register(object p)
        {

        }

        private void UpdateWindow()
        {
            UserName = _accountService.GetUserName();
            switch (_accountService.GetUserRole())
            {
                case UserRoles.Admin:
                    SelectedViewModel = _serviceProvider.GetRequiredService<AdminMainViewModel>();
                    break;
                case UserRoles.User:
                    SelectedViewModel = _serviceProvider.GetRequiredService<UserMainViewModel>();
                    break;
                default:
                    SelectedViewModel = _serviceProvider.GetRequiredService<AnonymMainViewModel>();
                    break;
            }
        }
        private bool CanUpdate(object p) => true;
        private void Update(object p)
        {
            UpdateWindow();
        }
    }
}
