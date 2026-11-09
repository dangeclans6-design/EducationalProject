using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankApplication
{
    #region BankCard + Interface

    #region Скрытое описание
    /*
    Интерфейс декларирурет, что свойства должны быть. В нем мы объявляем методы (без тела).
    Абстрактный класс подписывается на интерфейс и реализует эти свойства (выделяет под них память один раз для всех карт)
    */
    #endregion
    public interface IBankCard
    {
        public int Id { get; }                          // ID карты в БД
        public int ClientId { get; }                    // ID влдельца (клиента) в БД
        public string CardNumber { get; }               // Номер карты (20 цифр)
        public decimal Balance { get; set; }            // Баланс карты
        public decimal Deposit { get; set; }            // Баланс депозита
        public decimal LimitDeposit { get; set; }       // Лимит баланса депозита
        public CardStatus Status { get; set; }          // Статус карту (смотреть CardStatus)
        public string Reason { get; set; }              // Причина статуса (если не Active)

        PayStruct PreparePay(decimal amount, ITransactionParticipant target);
        Task PayCard(PayStruct preview);
    }

    #region Скрытое описание
    /*
    Этот интерфейс (контракт) обязует любой источник проверить, можно ли с него снять деньги, и списать их.
    NameMoneySource - Название для интерфейса (например, "Карта *4321" или "Bitcoin-кошелек")
    PossibleToWithdraw - Проверить, хватает ли денег на источнике
    */
    #endregion
    public interface ITransactionParticipant
    {
        string NameMoneySource { get; }

        void PossibleToWithdraw(decimal amount);        // Проверка для source-карты (источника)
        void CanDeposit(decimal amount);                // Проверка для target-карты (цели-получателя)
    }

    public abstract class BankCard(int id, int clientid, string cardNumber, decimal balance, decimal deposit, decimal limitDeposit, int status, string reason) : IBankCard, ITransactionParticipant
    {
        // IBankCard
        public int Id { get; } = id;
        public int ClientId { get; } = clientid;
        public string CardNumber { get; } = cardNumber;
        public decimal Balance { get; set; } = balance;
        public decimal Deposit { get; set; } = deposit;
        public decimal LimitDeposit { get; set; } = limitDeposit;
        public CardStatus Status { get; set; } = (CardStatus)status;
        public string Reason { get; set; } = reason;

        // IMoneySource
        public string NameMoneySource { get; } = $"Карта *{cardNumber}";

        // Методы
        public abstract PayStruct PreparePay(decimal amount, ITransactionParticipant target);
        public abstract Task PayCard(PayStruct preview);        

        public abstract void PossibleToWithdraw(decimal amount);
        public abstract void CanDeposit(decimal amount);
        public abstract Task ReplenishBalance(decimal amount, ITransactionParticipant moneytransfer);
    }

    #endregion


    #region Debit Card

    public class DebitCard(int id, int clientid, string cardnumber, decimal balance, decimal deposit, decimal limitDeposit, int status, string reason, int isCashbackEnabled, decimal cashback) :
        BankCard(id, clientid, cardnumber, balance, deposit, limitDeposit, status, reason)
    {
        public decimal LimitBalance { get; } = 6000000;
        public bool IsCashbackEnabled { get; set; } = (isCashbackEnabled == 1);
        decimal LimitCashback { get; } = 400000;
        decimal Cashback { get; set; } = cashback;

        // ВОТ ТУТ ОСТАНОВИЛСЯ
        public override void PossibleToWithdraw(decimal amount)     // Возможно ли с данной карты списать деньги
        {
            if (Status == CardStatus.Frozen) throw new Exception($"Карта, с которой Вы хотите перевести, заморожена. Повторите попытку позже или обратитесь в поддержку.");
            if (Balance < amount) throw new Exception($"Недостаточно средств!\n" + $"Ваш баланс: {Balance} рублей.");
            if (amount <= 0 || amount > 500000) throw new Exception("Сумма оплаты должна быть положительной и не более 500000 рублей за 1 раз!");
        }
        public override void CanDeposit(decimal amount)             // Возможно ли данную карту пополнить
        {
            if (Status == CardStatus.Frozen || Balance + amount > LimitBalance) throw new Exception($"Невозможно произвести оплату по указаным реквизитам. Повторите попытку позже или обратитесь в поддержку.");
        }

        #region Оплата

        public override PayStruct PreparePay(decimal amount, ITransactionParticipant target)
        {            
            PossibleToWithdraw(amount);        // Возможно ли с данной карты (донора) списать деньги
            target.CanDeposit(amount);         // Возможно ли карту-target (цель) пополнить

            // Заранее вычисляем новый баланс
            decimal newBalance = Balance - amount;
            CashBackStatus status;

            decimal cashback = 0;
            decimal newCashback = 0;

            if (IsCashbackEnabled)
            {               
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

                status = newCashback > LimitCashback ? CashBackStatus.LimitExceede : CashBackStatus.NormalCashback;
            }
            else status = CashBackStatus.NoCashback;

            return new PayStruct(amount, cashback, newCashback, newBalance, status, target);
        }

        public override async Task PayCard(PayStruct preview)
        {
            if (preview.Target is BankCard targetCard)
            {
                decimal newCashback = preview.CashBackStatus switch
                {
                    CashBackStatus.LimitExceede => LimitDeposit,
                    CashBackStatus.NormalCashback => preview.NewCashback,
                    _ => Cashback
                };

                // ВОТ ТУТ НАДО ДОПИСАТЬ МЕТОД ОПАЛТЫ (ДОБАВИТЬ ЗАЧИСЛЕНИЕ НА TARGET-КАРТУ)
                bool IsDbUpdate = await DataBase.PayDebitCard(preview.NewBalance, newCashback, Id, Balance, Deposit, LimitDeposit, Cashback);

                if (IsDbUpdate)
                {
                    Cashback = newCashback;
                    Balance = preview.NewBalance;
                }
                else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
            }            
        }
        #endregion        

        #region Пополннить баланс
        public ReplenishBalanceStruct PrepareReplenishBalance(decimal amount)
        {
            if (amount <= 0 || amount > 1000000) throw new Exception("Сумма пополнения баланса должна быть положительной и не более 1.000.000 рублей за 1 раз!");
            if (Balance + amount > LimitBalance) throw new Exception($"Невозможно пополнить баланс, так как Вы превышаете лимит для 'Дебитовой' карты! Ваш лимит: {LimitBalance} рублей.");

            decimal new_balance = Balance + amount;

            return new ReplenishBalanceStruct(amount, new_balance);
        }
        public async Task ReplenishBalance(ReplenishBalanceStruct preview)
        {
            bool IsDbUpdate = await DataBase.ReplenishBalanceDebitCard(id, preview.NewBalance, current_balance, current_deposit, current_limit, current_cashback);

            if (IsDbUpdate) Balance = preview.NewBalance;
            else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");


        }
        #endregion

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

        //--------------------------[Закрыть карту (запрос)]------------------------------//
        public readonly struct CloseCardPreview(string typeCard, string message)
        {
            public readonly string TypeCard = typeCard;
            public readonly string Message = message;
        }
        public CloseCardPreview PrepareCloseCard(string typeCard, string message)
        {
            if (Balance > 0 || Deposit > 0 || Debt > 0) throw new Exception("Чтобы отправить запрос на закрытие карты основной баланс и баланс депозита должны быть нулевыми. А также у Вас не должно быть задолженности.");
            if (Status == 0) throw new Exception("Ваша карта заморожена! Пожалуйста, обратитесь в поддержку!");

            return new CloseCardPreview(typeCard, message);
        }
        public async Task RequestCloseCard(CloseCardPreview preview)
        {
            await DataBase.C_RequestCloseCard(ClientId, CardNumber, preview.TypeCard, Id, preview.Message, Balance, Deposit, LimitDeposit, Debt);

            var find_request = await DataBase.SearchRequestClient(ClientId);
            Session.CurrentClient.OpenRequest = find_request.Request;
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


    #region Bussines Card (НУЖДАЕТСЯ В РЕФАКТОРИНГЕ)

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

        //--------------------------[Закрыть карту (запрос)]------------------------------//
        public readonly struct CloseCardPreview(string typeCard, string message)
        {
            public readonly string TypeCard = typeCard;
            public readonly string Message = message;
        }
        public CloseCardPreview PrepareCloseCard(string typeCard, string message)
        {
            if (Balance > 0 || Deposit > 0) throw new Exception("Чтобы отправить запрос на закрытие карты основной баланс и баланс депозита должны быть нулевыми.");
            if (Status == 0) throw new Exception("Ваша карта заморожена! Пожалуйста, обратитесь в поддержку!");

            return new CloseCardPreview(typeCard, message);
        }
        public async Task RequestCloseCard(CloseCardPreview preview)
        {
            await DataBase.B_RequestCloseCard(ClientId, CardNumber, preview.TypeCard, Id, preview.Message, Balance, Deposit, LimitDeposit);

            var find_request = await DataBase.SearchRequestClient(ClientId);
            Session.CurrentClient.OpenRequest = find_request.Request;
        }
    }

    #endregion


    #region Request

    // Класс для отображения данных запроса Клиента
    public class Request(int id, int type, string cardNumber, int typeCard, DateTime timestamp, decimal newDeposit)
    {
        // АвтоСвойства (Auto-Properties)
        public int Id = id;
        public TypeRequest Type = (TypeRequest)type;
        public string CardNumber = cardNumber;
        public TypeCard TypeCard = (TypeCard)typeCard;
        public DateTime Timestamp  = timestamp;
        public decimal NewDeposit = newDeposit;
    }

    // Класс для панели Администратора (запросы, которые необходимо рассмотреть)
    public class RequestAdmin(int id, int type, string cardNumber, int typeCard, DateTime timestamp, decimal newDeposit, int clientId,
                        string message, int idCard) : Request(id, type, cardNumber, typeCard, timestamp, newDeposit)
    {
        // АвтоСвойства (Auto-Properties)
        public int ClientId = clientId;
        public string Message = message;
        public int IdCard = idCard;
    }

    // Класс для панели Администратора (полные данные о запросе (если администратору необходимо првоерить кто дал ответ, когда и т.д.))
    public class RequestAdminFull(int id, int type, string cardNumber, string typeCard, DateTime timestamp, decimal newDeposit, int clientId,
                        string message, int idCard, string answer, string comment, int adminAnswer, DateTime dateAnswer, int done) : Request(id, type, cardNumber, typeCard, timestamp, newDeposit)
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
