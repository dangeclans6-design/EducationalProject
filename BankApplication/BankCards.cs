using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BankApplication
{
    #region BankCard

    public abstract class BankCard(int id, string cardnumber, decimal balance, decimal deposit, decimal limitdeposit, int clientid, int status, string reason)
    {
        // АвтоСвойства
        public int Id { get; } = id;
        public int ClientId { get; } = clientid;
        public string CardNumber { get; } = cardnumber;
        public decimal Balance { get; set; } = balance;
        public decimal Deposit { get; set; } = deposit;
        public decimal LimitDeposit { get; set; } = limitdeposit;
        public int Status { get; set; } = status;
        public string Reason { get; set; } = reason;
    }

    #endregion


    #region Usually Card

    public class UsuallyCard(int id, string cardnumber, decimal balance, decimal deposit, decimal limitdeposit, int clientid, int status, string reason) :
        BankCard(id, cardnumber, balance, deposit, limitdeposit, clientid, status, reason)
    {
        // АвтоСвойства
        public decimal LimitBalance { get; } = 4000000;

        // Методы
        //--------------------------[Оплата]------------------------------//
        public struct ToPayPreview(decimal amount, decimal newBalance)
        {
            public decimal Amount { get; set; } = amount;
            public decimal NewBalance { get; set; } = newBalance;
        }
        public ToPayPreview PreparePay(decimal amount)
        {
            if (amount <= 0 || amount > 1000000) throw new Exception("Сумма оплаты должна быть положительной и не более 1.000.000 рублей за 1 раз!");
            if (Balance < amount) throw new Exception($"Недостаточно средств! Ваш баланс: {Balance} рублей.");
            if (Status == 0) throw new Exception("Ваша карта заморожена! Пожалуйста, обратитесь в поддержку!");

            // Заранее вычисляю новый баланс
            decimal newBalance = Balance - amount;

            return new ToPayPreview(amount, newBalance);
        }
        public async Task Pay(ToPayPreview preview, int id, string current_CardNumber, decimal current_Balance, decimal current_Deposit, decimal current_LimitDeposit)
        {
            // Проверяем данные на совпадение и перезаписываем (если все совпало). Если все хорошо, вернется true, иначе - false
            bool IsDbUpdate = await DataBase.PayUsuallyCard(preview.NewBalance, id, current_CardNumber, current_Balance, current_Deposit, current_LimitDeposit);

            if (IsDbUpdate)
            {
                Balance = preview.NewBalance;
            }
            else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
        }

        //--------------------------[Пополнение баланса]------------------------------//
        public struct ReplenishBalancePreview(decimal amount, decimal new_balance)
        {
            public decimal Amount = amount;
            public decimal NewBalance = new_balance;
        }
        public ReplenishBalancePreview PrepareReplenishBalance(decimal amount)
        {
            if (amount <= 0 || amount > 1000000) throw new Exception("Сумма пополнения баланса должна быть положительной и не более 1.000.000 рублей за 1 раз!");
            if (Balance + amount > LimitBalance) throw new Exception($"Невозможно пополнить баланс, так как Вы превышаете лимит для 'Обычной' карты! Ваш лимит: {LimitBalance} рублей.");
            if (Status == 0) throw new Exception("Ваша карта заморожена! Пожалуйста, обратитесь в поддержку!");

            decimal new_balance = Balance + amount;

            return new ReplenishBalancePreview(amount, new_balance);
        }
        public async Task ReplenishBalance(ReplenishBalancePreview preview, int id, decimal current_balance, decimal current_deposit, decimal current_limit)
        {
            bool IsDbUpdate = await DataBase.ReplenishBalanceUsuallyCard(id, preview.NewBalance, current_balance, current_deposit, current_limit);

            if (IsDbUpdate) Balance = preview.NewBalance;
            else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
        }

        //--------------------------[Пополнение депозита]------------------------------//
        public readonly struct ReplenishDepositPreview(decimal amount, decimal new_deposit)
        {
            public readonly decimal Amount = amount;
            public readonly decimal NewDeposit = new_deposit;
        }
        public ReplenishDepositPreview PrepareReplenishDeposit(decimal amount)
        {
            if (amount <= 0 || amount > 100000) throw new Exception("Сумма пополнения депозита должна быть положительной и не более 100.000 рублей за 1 раз!");
            if (Deposit + amount > LimitDeposit) throw new Exception($"Вы превышаете лимит депозита! Ваш лимит: {LimitDeposit} рублей.");
            if (Status == 0) throw new Exception("Ваша карта заморожена! Пожалуйста, обратитесь в поддержку!");

            decimal new_deposit = Deposit + amount;

            return new ReplenishDepositPreview(amount, new_deposit);
        }
        public async Task ReplenishDeposit(ReplenishDepositPreview preview, int id, decimal current_balance, decimal current_deposit, decimal current_limit)
        {
            bool IsDbUpdate = await DataBase.ReplenishDepositUsuallyCard(id, preview.NewDeposit, current_balance, current_deposit, current_limit);

            if (IsDbUpdate) Deposit = preview.NewDeposit;
            else throw new Exception($"Произошла непредвиденная ошибка! Пожалуйста, обновите страницу или повторите попытку позже.");
        }

        //--------------------------[Изменить лимит депозита (запрос)]------------------------------//
        public readonly struct ChangeLimitDepositPreview(decimal newDeposit, string message)
        {
            public readonly decimal NewDeposit = newDeposit;
            public readonly string Message = message;            
        }
        public ChangeLimitDepositPreview PrepareChangeLimitDeposit(decimal newDeposit, string message)
        {
            if(newDeposit <= 10000 || newDeposit > 400000) throw new Exception("Лимит депозита для 'Обычной': от 10 до 400 тысяч рублей!");
            if(newDeposit == LimitDeposit) throw new Exception("Вы ввели значение, которое уже установлено как лимит!");
            if (Status == 0) throw new Exception("Ваша карта заморожена! Пожалуйста, обратитесь в поддержку!");

            return new ChangeLimitDepositPreview(newDeposit, message);
        }
        public async Task RequestChangeLimitDeposit(ChangeLimitDepositPreview preview)
        {
            bool IsUpdate = await DataBase.U_RequestChangeLimitDeposit(ClientId, CardNumber, "U", Id, preview.Message, preview.NewDeposit);

            if (IsUpdate)
            {
                var find_request = await DataBase.SearchRequestClient(ClientId);
                Session.CurrentClient.OpenRequest = find_request.Request;
            }
            else throw new Exception($"Произошла непредвиденная ошибка! Пожалуйста, обновите страницу или повторите попытку позже.");
        }

        //--------------------------[Закрыть карту (запрос)]------------------------------//
        public readonly struct CloseCardPreview(string typeCard, string message)
        {
            public readonly string TypeCard = typeCard;
            public readonly string Message = message;
        }
        public CloseCardPreview PrepareCloseCard(string typeCard, string message)
        {
            if (Balance > 0 || Deposit > 0) throw new Exception("Чтобы отправить запрос на закрытие карты, основной баланс и баланс депозита должны быть нулевыми!");
            if (Status == 0) throw new Exception("Ваша карта заморожена! Пожалуйста, обратитесь в поддержку!");

            return new CloseCardPreview(typeCard, message);
        }
        public async Task RequestCloseCard(CloseCardPreview preview)
        {
            await DataBase.U_RequestCloseCard(ClientId, CardNumber, preview.TypeCard, Id, preview.Message, Balance, Deposit, LimitDeposit);

            var find_request = await DataBase.SearchRequestClient(ClientId);
            Session.CurrentClient.OpenRequest = find_request.Request;
        }

        //--------------------------[Открыть карту (запрос)]------------------------------//
        public readonly struct OpenCardPreview(int clientId, string typeCard, string message)
        {
            public readonly int ClientId = clientId;
            public readonly string TypeCard = typeCard;
            public readonly string Message = message;
        }
        public OpenCardPreview PrepareOpenCard(int clientId, string typeCard, string message)
        {
            if (Status == 0) throw new Exception("Ваша карта заморожена! Пожалуйста, обратитесь в поддержку!");

            return new OpenCardPreview(clientId, typeCard, message);
        }
        public async Task RequestOpenCard(OpenCardPreview preview)
        {
            await DataBase.RequestOpenCard(preview.ClientId, preview.TypeCard, preview.Message);

            var find_request = await DataBase.SearchRequestClient(preview.ClientId);
            Session.CurrentClient.OpenRequest = find_request.Request;
        }
    }

    #endregion


    #region Debit Card

    public class DebitCard : BankCard
    {
        // Поля
        readonly decimal limitbalance = 6000000;
        decimal cashback;
        readonly decimal limitcashback = 400000;

        // Свойства
        public decimal LimitBalance { get { return limitbalance; } }
        public decimal Cashback { get { return cashback; } set { cashback = value;} }
        public decimal LimitCashback { get { return limitcashback; } }

        // Констурктор
        public DebitCard(int id, string cardnumber, decimal balance, decimal deposit, decimal limitdeposit, int clientid, decimal cashback, int status, string reason) : 
            base(id, cardnumber, balance, deposit, limitdeposit, clientid, status, reason)
        {
            Cashback = cashback;
        }

        // Методы
        //--------------------------[Оплата]------------------------------//
        public struct DebitPay(decimal amount, decimal cashback, decimal newCashback, decimal newBalance)
        {
            public decimal Amount = amount;
            public decimal Cashback = cashback;
            public decimal NewCashback = newCashback;
            public decimal NewBalance = newBalance;
        }
        public DebitPay PreparePay(decimal amount) // amount - сумма оплаты
        {
            if (amount <= 0 || amount > 1000000) throw new Exception("Сумма оплаты должна быть положительной и не более 1.000.000 рублей за 1 раз!");
            if (Balance < amount) throw new Exception($"Недостаточно средств!\n" + $"Ваш баланс: {Balance} рублей.");

            decimal cashback, newCashback;

            // Заранее вычисляем новый баланс
            decimal newBalance = Balance - amount;

            if (amount >= 10000)
            {
                cashback = amount * 0.01m;
                newCashback = Cashback + cashback;
            }
            else
            {
                cashback = 0;
                newCashback = Cashback;
            }

            return new DebitPay(amount, cashback, newCashback, newBalance);
        }
        public async Task Pay(DebitPay preview, int id, decimal current_balance, decimal current_deposit, decimal current_limit, decimal current_cashback)
        {
            if (preview.NewCashback > LimitCashback)
            {
                // Отправляем данные на проверку (сходство). Если все хорошо - данные перезапишутся и вернется true, иначе - ничего не перезапишется и вернется false
                bool IsDbUpdate = await DataBase.PayDebitCard(preview.NewBalance, LimitCashback, id, current_balance, current_deposit, current_limit, current_cashback);

                if (IsDbUpdate)
                {
                    Cashback = LimitCashback;
                    Balance = preview.NewBalance;
                }
                else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
            }
            else
            {
                // Отправляем данные на проверку (сходство). Если все хорошо - данные перезапишутся и вернется true, иначе - ничего не перезапишется и вернется false
                bool IsDbUpdate = await DataBase.PayDebitCard(preview.NewBalance, LimitCashback, id, current_balance, current_deposit, current_limit, current_cashback);

                if (IsDbUpdate)
                {
                    Cashback = preview.NewCashback;
                    Balance = preview.NewBalance;
                }
                else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
            }
        }

        //--------------------------[Пополнение баланса]------------------------------//
        public struct ReplenishBalancePreview(decimal amount, decimal new_balance)
        {
            public decimal Amount = amount;
            public decimal NewBalance = new_balance;
        }
        public ReplenishBalancePreview PrepareReplenishBalance(decimal amount)
        {
            if (amount <= 0 || amount > 1000000) throw new Exception("Сумма пополнения баланса должна быть положительной и не более 1.000.000 рублей за 1 раз!");
            if (Balance + amount > LimitBalance) throw new Exception($"Невозможно пополнить баланс, так как Вы превышаете лимит для 'Дебитовой' карты! Ваш лимит: {LimitBalance} рублей.");

            decimal new_balance = Balance + amount;

            return new ReplenishBalancePreview(amount, new_balance);
        }
        public async Task ReplenishBalance(ReplenishBalancePreview preview, int id, decimal current_balance, decimal current_deposit, decimal current_limit, decimal current_cashback)
        {
            bool IsDbUpdate = await DataBase.ReplenishBalanceDebitCard(id, preview.NewBalance, current_balance, current_deposit, current_limit, current_cashback);

            if (IsDbUpdate) Balance = preview.NewBalance;
            else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");


        }

        //--------------------------[Пополнение депозита]------------------------------//
        public readonly struct ReplenishDepositPreview(decimal amount, decimal new_deposit)
        {
            public readonly decimal Amount = amount;
            public readonly decimal NewDeposit = new_deposit;
        }

        public ReplenishDepositPreview PrepareReplenishDeposit(decimal amount)
        {
            if (amount <= 0 || amount > 200000) throw new Exception("Сумма пополнения депозита должна быть положительной и не более 200.000 рублей за 1 раз!");
            if (Deposit + amount > LimitDeposit) throw new Exception($"Вы превышаете лимит депозита! Ваш лимит: {LimitDeposit} рублей.");

            decimal new_deposit = Deposit + amount;

            return new ReplenishDepositPreview(amount, new_deposit);
        }

        public async Task ReplenishDeposit(ReplenishDepositPreview preview, int id, decimal current_balance, decimal current_deposit, decimal current_limit, decimal current_cashback)
        {
            bool IsDbUpdate = await DataBase.ReplenishDepositDebitCard(id, preview.NewDeposit, current_balance, current_deposit, current_limit, current_cashback);

            if (IsDbUpdate) Deposit = preview.NewDeposit;
            else throw new Exception($"Произошла непредвиденная ошибка! Пожалуйста, обновите страницу или повторите попытку позже.");
        }

        //--------------------------[Изменить лимит депозита (запрос)]------------------------------//
        public readonly struct ChangeLimitDepositPreview(decimal newDeposit, string message)
        {
            public readonly decimal NewDeposit = newDeposit;
            public readonly string Message = message;         
        }
        public ChangeLimitDepositPreview PrepareChangeLimitDeposit(decimal newDeposit, string message)
        {
            if (newDeposit <= 10000 || newDeposit > 400000) throw new Exception("Лимит депозита для 'Дебетовой': от 10 до 400 тысяч рублей!");
            if (newDeposit == LimitDeposit) throw new Exception("Вы ввели значение, которое уже установлено как лимит!");
            if (Status == 0) throw new Exception("Ваша карта заморожена! Пожалуйста, обратитесь в поддержку!");

            return new ChangeLimitDepositPreview(newDeposit, message);
        }
        public async Task RequestChangeLimitDeposit(ChangeLimitDepositPreview preview)
        {
            bool IsUpdate = await DataBase.D_RequestChangeLimitDeposit(ClientId, CardNumber, "D", Id, preview.Message, preview.NewDeposit);

            if (IsUpdate)
            {
                var find_request = await DataBase.SearchRequestClient(ClientId);
                Session.CurrentClient.OpenRequest = find_request.Request;
            }            
            else throw new Exception($"Произошла непредвиденная ошибка! Пожалуйста, обновите страницу или повторите попытку позже.");
        }

        //--------------------------[Закрыть карту (запрос)]------------------------------//
        public readonly struct CloseCardPreview(string typeCard, string message)
        {
            public readonly string TypeCard = typeCard;
            public readonly string Message = message;
        }
        public CloseCardPreview PrepareCloseCard(string typeCard, string message)
        {
            if (Balance > 0 || Deposit > 0 || Cashback > 0) throw new Exception("Чтобы отправить запрос на закрытие карты, основной баланс, баланс депозита и баланс кешбека должны быть нулевыми!");
            if (Status == 0) throw new Exception("Ваша карта заморожена! Пожалуйста, обратитесь в поддержку!");

            return new CloseCardPreview(typeCard, message);
        }
        public async Task RequestCloseCard(CloseCardPreview preview)
        {
            await DataBase.D_RequestCloseCard(ClientId, CardNumber, preview.TypeCard, Id, preview.Message, Balance, Deposit, LimitDeposit, Cashback);

            var find_request = await DataBase.SearchRequestClient(ClientId);
            Session.CurrentClient.OpenRequest = find_request.Request;
        }

        //--------------------------[Забрать кешбек баланс]------------------------------//
        public void ColletCashback(decimal amount)
        {
            if (Cashback == 0) throw new Exception("Ваш кешбек счет пуст!");
            if (amount <= 0) throw new Exception("Сумма списания кешбека должна быть положительной!");
            if (amount > Cashback) throw new Exception($"Вы пытаетесь списать сумму больше, чем у Вас есть! Ваш баланс кешбека: {Cashback} рублей.");

            decimal new_balance = Balance + amount;
            if (new_balance > LimitBalance) throw new Exception($"Вы не можете списать {amount} рублей с баланса кешбека, так как превышен лимит баланса!");
            else
            {
                Balance = new_balance;
                Cashback -= amount;
            }
        }
    }

    #endregion

    
    #region Credit Card

    public class CreditCard : BankCard
    {
        // Поля
        decimal debt;

        // Свойства
        public decimal LimitBalance { get; } = 10000000;
        public decimal Debt
        {
            get { return debt; }
            set
            {
                if (value < 0) throw new Exception("Задолженость не может быть отрицательной!");
                if (value > LimitDebt) throw new Exception($"Задолженность не может превышать лимит! Лимит: {LimitDebt} рублей.");

                debt = value;
            }
        }
        public decimal LimitDebt { get; } = 3000000;

        // Конструктор
        public CreditCard(int id, string cardnumber, decimal balance, decimal deposit, decimal limitdeposit, int clientid, decimal debt, int status, string reason) :
            base(id, cardnumber, balance, deposit, limitdeposit, clientid, status, reason)
        {
            Debt = debt;
        }

        // Методы
        //--------------------------[Оплата]------------------------------//
        public readonly struct CreditPay(decimal difference, decimal amount, decimal new_debt, decimal new_balance)
        {
            public readonly decimal Difference = difference;
            public readonly decimal Amount = amount;
            public readonly decimal NewDebt = new_debt;
            public readonly decimal NewBalance = new_balance;
        }
        public CreditPay PreparePay(decimal amount) // amount - сумма оплаты
        {
            if (amount <= 0 || amount > 1000000) throw new Exception("Сумма оплаты должна быть положительной и не более 1.000.000 рублей за 1 раз!");

            decimal new_debt, new_balance;
            decimal difference = Balance - amount;

            if (difference >= 0)
            {
                new_debt = Debt;
                new_balance = difference;
            }
            else
            {
                new_debt = Debt + Math.Abs(difference);
                new_balance = 0;
            }

            if (difference < 0 && new_debt > LimitDebt) throw new Exception($"Недостаточно средств!\n" +
                                                 $"С учетом текущего долга ({Debt} рублей) невозможно оплатить в долг, так будет превышен лимит долга ({LimitDebt} рублей).");

            return new CreditPay(difference, amount, new_debt, new_balance);
        }
        public async Task Pay(CreditPay preview, int id, decimal current_balance, decimal current_deposit, decimal current_limit, decimal current_debt)
        {
            // Отправляем данные на проверку (сходство). Если все хорошо - данные перезапишутся и вернется true, иначе - ничего не перезапишется и вернется false
            bool IsDbUpdate = await DataBase.PayCreditCard(preview.NewBalance, preview.NewDebt, id, current_balance, current_deposit, current_limit, current_debt);

            if (IsDbUpdate)
            {
                Balance = preview.NewBalance;
                Debt = preview.NewDebt;
            }
            else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
        }

        //--------------------------[Пополнение депозита]------------------------------//
        public readonly struct ReplenishDepositPreview(decimal amount, decimal new_deposit)
        {
            public readonly decimal Amount = amount;
            public readonly decimal NewDeposit = new_deposit;
        }

        public ReplenishDepositPreview PrepareReplenishDeposit(decimal amount)
        {
            if (amount <= 0 || amount > 200000) throw new Exception("Сумма пополнения депозита должна быть положительной и меньше 200.000 рублей за 1 раз!");
            if (Debt > 0) throw new Exception($"Невозможно пополнить депозит, так как у Вас не погашена задолженость!\nВаш долг: {Debt} рублей.");
            if (Deposit + amount > LimitDeposit) throw new Exception($"Вы превышаете лимит депозита!\nВаш лимит: {LimitDeposit} рублей.");

            decimal new_deposit = Deposit + amount;

            return new ReplenishDepositPreview(amount, new_deposit);
        }
        public async Task ReplenishDeposit(ReplenishDepositPreview preview, int id, decimal current_balance, decimal current_deposit, decimal current_limit, decimal current_debt)
        {
            bool IsDbUpdate = await DataBase.ReplenishDepositCreditCard(id, preview.NewDeposit, current_balance, current_deposit, current_limit, current_debt);

            if (IsDbUpdate) Deposit = preview.NewDeposit;
            else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
        }
        //--------------------------[Пополнение баланса]------------------------------//
        public struct ReplenishBalancePreview(decimal amount, decimal new_balance)
        {
            public decimal Amount = amount;
            public decimal NewBalance = new_balance;
        }
        public ReplenishBalancePreview PrepareReplenishBalance(decimal amount)
        {
            if (amount <= 0 || amount > 1000000) throw new Exception("Сумма пополнения баланса должна быть положительной и не более 1.000.000 рублей за 1 раз!");
            if (Debt > 0) throw new Exception($"Невозможно пополнить баланс, так как у Вас не погашена задолженость! Ваш долг: {Debt} рублей.");
            if (Balance + amount > LimitBalance) throw new Exception($"Невозможно пополнить баланс, так как Вы превышаете лимит для 'Дебитовой' карты! Ваш лимит: {LimitBalance} рублей.");

            decimal new_balance = Balance + amount;

            return new ReplenishBalancePreview(amount, new_balance);
        }
        public async Task ReplenishBalance(ReplenishBalancePreview preview, int id, decimal current_balance, decimal current_deposit, decimal current_limit, decimal current_debt)
        {
            bool IdDbUpdate = await DataBase.ReplenishBalanceCreditCard(id, preview.NewBalance, current_balance, current_deposit, current_limit, current_debt);

            if (IdDbUpdate) Balance = preview.NewBalance;
            else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
        }

        //--------------------------[Изменить лимит депозита (запрос)]------------------------------//
        public readonly struct ChangeLimitDepositPreview(decimal newDeposit, string message)
        {
            public readonly decimal NewDeposit = newDeposit;
            public readonly string Message = message;
        }
        public ChangeLimitDepositPreview PrepareChangeLimitDeposit(decimal newDeposit, string message)
        {
            if (newDeposit <= 10000 || newDeposit > 1500000) throw new Exception("Лимит депозита для 'Кредитной' карты должен быть больше 10-ти тысяч и меньше 1.500.000 рублей!");
            if (newDeposit == LimitDeposit) throw new Exception("Вы ввели значение, которое уже установлено как лимит!");
            if (Debt > 0) throw new Exception($"Невозможно изменить лимит депозита, так как у Вас непогашена задолженость! Ваш долг: {Debt} рублей.");
            if (Status == 0) throw new Exception("Ваша карта заморожена! Пожалуйста, обратитесь в поддержку!");

            return new ChangeLimitDepositPreview(newDeposit, message);
        }
        public async Task RequestChangeLimitDeposit(ChangeLimitDepositPreview preview)
        {
            bool IsUpdate = await DataBase.C_RequestChangeLimitDeposit(ClientId, CardNumber, "C", Id, preview.Message, preview.NewDeposit);

            if (IsUpdate)
            {
                var find_request = await DataBase.SearchRequestClient(ClientId);
                Session.CurrentClient.OpenRequest = find_request.Request;
            }
            else throw new Exception($"Произошла непредвиденная ошибка! Пожалуйста, обновите страницу или повторите попытку позже.");
        }

        //--------------------------[Погасить долг]------------------------------//
        public struct ToPayDeebtStruct
        {
            public decimal amount;

            public ToPayDeebtStruct(decimal amount)
            {
                this.amount = amount;
            }
        }
        public ToPayDeebtStruct PrepareToPayDebt(decimal amount)
        {
            if (Debt == 0) throw new Exception("У Вас нет долга!");
            if (amount <= 0 || amount > 1000000) throw new Exception("Размер погашения долга должен быть положительным и не больше 1.000.000 рублей за 1 раз!");
            if (Balance < amount) throw new Exception($"Недостаточно средств на балансе!\nВаш баланс: {Balance} рублей.");
            if (amount > Debt) throw new Exception($"Размер вносимой суммы больше, чем долг!\nВаш долг: {Debt} рублей.");

            return new ToPayDeebtStruct(amount);
        }
        public void ToPayDebt(ToPayDeebtStruct preview)
        {
            Balance -= preview.amount;
            Debt -= preview.amount;
        }
    }

    #endregion


    #region Bussines Card

    public class BusinessCard : BankCard
    {
        // Поля
        double percent;

        // Свойства
        public decimal LimitBalance { get; } = 35000000;
        public double Percent
        {
            get { return percent; }
            set
            {
                if (value < 2) throw new Exception("Процент не может быть ниже двух!");
                if (value > 20) throw new Exception("Процент не может быть больше 20!");

                percent = value;
            }
        }
        // Конструктор
        public BusinessCard(int id, string cardnumber, decimal balance, decimal deposit, decimal limitdeposit, int clientid, double percent, int status, string reason) :
            base(id, cardnumber, balance, deposit, limitdeposit, clientid, status, reason)
        {
            this.percent = percent;
        }

        // Методы
        //--------------------------[Оплата]------------------------------//
        public readonly struct BusinessPay(decimal new_pay, decimal new_balance)
        {
            public readonly decimal NewPay = new_pay;
            public readonly decimal NewBalance = new_balance;
        }
        public BusinessPay PreparePay(decimal amount) // amount - сумма оплаты
        {
            if (amount <= 0 || amount > 5000000) throw new Exception("Сумма оплаты должна быть положительной и меньше пяти миллионов за один раз!");

            decimal new_pay = amount * (decimal)(1 + percent / 100.0);
            decimal new_balance = Balance - new_pay;

            if (Balance < new_pay) throw new Exception($"Недостаточно средств!\nС учетом комиссии {percent}% Вы должны заплатить {new_pay} рублей.\nВаш баланс: {Balance} рублей.");

            return new BusinessPay(new_pay, new_balance);
        }
        public async Task Pay(BusinessPay preview, int id, decimal current_balance, decimal current_deposit, decimal current_limit, double current_percent)
        {
            bool IsDbUpdate = await DataBase.PayBusinessCard(id, preview.NewBalance, current_balance, current_deposit, current_limit, current_percent);

            if (IsDbUpdate) Balance = preview.NewBalance;
            else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
        }

        //--------------------------[Пополнение баланса]------------------------------//
        public struct ReplenishBalancePreview(decimal new_balance, decimal commission, decimal amount)
        {
            public decimal NewBalance = new_balance;
            public decimal Commission = commission;
            public decimal Amount = amount;
        }
        public ReplenishBalancePreview PrepareReplenishBalance(decimal amount) // amount - сумма пополнения
        {
            if (amount <= 0 || amount > 5000000) throw new Exception("Сумма пополнения баланса должна быть положительной и не более 5.000.000 рублей за 1 раз!");

            decimal commision = amount * 0.02m; // Комиссия при пополнении баланса 2 процента
            decimal new_balance = Balance + (amount - commision);

            if (new_balance > LimitBalance) throw new Exception($"Невозможно пополнить баланс! Вы превышаете лимит: {LimitBalance} рублей.");

            return new ReplenishBalancePreview(new_balance, commision, amount);
        }
        public async Task ReplenishBalance(ReplenishBalancePreview preview, int id, decimal current_balance, decimal current_deposit, decimal current_limit, double current_percent)
        {
            bool IsDbUpdate = await DataBase.ReplenishBalanceBusinessCard(id, preview.NewBalance, current_balance, current_deposit, current_limit, current_percent);

            if (IsDbUpdate) Balance = preview.NewBalance;
            else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
        }

        //--------------------------[Пополнение депозита]------------------------------//
        public struct ReplenishDepositPreview(decimal new_replenish, decimal commission, decimal new_deposit)
        {
            public decimal NewReplenish = new_replenish;
            public decimal Commission = commission;
            public decimal NewDeposit = new_deposit;
        }
        public ReplenishDepositPreview PrepareReplenishDeposit(decimal amount) // amount - сумма пополнения
        {
            if (amount <= 0 || amount > 1000000) throw new Exception("Сумма пополнения депозита должна быть положительной и меньше 1.000.000 рублей за 1 раз!");

            decimal commission = amount * 0.03m; // Комиссия при пополнении депозита 3 процента
            decimal new_replenish = amount - commission; // Сколько в итоге будет зачисленно на депозит

            if (Deposit + new_replenish > LimitDeposit) throw new Exception($"Вы превышаете лимит депозита (с учетом вычета комиссии)!\nВаш лимит: {LimitDeposit} рублей.\nКомиссия: {commission} рублей.");

            decimal new_deposit = Deposit + new_replenish;

            return new ReplenishDepositPreview(new_replenish, commission, new_deposit);
        }
        public async Task ReplenishDeposit(ReplenishDepositPreview preview, int id, decimal current_balance, decimal current_deposit, decimal current_limit, double current_percent)
        {
            bool IsDbUpdate = await DataBase.ReplenishDepositBusinessCard(id, preview.NewDeposit, current_balance, current_deposit, current_limit, current_percent);

            if (IsDbUpdate) Deposit = preview.NewReplenish;
            else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
        }

        //--------------------------[Изменить лимит депозита]------------------------------//
        public readonly struct ChangeLimitDepositPreview(decimal newDeposit, string message)
        {
            public readonly decimal NewDeposit = newDeposit;
            public readonly string Message = message;
        }
        public ChangeLimitDepositPreview PrepareChangeLimitDeposit(decimal newDeposit, string message)
        {
            if (newDeposit <= 10000 || newDeposit > 1500000) throw new Exception("Лимит депозита для 'Кредитной' карты должен быть больше 10-ти тысяч и меньше 1.500.000 рублей!");
            if (newDeposit == LimitDeposit) throw new Exception("Вы ввели значение, которое уже установлено как лимит!");
            if (Balance < 400000) throw new Exception($"Недостаточно средств! Для изменения лимита депозита для 'Бизнес' карты необходимо заплатить 400000 рублей. Ваш баланс: {Balance} рублей.");
            if (Status == 0) throw new Exception("Ваша карта заморожена! Пожалуйста, обратитесь в поддержку!");

            return new ChangeLimitDepositPreview(newDeposit, message);
        }
        public async Task RequestChangeLimitDeposit(ChangeLimitDepositPreview preview)
        {
            // Заранее вычисляем новый баланс
            decimal newBalance = Balance - 400000;

            bool IsUpdate = await DataBase.B_RequestChangeLimitDeposit(ClientId, CardNumber, "B", Id, preview.Message, preview.NewDeposit, newBalance, Balance);

            if (IsUpdate)
            {
                Balance = newBalance;

                var updateRequest = await DataBase.SearchRequestClient(ClientId);
                Session.CurrentClient.OpenRequest = updateRequest.Request;
            }            
            else throw new Exception("Произошла непредвиденная ошибка! Пожалуйста, повторите попытку позже.");           
        }
    }

    #endregion


    #region Request

    // Класс для отображения данных запроса Клиента
    public class Request(int id, int type, string defcard, string typeCard, DateTime timestamp, decimal new_deposit)
    {
        // АвтоСвойства (Auto-Properties)
        public int Id = id;
        public int Type = type;
        public string DefCard = defcard;
        public string TypeCard = typeCard;
        public DateTime Timestamp  = timestamp;
        public decimal NewDeposit = new_deposit;
    }

    // Класс для панели Администратора (запросы, которые необходимо рассмотреть)
    public class RequestAdmin(int id, int type, string defcard, string typeCard, DateTime timestamp, decimal new_deposit, int clientId,
                        string message, int idCard) : Request(id, type, defcard, typeCard, timestamp, new_deposit)
    {
        // АвтоСвойства (Auto-Properties)
        public int ClientId = clientId;
        public string Message = message;
        public int IdCard = idCard;
    }

    // Класс для панели Администратора (полные данные о запросе (если администратору необходимо првоерить кто дал ответ, когда и т.д.))
    public class RequestAdminFull(int id, int type, string defcard, string typeCard, DateTime timestamp, decimal new_deposit, int clientId,
                        string message, int idCard, string answer, string comment, int adminAnswer, DateTime dateAnswer, int done) : Request(id, type, defcard, typeCard, timestamp, new_deposit)
    {
        // АвтоСвойства (Auto-Properties)
        public int ClientId = clientId;
        public string Message = message;
        public int IdCard = idCard;
        public string Answer = answer;
        public string Comment = comment;
        public int AdminAnswer = adminAnswer;
        public DateTime DateAnswer = dateAnswer;
        public int Done = done;
    }

    #endregion


    #region Notifications

    public class Notifications(int id, int clientid, string title, string message, DateTime creaateat)
    {
        // АвтоСвойства (Auto-Properties)
        public int Id { get; } = id;
        public int ClientId { get; } = clientid;
        public string Title { get; } = title;
        public string Message { get; } = message;
        public DateTime CreateAt { get; } = creaateat;
    }

    #endregion
}
