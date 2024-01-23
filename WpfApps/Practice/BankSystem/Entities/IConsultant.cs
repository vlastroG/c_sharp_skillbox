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
        /// Пополняет баланс счета на заданную сумму
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <param name="amount"></param>
        void PutMoney(IBankAccountCovariant<BankAccountGeneral> bankAccount, decimal amount);

        /// <summary>
        /// Меняет телефон клиенту и возвращает клиента с новым номером телефона
        /// </summary>
        /// <param name="client"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        Client SetClientPhone(Client client, string phone);

        /// <summary>
        /// Переводит заданную сумму с счета отправителя на счет получателя
        /// </summary>
        /// <param name="bankAccountSource">Счет отправителя</param>
        /// <param name="bankAccountDestination">Счет получателя</param>
        /// <param name="amount">Сумма для перевода</param>
        void TransferMoney(
            IBankAccountContravariant<BankAccountDeposit> bankAccountSource,
            IBankAccountContravariant<BankAccountDeposit> bankAccountDestination,
            decimal amount);
    }
}
