﻿namespace BankSystem.Entities {
    internal interface IBankAccount<T> : IBankAccountCovariant<T>, IBankAccountContravariant<T> where T : BankAccountGeneral {
    }

    internal interface IBankAccount {
        string Number { get; }

        bool IsActive { get; }

        decimal Money { get; }

        void Close();

        void Open();

        void PutMoney(decimal amount);

        decimal GetMoney(decimal amount);
    }
}
