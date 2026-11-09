using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplication
{
    public class Enum
    {
        public enum CashBackStatus
        {
            NoCashback = 0,         // Сумма < 10000, кэшбека не будет
            NormalCashback = 1,     // Обычный кэшбек, всё в пределах лимита
            LimitExceede = 2        // Кэшбек превысит лимит, часть сгорит
        }

        public enum CardStatus
        {
            Frozen = 0, // Заморожена
            Active = 1, // Активна
            Closed = 2  // Закрыта
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
    }
}
