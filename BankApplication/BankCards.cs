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
    public interface IBankCard : ITarget, ITransferable
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

        public bool PossibleToWithdraw(decimal amount);        // Проверка для source-объекта (источника)\


        public BasePreparePay PreparePay(decimal amount, ITarget target);            // Проверка перед оплатой
        public Task Pay(BasePreparePay preview);                                     // Оплата
    }

    public interface ITarget
    {
        string NameTarget { get; }                              // Имя target-объекта
        bool IsCashbackAllowed { get; }                         // Начисляется ли кэшбэк при оплате на эту цель

        public bool CanDeposit(decimal amount);                // Проверка для target-объекта

        public BasePrepareReplenish PrepareReplenishBalance(decimal amount, ISource source);      // Проверка паеред пополнением баланса
        public Task ReplenishBalance(BasePrepareReplenish preview);                               // Пополнить баланс карты
    }

    public interface ITransferable : ISource
    {
        public BasePrepareTransfer PrepareTransfer(decimal amount, ITarget target);          // Проверка паеред перевовдом
        public Task Transfer(BasePrepareTransfer preview);                                   // Перевести
    }

    public abstract class BankCard(int id, int clientid, string cardNumber, decimal balance, int status, string reason, DateTime createAt, DateTime closeAt) : IBankCard
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

        public abstract BasePreparePay PreparePay(decimal amount, ITarget target);
        public abstract Task Pay(BasePreparePay preview);

        public abstract BasePrepareReplenish PrepareReplenishBalance(decimal amount, ISource source);
        public abstract Task ReplenishBalance(BasePrepareReplenish preview);

        // ITransferable : ISource
        public string NameSource { get; set; }
        public abstract bool PossibleToWithdraw(decimal amount);

        public abstract BasePrepareTransfer PrepareTransfer(decimal amount, ITarget target);
        public abstract Task Transfer(BasePrepareTransfer preview);

        // ITarget
        public string NameTarget { get; set; }
        public bool IsCashbackAllowed { get; } = false;         // Всегда ложно, так как оплатить с карты на карту невозможно (оплата всегда происходит наь счет)
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
        public override BasePreparePay PreparePay(decimal amount, ITarget target)
        {
            if (target is not PaymentAccount) throw new Exception("Оплата возможна только на расчетный счет.");             // Защита: оплатить что-либо можно только на РС

            bool withdraw = PossibleToWithdraw(amount);
            bool canDeposit = target.CanDeposit(amount);

            if (withdraw && canDeposit)
            {
                decimal newBalanceSource = Balance - amount;                          // Заранее вычисляем новый баланс
                CashBackStatus status;                                          // По умолчанию кешбека нет

                decimal cashbackAmount = 0;                                           // По умолчанию кешбек равен 0
                decimal newCashback = Cashback;                                 // По умолчанию новый кешбек равен старому

                if (IsCashbackEnabled && target.IsCashbackAllowed && amount >= 10000)
                {
                    cashbackAmount = amount * 0.01m;
                    newCashback = Cashback + cashbackAmount;

                    if (newCashback > LimitCashback)
                    {
                        newCashback = LimitCashback;
                        status = CashBackStatus.LimitExceede;
                    }
                    else status = CashBackStatus.NormalCashback;
                }
                else status = CashBackStatus.NoCashback;

                return new DebitCardPreparePay(amount, newBalanceSource, target, Id, Balance, Cashback, cashbackAmount, newCashback, status);
            }
            else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
        }

        public override async Task Pay(BasePreparePay preview)          // Тут BasePreparePay => DebitCardPreparePay и по другому быть не может
        {
            if (preview is not DebitCardPreparePay debitPreview) throw new ArgumentException("Неверный тип данных для оплаты дебетовой картой.");

            if (debitPreview.Target is PaymentAccount payment)
            {
                decimal newBalanceTarget = payment.Balance + debitPreview.Amount;                

                bool IsDbUpdate = await DataBase.PayDebitCardToPaymentAccount(debitPreview, payment.Id, payment.Balance, newBalanceTarget);

                if (!IsDbUpdate)
                    throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");

                Cashback = debitPreview.NewCashback;
                Balance = debitPreview.NewBalanceSource;
            }
            /*
            Тут иного развития события не может быть (else), так как оплата может происходить только на счет
            */
        }
        #endregion


        #region Пополнить баланс
        #region Скрытое описание
        /*
        На данный момент здесь имеется ввиду пополнить со своеей другой карты \ расчетного счета (в дальнейшем, возможно добавится функция пополнения с чужой карты,
        но необходимо подстверждение перевода от владельца в виде смс или др.)
        */
        #endregion
        public override BasePrepareReplenish PrepareReplenishBalance(decimal amount, ISource source)
        {
            bool canDeposit = CanDeposit(amount);                     // Возможно ли цель (target) пополнить
            bool withdraw = source.PossibleToWithdraw(amount);        // Возможно ли с источника (source) списать деньги

            if (withdraw && canDeposit)
            {
                decimal newBalance = Balance + amount;

                return new DebitCardPrepareReplenish(amount, newBalance, source);
            }
            else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
        }
        public async override Task ReplenishBalance(BasePrepareReplenish preview)
        {
            #region Скрытое описание
            /*
            Здесь необходимо разделить логику транзакций по разным типам карт, потому что каждый тип карт находится в своей таблице в БД (DebitCard, CreditCard, PaymentAccount)
            */
            #endregion

            if (preview is not DebitCardPrepareReplenish debitPreview)
                throw new ArgumentException("Неверный тип данных для пополнения дебетовой карты.");             // Защита, если вдруг будет передан неверный тип данных (на всякий случай)

            if (debitPreview.Source is DebitCard card)                                                          // Если источник пополнения - это дебетовая банковская карта
            {
                decimal newBalanceSource = card.Balance - debitPreview.Amount;                                  // Новый баланс карты-источника (source-карты)

                bool IsDbUpdate = await DataBase.ReplenishBalanceDebitCardFromDebitCard(Id, debitPreview.NewBalance, Balance, Cashback, card.Id, newBalanceSource, card.Balance, card.Cashback);

                if (IsDbUpdate)
                {
                    Balance = debitPreview.NewBalance;
                    card.Balance = newBalanceSource;
                }
                else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
            }
            if (debitPreview.Source is PaymentAccount paymentAccount)                                           // Если источник пополнения - это расчетный счет
            {
                decimal newBalanceSource = paymentAccount.Balance - debitPreview.Amount;                        // Новый баланс счета-источника (source-карты)

                bool IsDbUpdate = await DataBase.ReplenishBalanceDebitCardFromPaymentAccountCard(Id, debitPreview.NewBalance, Balance, Cashback, paymentAccount.Id, newBalanceSource);

                if (IsDbUpdate)
                {
                    Balance = debitPreview.NewBalance;
                    paymentAccount.Balance = newBalanceSource;
                }
                else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
            }
        }
        #endregion


        #region Перевод денег

        #region Скрытое описание
        /*
        На данный момент здесь имеется ввиду перевод между своими счетами \ картами
        */
        #endregion
        public override BasePrepareTransfer PrepareTransfer(decimal amount, ITarget target)
        {
            bool withdraw = PossibleToWithdraw(amount);
            bool canDeposit = target.CanDeposit(amount);

            if (withdraw && canDeposit)
            {
                decimal newBalance = Balance - amount;

                return new DebitCardPrepareTransfer(amount, newBalance, target);
            }
            else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
        }

        public async override Task Transfer(BasePrepareTransfer preview)
        {
            if (preview is not DebitCardPrepareTransfer)
                throw new ArgumentException("Неверный тип данных для перевода с дебетовой карты");             // Защита, если вдруг будет передан неверный тип данных (на всякий случай)

            if (preview.Target is DebitCard debit)
            {
                decimal newBalanceTarget = debit.Balance + preview.Amount;

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
                decimal newBalanceTarget = payment.Balance + preview.Amount;

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


        // Написать функцию "Забрать кешбек"
    }
    #endregion


    #region Расчетный счет

    #region Скрытое описание
    /*
    В рамках данного проекта (приложения) расчетный счет (PaymentAccount) — это просто отдельный самостоятельный счет (не привязанный к картам, как в настоящих банках. Сделано для простоты.).
    */
    #endregion
    public class PaymentAccount(int id, int clientId, string accountNumber, string bic, string corAccount, decimal balance, int currency, decimal frozenBalance, int status, string reason,
                                DateTime openedAt, DateTime? closeAt, int isCashbackAllowed) : ITarget, ITransferable
    {
        public int Id { get; } = id;                                                                  // Уникальный идентификатор счета в БД
        public int ClientId { get; } = clientId;                                                      // ID владельца счета (физического или юридического лица)
        public string NameTarget { get; } = $"Расчетный счет *{accountNumber.Substring(16)}";         // Имя для target
        public string NameSource { get; } = $"Расчетный счет *{accountNumber.Substring(16)}";         // Имя для source
        public string AccountNumber { get; } = accountNumber;                                         // Номер расчетного счета (20 цифр)
        public string Bic { get; } = bic;                                                             // БИК (Банковский идентификационный код) банка, в котором открыт этот счет (9 цифр)
        public string CorAccount { get; } = corAccount;                                               // Корреспондентский счет банка (20 цифр)
        public decimal Balance { get; set; } = balance;                                               // Текущий доступный остаток реальных денег на счете
        public Currency Currency { get; set; } = (Currency)currency;                                  // Валюта счета
        public decimal FrozenBalance { get; set; } = frozenBalance;                                   // Заблокированный или «замороженный» баланс
        public CardStatus Status { get; set; } = (CardStatus)status;                                  // Состояние счета
        public string Reason { get; set; } = reason;                                                  // Причина статуса
        public DateTime OpenedAt { get; } = openedAt;                                                 // Дата открытия счета
        public DateTime? ClosedAt { get; } = closeAt;                                                 // Дата закрытия счета

        public bool IsCashbackAllowed { get; } = (isCashbackAllowed == 1);


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
        public BasePrepareTransfer PrepareTransfer(decimal amount, ITarget target)
        {
            bool withdraw = PossibleToWithdraw(amount);
            bool canDeposit = target.CanDeposit(amount);

            if (withdraw && canDeposit)
            {
                decimal newBalance = Balance - amount;

                return new PaymountAccountPrepareTransfer(amount, newBalance, target);
            }
            else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
        }

        public async Task Transfer(BasePrepareTransfer preview)
        {
            if (preview is not PaymountAccountPrepareTransfer)
                throw new ArgumentException("Неверный тип данных для перевода с расчетного счета");             // Защита, если вдруг будет передан неверный тип данных (на всякий случай)

            if (preview.Target is DebitCard debit)
            {
                decimal newBalanceTarget = debit.Balance + preview.Amount;

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
                decimal newBalanceTarget = payment.Balance + preview.Amount;

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

        #region Оплата
        public BasePreparePay PreparePay(decimal amount, ITarget target)
        {
            bool withdraw = PossibleToWithdraw(amount);
            bool candeposit = target.CanDeposit(amount);

            if (withdraw && candeposit)
            {
                decimal newBalance = Balance - amount;

                return new PaymountAccountPreparePay(amount, newBalance, target);
            }
            else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
        }

        public async Task Pay(BasePreparePay preview)
        {
            if (preview is PaymountAccountPreparePay paymountPreview)
            {
                if (paymountPreview.Target is PaymentAccount target)
                {
                    decimal newBalanceTarget = target.Balance + paymountPreview.Amount;

                    bool IsDbUpdate = await DataBase.PayPaymentToPaymentAccount(Id, paymountPreview.NewBalanceSource, Balance, target.Id, newBalanceTarget, target.Balance);

                    if (IsDbUpdate)
                    {
                        Balance = paymountPreview.NewBalanceSource;
                        target.Balance = newBalanceTarget;
                    }
                    else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
                }
            }
            else throw new ArgumentException("Неверный тип данных для оплаты дебетовой картой.");
        }
        #endregion

        #region Пополнение баланса (ПОКА НЕДОСТУПНО)
        public BasePrepareReplenish PrepareReplenishBalance(decimal amount, ISource source)
        {
            throw new NotImplementedException("Временно недоустпуно.");
        }
        public async Task ReplenishBalance(BasePrepareReplenish preview)
        {

        }
        #endregion
    }
    #endregion

    #region Категории для оплаты
    public interface IPayCatalog
    {
        public int Id {  get; }
        public string Name { get; }
        public PaymentCategories CategoryType { get; }
    }

    public class PayCategories(int id, string name) : IPayCatalog
    {
        public int Id { get; } = id;        
        public string Name { get; } = name;
        public PaymentCategories CategoryType { get; } = (PaymentCategories)id;
    }

    public class PayCatalog(int id, int categories, int keyDraw, string name, PaymentAccount paymentAccount, string supportText) : IPayCatalog
    {
        public int Id { get; } = id;
        public PaymentCategories CategoryType { get; } = (PaymentCategories)categories;
        public int KeyDraw { get; } = keyDraw;
        public string Name { get; } = name;
        public PaymentAccount PaymentAccount { get; } = paymentAccount;
        public string SupportText { get; } = supportText;
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