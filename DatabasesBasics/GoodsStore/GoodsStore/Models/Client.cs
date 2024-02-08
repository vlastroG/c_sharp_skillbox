namespace GoodsStore.Models {
    internal class Client {
        public Client() {

        }


        public int Id { get; set; }

        public string Surname { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Patronymic { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public string Email { get; set; } = string.Empty;
    }
}
