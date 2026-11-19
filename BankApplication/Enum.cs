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

    public enum EnumChangeDataClient
    {
        Login = 0,          // Изменить логин
        Password = 1,       // Изменить пароль
        Mail = 2            // Изменить почту
    }

    public readonly struct StructChangeDataClient(int idClient, string currentLogin, string newLogin, string currentPassword, string newPassword, string currentMail, string newMail)
    {
        public readonly int IdClient = idClient;
        public readonly string CurrentLogin = currentLogin;
        public readonly string NewLogin = newLogin;
        public readonly string CurrentPassword = currentPassword;
        public readonly string NewPassword = newPassword;
        public readonly string CurrentMail = currentMail;
        public readonly string NewMail = newMail;

    }


    #region Классы-хранилища для методов перевода
    public class BasePrepareTransfer(decimal amount, decimal newBalance, ITarget target)
    {
        public decimal Amount { get; } = amount;                     // Сумма перевода
        public decimal NewBalance { get; } = newBalance;             // Заранее вычесленный новый баланс
        public ITarget Target { get; } = target;                     // Цель (target). Куда "уходят" деньги
    }

    public class DebitCardPrepareTransfer(decimal amount, decimal newBalance, ITarget target) : BasePrepareTransfer(amount, newBalance, target) { }

    public class PaymountAccountPrepareTransfer(decimal amount, decimal newBalance, ITarget target) : BasePrepareTransfer(amount, newBalance, target) { }

    #endregion

    #region Классы-хранилища для методов пополнения
    public abstract class BasePrepareReplenish(decimal amount, decimal newBalance, ISource source)
    {
        public decimal Amount { get; } = amount;                                // Сумма пополнения
        public decimal NewBalance { get; } = newBalance;                        // Заранее вычесленный новый баланс
        public ISource Source { get; } = source;                                // Источник (source). Откуда "берутся" деньги
    }

    public class DebitCardPrepareReplenish(decimal amount, decimal newBalance, ISource source) : BasePrepareReplenish(amount, newBalance, source) { }

    public class PaymountAccountPrepareReplenish(decimal amount, decimal newBalance, ISource source) : BasePrepareReplenish(amount, newBalance, source) { }
    #endregion

    #region Классы-хранилища для методов оплаты
    public abstract class BasePreparePay(decimal amount, decimal newBalance, ITarget target)
    {
        public decimal Amount { get; } = amount;                        // Сумма оплаты
        public decimal NewBalance { get; } = newBalance;                    // Заранее вычесленный новый баланс
        public ITarget Target { get; } = target;                        // Цель (target). Куда "уходят" деньги
    }

    public class DebitCardPreparePay(decimal amount, decimal newBalance, ITarget target, decimal cashback, decimal newCashback, CashBackStatus cashBackStatus) : BasePreparePay(amount, newBalance, target)
    {
        public decimal Cashback { get; } = cashback;
        public decimal NewCashback { get; } = newCashback;
        public CashBackStatus CashBackStatus { get; } = cashBackStatus;
    }

    public class PaymountAccountPreparePay(decimal amount, decimal newBalance, ITarget target) : BasePreparePay(amount, newBalance, target) { }
    #endregion
}
