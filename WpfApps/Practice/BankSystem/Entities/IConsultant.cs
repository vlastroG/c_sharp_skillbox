namespace BankSystem.Entities {
    interface IConsultant {
        /// <summary>
        /// Возвращает имя клиента
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        string GetClientName(Client client);

        /// <summary>
        /// Возвращает серию и номер паспорта клиента
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        string GetClientPassport(Client client);

        /// <summary>
        /// Возвращает отчество клиента
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        string GetClientPatronymic(Client client);

        /// <summary>
        /// Возвращает телефон клиента
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        string GetClientPhone(Client client);

        /// <summary>
        /// Возвращает отчество клиента
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        string GetClientSurname(Client client);

        /// <summary>
        /// Меняет телефон клиенту и возвращает клиента с новым номером телефона
        /// </summary>
        /// <param name="client"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        Client SetClientPhone(Client client, string phone);
    }
}
