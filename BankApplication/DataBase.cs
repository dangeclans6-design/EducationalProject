using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BankApplication
{
    internal class DataBase
    {
        public async static Task Main()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //---------[Для подключения к БД MySql]--------// 
        readonly static string Server = "192.168.0.161";
        readonly static string UserID = "BankApplication";
        readonly static string Password = "I7oYK7t(eVx@k9Hm";
        readonly static string Database = "myDB";
        //--------------------------------------------//

        //---------[Для подключения к БД SqlBrowser]--------// 
        //readonly static string connectionString = @"Data Source=C:\Users\Gleb24\Desktop\my_database\myDB.db";
        //--------------------------------------------------//

        #region Оплата на расчетный счет (D)
        public async static Task<bool> PayDebitCardToPaymentAccount(decimal newBalanceSource, decimal newCashbackSource, int id, decimal currentBalanceSource,
                                                    decimal currentCashbackSource, int idPayment, decimal newBalanceTarget, decimal currentBalanceTarget)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();
            
            await using var transaction = await connection.BeginTransactionAsync();     // Открываем транзакцию

            try
            {
                #region Действие 1: Списываем с source
                await using var cmdSource = connection.CreateCommand();

                cmdSource.Transaction = transaction;         // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdSource.CommandText = @"
                    UPDATE DebitCard
                    SET Balance = @newBalanceSource, CashBack = @newCashbackSource
                    WHERE Id = @id
                    AND Balance = @currentBalanceSource
                    AND Status = 1
                    AND Reason IS NULL
                    AND IsCashbackEnabled = 1
                    AND CashBack = @currentCashbackSource";
                cmdSource.Parameters.AddWithValue("newBalanceSource", newBalanceSource);
                cmdSource.Parameters.AddWithValue("newCashback", newCashbackSource);
                cmdSource.Parameters.AddWithValue("id", id);
                cmdSource.Parameters.AddWithValue("currentBalance", currentBalanceSource);;
                cmdSource.Parameters.AddWithValue("currentCashback", currentCashbackSource);

                int sourceRows = await cmdSource.ExecuteNonQueryAsync();

                if (sourceRows == 0) return false;
                #endregion

                #region Действие 2: Зачисляем на target
                await using var cmdTarget = connection.CreateCommand();

                cmdTarget.Transaction = transaction;         // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdTarget.CommandText = @"
                    UPDATE PaymentAccount
                    SET Balance = @newBalanceTarget
                    WHERE Id = @idPayment
                    AND Balance = @currentBalanceTarget
                    AND Status = 1";
                cmdTarget.Parameters.AddWithValue("newBalanceTarget", newBalanceTarget);
                cmdTarget.Parameters.AddWithValue("currentBalanceTarget", currentBalanceTarget);
                cmdTarget.Parameters.AddWithValue("idPayment", idPayment);

                int targetRows = await cmdTarget.ExecuteNonQueryAsync();

                if (targetRows == 0) return false;
                #endregion

                await transaction.CommitAsync();            // Если все хорошо: Сохраняем оба действия в БД разом
                return true;
            }
            catch
            {
                if (transaction != null) await transaction.RollbackAsync();
                throw;
            }            
        }
        #endregion


        #region Пополнить баланс дебетовой карты с другой дебетовой карты (D - D)
        public async static Task<bool> ReplenishBalanceDebitCardFromDebitCard(int idTarget, decimal newBalanceTarget, decimal currentBalanceTarget,
                        decimal currentCashbackTarget, int idSource, decimal newBalanceSource, decimal currentBalanceSource, decimal currentCashbackSource)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();     // Открываем транзакцию

            try
            {
                #region Действие 1: Зачисляем на target-карту
                await using var cmdTarget = connection.CreateCommand();

                cmdTarget.Transaction = transaction;
                cmdTarget.CommandText = @"
                    UPDATE DebitCard
                    SET Balance = @newBalanceTarget
                    WHERE Id = @idTarget
                    AND Balance = @currentBalanceTarget
                    AND CashBack = @currentCashbackTarget";
                cmdTarget.Parameters.AddWithValue("newBalanceTarget", newBalanceTarget);
                cmdTarget.Parameters.AddWithValue("idTarget", idTarget);
                cmdTarget.Parameters.AddWithValue("currentBalanceTarget", currentBalanceTarget);
                cmdTarget.Parameters.AddWithValue("currentCashbackTarget", currentCashbackTarget);

                var readerTarget = await cmdTarget.ExecuteNonQueryAsync();

                if (readerTarget == 0) return false;
                #endregion

                #region Действие 2: Списываем с source-карты
                await using var cmdSource = connection.CreateCommand();

                cmdSource.Transaction = transaction;
                cmdSource.CommandText = @"
                    UPDATE DebitCard
                    SET Balance = @newBalanceSource
                    WHERE Id = @idSource
                    AND Balance = @currentBalanceSource
                    AND CashBack = @currentCashbackSource";
                cmdSource.Parameters.AddWithValue("newBalanceSource", newBalanceSource);
                cmdSource.Parameters.AddWithValue("idSource", idSource);
                cmdSource.Parameters.AddWithValue("currentBalanceSource", currentBalanceSource);
                cmdSource.Parameters.AddWithValue("currentCashbackSource", currentCashbackSource);

                var readerSource = await cmdSource.ExecuteNonQueryAsync();

                if (readerSource == 0) return false;
                #endregion

                await transaction.CommitAsync();            // Если все хорошо: Сохраняем оба действия в БД разом
                return true;
            }
            catch
            {
                if (transaction != null) await transaction.RollbackAsync();
                throw;
            }            
        }
        #endregion

        #region Пополнить баланс дебетовой карты с счета (PaymentAccount - D)
        public async static Task<bool> ReplenishBalanceDebitCardFromPaymentAccountCard(int idTarget, decimal newBalanceTarget, decimal currentBalanceTarget, decimal currentCashbackTarget, int idSource, decimal newBalanceSource)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();     // Открываем транзакцию

            try
            {
                #region Действие 1: Зачисляем на target-карту
                await using var cmdTarget = connection.CreateCommand();

                cmdTarget.Transaction = transaction;            // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО
                cmdTarget.CommandText = @"
                    UPDATE DebitCard
                    SET Balance = @newBalanceTarget
                    WHERE Id = @idTarget
                    AND Balance = @currentBalanceTarget
                    AND CashBack = @currentCashbackTarget
                    AND Status = 1";
                cmdTarget.Parameters.AddWithValue("newBalanceTarget", newBalanceTarget);
                cmdTarget.Parameters.AddWithValue("idTarget", idTarget);
                cmdTarget.Parameters.AddWithValue("currentBalanceTarget", currentBalanceTarget);
                cmdTarget.Parameters.AddWithValue("currentCashbackTarget", currentCashbackTarget);

                var readerTarget = await cmdTarget.ExecuteNonQueryAsync();

                if (readerTarget == 0) return false;
                #endregion

                #region Действие 2: Списываем с source-счета
                await using var cmdSource = connection.CreateCommand();

                cmdSource.Transaction = transaction;                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО
                cmdSource.CommandText = @"
                    UPDATE PaymentAccount
                    SET Balance = @newBalanceSource
                    WHERE Id = @idSource
                    AND Status = 1";
                cmdSource.Parameters.AddWithValue("newBalanceSource", newBalanceSource);
                cmdSource.Parameters.AddWithValue("idSource", idSource);

                var readerSource = await cmdSource.ExecuteNonQueryAsync();

                if (readerSource == 0) return false;
                #endregion

                await transaction.CommitAsync();            // Если все хорошо: Сохраняем оба действия в БД разом
                return true;
            }
            catch
            {
                if (transaction != null) await transaction.RollbackAsync();
                throw;
            }
        }
        #endregion


        #region Поиск карт и счетов клиента по ID клиента
        public struct StructCheckCardsPaymentClient(List<BankCard> cards, List<PaymentAccount> paymentAccounts)
        {
            public List<BankCard> cards = cards;
            public List<PaymentAccount> paymentAccounts = paymentAccounts;
        }
        public async static Task<StructCheckCardsPaymentClient> FindCardsClient(int clientId)
        {
            List<BankCard> cards = [];
            List<PaymentAccount> paymentAccounts = [];

            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                #region Действие 1: Поиск дебетовых карт (DebitCard)
                await using var cmdDebit = connection.CreateCommand();

                cmdDebit.Transaction = transaction;             // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО
                cmdDebit.CommandText = @"
                    SELECT Id, CardNumber, Balance, CashBack, Status, Reason, CreateAt, CloseAt, IsCashbackEnabled
                    FROM DebitCard WHERE ClientId = @clientId";
                cmdDebit.Parameters.AddWithValue("clientId", clientId);

                await using (var readerDebit = await cmdDebit.ExecuteReaderAsync())
                {
                    while (await readerDebit.ReadAsync())
                    {
                        int idcard = readerDebit.GetInt32(0);
                        string cardnumber = readerDebit.GetString(1);
                        decimal balance = readerDebit.GetDecimal(2);
                        decimal cashback = readerDebit.GetDecimal(3);
                        int status = readerDebit.GetInt32(4);
                        string reason = readerDebit.IsDBNull(5) ? "" : readerDebit.GetString(5);
                        DateTime createAt = readerDebit.GetDateTime(6);
                        DateTime closeAt = readerDebit.IsDBNull(7) ? default : readerDebit.GetDateTime(7);
                        int isCashbackEnabled = readerDebit.GetInt32(8);

                        cards.Add(new DebitCard(idcard, clientId, cardnumber, balance, cashback, status, reason, createAt, closeAt, isCashbackEnabled));
                    }
                }     
                #endregion

                #region Действие 2: Поиск кредитных карт клиента (ПОКА ЧТО ПУСТО)

                #endregion

                #region Действие 3: Поиск счетов (PaymentAccount)
                await using var cmdPaymentAccount = connection.CreateCommand();

                cmdPaymentAccount.Transaction = transaction;             // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО
                cmdPaymentAccount.CommandText = @"
                    SELECT Id, AccountNumber, Bic, CorAccount, Balance, Currency, FrozenBalance, Status, OpenedAt, ClosedAt
                    FROM PaymentAccount WHERE ClientId = @clientId";
                cmdPaymentAccount.Parameters.AddWithValue("clientId", clientId);

                await using (var readerPayment = await cmdPaymentAccount.ExecuteReaderAsync())
                {
                    while (await readerPayment.ReadAsync())
                    {
                        int idPayment = readerPayment.GetInt32(0);
                        string accountNumber = readerPayment.GetString(1);
                        string bic = readerPayment.GetString(2);
                        string corAccount = readerPayment.GetString(3);
                        decimal balance = readerPayment.GetDecimal(4);
                        int currency = readerPayment.GetInt32(5);
                        decimal frozenBalance = readerPayment.GetDecimal(6);
                        int status = readerPayment.GetInt32(7);
                        DateTime openedAt = readerPayment.GetDateTime(8);
                        DateTime closedAt = readerPayment.IsDBNull(8) ? default : readerPayment.GetDateTime(8);

                        paymentAccounts.Add(new PaymentAccount(idPayment, clientId, bic, corAccount, balance, currency, frozenBalance, status, openedAt, closedAt, accountNumber));
                    }
                }                    
                #endregion

                await transaction.CommitAsync();                                           // Если все хорошо: Сохраняем все действия в БД разом

                return new StructCheckCardsPaymentClient(cards, paymentAccounts);            // Отдаем структуре полученные списоки
            }
            catch
            {
                if (transaction != null) await transaction.RollbackAsync();
                throw;
            }            
        }
        #endregion


        #region Проверка пароля (Авторизация)
        public readonly struct StructCheckPassword(int id, int clientType, string login, string password, string name1, string name2, string name3, string dateBirth, string numberPhone, string mail, DateTime createAt)
        {
            public readonly int Id = id;
            public readonly int ClientType = clientType;
            public readonly string Login = login;
            public readonly string Password = password;
            public readonly string Name1 = name1;
            public readonly string Name2 = name2;
            public readonly string Name3 = name3;
            public readonly string DateBirth = dateBirth;
            public readonly string NumberPhone = numberPhone;
            public readonly string Mail = mail;
            public readonly DateTime CreateAt = createAt;
        }
        public async static Task<StructCheckPassword> CheckPasswordAndOutDataClient(string login)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    SELECT Id, ClientType, Password, Name1, Name2, Name3, DateBirth, NumberPhone, Mail, CreateAt
                    FROM Client
                    WHERE Login = @login";
            command.Parameters.AddWithValue("login", login);

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                int id = reader.GetInt32(0);
                int clientType = reader.GetInt32(1);
                string password = reader.GetString(2);
                string name1 = reader.GetString(3);
                string name2 = reader.GetString(4);
                string name3 = reader.IsDBNull(5) ? "" : reader.GetString(5);
                string datebirth = reader.GetString(6);
                string numberPhone = reader.GetString(7);
                string mail = reader.GetString(8);
                DateTime createAt = reader.GetDateTime(9);

                return new StructCheckPassword(id, clientType, login, password, name1, name2, name3, datebirth, numberPhone, mail, createAt);
            }
            else return new StructCheckPassword();
        }
        #endregion

        #region Проверка логина (Регистрация)
        public async static Task<bool> CheckLoginForReg(string login)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    SELECT COUNT(*)
                    FROM Client 
                    WHERE Login = @login";
            command.Parameters.AddWithValue("login", login);

            var result = await command.ExecuteScalarAsync();
            int count = Convert.ToInt32(result);

            return count > 0;
        }
        #endregion


        #region Добавление клиента в БД без фамилии (Регистрация)
        public async static Task<bool> AddClientNoPatronymic(string login, string password, string name1, string name2, string datebirth, string numberPhone, string mail)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    INSERT INTO Client (Login, Password, Name1, Name2, DateBirth, NumberPhone, Mail)
                    VALUES (@login, @password, @name1, @name2, @datebirth, @numberphone, @mail)";
            command.Parameters.AddWithValue("login", login);
            command.Parameters.AddWithValue("password", password);
            command.Parameters.AddWithValue("name1", name1);
            command.Parameters.AddWithValue("name2", name2);
            command.Parameters.AddWithValue("datebirth", datebirth);
            command.Parameters.AddWithValue("numberphone", numberPhone);
            command.Parameters.AddWithValue("mail", mail);

            var reader = await command.ExecuteNonQueryAsync();

            return reader > 0;
        }
        #endregion

        #region Добавление клиента в БД с фамилией (Регистрация)
        public async static Task<bool> AddClientPatronymic(string login, string password, string name1, string name2, string name3, string datebirth, string numberPhone, string mail)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    INSERT INTO Client (Login, Password, Name1, Name2, Name3, DateBirth, NumberPhone, Mail)
                    VALUES (@login, @password, @name1, @name2, @name3, @datebirth, @numberphone, @mail)";
            command.Parameters.AddWithValue("login", login);
            command.Parameters.AddWithValue("password", password);
            command.Parameters.AddWithValue("name1", name1);
            command.Parameters.AddWithValue("name2", name2);
            command.Parameters.AddWithValue("name3", name3);
            command.Parameters.AddWithValue("datebirth", datebirth);
            command.Parameters.AddWithValue("numberphone", numberPhone);
            command.Parameters.AddWithValue("mail", mail);

            var reader = await command.ExecuteNonQueryAsync();

            return reader > 0;
        }
        #endregion


        #region Перевод с расчетного счета на дебетовую карту (Payment - D)
        public async static Task<bool> TransferPaymentToDebitCard(int idPayment, decimal newBalanceSource, decimal currentBalanceSource, int idCard, decimal newBalanceTarget, decimal currentBalanceTarget, decimal currentCashbackTarget)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();     // Открываем транзакцию

            try
            {
                #region Действие 1: Списываем с source
                await using var cmdSource = connection.CreateCommand();

                cmdSource.Transaction = transaction;         // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdSource.CommandText = @"
                    UPDATE PaymentAccount
                    SET Balance = @newBalanceSource
                    WHERE Id = @idPayment
                    AND Balance = @currentBalanceSource
                    AND Status = 1";
                cmdSource.Parameters.AddWithValue("newBalanceSource", newBalanceSource);
                cmdSource.Parameters.AddWithValue("idPayment", idPayment);
                cmdSource.Parameters.AddWithValue("currentBalanceSource", currentBalanceSource);

                int sourceRows = await cmdSource.ExecuteNonQueryAsync();

                if (sourceRows == 0) return false;
                #endregion

                #region Действие 2: Зачисляем на target
                await using var cmdTarget = connection.CreateCommand();

                cmdTarget.Transaction = transaction;         // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdTarget.CommandText = @"
                    UPDATE DebitCard
                    SET Balance = @newBalanceTarget
                    WHERE Id = @idCard
                    AND Balance = @currentBalanceTarget                    
                    AND CashBack = @currentCashbackTarget
                    AND Status = 1";
                cmdTarget.Parameters.AddWithValue("newBalanceTarget", newBalanceTarget);
                cmdTarget.Parameters.AddWithValue("idCard", idCard);
                cmdTarget.Parameters.AddWithValue("currentBalanceTarget", currentBalanceTarget);
                cmdTarget.Parameters.AddWithValue("currentCashbackTarget", currentCashbackTarget); ;

                int targetRows = await cmdTarget.ExecuteNonQueryAsync();

                if (targetRows == 0) return false;
                #endregion

                await transaction.CommitAsync();            // Если все хорошо: Сохраняем оба действия в БД разом
                return true;
            }
            catch
            {
                if (transaction != null) await transaction.RollbackAsync();
                throw;
            }
        }
        #endregion

        #region Перевод с расчетного счета на расчетный счет (Payment - Payment)
        public async static Task<bool> TransferPaymentToPayment(int idPayment, decimal newBalanceSource, decimal currentBalanceSource, int idPaymentTo, decimal newBalanceTarget, decimal currentBalanceTarget)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();     // Открываем транзакцию

            try
            {
                #region Действие 1: Списываем с source
                await using var cmdSource = connection.CreateCommand();

                cmdSource.Transaction = transaction;         // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdSource.CommandText = @"
                    UPDATE PaymentAccount
                    SET Balance = @newBalanceSource
                    WHERE Id = @idPayment
                    AND Balance = @currentBalanceSource
                    AND Status = 1";
                cmdSource.Parameters.AddWithValue("newBalanceSource", newBalanceSource);
                cmdSource.Parameters.AddWithValue("idPayment", idPayment);
                cmdSource.Parameters.AddWithValue("currentBalanceSource", currentBalanceSource);

                int sourceRows = await cmdSource.ExecuteNonQueryAsync();

                if (sourceRows == 0) return false;
                #endregion

                #region Действие 2: Зачисляем на target
                await using var cmdTarget = connection.CreateCommand();

                cmdTarget.Transaction = transaction;         // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdTarget.CommandText = @"
                    UPDATE PaymentAccount
                    SET Balance = @newBalanceSource
                    WHERE Id = @idPaymentTo
                    AND Balance = @currentBalanceSource
                    AND Status = 1";
                cmdTarget.Parameters.AddWithValue("newBalanceTarget", newBalanceTarget);
                cmdTarget.Parameters.AddWithValue("idPaymentTo", idPaymentTo);
                cmdTarget.Parameters.AddWithValue("currentBalanceTarget", currentBalanceTarget);

                int targetRows = await cmdTarget.ExecuteNonQueryAsync();

                if (targetRows == 0) return false;
                #endregion

                await transaction.CommitAsync();            // Если все хорошо: Сохраняем оба действия в БД разом
                return true;
            }
            catch
            {
                if (transaction != null) await transaction.RollbackAsync();
                throw;
            }
        }
        #endregion

        #region Перевод с дебетовой карты на расчетный счет (D - Payment)
        public async static Task<bool> TransferDebitCardToPayment(int idCard, decimal newBalanceSource, decimal currentBalanceSource, decimal currentCashbackSource, int idPayment, decimal newBalanceTarget, decimal currentBalanceTarget)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();     // Открываем транзакцию

            try
            {
                #region Действие 1: Списываем с source
                await using var cmdSource = connection.CreateCommand();

                cmdSource.Transaction = transaction;         // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdSource.CommandText = @"
                    UPDATE DebitCard
                    SET Balance = @newBalanceSource
                    WHERE Id = @idCard
                    AND Balance = @currentBalanceSource           
                    AND CashBack = @currentCashbackSource
                    AND Status = 1";
                cmdSource.Parameters.AddWithValue("newBalanceSource", newBalanceSource);
                cmdSource.Parameters.AddWithValue("idCard", idCard);
                cmdSource.Parameters.AddWithValue("currentBalanceSource", currentBalanceSource);
                cmdSource.Parameters.AddWithValue("currentCashbackSource", currentCashbackSource); ;

                int sourceRows = await cmdSource.ExecuteNonQueryAsync();

                if (sourceRows == 0) return false;
                #endregion

                #region Действие 2: Зачисляем на target
                await using var cmdTarget = connection.CreateCommand();

                cmdTarget.Transaction = transaction;         // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdTarget.CommandText = @"
                    UPDATE PaymentAccount
                    SET Balance = @newBalanceTarget
                    WHERE Id = @idPayment
                    AND Balance = @currentBalanceTarget
                    AND Status = 1";
                cmdTarget.Parameters.AddWithValue("newBalanceTarget", newBalanceTarget);
                cmdTarget.Parameters.AddWithValue("idPayment", idPayment);
                cmdTarget.Parameters.AddWithValue("currentBalanceTarget", currentBalanceTarget);

                int targetRows = await cmdSource.ExecuteNonQueryAsync();

                if (targetRows == 0) return false;
                #endregion

                await transaction.CommitAsync();            // Если все хорошо: Сохраняем оба действия в БД разом
                return true;
            }
            catch
            {
                if (transaction != null) await transaction.RollbackAsync();
                throw;
            }
        }
        #endregion

        #region Перевод с дебетовой карты на дебетовую карту (D - D)
        public async static Task<bool> TransferDebitCardToDebitCard(int idCard, decimal newBalanceSource, decimal currentBalanceSource, decimal currentCashbackSource,
                                                                    int idCardTarget, decimal newBalanceTarget, decimal currentBalanceTarget, decimal currentCashbackTarget)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();     // Открываем транзакцию

            try
            {
                #region Действие 1: Списываем с source
                await using var cmdSource = connection.CreateCommand();

                cmdSource.Transaction = transaction;         // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdSource.CommandText = @"
                    UPDATE DebitCard
                    SET Balance = @newBalanceSource
                    WHERE Id = @idCard
                    AND Balance = @currentBalanceSource           
                    AND CashBack = @currentCashbackSource
                    AND Status = 1";
                cmdSource.Parameters.AddWithValue("newBalanceSource", newBalanceSource);
                cmdSource.Parameters.AddWithValue("idCard", idCard);
                cmdSource.Parameters.AddWithValue("currentBalanceSource", currentBalanceSource);
                cmdSource.Parameters.AddWithValue("currentCashbackSource", currentCashbackSource); ;

                int sourceRows = await cmdSource.ExecuteNonQueryAsync();

                if (sourceRows == 0) return false;
                #endregion

                #region Действие 2: Зачисляем на target
                await using var cmdTarget = connection.CreateCommand();

                cmdTarget.Transaction = transaction;         // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdTarget.CommandText = @"
                    UPDATE DebitCard
                    SET Balance = @newBalanceTarget
                    WHERE Id = @idCardTarget
                    AND Balance = @currentBalanceTarget           
                    AND CashBack = @currentCashbackTarget
                    AND Status = 1";
                cmdTarget.Parameters.AddWithValue("newBalanceTarget", newBalanceTarget);
                cmdTarget.Parameters.AddWithValue("idCardTarget", idCardTarget);
                cmdTarget.Parameters.AddWithValue("currentBalanceTarget", currentBalanceTarget);
                cmdTarget.Parameters.AddWithValue("currentCashbackTarget", currentCashbackTarget);

                int targetRows = await cmdSource.ExecuteNonQueryAsync();

                if (targetRows == 0) return false;
                #endregion

                await transaction.CommitAsync();            // Если все хорошо: Сохраняем оба действия в БД разом
                return true;
            }
            catch
            {
                if (transaction != null) await transaction.RollbackAsync();
                throw;
            }
        }
        #endregion



        #region Аторизация/Регистрация

        //-------------------[Добавление лога в БД]-------------------//
        public async static Task AddLogs(int clientId, string level, string messege, string source)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    INSERT INTO Logs (ClientId, Level, Messege, Source)
                    VALUES (@clientId, @level, @messege, @source)";
            command.Parameters.AddWithValue("clientId", clientId); // ID клиента
            command.Parameters.AddWithValue("level", level); // INFO, WARNING, ERROR
            command.Parameters.AddWithValue("messege", messege); // Что произошло
            command.Parameters.AddWithValue("source", source); // Откуда вызов

            await command.ExecuteNonQueryAsync();
        }

        //-------------------[Добавление лога админа в БД]-------------------//
        public async static Task AddLogsAdmin(int idAdmin, string level, string doAdmin, string source)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    INSERT INTO LogsAdmins (IdAdmin, Level, Do, Source)
                    VALUES (@idAdmin, @level, @doAdmin, @source)";
            command.Parameters.AddWithValue("idAdmin", idAdmin); // ID админа
            command.Parameters.AddWithValue("level", level); // INFO, WARNING, ERROR
            command.Parameters.AddWithValue("doAdmin", doAdmin); // Что произошло
            command.Parameters.AddWithValue("source", source); // Откуда вызов

            await command.ExecuteNonQueryAsync();
        }
        

        //-------------------[Проверки почты при регистрации (true - занята, false - свободна)]-------------------//
        public async static Task<bool> CheckMailForReg(string mail)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    SELECT COUNT(*)
                    FROM Client
                    WHERE Mail = @mail";
            command.Parameters.AddWithValue("mail", mail);

            var result = await command.ExecuteScalarAsync();
            int count = Convert.ToInt32(result);

            return count > 0;
        }
        

        //-------------------[Проверка пароля (Панель Администратора)]-------------------//
        public readonly struct StructCheckPasswordAdmin(int id, string login, string password, string secureCode, int status, bool checkStatus)
        {
            public readonly int id = id;
            public readonly string login = login;
            public readonly string password = password;
            public readonly string secureCode = secureCode;
            public readonly bool checkStatus = checkStatus;
            public readonly int status = status;
        }
        public async static Task<StructCheckPasswordAdmin> CheckPasswordAndOutDataAdmin(string login)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    SELECT Id, Password, SecurityCode, Status
                    FROM Admins
                    WHERE Login = @login";
            command.Parameters.AddWithValue("login", login);

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                int id = reader.GetInt32(0);
                string password = reader.GetString(1);
                string secureCode = reader.GetString(2);
                int status = reader.GetInt32(3);

                return new StructCheckPasswordAdmin(id, login, password, secureCode, status, true);
            }
            else return new StructCheckPasswordAdmin();
        }


        //-------------------[Поиск уведомлений клиента (не прочитанные)]-------------------//
        public readonly struct StructNotificationsClient(List<Notifications> notifications)
        {
            public readonly List<Notifications> Notifications = notifications;
        }
        public async static Task<StructNotificationsClient> SearchNotificationsClient(int clientid)
        {
            // Создаем пустой список
            List<Notifications> notifications = [];

            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    SELECT Id, Title, Message, CreateAt
                    FROM Notifications
                    WHERE ClientId = @clientid
                    AND IsRead = 0";
            command.Parameters.AddWithValue("clientid", clientid);

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                int id = reader.GetInt32(0);
                string title = reader.GetString(1);
                string message = reader.GetString(2);
                DateTime timestamp = reader.GetDateTime(3);

                notifications.Add(new Notifications(id, clientid, title, message, timestamp));
            }

            // Отдаем структуре полученный список из открытых запросов клиента
            return new StructNotificationsClient(notifications);
        }

        #endregion



        #region Закрыть карту + закрыть Request (D)
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
        #endregion

        #region Закрыть карту + закрыть Request (C)
        public async static Task C_CloseAccept(int idCard, string answer, string comment, DateTime dateAnswer,
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
                    DELETE FROM CreditCard
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
        #endregion

        



        #region Открыть карту + закрыть Request (D)
        public async static Task D_AcceptOpenCard(int clientId, string newCardNumber, string answer, string comment, DateTime dateAnswer, int idRequest, string title)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (действия зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                #region Действие 1: Создаем карту
                await using var cmdOpenCard = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdOpenCard.Transaction = transaction;
                cmdOpenCard.CommandText = @"
                    INSERT INTO DebitCard (ClientId, CardNumber)
                    VALUES (@clientid, @cardnumber)";
                cmdOpenCard.Parameters.AddWithValue("clientid", clientId);
                cmdOpenCard.Parameters.AddWithValue("cardnumber", newCardNumber);

                // ExecuteNonQueryAsync вернет количество вставленных строк
                int openCardRows = await cmdOpenCard.ExecuteNonQueryAsync();

                if (openCardRows == 0) throw new Exception("Произошла непредвиденная ошибка. Пожалейства, повторите попытку позже.");
                #endregion

                #region Действие 2: Отправляем ответ на запрос
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

                // ExecuteNonQueryAsync вернет количество вставленных строк
                int updateRequestRows = await cmdUpdateRequest.ExecuteNonQueryAsync();

                if (updateRequestRows == 0) throw new Exception("Произошла непредвиденная ошибка! Пожалуйста, повторите попытку позже.");
                #endregion

                #region Действие 3: Отправляем уведомление клиенту
                await using var notifications = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                notifications.Transaction = transaction;
                notifications.CommandText = @"
                    INSERT INTO Notifications (ClientId, Title, Message)
                    VALUES (@clientId, @title, @message)";
                notifications.Parameters.AddWithValue("clientId", clientId);
                notifications.Parameters.AddWithValue("title", title);
                notifications.Parameters.AddWithValue("message", comment);

                // ExecuteNonQueryAsync вернет количество вставленных строк
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
        #endregion

        #region Открыть карту + закрыть Request (C)
        public async static Task C_AcceptOpenCard(int clientId, string newCardNumber, string answer, string comment, DateTime dateAnswer, int idRequest, string title)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (действия зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                #region Действие 1: Создаем карту
                await using var cmdOpenCard = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdOpenCard.Transaction = transaction;
                cmdOpenCard.CommandText = @"
                    INSERT INTO CreditCard (ClientId, CardNumber)
                    VALUES (@clientid, @cardnumber)";
                cmdOpenCard.Parameters.AddWithValue("clientid", clientId);
                cmdOpenCard.Parameters.AddWithValue("cardnumber", newCardNumber);

                // ExecuteNonQueryAsync вернет количество вставленных строк
                int openCardRows = await cmdOpenCard.ExecuteNonQueryAsync();

                if (openCardRows == 0) throw new Exception("Произошла непредвиденная ошибка. Пожалейства, повторите попытку позже.");
                #endregion

                #region Действие 2: Отправляем ответ на запрос
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

                // ExecuteNonQueryAsync вернет количество вставленных строк
                int updateRequestRows = await cmdUpdateRequest.ExecuteNonQueryAsync();

                if (updateRequestRows == 0) throw new Exception("Произошла непредвиденная ошибка! Пожалуйста, повторите попытку позже.");
                #endregion

                #region Действие 3: Отправляем уведомление клиенту
                await using var notifications = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                notifications.Transaction = transaction;
                notifications.CommandText = @"
                    INSERT INTO Notifications (ClientId, Title, Message)
                    VALUES (@clientId, @title, @message)";
                notifications.Parameters.AddWithValue("clientId", clientId);
                notifications.Parameters.AddWithValue("title", title);
                notifications.Parameters.AddWithValue("message", comment);

                // ExecuteNonQueryAsync вернет количество вставленных строк
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
        #endregion

        



        #region Отказано (Ответить на запрос - Открыть карту (D))
        public async static Task D_RefusalOpenCard(int idRequest, string answer, string comment, DateTime dateAnswer,
            int clientId, string title)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (оба действия зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                #region Действие 1: Обновляем запрос (даем ответ)
                await using var udpade = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                udpade.Transaction = transaction;
                udpade.CommandText = @"
                    UPDATE Request
                    SET Done = 1, Answer = @answer, Comment = @comment, DateAnswer = @dateAnswer
                    WHERE Id = @idRequest
                    AND Answer IS NULL
                    AND Comment IS NULL
                    AND DateAnswer IS NULL";
                udpade.Parameters.AddWithValue("answer", answer);
                udpade.Parameters.AddWithValue("comment", comment);
                udpade.Parameters.AddWithValue("dateAnswer", dateAnswer);
                udpade.Parameters.AddWithValue("idRequest", idRequest);

                var update = await udpade.ExecuteNonQueryAsync();

                if (update == 0) throw new Exception("Произошла непредвиденная ошибка. Пожалуйста, повторите попытку позже.");
                #endregion

                #region Действие 2: Отправляем уведомление клиенту
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

                // Сохраняем оба действия в БД разом (если все хорошо)
                await transaction.CommitAsync();
            }
            catch
            {
                if (transaction != null) await transaction.RollbackAsync();
                throw;
            }
        }
        #endregion

        #region Отказано (Ответить на запрос - Открыть карту (C))
        public async static Task C_RefusalOpenCard(int idRequest, string answer, string comment, DateTime dateAnswer,
            int clientId, string title)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (оба действия зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                #region Действие 1: Обновляем запрос (даем ответ)
                await using var udpade = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                udpade.Transaction = transaction;
                udpade.CommandText = @"
                    UPDATE Request
                    SET Done = 1, Answer = @answer, Comment = @comment, DateAnswer = @dateAnswer
                    WHERE Id = @idRequest
                    AND Answer IS NULL
                    AND Comment IS NULL
                    AND DateAnswer IS NULL";
                udpade.Parameters.AddWithValue("answer", answer);
                udpade.Parameters.AddWithValue("comment", comment);
                udpade.Parameters.AddWithValue("dateAnswer", dateAnswer);
                udpade.Parameters.AddWithValue("idRequest", idRequest);

                var update = await udpade.ExecuteNonQueryAsync();

                if (update == 0) throw new Exception("Произошла непредвиденная ошибка. Пожалуйста, повторите попытку позже.");
                #endregion

                #region Действие 2: Отправляем уведомление клиенту
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

                // Сохраняем оба действия в БД разом (если все хорошо)
                await transaction.CommitAsync();
            }
            catch
            {
                if (transaction != null) await transaction.RollbackAsync();
                throw;
            }
        }
        #endregion

        


        #region Запросы


        #region Запрос - Измененить лимита депозита (D)
        public async static Task<bool> D_RequestChangeLimitDeposit(int clientId, string defcard, string typeCard, int idCard, string message, decimal newdeposit)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (оба действия зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                // Действие 1: Проверяем статус карты (в зависимости от типа карты)
                await using var cmdSelect = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdSelect.Transaction = transaction;
                cmdSelect.CommandText = @"
                    SELECT 1
                    FROM DebitCard
                    WHERE Id = @idCard
                    AND Status = 1";
                cmdSelect.Parameters.AddWithValue("idCard", idCard);

                // ExecuteNonQueryAsync вернет количество найденных строк (должна быть одна строка)
                var select = await cmdSelect.ExecuteNonQueryAsync();

                if (select == 0) throw new Exception("Произошла непредвиденная ошибка! Пожалуйста, повторите попытку позже.");

                // Действие 2: Отправляем запрос в БД
                await using var cmdUpdate = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdUpdate.Transaction = transaction;
                cmdUpdate.CommandText = @"
                    INSERT INTO Request (ClientId, Type, DefCard, TypeCard, IdCard, Message, Done, NewDeposit)
                    SELECT @clientId, @type, @defcard, @typeCard, @idCard, @message, @done, @newdeposit
                    FROM DUAL
                    WHERE NOT EXISTS (
                        SELECT 1 
                        FROM Request 
                        WHERE ClientId = @clientId 
                        AND Type = @type 
                        AND Done = 0);";
                cmdUpdate.Parameters.AddWithValue("clientId", clientId); // ID клиента (отправивший запрос)
                cmdUpdate.Parameters.AddWithValue("type", 1); // 1 - лимит депозита, 2 - удалить карту, 3 - открыть карту
                cmdUpdate.Parameters.AddWithValue("defcard", defcard); // Номер карты
                cmdUpdate.Parameters.AddWithValue("typeCard", typeCard); // Тип карты (U, D, C, B)
                cmdUpdate.Parameters.AddWithValue("idCard", idCard); // ID карты в БД
                cmdUpdate.Parameters.AddWithValue("message", message); // Описание
                cmdUpdate.Parameters.AddWithValue("done", 0); // 0 - на рассмотрении, 1 - рассмотренно
                cmdUpdate.Parameters.AddWithValue("newdeposit", newdeposit); // новый депозит (если type 1, иначе NULL)

                int update = await cmdUpdate.ExecuteNonQueryAsync();

                if (update == 0) throw new Exception("Вы уже отправляли заявку такого типа (Изменение депозита). Пожалуйста, дождитесь ее рассмотрения.");

                // Сохраняем оба действия в БД разом (если все хорошо)
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                // В случае любой непредвиденной ошибки отменяем все изменения
                if (transaction != null) await transaction.RollbackAsync();
                throw;
            }
        }
        #endregion

        #region Запрос - Измененить лимита депозита (C)
        public async static Task<bool> C_RequestChangeLimitDeposit(int clientId, string defcard, string typeCard, int idCard, string message, decimal newdeposit)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (оба действия зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                // Действие 1: Узнаем фактическую задолженность
                await using var cmdGetDebt = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdGetDebt.Transaction = transaction;
                cmdGetDebt.CommandText = @"
                    SELECT Debt
                    FROM CreditCard
                    WHERE Id = @idCard
                    AND Status = 1";
                cmdGetDebt.Parameters.AddWithValue("idCard", idCard);

                decimal factDebt = -1;                

                // "Достаем" задолженность из полученной строки
                await using (var reader = await cmdGetDebt.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        factDebt = reader.GetDecimal(0);
                    }
                }

                // Ппрвоеряем, нашлась ли карта и действительно ли задоолженность равна 0
                if (factDebt == -1 & factDebt != 0) throw new Exception("Произошла непредвиденная ошибка! Пожалуйста, повторите попытку позже.");

                // Действие 2: Отправляем запрос в БД
                await using var cmdUpdate = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdUpdate.Transaction = transaction;
                cmdUpdate.CommandText = @"
                    INSERT INTO Request (ClientId, Type, DefCard, TypeCard, IdCard, Message, Done, NewDeposit)
                    SELECT @clientId, @type, @defcard, @typeCard, @idCard, @message, @done, @newdeposit
                    FROM DUAL
                    WHERE NOT EXISTS (
                        SELECT 1 
                        FROM Request 
                        WHERE ClientId = @clientId 
                        AND Type = @type 
                        AND Done = 0);";
                cmdUpdate.Parameters.AddWithValue("clientId", clientId); // ID клиента (отправивший запрос)
                cmdUpdate.Parameters.AddWithValue("type", 1); // 1 - лимит депозита, 2 - удалить карту, 3 - открыть карту
                cmdUpdate.Parameters.AddWithValue("defcard", defcard); // Номер карты
                cmdUpdate.Parameters.AddWithValue("typeCard", typeCard); // Тип карты (U, D, C, B)
                cmdUpdate.Parameters.AddWithValue("idCard", idCard); // ID карты в БД
                cmdUpdate.Parameters.AddWithValue("message", message); // Описание
                cmdUpdate.Parameters.AddWithValue("done", 0); // 0 - на рассмотрении, 1 - рассмотренно
                cmdUpdate.Parameters.AddWithValue("newdeposit", newdeposit); // новый депозит (если type 1, иначе NULL)

                int update = await cmdUpdate.ExecuteNonQueryAsync();

                if (update == 0) throw new Exception("Вы уже отправляли заявку такого типа (Изменение депозита). Пожалуйста, дождитесь ее рассмотрения.");

                // Сохраняем оба действия в БД разом (если все хорошо)
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                // В случае любой непредвиденной ошибки отменяем все изменения
                if (transaction != null) await transaction.RollbackAsync();
                throw;
            }
        }
        #endregion

        



        #region Одобрено (Изменить лимит депозита) + закрыть Request (D)
        public async static Task D_ChangeLimitDepositAndAnswerRequest(decimal newLimit, int idCard, string answer, string comment, DateTime dateAnswer, int idRequest,
            int clientId, string title)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (оба действия зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                #region Действие 1: Меняем лимит депозита
                await using var cmdUpdateDeposit = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdUpdateDeposit.Transaction = transaction;
                cmdUpdateDeposit.CommandText = @"
                    UPDATE DebitCard
                    SET LimitDeposit = @newLimit
                    WHERE Id = @idCard
                    AND Status = 1";
                cmdUpdateDeposit.Parameters.AddWithValue("newLimit", newLimit);
                cmdUpdateDeposit.Parameters.AddWithValue("idCard", idCard);

                // ExecuteNonQueryAsync вернет количество вставленных строк
                int updateDepositRows = await cmdUpdateDeposit.ExecuteNonQueryAsync();

                // Если депозит не сменился (по какой-то причине)
                if (updateDepositRows == 0) throw new Exception("Произошла непредвиденная ошибка. Пожалуйста, повторите попытку позже.");
                #endregion

                #region Действие 2: Отправляем ответ на запрос
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

                // Если ответ не записался в БД (по какой-то причине)
                if (updateRequestRows == 0) throw new Exception("Произошла непредвиденная ошибка! Пожалуйста, повторите попытку позже.");
                #endregion

                #region Действие 3: Отправляем уведомление клиенту
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
        #endregion

        #region Одобрено (Изменить лимит депозита) + закрыть Request (C)
        public async static Task C_ChangeLimitDepositAndAnswerRequest(decimal newLimit, int idCard, string answer, string comment, DateTime dateAnswer, int idRequest,
            int clientId, string title)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (оба действия зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                #region Действие 1: Меняем лимит депозита
                await using var cmdUpdateDeposit = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdUpdateDeposit.Transaction = transaction;
                cmdUpdateDeposit.CommandText = @"
                    UPDATE CreditCard
                    SET LimitDeposit = @newLimit
                    WHERE Id = @idCard
                    AND Status = 1";
                cmdUpdateDeposit.Parameters.AddWithValue("newLimit", newLimit);
                cmdUpdateDeposit.Parameters.AddWithValue("idCard", idCard);

                // ExecuteNonQueryAsync вернет количество вставленных строк
                int updateDepositRows = await cmdUpdateDeposit.ExecuteNonQueryAsync();

                // Если депозит не сменился (по какой-то причине)
                if (updateDepositRows == 0) throw new Exception("Произошла непредвиденная ошибка. Пожалуйста, повторите попытку позже.");
                #endregion

                #region Действие 2: Отправляем ответ на запрос
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

                // Если ответ не записался в БД (по какой-то причине)
                if (updateRequestRows == 0) throw new Exception("Произошла непредвиденная ошибка! Пожалуйста, повторите попытку позже.");
                #endregion

                #region Действие 3: Отправляем уведомление клиенту
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
        #endregion

        



        #region Запрос - Закрыть карту (D)
        public async static Task D_RequestCloseCard(int clientId, string defcard, string typeCard, int idCard, string message,
                                decimal current_balance, decimal current_deposit, decimal current_limit, decimal current_cashback)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (команды зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                #region Действие 1: Создаем заявку в таблице Request
                await using var cmdInsert = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdInsert.Transaction = transaction;
                cmdInsert.CommandText = @"
                    INSERT INTO Request (ClientId, Type, DefCard, TypeCard, IdCard, Message, Done)
                    SELECT @clientId, @type, @defcard, @typeCard, @idCard, @message, @done
                    FROM DUAL
                    WHERE NOT EXISTS (
                        SELECT 1
                        FROM Request 
                        WHERE ClientId = @clientId 
                        AND Type = @type 
                        AND Done = 0);";
                cmdInsert.Parameters.AddWithValue("clientId", clientId); // ID клиента (отправивший запрос)
                cmdInsert.Parameters.AddWithValue("type", 2); // 1 - лимит депозита, 2 - удалить карту, 3 - открыть карту
                cmdInsert.Parameters.AddWithValue("defcard", defcard); // Номер карты
                cmdInsert.Parameters.AddWithValue("typeCard", typeCard); // Тип карты
                cmdInsert.Parameters.AddWithValue("idCard", idCard); // ID карты в БД
                cmdInsert.Parameters.AddWithValue("message", message); // Описание
                cmdInsert.Parameters.AddWithValue("done", 0); // 0 - на рассмотрении, 1 - рассмотренно

                // ExecuteNonQueryAsync вернет количество вставленных строк
                int insertRows = await cmdInsert.ExecuteNonQueryAsync();

                // Если такая активная заявка уже была (вставка не произошла), прерываем процесс
                if (insertRows == 0) throw new Exception("Вы уже отправляли заявку такого типа! Дождитесь ее рассмотрения.");
                #endregion

                #region Действие 2: Меняем статус карты в таблице DebitCard
                await using var cmdUpdate = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdUpdate.Transaction = transaction;
                cmdUpdate.CommandText = @"
                    UPDATE DebitCard 
                    SET Status = 0, Reason = @reason
                    WHERE Id = @idCard
                    AND Balance = @current_balance
                    AND Deposit = @current_deposit
                    AND LimitDeposit = @current_limit
                    AND CashBack = @current_cashback
                    AND Status = 1
                    AND Reason IS NULL";
                cmdUpdate.Parameters.AddWithValue("idCard", idCard);
                cmdUpdate.Parameters.AddWithValue("current_balance", current_balance);
                cmdUpdate.Parameters.AddWithValue("current_deposit", current_deposit);
                cmdUpdate.Parameters.AddWithValue("current_limit", current_limit);
                cmdUpdate.Parameters.AddWithValue("current_cashback", current_cashback);
                cmdUpdate.Parameters.AddWithValue("reason", $"Заявка - Закрыть карту");

                // ExecuteNonQueryAsync вернет количество вставленных строк
                int updateRows = await cmdUpdate.ExecuteNonQueryAsync();

                if (updateRows == 0) throw new Exception("Произошла непредвиденная ошибка! Пожалуйста, повторите попытку позже.");
                #endregion

                // Сохраняем оба действия в БД разом (если все хорошо)
                await transaction.CommitAsync();
            }
            catch
            {
                // В случае любой непредвиденной ошибки отменяем все изменения
                if (transaction != null) await transaction.RollbackAsync();
                throw;
            }
        }
        #endregion

        #region Запрос - Закрыть карту (C)
        public async static Task C_RequestCloseCard(int clientId, string defcard, string typeCard, int idCard, string message,
                                decimal current_balance, decimal current_deposit, decimal current_limit, decimal current_debt)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (команды зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                #region Действие 1: Создаем заявку в таблице Request
                await using var cmdInsert = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdInsert.Transaction = transaction;
                cmdInsert.CommandText = @"
                    INSERT INTO Request (ClientId, Type, DefCard, TypeCard, IdCard, Message, Done)
                    SELECT @clientId, @type, @defcard, @typeCard, @idCard, @message, @done
                    FROM DUAL
                    WHERE NOT EXISTS (
                        SELECT 1
                        FROM Request 
                        WHERE ClientId = @clientId 
                        AND Type = @type 
                        AND Done = 0);";
                cmdInsert.Parameters.AddWithValue("clientId", clientId); // ID клиента (отправивший запрос)
                cmdInsert.Parameters.AddWithValue("type", 2); // 1 - лимит депозита, 2 - удалить карту, 3 - открыть карту
                cmdInsert.Parameters.AddWithValue("defcard", defcard); // Номер карты
                cmdInsert.Parameters.AddWithValue("typeCard", typeCard); // Тип карты
                cmdInsert.Parameters.AddWithValue("idCard", idCard); // ID карты в БД
                cmdInsert.Parameters.AddWithValue("message", message); // Описание
                cmdInsert.Parameters.AddWithValue("done", 0); // 0 - на рассмотрении, 1 - рассмотренно

                // ExecuteNonQueryAsync вернет количество вставленных строк
                int insertRows = await cmdInsert.ExecuteNonQueryAsync();

                // Если такая активная заявка уже была (вставка не произошла), прерываем процесс
                if (insertRows == 0) throw new Exception("Вы уже отправляли заявку такого типа! Дождитесь ее рассмотрения.");
                #endregion

                #region Действие 2: Меняем статус карты в таблице CreditCard
                await using var cmdUpdate = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdUpdate.Transaction = transaction;
                cmdUpdate.CommandText = @"
                    UPDATE CreditCard 
                    SET Status = 0, Reason = @reason
                    WHERE Id = @idCard
                    AND Balance = @current_balance
                    AND Deposit = @current_deposit
                    AND LimitDeposit = @current_limit
                    AND Debt = @current_debt
                    AND Status = 1
                    AND Reason IS NULL";
                cmdUpdate.Parameters.AddWithValue("idCard", idCard);
                cmdUpdate.Parameters.AddWithValue("current_balance", current_balance);
                cmdUpdate.Parameters.AddWithValue("current_deposit", current_deposit);
                cmdUpdate.Parameters.AddWithValue("current_limit", current_limit);
                cmdUpdate.Parameters.AddWithValue("current_debt", current_debt);
                cmdUpdate.Parameters.AddWithValue("reason", $"Заявка - Закрыть карту");

                int updateRows = await cmdUpdate.ExecuteNonQueryAsync();

                // Если карта изменилась, фиксируем транзакцию. Если нет — отменяем всё.
                if (updateRows == 0) throw new Exception("Произошла непредвиденная ошибка! Пожалуйста, повторите попытку позже.");
                #endregion

                // Сохраняем оба действия в БД разом (если все хорошо)
                await transaction.CommitAsync();
            }
            catch
            {
                // В случае любой непредвиденной ошибки отменяем все изменения
                if (transaction != null) await transaction.RollbackAsync();
                throw;
            }
        }
        #endregion

        #region Запрос - Закрыть карту (B)
        public async static Task B_RequestCloseCard(int clientId, string defcard, string typeCard, int idCard, string message,
                                decimal current_balance, decimal current_deposit, decimal current_limit)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (команды зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                #region Действие 1: Создаем заявку в таблице Request
                await using var cmdInsert = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdInsert.Transaction = transaction;
                cmdInsert.CommandText = @"
                    INSERT INTO Request (ClientId, Type, DefCard, TypeCard, IdCard, Message, Done)
                    SELECT @clientId, @type, @defcard, @typeCard, @idCard, @message, @done
                    FROM DUAL
                    WHERE NOT EXISTS (
                        SELECT 1
                        FROM Request 
                        WHERE ClientId = @clientId 
                        AND Type = @type 
                        AND Done = 0);";
                cmdInsert.Parameters.AddWithValue("clientId", clientId); // ID клиента (отправивший запрос)
                cmdInsert.Parameters.AddWithValue("type", 2); // 1 - лимит депозита, 2 - удалить карту, 3 - открыть карту
                cmdInsert.Parameters.AddWithValue("defcard", defcard); // Номер карты
                cmdInsert.Parameters.AddWithValue("typeCard", typeCard); // Тип карты (U, D, C, B)
                cmdInsert.Parameters.AddWithValue("idCard", idCard); // ID карты в БД
                cmdInsert.Parameters.AddWithValue("message", message); // Описание
                cmdInsert.Parameters.AddWithValue("done", 0); // 0 - на рассмотрении, 1 - рассмотренно

                // ExecuteNonQueryAsync вернет количество вставленных строк
                int insertRows = await cmdInsert.ExecuteNonQueryAsync();

                if (insertRows == 0) throw new Exception("Вы уже отправляли заявку такого типа! Дождитесь ее рассмотрения.");
                #endregion

                #region Действие 2: Меняем статус карты в таблице BusinessCard
                await using var cmdUpdate = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdUpdate.Transaction = transaction;
                cmdUpdate.CommandText = @"
                    UPDATE BusinessCard 
                    SET Status = 0, Reason = @reason
                    WHERE Id = @idCard
                    AND Balance = @current_balance
                    AND Deposit = @current_deposit
                    AND LimitDeposit = @current_limit
                    AND Status = 1
                    AND Reason IS NULL";
                cmdUpdate.Parameters.AddWithValue("idCard", idCard);
                cmdUpdate.Parameters.AddWithValue("current_balance", current_balance);
                cmdUpdate.Parameters.AddWithValue("current_deposit", current_deposit);
                cmdUpdate.Parameters.AddWithValue("current_limit", current_limit);
                cmdUpdate.Parameters.AddWithValue("reason", $"Заявка - Закрыть карту");

                int updateRows = await cmdUpdate.ExecuteNonQueryAsync();

                if (updateRows == 0) throw new Exception("Произошла непредвиденная ошибка! Пожалуйста, повторите попытку позже.");
                #endregion

                // Сохраняем оба действия в БД разом (если все хорошо)
                await transaction.CommitAsync();
            }
            catch
            {
                // В случае любой непредвиденной ошибки отменяем все изменения
                if (transaction != null) await transaction.RollbackAsync();
                throw;
            }
        }
        #endregion



        #region Запрос - Открыть карту (D)
        public async static Task D_RequestOpenCard(int clientId, string typeCard, string message, int countDebit)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (оба действия зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                #region Действие 1: Создаем заявку в таблице Request
                await using var cmdInsert = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdInsert.Transaction = transaction;
                cmdInsert.CommandText = @"
                    INSERT INTO Request (ClientId, Type, TypeCard, Message, Done)
                    SELECT @clientId, @type, @typeCard, @message, @done
                    FROM DUAL
                    WHERE NOT EXISTS (
                        SELECT 1 
                        FROM Request 
                        WHERE ClientId = @clientId 
                        AND Type = @type 
                        AND Done = 0);";
                cmdInsert.Parameters.AddWithValue("clientId", clientId); // ID клиента (отправивший запрос)
                cmdInsert.Parameters.AddWithValue("type", 3); // 1 - лимит депозита, 2 - удалить карту, 3 - открыть карту
                cmdInsert.Parameters.AddWithValue("typeCard", typeCard); // Тип карты
                cmdInsert.Parameters.AddWithValue("message", message); // Описание
                cmdInsert.Parameters.AddWithValue("done", 0); // 0 - на рассмотрении, 1 - рассмотренно

                // ExecuteNonQueryAsync вернет количество вставленных строк
                int insertRows = await cmdInsert.ExecuteNonQueryAsync();

                if (insertRows == 0) throw new Exception("Вы уже отправляли заявку такого типа! Дождитесь ее рассмотрения.");
                #endregion

                #region Действие 2: Считаем количество открытых дебетовых карт клиента
                await using var cmdCount = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdCount.Transaction = transaction;
                cmdCount.CommandText = @"
                    SELECT COUNT(*)
                    FROM DebitCard 
                    WHERE ClientId = @clientid";
                cmdCount.Parameters.AddWithValue("clientid", clientId);

                var countDebitDB = Convert.ToInt32(await cmdCount.ExecuteScalarAsync());

                //  Проверяем совпадение количества и общее количество
                if (countDebitDB != countDebit) throw new Exception("Возникла непредвиденная ошибка! Пожалуйста, повторите попытку позже.");
                if (countDebitDB == 3) throw new Exception("У Вас максимальное количество открытых обычных карт!");
                #endregion

                // Сохраняем оба действия в БД разом (если все хорошо)
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // В случае любой непредвиденной ошибки отменяем все изменения
                if (transaction != null) await transaction.RollbackAsync();
                throw;
            }
        }
        #endregion

        #region Запрос - Открыть карту (C)
        public async static Task C_RequestOpenCard(int clientId, string typeCard, string message, int countCredit)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (оба действия зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                #region Действие 1: Создаем заявку в таблице Request
                await using var cmdInsert = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdInsert.Transaction = transaction;
                cmdInsert.CommandText = @"
                    INSERT INTO Request (ClientId, Type, TypeCard, Message, Done)
                    SELECT @clientId, @type, @typeCard, @message, @done
                    FROM DUAL
                    WHERE NOT EXISTS (
                        SELECT 1 
                        FROM Request 
                        WHERE ClientId = @clientId 
                        AND Type = @type 
                        AND Done = 0);";
                cmdInsert.Parameters.AddWithValue("clientId", clientId); // ID клиента (отправивший запрос)
                cmdInsert.Parameters.AddWithValue("type", 3); // 1 - лимит депозита, 2 - удалить карту, 3 - открыть карту
                cmdInsert.Parameters.AddWithValue("typeCard", typeCard); // Тип карты
                cmdInsert.Parameters.AddWithValue("message", message); // Описание
                cmdInsert.Parameters.AddWithValue("done", 0); // 0 - на рассмотрении, 1 - рассмотренно

                // ExecuteNonQueryAsync вернет количество вставленных строк
                int insertRows = await cmdInsert.ExecuteNonQueryAsync();

                if (insertRows == 0) throw new Exception("Вы уже отправляли заявку такого типа! Дождитесь ее рассмотрения.");
                #endregion

                #region Действие 2: Считаем количество открытых дебетовых карт клиента
                await using var cmdCount = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdCount.Transaction = transaction;
                cmdCount.CommandText = @"
                    SELECT COUNT(*)
                    FROM CreditCard 
                    WHERE ClientId = @clientid";
                cmdCount.Parameters.AddWithValue("clientid", clientId);

                var countCreditDB = Convert.ToInt32(await cmdCount.ExecuteScalarAsync());

                //  Проверяем совпадение количества и общее количество
                if (countCreditDB != countCredit) throw new Exception("Возникла непредвиденная ошибка! Пожалуйста, повторите попытку позже.");
                if (countCreditDB == 3) throw new Exception("У Вас максимальное количество открытых обычных карт!");
                #endregion

                // Сохраняем оба действия в БД разом (если все хорошо)
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // В случае любой непредвиденной ошибки отменяем все изменения
                if (transaction != null) await transaction.RollbackAsync();
                throw;
            }
        }
        #endregion

        #region Запрос - Открыть карту (B)
        public async static Task B_RequestOpenCard(int clientId, string typeCard, string message, int countBussines)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (оба действия зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                #region Действие 1: Создаем заявку в таблице Request
                await using var cmdInsert = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdInsert.Transaction = transaction;
                cmdInsert.CommandText = @"
                    INSERT INTO Request (ClientId, Type, TypeCard, Message, Done)
                    SELECT @clientId, @type, @typeCard, @message, @done
                    FROM DUAL
                    WHERE NOT EXISTS (
                        SELECT 1 
                        FROM Request 
                        WHERE ClientId = @clientId 
                        AND Type = @type 
                        AND Done = 0);";
                cmdInsert.Parameters.AddWithValue("clientId", clientId); // ID клиента (отправивший запрос)
                cmdInsert.Parameters.AddWithValue("type", 3); // 1 - лимит депозита, 2 - удалить карту, 3 - открыть карту
                cmdInsert.Parameters.AddWithValue("typeCard", typeCard); // Тип карты
                cmdInsert.Parameters.AddWithValue("message", message); // Описание
                cmdInsert.Parameters.AddWithValue("done", 0); // 0 - на рассмотрении, 1 - рассмотренно

                // ExecuteNonQueryAsync вернет количество вставленных строк
                int insertRows = await cmdInsert.ExecuteNonQueryAsync();

                if (insertRows == 0) throw new Exception("Вы уже отправляли заявку такого типа! Дождитесь ее рассмотрения.");
                #endregion

                #region Действие 2: Считаем количество открытых дебетовых карт клиента
                await using var cmdCount = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdCount.Transaction = transaction;
                cmdCount.CommandText = @"
                    SELECT COUNT(*)
                    FROM BusinessCard 
                    WHERE ClientId = @clientid";
                cmdCount.Parameters.AddWithValue("clientid", clientId);

                var countBussinesDB = Convert.ToInt32(await cmdCount.ExecuteScalarAsync());

                //  Проверяем совпадение количества и общее количество
                if (countBussinesDB != countBussines) throw new Exception("Возникла непредвиденная ошибка! Пожалуйста, повторите попытку позже.");
                if (countBussinesDB == 3) throw new Exception("У Вас максимальное количество открытых обычных карт!");
                #endregion

                // Сохраняем оба действия в БД разом (если все хорошо)
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // В случае любой непредвиденной ошибки отменяем все изменения
                if (transaction != null) await transaction.RollbackAsync();
                throw;
            }
        }
        #endregion


        #region Поиск открытых запросов (клиент)
        //public readonly struct StructRequestClient(List<Request> request)
        //{
        //    public readonly List<Request> Request = request;
        //}
        //public async static Task<StructRequestClient> SearchRequestClient(int clientid)
        //{
        //    // Создаем пустой список (в него будем "складывать" все найденные открытые запросы)
        //    List<Request> request = [];

        //    var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

        //    await using var connection = new MySqlConnection(builder.ConnectionString);
        //    await connection.OpenAsync();

        //    await using var command = connection.CreateCommand();
        //    command.CommandText = @"
        //            SELECT Id, Type, DefCard, TypeCard, Timestamp, NewDeposit
        //            FROM Request
        //            WHERE ClientId = @clientid
        //            AND Done = 0";
        //    command.Parameters.AddWithValue("clientid", clientid);

        //    await using var reader = await command.ExecuteReaderAsync();

        //    while (await reader.ReadAsync())
        //    {
        //        int id = reader.GetInt32(0);
        //        int type = reader.GetInt32(1);
        //        string defcard = reader.IsDBNull(2) ? "" : reader.GetString(2);
        //        string typeCard = reader.GetString(3);
        //        DateTime timestamp = reader.GetDateTime(4);
        //        decimal new_deposit = reader.IsDBNull(5) ? 0m : reader.GetDecimal(5);

        //        request.Add(new Request(id, type, defcard, typeCard, timestamp, new_deposit));
        //    }

        //    // Отдаем структуре полученный список из открытых запросов клиента
        //    return new StructRequestClient(request);
        //}
        #endregion



        #region Отказано (Ответить на запрос - Закрыть карту (D))
        public async static Task D_RefusalClose(int idRequest, string answer, string comment, DateTime dateAnswer,
            int clientId, string title, int idCard)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (оба действия зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                #region Действие 1: Обновляем запрос (даем ответ)
                await using var udpade = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                udpade.Transaction = transaction;
                udpade.CommandText = @"
                    UPDATE Request
                    SET Done = 1, Answer = @answer, Comment = @comment, DateAnswer = @dateAnswer
                    WHERE Id = @idRequest
                    AND Answer IS NULL
                    AND Comment IS NULL
                    AND DateAnswer IS NULL";
                udpade.Parameters.AddWithValue("answer", answer);
                udpade.Parameters.AddWithValue("comment", comment);
                udpade.Parameters.AddWithValue("dateAnswer", dateAnswer);
                udpade.Parameters.AddWithValue("idRequest", idRequest);

                var update = await udpade.ExecuteNonQueryAsync();

                if (update == 0) throw new Exception("Произошла непредвиденная ошибка. Пожалуйста, повторите попытку позже.");
                #endregion

                #region Действие 2: Разблокируем карту
                await using var unlock = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                unlock.Transaction = transaction;
                unlock.CommandText = @"
                    UPDATE DebitCard
                    SET Status = 1, Reason = NULL
                    WHERE Id = @idCard";
                unlock.Parameters.AddWithValue("idCard", idCard);

                var unlockRows = await unlock.ExecuteNonQueryAsync();

                if (unlockRows == 0) throw new Exception("Произошла непредвиденная ошибка. Пожалуйста, повторите попытку позже.");
                #endregion

                #region Действие 3: Отправляем уведомление клиенту
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

                // Сохраняем оба действия в БД разом (если все хорошо)
                await transaction.CommitAsync();
            }
            catch
            {
                if (transaction != null) await transaction.RollbackAsync();
                throw;
            }
        }
        #endregion

        #region Отказано (Ответить на запрос - Закрыть карту (C))
        public async static Task C_RefusalClose(int idRequest, string answer, string comment, DateTime dateAnswer,
            int clientId, string title, int idCard)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (оба действия зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                #region Действие 1: Обновляем запрос (даем ответ)
                await using var udpade = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                udpade.Transaction = transaction;
                udpade.CommandText = @"
                    UPDATE Request
                    SET Done = 1, Answer = @answer, Comment = @comment, DateAnswer = @dateAnswer
                    WHERE Id = @idRequest
                    AND Answer IS NULL
                    AND Comment IS NULL
                    AND DateAnswer IS NULL";
                udpade.Parameters.AddWithValue("answer", answer);
                udpade.Parameters.AddWithValue("comment", comment);
                udpade.Parameters.AddWithValue("dateAnswer", dateAnswer);
                udpade.Parameters.AddWithValue("idRequest", idRequest);

                var update = await udpade.ExecuteNonQueryAsync();

                if (update == 0) throw new Exception("Произошла непредвиденная ошибка. Пожалуйста, повторите попытку позже.");
                #endregion

                #region Действие 2: Разблокируем карту
                await using var unlock = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                unlock.Transaction = transaction;
                unlock.CommandText = @"
                    UPDATE CreditCard
                    SET Status = 1, Reason = NULL
                    WHERE Id = @idCard";
                unlock.Parameters.AddWithValue("idCard", idCard);

                var unlockRows = await unlock.ExecuteNonQueryAsync();

                if (unlockRows == 0) throw new Exception("Произошла непредвиденная ошибка. Пожалуйста, повторите попытку позже.");
                #endregion

                #region Действие 3: Отправляем уведомление клиенту
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

                // Сохраняем оба действия в БД разом (если все хорошо)
                await transaction.CommitAsync();
            }
            catch
            {
                if (transaction != null) await transaction.RollbackAsync();
                throw;
            }
        }
        #endregion



        #region Уведомление (simple)
        public async static Task AddNotification(int clientId, string title, string message)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var notifications = connection.CreateCommand();
            notifications.CommandText = @"
                    INSERT INTO Notifications (ClientId, Title, Message)
                    VALUES (@clientId, @title, @message)";
            notifications.Parameters.AddWithValue("clientId", clientId);
            notifications.Parameters.AddWithValue("title", title);
            notifications.Parameters.AddWithValue("message", message);

            await notifications.ExecuteNonQueryAsync();
        }
        #endregion

        #endregion
    }
}
