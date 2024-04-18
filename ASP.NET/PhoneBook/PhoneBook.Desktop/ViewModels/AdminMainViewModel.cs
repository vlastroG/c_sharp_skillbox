using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Desktop.Commands;
using PhoneBook.Desktop.Services;
using PhoneBook.Desktop.Views;
using PhoneBook.Exceptions;
using PhoneBook.Models;
using System.Windows.Input;

namespace PhoneBook.Desktop.ViewModels
{
    public class AdminMainViewModel : UserMainViewModel
    {
        private readonly AccountService _accountService;

        public AdminMainViewModel(
            ContactsRepository contactsRepository,
            MessageBoxService messageBoxService,
            AccountService accountService,
            IServiceProvider serviceProvider) : base(contactsRepository, messageBoxService, serviceProvider)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));

            DeleteContactCommand = new LambdaCommandAsync(DeleteContact, CanDeleteContact);
            EditContactCommand = new LambdaCommandAsync(EditContact, CanEditContact);
        }


        public ICommand DeleteContactCommand { get; }

        public ICommand EditContactCommand { get; }


        private async Task DeleteContact(object? p)
        {
            var contact = (p as Contact)!;
            if (_messageBoxService.ConfirmWarning($"Вы действительно хотите удалить контакт с id = {contact.Id}", "Предупреждение"))
            {
                try
                {
                    CommandExecuting = true;
                    var success = await _contactsRepository.Delete(contact.Id);
                    if (success)
                    {
                        _messageBoxService.ShowInfo("Контакт успешно удален", "Удаление контакта");
                        await Update();
                    } else
                    {
                        _messageBoxService.ShowError("Не удалось удалить контакт, возможно он уже был удален", "Удаление контакта");
                        await Update();
                    }
                } catch (ServerNotResponseException)
                {
                    _messageBoxService.ShowError("Сервер не отвечает", "Ошибка");
                } catch (NotAuthorizedUserException)
                {
                    _messageBoxService.ShowError("Ваша сессия истекла, войдите заново", "Ошибка авторизации");
                    _accountService.Logout();
                } catch (AccessDeniedException)
                {
                    _messageBoxService.ShowError("У вас нет прав для совершения данной операции", "Ошибка удаления");
                    _accountService.Logout();
                }
                finally
                {
                    CommandExecuting = false;
                }
            }
        }
        private bool CanDeleteContact(object? p) => p is not null && p is Contact;


        private async Task EditContact(object? p)
        {
            var contact = (p as Contact)!;
            var viewModel = _serviceProvider.GetRequiredService<ContactEditionWindowViewModel>();
            viewModel.LoadContact(contact);
            var window = _serviceProvider.GetRequiredService<ContactEditionWindow>();
            window.DataContext = viewModel;
            window.ShowDialog();
            await Update();
        }
        private bool CanEditContact(object? p) => p is not null && p is Contact;
    }
}
