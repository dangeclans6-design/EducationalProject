using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BankApplication
{
    // Enum-ы
    public enum CashBackStatus
    {
        NoCashback = 0,         // Сумма < 10000, кэшбека не будет
        NormalCashback = 1,     // Обычный кэшбек, всё в пределах лимита
        LimitExceede = 2        // Кэшбек превысит лимит, часть сгорит
    }

    public enum CardStatus
    {
        Frozen = 0,     // Заморожена
        Active = 1,     // Активна
        Closed = 2      // Закрыта
    }

    public enum ClientType
    {
        Individual = 0,  // Физическое лицо
        Business = 1     // Юридическое лицо (Бизнес)
    }

    public enum TypeRequest
    {
        ChangeLimitDeposit = 0,     // Изменить депозит
        CloseCard = 1,              // Закрыть карту
        OpenCard = 2                // Открыть карту
    }

    public enum TypeCard
    {
        DebitCard = 0,      // Дебетовая карта
        CreditCard = 1      // Кредитная карта
    }

    public enum Currency
    {
        RUB = 0,        // Валюта рубли
        USD = 1,        // Валюта доллары
        EUR = 2         // Валюта евро
    }

    // Struct-уры
    public readonly struct PayStruct
    {
        // Общие
        public decimal Amount { get; }                        // Сумма оплаты
        public decimal NewBalance { get; }                    // Заранее вычесленный новый баланс
        public ITarget Target { get; }                        // Цель (target). Куда "уходят" деньги

        // Для Дебетовой карты
        public decimal Cashback { get; }                      // Сумма кешбека
        public decimal NewCashback { get; }                   // Заранее вычесленный новый баланс кешбека        
        public CashBackStatus CashBackStatus { get; }         // Сюда будет смотреть форма, чтобы предложить клиенту нужное окно        

        // Для кредитной карты
        public decimal Debt { get; }                           // Сумма долга

        public PayStruct(decimal amount, decimal newBalance, ITarget target, decimal cashback, decimal newCashback, CashBackStatus cashbackStatus)
        {
            Amount = amount;
            NewBalance = newBalance;
            Target = target;
            Cashback = cashback;
            NewCashback = newCashback;
            CashBackStatus = cashbackStatus;
        }
        public PayStruct(decimal amount, decimal newBalance, ITarget target, decimal debt)
        {
            Amount = amount;
            NewBalance = newBalance;
            Target = target;
            Debt = debt;
        }
        public PayStruct(decimal amount, decimal newBalance, ITarget target)
        {
            Amount = amount;
            NewBalance = newBalance;
            Target = target;
        }
    }

    public readonly struct ReplenishBalanceStruct(decimal amount, decimal newBalance, ISource source)
    {
        // Общие
        public decimal Amount { get; } = amount;                     // Сумма пополнения
        public decimal NewBalance { get; } = newBalance;             // Заранее вычесленный новый баланс
        public ISource Source { get; } = source;                     // Источник (source). Откуда "берутся" деньги


    }

    public readonly struct TransferToStruct(decimal amount, decimal newBalance, ITarget target)
    {
        // Общие
        public decimal Amount { get; } = amount;                     // Сумма перевода
        public decimal NewBalance { get; } = newBalance;             // Заранее вычесленный новый баланс
        public ITarget Target { get; } = target;                     // Цель (target). Куда "уходят" деньги
    }
}
