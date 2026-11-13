using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace BankApplication
{
    #region BankCard + Interface

    #region Скрытое описание
    /*
    Интерфейс декларирурет, что свойства должны быть. В нем мы объявляем методы (без тела).
    Данный интерфейс показывает что может делать каждая карта и какими свойствами она обладает обязательно

    !!! На данный момент этот интерфейс является изыбточным (так как всего 1 класс BankCard подписан на него) !!!
    */
    #endregion
    public interface IBankCard
    {
        public int Id { get; }                          // ID карты в БД
        public int ClientId { get; }                    // ID влдельца (клиента) в БД
        public string CardNumber { get; }               // Номер карты (16 цифр)
        public decimal Balance { get; }                 // Баланс карты
        public CardStatus Status { get; }               // Статус карту (смотреть CardStatus)
        public string Reason { get; }                   // Причина статуса (если не Active)
        public DateTime CreateAt { get; }               // Дата открытия карты
        public DateTime CloseAt { get; }                // Дата закрытия карты
        public string Name { get; }                     // Имя карты (для форм)

        public PayStruct PreparePay(decimal amount, ITarget target);                                // Проверка перед оплатой
        public Task PayCard(PayStruct preview);                                                     // Оплата
        public ReplenishBalanceStruct PrepareReplenishBalance(decimal amount, ISource source);      // Проверка паеред пополнением баланса
        public abstract Task ReplenishBalance(ReplenishBalanceStruct preview);                      // Пополнить баланс карты
    }

    #region Скрытое описание
    /*
    Эти интерфейсы обязуют любой объект (который его наследует) проверить, можно ли с него снять деньги
    или проверить можно ли на него зачислить деньги.

    ISource - интерфейс для объектов, которые могут быть источником (source)
    ITarget - интерфейс для объектов, которые могут быть целью (target)

    NameSource \ NameTarget - Название (например, "Карта *номер*" или "Bitcoin-кошелек")
    PossibleToWithdraw - Проверить, если объект является источником (source) - можно ли списать деньги
    CanDeposit - Проверка, если объект является целью (target) - можно ли пополнить деньги
    */
    #endregion
    public interface ISource
    {
        string NameSource { get; }                              // Имя source-объекта (источника)

        public bool PossibleToWithdraw(decimal amount);        // Проверка для source-объекта (источника)
    }

    public interface ITarget
    {
        string NameTarget { get; }                              // Имя target-объекта

        public bool CanDeposit(decimal amount);                // Проверка для target-объекта
    }

    public interface ITransferable : ISource
    {
        public TransferToStruct PrepareTransferTo(decimal amount, ITarget target);
        public Task TransferTo(TransferToStruct preview);
    }

    public abstract class BankCard(int id, int clientid, string cardNumber, decimal balance, int status, string reason, DateTime createAt, DateTime closeAt) : IBankCard, ITransferable, ITarget
    {
        // IBankCard
        public int Id { get; } = id;
        public int ClientId { get; } = clientid;
        public string CardNumber { get; } = cardNumber;
        public decimal Balance { get; protected internal set; } = balance;
        public CardStatus Status { get; protected set; } = (CardStatus)status;
        public string Reason { get; protected set; } = reason;
        public DateTime CreateAt { get; } = createAt;
        public DateTime CloseAt { get; } = closeAt;
        public string Name { get; protected set; }

        public abstract PayStruct PreparePay(decimal amount, ITarget target);
        public abstract Task PayCard(PayStruct preview);

        public abstract ReplenishBalanceStruct PrepareReplenishBalance(decimal amount, ISource source);
        public abstract Task ReplenishBalance(ReplenishBalanceStruct preview);

        // ITransferable : ISource
        public string NameSource { get; set; }
        public abstract bool PossibleToWithdraw(decimal amount);

        public abstract TransferToStruct PrepareTransferTo(decimal amount, ITarget target);
        public abstract Task TransferTo(TransferToStruct preview);

        // ITarget
        public string NameTarget { get; set; }
        public abstract bool CanDeposit(decimal amount);
        
    }
    #endregion


    #region Debit Card
    public class DebitCard : BankCard
    {
        public decimal LimitBalance { get; } = 6000000;
        public bool IsCashbackEnabled { get; set; }
        public decimal LimitCashback { get; } = 400000;
        public decimal Cashback { get; protected internal set; }

        public DebitCard(int id, int clientid, string cardnumber, decimal balance, decimal cashback, int status, string reason, DateTime createAt, DateTime closeAt, int isCashbackEnabled) :
        base(id, clientid, cardnumber, balance, status, reason, createAt, closeAt)
        {
            Cashback = cashback;
            IsCashbackEnabled = (isCashbackEnabled == 1);
            Name = $"Дебетовая карта ****{CardNumber.Substring(12)}";

            NameSource = $"Дебетовая карта *{cardnumber.Substring(12)}";
            NameTarget = $"Дебетовая карта *{cardnumber.Substring(12)}";
        }


        public override bool PossibleToWithdraw(decimal amount)
        {
            if (Status != CardStatus.Active) return false;
            if (Balance < amount) return false;
            if (amount <= 0 || amount > 500000) return false;

            return true;
        }
        public override bool CanDeposit(decimal amount)
        {
            if (Status != CardStatus.Active || Balance + amount > LimitBalance) return false;
            if (amount <= 0) return false;

            return true;
        }


        #region Оплата
        public override PayStruct PreparePay(decimal amount, ITarget target)
        {            
            bool withdraw = PossibleToWithdraw(amount);
            bool canDeposit = target.CanDeposit(amount);

            if (withdraw && canDeposit)
            {
                decimal newBalance = Balance - amount;      // Заранее вычисляем новый баланс
                CashBackStatus status;                      // Объявляем переменную - статус кешбека

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

                return new PayStruct(amount, newBalance, target, cashback, newCashback, status);
            }
            else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
        }

        public override async Task PayCard(PayStruct preview)
        {
            if (preview.Target is PaymentAccount target)
            {
                decimal newCashback = preview.CashBackStatus switch
                {
                    CashBackStatus.LimitExceede => LimitCashback,
                    CashBackStatus.NormalCashback => preview.NewCashback,
                    _ => Cashback
                };

                decimal newBalanceTarget = target.Balance + preview.Amount;

                bool IsDbUpdate = await DataBase.PayDebitCardToPaymentAccount(preview.NewBalance, newCashback, Id, Balance, Cashback, target.Id, newBalanceTarget, target.Balance);

                if (IsDbUpdate)
                {
                    Cashback = newCashback;
                    Balance = preview.NewBalance;
                    target.Balance = newBalanceTarget;
                }
                else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
            }            
        }
        #endregion

        #region Скрытое описание
        /*
        На данный момент здесь имеется ввиду пополнить со своеей другой карты \ расчетного счета (в дальнейшем, возможно добавится функция пополнения с чужой карты,
        но необходимо подстверждение перевода от владельца в виде смс или др.)
        */
        #endregion

        #region Пополнить баланс
        public override ReplenishBalanceStruct PrepareReplenishBalance(decimal amount, ISource source)
        {
            bool canDeposit = CanDeposit(amount);                     // Возможно ли цель (target) пополнить
            bool withdraw = source.PossibleToWithdraw(amount);        // Возможно ли с источника (source) списать деньги

            if (withdraw && canDeposit)
            {
                decimal newBalance = Balance + amount;

                return new ReplenishBalanceStruct(amount, newBalance, source);
            }
            else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
        }
        public async override Task ReplenishBalance(ReplenishBalanceStruct preview)
        {
            #region Скрытое описание
            /*
            Здесь необходимо разделить логику транзакций по разным типам карт, потому что каждый тип карт находится в своей таблице в БД (DebitCard, CreditCard)
            */
            #endregion

            if (preview.Source is DebitCard card)            // Если источник пополнения - это дебетовая банковская карта
            {
                decimal newBalanceSource = card.Balance - preview.Amount;       // Новый баланс карты-источника (source-карты)

                bool IsDbUpdate = await DataBase.ReplenishBalanceDebitCardFromDebitCard(Id, preview.NewBalance, Balance, Cashback, card.Id, newBalanceSource, card.Balance, card.Cashback);

                if (IsDbUpdate)
                {
                    Balance = preview.NewBalance;
                    card.Balance = newBalanceSource;
                }
                else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
            }
            if (preview.Source is PaymentAccount paymentAccount)            // Если источник пополнения - это расчетный счет
            {
                decimal newBalanceSource = paymentAccount.Balance - preview.Amount;       // Новый баланс счета-источника (source-карты)

                bool IsDbUpdate = await DataBase.ReplenishBalanceDebitCardFromPaymentAccountCard(Id, preview.NewBalance, Balance, Cashback, paymentAccount.Id, newBalanceSource);

                if (IsDbUpdate)
                {
                    Balance = preview.NewBalance;
                    paymentAccount.Balance = newBalanceSource;
                }
                else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
            }
        }
        #endregion

        #region Скрытое описание
        /*
        На данный момент здесь имеется ввиду перевод между своими счетами \ картами
        */
        #endregion

        #region Перевод денег
        public override TransferToStruct PrepareTransferTo(decimal amount, ITarget target)
        {
            bool withdraw = PossibleToWithdraw(amount);
            bool canDeposit = target.CanDeposit(amount);

            if (withdraw && canDeposit)
            {
                decimal newBalance = Balance - amount;

                return new TransferToStruct(amount, newBalance, target);
            }
            else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
        }

        public async override Task TransferTo(TransferToStruct preview)
        {
            if (preview.Target is DebitCard debit)
            {
                decimal newBalanceTarget = debit.Balance - preview.Amount;

                bool IsDbUpdate = await DataBase.TransferDebitCardToDebitCard(Id, preview.NewBalance, Balance, Cashback, debit.Id, newBalanceTarget, debit.Balance, debit.Cashback);

                if (IsDbUpdate)
                {
                    Balance = preview.NewBalance;
                    debit.Balance = newBalanceTarget;
                }
                else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
            }

            if (preview.Target is PaymentAccount payment)
            {
                decimal newBalanceTarget = payment.Balance - preview.Amount;

                bool IsDbUpdate = await DataBase.TransferDebitCardToPayment(Id, preview.NewBalance, Balance, Cashback, payment.Id, newBalanceTarget, payment.Balance);

                if (IsDbUpdate)
                {
                    Balance = preview.NewBalance;
                    payment.Balance = newBalanceTarget;
                }
                else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
            }
        }
        #endregion


        /*
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
        }*/         // РЕФАКТОРИНГ
    }
    #endregion


    /*
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
    }*/             // РЕФАКТОРИНГ


    #region Расчетный счет

    #region Скрытое описание
    /*
    В рамках данного проекта (приложения) расчетный счет (PaymentAccount) — это просто отдельный самостоятельный счет (не привязанный к картам, как в настоящих банках. Сделано для простоты.).
    */
    #endregion
    public class PaymentAccount(int id, int clientId, string bic, string corAccount, decimal balance, int currency, decimal frozenBalance, int status,
                                DateTime openedAt, DateTime closeAt, string accountNumber) : ITarget, ITransferable
    {
        public int Id { get; } = id;                                                    // Уникальный идентификатор счета в БД
        public int ClientId { get; } = clientId;                                        // ID владельца счета (физического или юридического лица)
        public string NameTarget { get; } = $"Расчетный счет #{accountNumber}";         // Имя для target
        public string NameSource { get; } = $"Расчетный счет #{accountNumber}";         // Имя для source
        public string AccountNumber { get; } = accountNumber;                           // Номер расчетного счета (20 цифр)
        public string Bic { get; } = bic;                                               // БИК (Банковский идентификационный код) банка, в котором открыт этот счет (9 цифр)
        public string CorAccount { get; } = corAccount;                                 // Корреспондентский счет банка (20 цифр)
        public decimal Balance { get; set; } = balance;                                 // Текущий доступный остаток реальных денег на счете
        public Currency Currency { get; set; } = (Currency)currency;                    // Валюта счета
        public decimal FrozenBalance { get; set; } = frozenBalance;                     // Заблокированный или «замороженный» баланс
        public CardStatus Status { get; set; } = (CardStatus)status;                    // Состояние счета
        public DateTime OpenedAt { get; } = openedAt;                                   // Дата открытия счета
        public DateTime ClosedAt { get; } = closeAt;                                    // Дата закрытия счета        


        public bool CanDeposit(decimal amount)              // Возможно ли сюда пополнить баланс (target)
        {
            if (amount < 0) return false;
            if (Status != CardStatus.Active) return false;
            
            return true;
        }

        public bool PossibleToWithdraw(decimal amount)      // Возможно от сюда что-либо оплатить (source)
        {
            if (amount < 0) return false;
            if (Balance < amount) return false;            
            if (Status != CardStatus.Active) return false;

            return true;
        }


        #region Перевод денег
        public TransferToStruct PrepareTransferTo(decimal amount, ITarget target)
        {
            bool withdraw = PossibleToWithdraw(amount);
            bool canDeposit = target.CanDeposit(amount);

            if (withdraw && canDeposit)
            {
                decimal newBalance = Balance - amount;

                return new TransferToStruct(amount, newBalance, target);
            }
            else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
        }

        public async Task TransferTo(TransferToStruct preview)
        {
            if (preview.Target is DebitCard debit)
            {
                decimal newBalanceTarget = debit.Balance - preview.Amount;

                bool IsDbUpdate = await DataBase.TransferPaymentToDebitCard(Id, preview.NewBalance, Balance, debit.Id, newBalanceTarget, debit.Balance, debit.Cashback);

                if (IsDbUpdate)
                {
                    Balance = preview.NewBalance;
                    debit.Balance = newBalanceTarget;
                }
                else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
            }

            if (preview.Target is PaymentAccount payment)
            {
                decimal newBalanceTarget = payment.Balance - preview.Amount;

                bool IsDbUpdate = await DataBase.TransferPaymentToPayment(Id, preview.NewBalance, Balance, payment.Id, newBalanceTarget, payment.Balance);

                if (IsDbUpdate)
                {
                    Balance = preview.NewBalance;
                    payment.Balance = newBalanceTarget;
                }
                else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
            }
        }
        #endregion
    }
    #endregion


    #region Request

    // Класс для отображения данных запроса Клиента
    public class Request(int id, int type, string cardNumber, int typeCard, DateTime timestamp, decimal newDeposit)
    {
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
        public int ClientId = clientId;
        public string Message = message;
        public int IdCard = idCard;
    }

    // Класс для панели Администратора (полные данные о запросе (если администратору необходимо првоерить кто дал ответ, когда и т.д.))
    public class RequestAdminFull(int id, int type, string cardNumber, int typeCard, DateTime timestamp, decimal newDeposit, int clientId,
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