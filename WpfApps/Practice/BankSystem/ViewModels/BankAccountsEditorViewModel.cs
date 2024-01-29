using BankSystem.Entities;
using BankSystem.Repositories;
using System.Collections.ObjectModel;
using System.Windows.Input;
using vlastroG.WPF.Commands;
using vlastroG.WPF.ViewModels;

namespace BankSystem.ViewModels {
    internal class BankAccountsEditorViewModel : BaseViewModel {
        private readonly Consultant _consultant;
        private readonly Client _client;
        private readonly ClientsRepository _clientsRepository;
        private readonly BankAccountsGeneralRepository _bankAccountsGeneralRepository;
        private readonly BankAccountsDepositRepository _bankAccountsDepositRepository;

        private const string _openedStatus = "Открыт";
        private const string _closedStatus = "Закрыт";

        public BankAccountsEditorViewModel(
            Consultant consultant,
            Client client,
            ClientsRepository clientsRepository,
            BankAccountsGeneralRepository bankAccountsGeneralRepository,
            BankAccountsDepositRepository bankAccountsDepositRepository) {

            _consultant = consultant ?? throw new ArgumentNullException(nameof(consultant));
            _client = client ?? throw new ArgumentNullException(nameof(client));
            if (_client.BankAccountGeneral is null || _client.BankAccountDeposit is null) {
                throw new ArgumentNullException(nameof(_client));
            }
            _clientsRepository = clientsRepository ?? throw new ArgumentNullException(nameof(clientsRepository));
            _bankAccountsGeneralRepository = bankAccountsGeneralRepository ?? throw new ArgumentNullException(nameof(bankAccountsGeneralRepository));
            _bankAccountsDepositRepository = bankAccountsDepositRepository ?? throw new ArgumentNullException(nameof(bankAccountsDepositRepository));
            ClientsToTransfer = new ObservableCollection<Client>(_clientsRepository.Items.Where(item => (item.BankAccountGeneral != null) && (item.BankAccountDeposit != null)));

            StatusAccountGeneral = _client.BankAccountGeneral!.IsActive ? _openedStatus : _closedStatus;
            MoneyAccountGeneral = _client.BankAccountGeneral!.Money;

            StatusAccountDeposit = _client.BankAccountDeposit!.IsActive ? _openedStatus : _closedStatus;
            MoneyAccountDeposit = _client.BankAccountDeposit!.Money;

            OpenAccountCommand = new LambdaCommand(OpenAccount, CanOpenAccount);
            CloseAccountCommand = new LambdaCommand(CloseAccount, CanCloseAccount);
            PutMoneyToAccountCommand = new LambdaCommand(PutMoneyToAccount, CanPutMoneyToAccount);
            TransferMoneyToAccountCommand = new LambdaCommand(TransferMoney, CanTransferMoney);
        }


        private string _statusAccountGeneral = string.Empty;
        public string StatusAccountGeneral {
            get => _statusAccountGeneral;
            set => Set(ref _statusAccountGeneral, value);
        }


        private decimal _moneyAccountGeneral;
        public decimal MoneyAccountGeneral {
            get => _moneyAccountGeneral;
            set => Set(ref _moneyAccountGeneral, value);
        }

        private string _statusAccountDeposit = string.Empty;
        public string StatusAccountDeposit {
            get => _statusAccountDeposit;
            set => Set(ref _statusAccountDeposit, value);
        }


        private decimal _moneyAccountDeposit;
        public decimal MoneyAccountDeposit {
            get => _moneyAccountDeposit;
            set => Set(ref _moneyAccountDeposit, value);
        }

        private bool _operationsOnAccountGeneral;
        public bool OperationsOnAccountGeneral {
            get => _operationsOnAccountGeneral;
            set => Set(ref _operationsOnAccountGeneral, value);
        }

        private decimal _moneyToPut;
        public decimal MoneyToPut {
            get => _moneyToPut;
            set => Set(ref _moneyToPut, value);
        }

        private decimal _moneyToTransfer;
        public decimal MoneyToTransfer {
            get => _moneyToTransfer;
            set => Set(ref _moneyToTransfer, value);
        }

        private bool _transferToAccountGeneral;
        public bool TransferToAccountGeneral {
            get => _transferToAccountGeneral;
            set => Set(ref _transferToAccountGeneral, value);
        }


        private Client? _selectedTransferDestinationClient;
        public Client? SelectedTransferDestinationClient {
            get => _selectedTransferDestinationClient;
            set => Set(ref _selectedTransferDestinationClient, value);
        }

        public ObservableCollection<Client> ClientsToTransfer { get; }


        public ICommand OpenAccountCommand { get; }

        public ICommand CloseAccountCommand { get; }

        public ICommand PutMoneyToAccountCommand { get; }

        public ICommand TransferMoneyToAccountCommand { get; }


        private void OpenAccount(object p) {
            if (OperationsOnAccountGeneral) {
                StatusAccountGeneral = _openedStatus;
                _consultant.OpenAccount(_client.BankAccountGeneral!);
                _bankAccountsGeneralRepository.Update(_client.BankAccountGeneral!);
            } else {
                StatusAccountDeposit = _openedStatus;
                _consultant.OpenAccount(_client.BankAccountDeposit!);
                _bankAccountsDepositRepository.Update(_client.BankAccountDeposit!);
            }
        }

        private bool CanOpenAccount(object p) {
            return (OperationsOnAccountGeneral && !_client.BankAccountGeneral!.IsActive)
                || (!OperationsOnAccountGeneral && !_client.BankAccountDeposit!.IsActive);
        }


        private void CloseAccount(object p) {
            if (OperationsOnAccountGeneral) {
                StatusAccountGeneral = _closedStatus;
                _consultant.CloseAccount(_client.BankAccountGeneral!);
                _bankAccountsGeneralRepository.Update(_client.BankAccountGeneral!);
            } else {
                StatusAccountDeposit = _closedStatus;
                _consultant.CloseAccount(_client.BankAccountDeposit!);
                _bankAccountsDepositRepository.Update(_client.BankAccountDeposit!);
            }
        }

        private bool CanCloseAccount(object p) {
            return (OperationsOnAccountGeneral && _client.BankAccountGeneral!.IsActive)
                || (!OperationsOnAccountGeneral && _client.BankAccountDeposit!.IsActive);
        }


        private void PutMoneyToAccount(object p) {
            if (OperationsOnAccountGeneral) {
                _consultant.PutMoney(_client.BankAccountGeneral!, MoneyToPut);
                _bankAccountsGeneralRepository.Update(_client.BankAccountGeneral!);
                MoneyAccountGeneral = _client.BankAccountGeneral!.Money;
            } else {
                _consultant.PutMoney(_client.BankAccountDeposit!, MoneyToPut);
                _bankAccountsDepositRepository.Update(_client.BankAccountDeposit!);
                MoneyAccountDeposit = _client.BankAccountDeposit!.Money;
            }
        }

        private bool CanPutMoneyToAccount(object p) {
            return (MoneyToPut > 0)
                && ((OperationsOnAccountGeneral && _client.BankAccountGeneral!.IsActive)
                || (!OperationsOnAccountGeneral && _client.BankAccountDeposit!.IsActive));
        }


        private void TransferMoney(object p) {
            if (OperationsOnAccountGeneral) {
                if (TransferToAccountGeneral) {
                    _consultant.TransferMoney(_client.BankAccountGeneral!, SelectedTransferDestinationClient!.BankAccountGeneral!, MoneyToTransfer);
                    _bankAccountsGeneralRepository.Update(SelectedTransferDestinationClient.BankAccountGeneral!);

                } else {
                    _consultant.TransferMoney(_client.BankAccountGeneral!, SelectedTransferDestinationClient!.BankAccountDeposit!, MoneyToTransfer);
                    _bankAccountsDepositRepository.Update(SelectedTransferDestinationClient.BankAccountDeposit!);
                }
                MoneyAccountGeneral = _client.BankAccountGeneral!.Money;
                if (_client.Equals(SelectedTransferDestinationClient)) {
                    MoneyAccountDeposit = _client.BankAccountDeposit!.Money;
                }
                _bankAccountsGeneralRepository.Update(_client.BankAccountGeneral!);
            } else {
                if (TransferToAccountGeneral) {
                    _consultant.TransferMoney(_client.BankAccountDeposit!, SelectedTransferDestinationClient!.BankAccountGeneral!, MoneyToTransfer);
                    _bankAccountsGeneralRepository.Update(SelectedTransferDestinationClient.BankAccountGeneral!);

                } else {
                    _consultant.TransferMoney(_client.BankAccountDeposit!, SelectedTransferDestinationClient!.BankAccountDeposit!, MoneyToTransfer);
                    _bankAccountsDepositRepository.Update(SelectedTransferDestinationClient.BankAccountDeposit!);
                }
                _bankAccountsDepositRepository.Update(_client.BankAccountDeposit!);
                if (_client.Equals(SelectedTransferDestinationClient)) {
                    MoneyAccountGeneral = _client.BankAccountGeneral!.Money;
                }
                MoneyAccountDeposit = _client.BankAccountDeposit!.Money;
            }
        }

        private bool CanTransferMoney(object p) {
            return (MoneyToTransfer > 0) && (SelectedTransferDestinationClient is not null)
                && ((OperationsOnAccountGeneral && _client.BankAccountGeneral!.IsActive)
                || (!OperationsOnAccountGeneral && _client.BankAccountDeposit!.IsActive));
        }
    }
}
