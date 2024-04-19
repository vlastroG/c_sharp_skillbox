﻿using PhoneBook.Desktop.Commands;
using PhoneBook.Desktop.Services;
using PhoneBook.Exceptions;
using PhoneBook.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace PhoneBook.Desktop.ViewModels
{
    public class ContactCreationWindowViewModel : BaseViewModel, IValidatableObject
    {
        private readonly ContactsRepository _contactsRepository;
        private readonly MessageBoxService _messageBoxService;
        private readonly AccountService _accountService;

        public ContactCreationWindowViewModel(ContactsRepository contactsRepository, MessageBoxService messageBoxService, AccountService accountService)
        {
            _contactsRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
            _messageBoxService = messageBoxService ?? throw new ArgumentNullException(nameof(messageBoxService));
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));

            CreateContactCommand = new LambdaCommandAsync(CreateContact, CanCreateContact);
        }


        private string _surname = string.Empty;
        [DisplayName("Фамилия")]
        [MaxLength(50)]
        public string Surname
        {
            get => _surname;
            set => Set(ref _surname, value);
        }


        private string _name = string.Empty;
        [DisplayName("Имя")]
        [MaxLength(50)]
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }


        private string _patronymic = string.Empty;
        [DisplayName("Отчество")]
        [MaxLength(50)]
        public string Patronymic
        {
            get => _patronymic;
            set => Set(ref _patronymic, value);
        }


        private string _phoneNumber = string.Empty;
        [DisplayName("Телефон")]
        [MaxLength(25)]
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => Set(ref _phoneNumber, value);
        }


        private string _address = string.Empty;
        [DisplayName("Адрес")]
        [MaxLength(255)]
        public string Address
        {
            get => _address;
            set => Set(ref _address, value);
        }


        private string _description = string.Empty;
        [DisplayName("Описание")]
        [MaxLength(255)]
        public string Description
        {
            get => _description;
            set => Set(ref _description, value);
        }


        private bool _commandExecuting;
        public bool CommandExecuting
        {
            get => _commandExecuting;
            set => Set(ref _commandExecuting, value);
        }


        public ICommand CreateContactCommand { get; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var properties = GetType().GetProperties().Where(p => p.GetAccessors().Any(a => a.IsPublic) && p.PropertyType.Equals(typeof(string)));
            foreach (var field in properties)
            {
                string str = field.GetValue(this) as string ?? string.Empty;
                if (string.IsNullOrWhiteSpace(str))
                {
                    yield return new ValidationResult("Поле не может быть пустым", new[] { field.Name });
                } else if (str.Length != str.Trim().Length)
                {
                    yield return new ValidationResult("Поле не может начинаться или заканчиваться пробелом", new[] { field.Name });
                }
            }
        }


        private async Task CreateContact(object? p)
        {
            var contact = new Contact()
            {
                Surname = Surname,
                Name = Name,
                Patronymic = Patronymic,
                PhoneNumber = PhoneNumber,
                Address = Address,
                Description = Description
            };
            try
            {
                CommandExecuting = true;
                var success = await _contactsRepository.Create(contact);
                if (success)
                {
                    _messageBoxService.ShowInfo("Контакт успешно добавлен, можете закрыть окно", "Создание контакта");
                } else
                {
                    _messageBoxService.ShowError("Не удалось создать контакт с данными параметрами", "Ошибка создания контакта");
                }
            } catch (ServerNotResponseException)
            {
                _messageBoxService.ShowError("Сервер не отвечает", "Создание контакта");
            } catch (NotAuthorizedUserException)
            {
                _messageBoxService.ShowError("Ваша сессия истекла, войдите заново", "Ошибка авторизации");
                _accountService.Logout();
            }
            finally
            {
                CommandExecuting = false;
            }
        }
        private bool CanCreateContact(object? p) => true;
    }
}
