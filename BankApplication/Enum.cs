using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplication
{
    public class Enum
    {
        public enum CashBackStatus
        {
            NoCashback,         // Сумма < 10000, кэшбека не будет
            NormalCashback,     // Обычный кэшбек, всё в пределах лимита
            LimitExceede        // Кэшбек превысит лимит, часть сгорит
        }
    }
}
