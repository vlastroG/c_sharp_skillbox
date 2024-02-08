using GoodsStore.Models;
using vlastroG.WPF.ViewModels;

namespace GoodsStore.ViewModels {
    internal class ClientViewModel : BaseViewModel, IEquatable<ClientViewModel> {
        private readonly Client _client;


        public ClientViewModel(Client client) {
            _client = client ?? throw new ArgumentNullException(nameof(client));

            Id = client.Id;
            _surname = client.Surname;
            _name = client.Name;
            _patronymic = client.Patronymic;
            _phone = client.Phone;
            Email = client.Email;
        }


        public int Id { get; }

        private string _surname;
        public string Surname {
            get => _surname;
            set => Set(ref _surname, value);
        }

        private string _name;
        public string Name {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _patronymic;
        public string Patronymic {
            get => _patronymic;
            set => Set(ref _patronymic, value);
        }

        private string? _phone;
        public string? Phone {
            get => _phone;
            set => Set(ref _phone, value);
        }

        public string Email { get; }


        public Client GetUpdatedClient() {
            _client.Name = Name;
            _client.Surname = Surname;
            _client.Patronymic = Patronymic;
            _client.Phone = Phone;
            return _client;
        }


        public override bool Equals(object? obj) {
            return Equals(obj as ClientViewModel);
        }


        public bool Equals(ClientViewModel? other) {
            if (other is null) { return false; }
            if (ReferenceEquals(this, other)) { return true; }

            return Id == other.Id
                && Email == other.Email
                && Name == other.Name
                && Patronymic == other.Patronymic
                && Surname == other.Surname
                && Phone == other.Phone
                ;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Id, Surname, Name, Patronymic, Phone, Email);
        }

        public override string ToString() {
            return $"{Surname} {Name} {Patronymic}";
        }
    }
}
