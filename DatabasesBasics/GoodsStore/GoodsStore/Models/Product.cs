namespace GoodsStore.Models {
    internal class Product {
        public Product() {

        }


        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string ProductCode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
    }
}
