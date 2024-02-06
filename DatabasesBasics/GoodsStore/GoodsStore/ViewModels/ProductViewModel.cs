using GoodsStore.Models;
using vlastroG.WPF.ViewModels;

namespace GoodsStore.ViewModels {
    internal class ProductViewModel : BaseViewModel, IEquatable<ProductViewModel> {
        private readonly Product _product;


        public ProductViewModel(Product product) {
            _product = product ?? throw new ArgumentNullException(nameof(product));

            Id = product.Id;
            Email = product.Email;
            _name = product.Name;
            _productCode = product.ProductCode;
        }


        public int Id { get; }

        public string Email { get; }


        private string _name;
        public string Name {
            get => _name;
            set => Set(ref _name, value);
        }


        private string _productCode;
        public string ProductCode {
            get => _productCode;
            set => Set(ref _productCode, value);
        }


        public Product GetUpdatedProduct() {
            _product.Name = Name;
            _product.ProductCode = _productCode;
            return _product;
        }

        public override bool Equals(object? obj) {
            return Equals(obj as ProductViewModel);
        }

        public bool Equals(ProductViewModel? other) {
            if (other is null) { return false; }
            if (ReferenceEquals(this, other)) { return true; };

            return Id == other.Id
                && Email == other.Email
                && Name == other.Name
                && ProductCode == other.ProductCode
                ;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Id, Email, Name, ProductCode);
        }
    }
}
