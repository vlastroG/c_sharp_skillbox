namespace BankSystem.Data.Entities {
    public interface IManager : IConsultant {
        /// <summary>
        /// Меняет имя клиенту и возвращает измененного клиента
        /// </summary>
        /// <param name="client"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Client SetClientName(Client client, string name);

        /// <summary>
        /// Меняет паспорт клиенту и возвращает измененного клиента
        /// </summary>
        /// <param name="client"></param>
        /// <param name="passport"></param>
        /// <returns></returns>
        Client SetClientPassport(Client client, string passport);

        /// <summary>
        /// Меняет отчество клиенту и возвращает измененного клиента
        /// </summary>
        /// <param name="client"></param>
        /// <param name="patronymic"></param>
        /// <returns></returns>
        Client SetClientPatronymic(Client client, string patronymic);

        /// <summary>
        /// Меняет фамилию клиента и возвращает измененного клиента
        /// </summary>
        /// <param name="client"></param>
        /// <param name="surname"></param>
        /// <returns></returns>
        Client SetClientSurname(Client client, string surname);

        /// <summary>
        /// Создает нового клиента
        /// </summary>
        /// <returns></returns>
        Client CreateNewClient(string surname, string name, string patronymic, string passport);
    }
}
