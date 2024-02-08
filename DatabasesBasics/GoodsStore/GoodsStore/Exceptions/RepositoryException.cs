namespace GoodsStore.Exceptions {
    internal class RepositoryException : Exception {
        public RepositoryException() : base() { }

        public RepositoryException(string message) : base(message) { }
    }
}
