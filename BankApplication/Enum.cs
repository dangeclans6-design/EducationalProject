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

    public enum Currency
    {
        RUB = 0,        // Валюта рубли
        USD = 1,        // Валюта доллары
        УГК = 2         // Валюта евро
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
        public decimal Amount = amount;
        public decimal NewBalance = newBalance;
        public ISource Source = source;
    }

    public async static Task D_CloseAccept(int idCard, string answer, string comment, DateTime dateAnswer,
                    int idRequest, string typeCard, string cardNumber, int clientId, string title)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (оба действия зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                #region Действие 1: Удаляем карту из основного списка
                await using var cmdDelete = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdDelete.Transaction = transaction;
                cmdDelete.CommandText = @"
                    DELETE FROM DebitCard
                    WHERE Id = @idCard
                    AND Status = 0";
                cmdDelete.Parameters.AddWithValue("idCard", idCard);

                // ExecuteNonQueryAsync вернет количество вставленных строк
                int deleteRows = await cmdDelete.ExecuteNonQueryAsync();

                if (deleteRows == 0) throw new Exception("Произошла непредвиденная ошибка. Пожалейства, повторите попытку позже.");
                #endregion

                #region Действие 2: Добавляем карту в архив
                await using var cmdInsert = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdInsert.Transaction = transaction;
                cmdInsert.CommandText = @"
                    INSERT INTO ArchiveCard (TypeCard, CardNumber, ClientId, Reason)
                    VALUES (@typeCard, @cardNumber, @clietnId, @reason)";
                cmdInsert.Parameters.AddWithValue("typeCard", typeCard);
                cmdInsert.Parameters.AddWithValue("cardNumber", cardNumber);
                cmdInsert.Parameters.AddWithValue("clietnId", clientId);
                cmdInsert.Parameters.AddWithValue("reason", $"Карта закрыта по заявке владельца (номер заявки: {idRequest})");

                // ExecuteNonQueryAsync вернет количество вставленных строк
                int insertRows = await cmdInsert.ExecuteNonQueryAsync();

                if (insertRows == 0) throw new Exception("Произошла непредвиденная ошибка. Пожалейства, повторите попытку позже.");
                #endregion

                #region Действие 3: Отправляем ответ на запрос
                await using var cmdUpdateRequest = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdUpdateRequest.Transaction = transaction;
                cmdUpdateRequest.CommandText = @"
                    UPDATE Request
                    SET Done = 1, Answer = @answer, Comment = @comment, DateAnswer = @dateAnswer
                    WHERE Id = @idRequest
                    AND Answer IS NULL
                    AND Comment IS NULL
                    AND DateAnswer IS NULL";
                cmdUpdateRequest.Parameters.AddWithValue("answer", answer);
                cmdUpdateRequest.Parameters.AddWithValue("comment", comment);
                cmdUpdateRequest.Parameters.AddWithValue("dateAnswer", dateAnswer);
                cmdUpdateRequest.Parameters.AddWithValue("idRequest", idRequest);

                int updateRequestRows = await cmdUpdateRequest.ExecuteNonQueryAsync();

                if (updateRequestRows == 0) throw new Exception("Произошла непредвиденная ошибка! Пожалуйста, повторите попытку позже.");
                #endregion

                #region Действие 4: Отправляем уведомление клиенту
                await using var notifications = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                notifications.Transaction = transaction;
                notifications.CommandText = @"
                    INSERT INTO Notifications (ClientId, Title, Message)
                    VALUES (@clientId, @title, @message)";
                notifications.Parameters.AddWithValue("clientId", clientId);
                notifications.Parameters.AddWithValue("title", title);
                notifications.Parameters.AddWithValue("message", comment);

                var addNoti = await notifications.ExecuteNonQueryAsync();

                if (addNoti == 0) throw new Exception("Произошла непредвиденная ошибка. Пожалуйста, повторите попытку позже.");
                #endregion

                // Если все хорошо: Сохраняем оба действия в БД разом
                await transaction.CommitAsync();
            }
            catch
            {
                // В случае любой непредвиденной ошибки отменяем все изменения
                if (transaction != null) await transaction.RollbackAsync();
                throw;
            }
        }
    }
