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
    public readonly struct PayStruct(decimal amount, decimal cashback, decimal newCashback, decimal newBalance, CashBackStatus cashbackStatus, ITarget target)
    {
        // Общие
        public decimal Amount { get; } = amount;                            // Сумма оплаты
        public decimal NewBalance { get; } = newBalance;                    // Заранее вычесленный новый баланс
        public ITarget Target { get; } = target;                            // Цель (target). Куда "уходят" деньги

        // Для Дебетовой карты
        public decimal Cashback { get; } = cashback;                        // Сумма кешбека
        public decimal NewCashback { get; } = newCashback;                  // Заранее вычесленный новый баланс кешбека        
        public CashBackStatus CashBackStatus { get; } = cashbackStatus;     // Сюда будет смотреть форма, чтобы предложить клиенту нужное окно        

        // Для кредитной карты

    }

    public struct ReplenishBalanceStruct(decimal amount, decimal newBalance, ISource source)
    {
        // Общие
        public decimal Amount = amount;                     // Сумма пополнения
        public decimal NewBalance = newBalance;             // Заранее вычесленный новый баланс
        public ISource Source = source;                     // Источник (source). Откуда "берутся" деньги
    }
}
