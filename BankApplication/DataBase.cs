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
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static ReaLTaiizor.Drawing.Poison.PoisonPaint.ForeColor;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace BankApplication
{
    internal class DataBase
    {
        public async static Task Main(string[] args)
        {
            try
            {
                var check = await CheckPasswordAndOutDataAdmin("1");
                if (check.checkStatus) MessageBox.Show($"{check.login}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else throw new Exception("");


            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Поля
        //---------[Для подключения к БД MySql]--------// 
        readonly static string Server = "192.168.0.161";
        readonly static string UserID = "bank_user";
        readonly static string Password = "123456";
        readonly static string Database = "myDB";
        //--------------------------------------------//

        //---------[Для подключения к БД SqlBrowser]--------// 
        readonly static string connectionString = @"Data Source=C:\Users\Gleb24\Desktop\my_database\myDB.db";
        //--------------------------------------------------//

        // Методы

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

        //-------------------[Проверки логина при регистрации (true - занят, false - свободен)]-------------------//
        public async static Task<bool> CheckLoginForReg(string login)
        {
            // set these values correctly for your database server
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            // open a connection asynchronously
            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // create a DB command and set the SQL statement with parameters
            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    SELECT COUNT(*)
                    FROM Client 
                    WHERE Login = @login";
            command.Parameters.AddWithValue("login", login);

            // execute the command and read the results
            var result = await command.ExecuteScalarAsync();
            int count = Convert.ToInt32(result);

            return count > 0;
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

        //-------------------[Проверка максимального ID]-------------------//
        public async static Task<int> CheckMaxId()
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    SELECT MAX(Id)
                    FROM Client";

            var result = await command.ExecuteScalarAsync();
            int count = Convert.ToInt32(result);

            return count;
        }

        //-------------------[Добавление клиента в БД (Регистрация)]-------------------//
        public async static Task AddClient(string login, string password, string name1, string name2, string name3, string datebirth, string numberPhone, string mail)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    INSERT INTO Client (Login, Password, name1, name2, name3, DateBirth, NumberPhone, Mail)
                    VALUES (@login, @password, @name1, @name2, @name3, @datebirth, @numberphone, @mail)";
            command.Parameters.AddWithValue("login", login);
            command.Parameters.AddWithValue("password", password);
            command.Parameters.AddWithValue("name1", name1);
            command.Parameters.AddWithValue("name2", name2);
            command.Parameters.AddWithValue("name3", name3);
            command.Parameters.AddWithValue("datebirth", datebirth);
            command.Parameters.AddWithValue("numberphone", numberPhone);
            command.Parameters.AddWithValue("mail", mail);

            await command.ExecuteNonQueryAsync();
        }

        //-------------------[Проверка пароля (Авторизация). Возвращает структуру с данными пользователя]-------------------//
        public readonly struct StructCheckPassword(int id, string login, string password, string name1, string name2, string name3,
                                          string dateBirth, string numberPhone, string mail, bool checkStatus)
        {
            public readonly int id = id;
            public readonly string login = login;
            public readonly string password = password;
            public readonly string name1 = name1;
            public readonly string name2 = name2;
            public readonly string name3 = name3;
            public readonly string dateBirth = dateBirth;
            public readonly string numberPhone = numberPhone;
            public readonly string mail = mail;
            public readonly bool checkStatus = checkStatus;
        }
        public async static Task<StructCheckPassword> CheckPasswordAndOutDataClient(string login)
        {
            // set these values correctly for your database server
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            // open a connection asynchronously
            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // create a DB command and set the SQL statement with parameters
            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    SELECT Id, Password, name1, name2, name3, DateBirth, NumberPhone, Mail
                    FROM Client
                    WHERE Login = @login";
            command.Parameters.AddWithValue("login", login);

            // execute the command and read the results
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                int id = reader.GetInt32(0);
                string password = reader.GetString(1);
                string name1 = reader.GetString(2);
                string name2 = reader.GetString(3);
                string name3 = reader.GetString(4);
                string datebirth = reader.GetString(5);
                string numberPhone = reader.GetString(6);
                string mail = reader.GetString(7);

                return new StructCheckPassword(id, login, password, name1, name2, name3, datebirth, numberPhone, mail, true);
            }
            else return new StructCheckPassword();
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

        //-------------------[Создание номера карты + проверка]-------------------//
        public async static Task<string> CreateNumberCardAndCheck()
        {
            Random rnd = new Random();
            bool check_numbercard = true;
            string number;

            do
            {
                int number14 = rnd.Next(1000, 10000);
                int number24 = rnd.Next(1000, 10000);
                int number34 = rnd.Next(1000, 10000);
                int number44 = rnd.Next(1000, 10000);

                number = $"{number14}{number24}{number34}{number44}";

                // Работа с БД 
                var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

                await using var connection = new MySqlConnection(builder.ConnectionString);
                await connection.OpenAsync();

                await using var command = connection.CreateCommand();
                command.CommandText = @"SELECT (
                                (SELECT COUNT(*) FROM UsuallyCard WHERE CardNumber = @cardnumber) +
                                (SELECT COUNT(*) FROM DebitCard WHERE CardNumber = @cardnumber) +
                                (SELECT COUNT(*) FROM CreditCard WHERE CardNumber = @cardnumber) +
                                (SELECT COUNT(*) FROM BusinessCard WHERE CardNumber = @cardnumber));";
                command.Parameters.AddWithValue("cardnumber", number);

                var reader = Convert.ToInt32(await command.ExecuteScalarAsync());

                if (reader == 0) check_numbercard = false;
            }
            while (check_numbercard);

            return number;
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


        #region Работа с клиентом

        //-------------------[Изменать баланс клиенту (Bussines)]-------------------//
        public async static Task ReturnBalance(int clientId, int idCard, decimal delta)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (действия зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                // Действие 1: Узнаем баланс карты
                await using var cmdSelect = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdSelect.Transaction = transaction;
                cmdSelect.CommandText = @"
                    SELECT Balance
                    FROM BusinessCard
                    WHERE ClientId = @clientid
                    AND Id = @idCard";
                cmdSelect.Parameters.AddWithValue("clientid", clientId);
                cmdSelect.Parameters.AddWithValue("idCard", idCard);

                decimal currentBalance = -1;

                await using (var reader = await cmdSelect.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        currentBalance = reader.GetDecimal(0);
                    }
                }

                // Если не нашел карту — отменяем
                if (currentBalance == -1) throw new Exception("Произошла непредвиденная ошибка! Пожалуйста, повторите попытку позже.");

                // Еслив все хорошо считаем новый баланс
                decimal newBalance = currentBalance + delta;

                // Действие 2: Меняем баланс (+ проверяем)
                await using var cmdUpdate = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdUpdate.Transaction = transaction;
                cmdUpdate.CommandText = @"
                    UPDATE BusinessCard
                    SET Balance = @newBalance
                    WHERE Id = @idCard
                    AND Balance = @currentBalance";
                cmdUpdate.Parameters.AddWithValue("newBalance", newBalance);
                cmdUpdate.Parameters.AddWithValue("idCard", idCard);
                cmdUpdate.Parameters.AddWithValue("currentBalance", currentBalance);

                var updateRows = await cmdUpdate.ExecuteNonQueryAsync();

                // Если ответ не записался в БД (по какой-то причине)
                if (updateRows == 0) throw new Exception("Произошла непредвиденная ошибка! Пожалуйста, повторите попытку позже.");

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

        //-------------------[Изменение логина]-------------------//
        public async static Task ChangeLogin(int id, string new_login)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqliteCommand("UPDATE Client SET Login = @new_login WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@new_login", new_login);
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
            }
        }

        //-------------------[Изменение пароля]-------------------//
        public async static Task UpdatePassword(int id, string new_password)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqliteCommand("UPDATE Client SET Password = @new_password WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@new_password", new_password);
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
            }
        }

        //-------------------[Изменение Имени]-------------------//
        public async static Task UpdateName1(int id, string new_name1)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqliteCommand("UPDATE Client SET name1 = @name1 WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@name1", new_name1);
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
            }
        }

        //-------------------[Изменение Фамилии]-------------------//
        public async static Task UpdateName2(int id, string new_name2)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqliteCommand("UPDATE Client SET name2 = @name2 WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@name2", new_name2);
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
            }
        }

        //-------------------[Изменение Отчества]-------------------//
        public async static Task UpdateName3(int id, string new_name3)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqliteCommand("UPDATE Client SET name3 = @name3 WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@name3", new_name3);
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
            }
        }

        //-------------------[Изменение Даты рождения]-------------------//
        public async static Task UpdateDateBirth(int id, string datebirth_new)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqliteCommand("UPDATE Client SET DateBirth = @datebirth WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@datebirth", datebirth_new);
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
            }
        }

        #endregion


        #region Работа с Usually картами

        //-------------------[Поиск обычных карт клиента по ID клиента. Возвращает структуру с обычными картами пользователя]-------------------//
        public readonly struct StructCheckUsuallyCardsClient(List<UsuallyCard> usually_cards)
        {
            public readonly List<UsuallyCard> UsuallyCards = usually_cards;
        }
        public async static Task<StructCheckUsuallyCardsClient> CheckUsuallyCardsClient(int clientid)
        {
            // Создаем пустой список (в него будем "складывать" все найденные карты)
            List<UsuallyCard> usually_cards = [];

            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    SELECT Id, CardNumber, Balance, Deposit, LimitDeposit, Status, Reason
                    FROM UsuallyCard
                    WHERE ClientId = @clientid";
            command.Parameters.AddWithValue("clientid", clientid);

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                int idcard = reader.GetInt32(0);
                string cardnumber = reader.GetString(1);
                decimal balance = reader.GetDecimal(2);
                decimal deposit = reader.GetDecimal(3);
                decimal limitdeposit = reader.GetDecimal(4);
                int status = reader.GetInt32(5);
                string reason = reader.IsDBNull(6) ? "" : reader.GetString(6);

                usually_cards.Add(new UsuallyCard(idcard, cardnumber, balance, deposit, limitdeposit, clientid, status, reason));
            }

            // Отдаем структуре полученный список из обычных карт
            return new StructCheckUsuallyCardsClient(usually_cards);
        }

        //-------------------[Количество обычных карт клиента]-------------------//
        public async static Task<int> CountUsuallyCardClient(int clientid)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    SELECT COUNT(*)
                    FROM UsuallyCard 
                    WHERE ClientId = @clientid";
            command.Parameters.AddWithValue("clientid", clientid);

            var result = Convert.ToInt32(await command.ExecuteScalarAsync());

            return result;
        }

        //-------------------[Оплата для обычной карты]-------------------//
        public async static Task<bool> PayUsuallyCard(decimal new_balance, int id, string current_number, decimal current_balance, decimal current_deposit, decimal current_limit)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    UPDATE UsuallyCard 
                    SET Balance = @new_balance
                    WHERE Id = @id
                    AND CardNumber = @current_number
                    AND Balance = @current_balance
                    AND Deposit = @current_deposit
                    AND LimitDeposit = @current_limit";
            command.Parameters.AddWithValue("new_balance", new_balance);
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("current_number", current_number);
            command.Parameters.AddWithValue("current_balance", current_balance);
            command.Parameters.AddWithValue("current_deposit", current_deposit);
            command.Parameters.AddWithValue("current_limit", current_limit);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        //-------------------[Пополнение баланса для обычной карты]-------------------//
        public async static Task<bool> ReplenishBalanceUsuallyCard(int id, decimal new_balance, decimal current_balance, decimal current_deposit, decimal current_limit)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    UPDATE UsuallyCard
                    SET Balance = @new_balance
                    WHERE Id = @id
                    AND Balance = @current_balance
                    AND Deposit = @current_deposit
                    AND LimitDeposit = @current_limitDeposit";
            command.Parameters.AddWithValue("new_balance", new_balance);
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("current_balance", current_balance);
            command.Parameters.AddWithValue("current_deposit", current_deposit);
            command.Parameters.AddWithValue("current_limitDeposit", current_limit);

            var reader = Convert.ToInt32(await command.ExecuteNonQueryAsync());

            return reader > 0;
        }

        //-------------------[Пополнение депозита для обычной карты]-------------------//
        public async static Task<bool> ReplenishDepositUsuallyCard(int id, decimal new_deposit, decimal current_balance, decimal current_deposit, decimal current_limit)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    UPDATE UsuallyCard
                    SET Deposit = @new_deposit
                    WHERE Id = @id
                    AND Balance = @current_balance
                    AND Deposit = @current_deposit
                    AND LimitDeposit = @current_limitDeposit";
            command.Parameters.AddWithValue("new_deposit", new_deposit);
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("current_balance", current_balance);
            command.Parameters.AddWithValue("current_deposit", current_deposit);
            command.Parameters.AddWithValue("current_limitDeposit", current_limit);

            var reader = Convert.ToInt32(await command.ExecuteNonQueryAsync());

            return reader > 0;
        }

        #region Закрыть карту + закрыть Request (U)
        public async static Task U_CloseAccept(int idCard, string answer, string comment, DateTime dateAnswer,
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
                    DELETE FROM UsuallyCard
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

        //-------------------[Открыть карту + закрыть Request]-------------------//
        public async static Task OpenCardUAndAnswerRequest(int clientId, string cardNumber, string answer, string comment, DateTime dateAnswer, int idRequest)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (оба действия зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                // Действие 1: Закрываем карту
                await using var cmdOpenCard = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdOpenCard.Transaction = transaction;
                cmdOpenCard.CommandText = @"
                    INSERT INTO UsuallyCard (ClientId, CardNumber, Balance, Deposit, LimitDeposit)
                    VALUES (@clientid, @cardnumber, @balance, @deposit, @limitdeposit)";
                cmdOpenCard.Parameters.AddWithValue("clientid", clientId);
                cmdOpenCard.Parameters.AddWithValue("cardnumber", cardNumber);
                cmdOpenCard.Parameters.AddWithValue("balance", 0);
                cmdOpenCard.Parameters.AddWithValue("deposit", 0);
                cmdOpenCard.Parameters.AddWithValue("limitdeposit", 10000);

                // ExecuteNonQueryAsync вернет количество вставленных строк
                int openCardRows = await cmdOpenCard.ExecuteNonQueryAsync();

                // Если депозит не сменился (по какой-то причине)
                if (openCardRows == 0) throw new Exception("Произошла непредвиденная ошибка. Пожалейства, повторите попытку позже.");

                // Действие 2: Отправляем ответ на запрос
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




        //-------------------[Открытие обычной карты]-------------------//
        public async static Task AddUsuallyCard(int clientid, string cardnumber, decimal balance, decimal deposit, decimal limitdeposit)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqliteCommand("INSERT INTO UsuallyCard (ClientId, CardNumber, Balance, Deposit, LimitDeposit) VALUES (@clientid, @cardnumber, @balance, @deposit, @limitdeposit)", connection);

                command.Parameters.AddWithValue("@clientid", clientid);
                command.Parameters.AddWithValue("@cardnumber", cardnumber);
                command.Parameters.AddWithValue("@balance", balance);
                command.Parameters.AddWithValue("@deposit", deposit);
                command.Parameters.AddWithValue("@limitdeposit", limitdeposit);

                await command.ExecuteNonQueryAsync();
            }
        }

        #endregion


        #region Работа с Debit картами

        //-------------------[Поиск дебетовых карт клиента по ID клиента]-------------------//
        public struct StructCheckDebitCardsClient(List<DebitCard> debit_cards)
        {
            public List<DebitCard> DebitCards = debit_cards;
        }
        public async static Task<StructCheckDebitCardsClient> CheckDebitCardsClient(int clientid)
        {
            List<DebitCard> debit_cards = [];

            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    SELECT Id, CardNumber, Balance, Deposit, LimitDeposit, CashBack, Status, Reason
                    FROM DebitCard WHERE ClientId = @clientid";
            command.Parameters.AddWithValue("clientid", clientid);

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                int idcard = reader.GetInt32(0);
                string cardnumber = reader.GetString(1);
                decimal balance = reader.GetDecimal(2);
                decimal deposit = reader.GetDecimal(3);
                decimal limitdeposit = reader.GetDecimal(4);
                decimal cashback = reader.GetDecimal(5);
                int status = reader.GetInt32(6);
                string reason = reader.IsDBNull(7) ? "" : reader.GetString(7);

                debit_cards.Add(new DebitCard(idcard, cardnumber, balance, deposit, limitdeposit, clientid, cashback, status, reason));
            }

            // Отдаем структуре полученный список из дебетовых карт
            return new StructCheckDebitCardsClient(debit_cards);
        }

        //-------------------[Открытие дебетовой карты]-------------------//
        public async static Task AddDebitCard(int clientid, string cardnumber, decimal balance, decimal deposit, decimal limitdeposit, decimal cashback)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqliteCommand("INSERT INTO DebitCard (ClientId, CardNumber, Balance, Deposit, LimitDeposit, CashBack) VALUES (@clientid, @cardnumber, @balance, @deposit, @limitdeposit, @cashback)", connection);

                command.Parameters.AddWithValue("@clientid", clientid);
                command.Parameters.AddWithValue("@cardnumber", cardnumber);
                command.Parameters.AddWithValue("@balance", balance);
                command.Parameters.AddWithValue("@deposit", deposit);
                command.Parameters.AddWithValue("@limitdeposit", limitdeposit);
                command.Parameters.AddWithValue("@cashback", cashback);

                await command.ExecuteNonQueryAsync();
            }
        }

        //-------------------[Удаление дебетовой карты]-------------------//
        public async static Task DeleteDebitCard(int idcard)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqliteCommand("DELETE FROM DebitCard WHERE Id = @idcard", connection);
                command.Parameters.AddWithValue("@idcard", idcard);

                await command.ExecuteNonQueryAsync();
            }
        }

        //-------------------[Оплата для дебетовой карты + обновление кешбека]-------------------//
        public async static Task<bool> PayDebitCard(decimal new_balance, decimal new_cashback, int id,
                                              decimal current_balance, decimal current_deposit, decimal current_limit, decimal current_cashback)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    UPDATE DebitCard
                    SET Balance = @new_balance, CashBack = @new_cashback
                    WHERE Id = @id
                    AND Balance = @current_balance
                    AND Deposit = @current_deposit
                    AND LimitDeposit = @current_limit
                    AND CashBack = @current_cashback";
            command.Parameters.AddWithValue("new_balance", new_balance);
            command.Parameters.AddWithValue("new_cashback", new_cashback);
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("current_balance", current_balance);
            command.Parameters.AddWithValue("current_deposit", current_deposit);
            command.Parameters.AddWithValue("current_limit", current_limit);
            command.Parameters.AddWithValue("current_cashback", current_cashback);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        //-------------------[Пополнение баланса для дебетовой карты]-------------------//
        public async static Task<bool> ReplenishBalanceDebitCard(int id, decimal new_balance, decimal current_balance, decimal current_deposit, decimal current_limit, decimal current_cashback)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    UPDATE DebitCard
                    SET Balance = @new_balance
                    WHERE Id = @id
                    AND Balance = @current_balance
                    AND Deposit = @current_deposit
                    AND LimitDeposit = @current_limitDeposit
                    AND CashBack = @current_cashback";
            command.Parameters.AddWithValue("new_balance", new_balance);
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("current_balance", current_balance);
            command.Parameters.AddWithValue("current_deposit", current_deposit);
            command.Parameters.AddWithValue("current_limitDeposit", current_limit);
            command.Parameters.AddWithValue("current_cashback", current_cashback);

            var reader = Convert.ToInt32(await command.ExecuteNonQueryAsync());

            return reader > 0;
        }

        //-------------------[Пополнение депозита для дебетовой карты]-------------------//
        public async static Task<bool> ReplenishDepositDebitCard(int id, decimal new_deposit, decimal current_balance, decimal current_deposit, decimal current_limit, decimal current_cashback) 
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    UPDATE DebitCard
                    SET Deposit = @new_deposit
                    WHERE Id = @id
                    AND Balance = @current_balance
                    AND Deposit = @current_deposit
                    AND LimitDeposit = @current_limitDeposit
                    AND CashBack = @current_cashback";
            command.Parameters.AddWithValue("new_deposit", new_deposit);
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("current_balance", current_balance);
            command.Parameters.AddWithValue("current_deposit", current_deposit);
            command.Parameters.AddWithValue("current_limitDeposit", current_limit);
            command.Parameters.AddWithValue("current_cashback", current_cashback);

            var reader = Convert.ToInt32(await command.ExecuteNonQueryAsync());

            return reader > 0;
        }

        #endregion


        #region Работа с Credit картами

        //-------------------[Поиск КРЕДИТНЫХ карт клиента по ID клиента]-------------------//
        public struct StructCheckCreditCardsClient(List<CreditCard> credit_cards)
        {
            public List<CreditCard> CreditCards = credit_cards;
        }
        public async static Task<StructCheckCreditCardsClient> CheckCreditCardsClient(int clientid)
        {
            List<CreditCard> credit_cards = new List<CreditCard>();

            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    SELECT Id, CardNumber, Balance, Deposit, LimitDeposit, Debt, Status, Reason
                    FROM CreditCard WHERE ClientId = @clientid";
            command.Parameters.AddWithValue("clientid", clientid);

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                int idcard = reader.GetInt32(0);
                string cardnumber = reader.GetString(1);
                decimal balance = reader.GetDecimal(2);
                decimal deposit = reader.GetDecimal(3);
                decimal limitdeposit = reader.GetDecimal(4);
                decimal debt = reader.GetDecimal(5);
                int status = reader.GetInt32(6);
                string reason = reader.IsDBNull(7) ? "" : reader.GetString(7);

                credit_cards.Add(new CreditCard(idcard, cardnumber, balance, deposit, limitdeposit, clientid, debt, status, reason));
            }

            // Отдаем структуре полученный список из кредитных карт
            return new StructCheckCreditCardsClient(credit_cards);
        }

        //-------------------[Открытие кредитной карты]-------------------//
        public async static Task AddCreditCard(int clientid, string cardnumber, decimal balance, decimal deposit, decimal limitdeposit, decimal debt)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqliteCommand("INSERT INTO CreditCard (ClientId, CardNumber, Balance, Deposit, LimitDeposit, Debt) VALUES (@clientid, @cardnumber, @balance, @deposit, @limitdeposit, @debt)", connection);

                command.Parameters.AddWithValue("@clientid", clientid);
                command.Parameters.AddWithValue("@cardnumber", cardnumber);
                command.Parameters.AddWithValue("@balance", balance);
                command.Parameters.AddWithValue("@deposit", deposit);
                command.Parameters.AddWithValue("@limitdeposit", limitdeposit);
                command.Parameters.AddWithValue("@debt", debt);

                await command.ExecuteNonQueryAsync();
            }
        }

        //-------------------[Удаление кредитной карты]-------------------//
        public async static Task DeleteCreditCard(int idcard)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqliteCommand("DELETE FROM CreditCard WHERE Id = @idcard", connection);
                command.Parameters.AddWithValue("@ididcard0", idcard);

                await command.ExecuteNonQueryAsync();
            }
        }

        //-------------------[Оплата для кредитной карты + обновление задолженности]-------------------//
        public async static Task<bool> PayCreditCard(decimal new_balance, decimal new_debt, int id, decimal current_balance,
                                               decimal current_deposit, decimal current_limitDeposit, decimal current_debt)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    UPDATE CreditCard
                    SET Balance = @new_balance, Debt = @new_debt
                    WHERE Id = @id
                    AND Balance = @current_balance
                    AND Deposit = @current_deposit
                    AND LimitDeposit = @current_limitDeposit
                    AND Debt = @current_debt";
            command.Parameters.AddWithValue("new_balance", new_balance);
            command.Parameters.AddWithValue("new_debt", new_debt);
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("current_balance", current_balance);
            command.Parameters.AddWithValue("current_deposit", current_deposit);
            command.Parameters.AddWithValue("current_limitDeposit", current_limitDeposit);
            command.Parameters.AddWithValue("current_debt", current_debt);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        //-------------------[Пополнение баланса для кредитной карты]-------------------//
        public async static Task<bool> ReplenishBalanceCreditCard(int id, decimal new_balance, decimal current_balance, decimal current_deposit, decimal current_limit, decimal current_debt)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    UPDATE CreditCard
                    SET Balance = @new_balance
                    WHERE Id = @id
                    AND Balance = @current_balance
                    AND Deposit = @current_deposit
                    AND LimitDeposit = @current_limitDeposit
                    AND Debt = @current_debt";
            command.Parameters.AddWithValue("new_balance", new_balance);
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("current_balance", current_balance);
            command.Parameters.AddWithValue("current_deposit", current_deposit);
            command.Parameters.AddWithValue("current_limitDeposit", current_limit);
            command.Parameters.AddWithValue("current_debt", current_debt);

            var reader = Convert.ToInt32(await command.ExecuteNonQueryAsync());

            return reader > 0;
        }

        //-------------------[Пополнение депозита для кредитной карты]-------------------//
        public async static Task<bool> ReplenishDepositCreditCard(int id, decimal new_deposit, decimal current_balance, decimal current_deposit, decimal current_limit, decimal current_debt)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    UPDATE CreditCard
                    SET Deposit = @new_deposit
                    WHERE Id = @id
                    AND Balance = @current_balance
                    AND Deposit = @current_deposit
                    AND LimitDeposit = @current_limitDeposit
                    AND Debt = @current_debt";
            command.Parameters.AddWithValue("new_deposit", new_deposit);
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("current_balance", current_balance);
            command.Parameters.AddWithValue("current_deposit", current_deposit);
            command.Parameters.AddWithValue("current_limitDeposit", current_limit);
            command.Parameters.AddWithValue("current_debt", current_debt);

            var reader = Convert.ToInt32(await command.ExecuteNonQueryAsync());

            return reader > 0;
        }

        #endregion


        #region Работа с Bussines картами

        //-------------------[Поиск БИЗНЕС карт клиента по ID клиента]-------------------//
        public struct StructCheckBusinessCardsClient(List<BusinessCard> business_cards)
        {
            public List<BusinessCard> BussinesCards = business_cards;
        }
        public async static Task<StructCheckBusinessCardsClient> CheckBusinessCardsClient(int clientid)
        {
            List<BusinessCard> business_cards = new List<BusinessCard>();

            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    SELECT Id, CardNumber, Balance, Deposit, LimitDeposit, Percent, Status, Reason
                    FROM BusinessCard WHERE ClientId = @clientid";
            command.Parameters.AddWithValue("clientid", clientid);

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                int idcard = reader.GetInt32(0);
                string cardnumber = reader.GetString(1);
                decimal balance = reader.GetDecimal(2);
                decimal deposit = reader.GetDecimal(3);
                decimal limitdeposit = reader.GetDecimal(4);
                double percent = reader.GetDouble(5);
                int status = reader.GetInt32(6);
                string reason = reader.IsDBNull(7) ? "" : reader.GetString(7);

                business_cards.Add(new BusinessCard(idcard, cardnumber, balance, deposit, limitdeposit, clientid, percent, status, reason));
            }

            return new StructCheckBusinessCardsClient(business_cards); // Отдаем СТРУКТУРЕ полученный список из БИЗНЕС карт
        }

        //-------------------[Открытие бизнес карты]-------------------//
        public async static Task AddBusinessCard(int clientid, string cardnumber, decimal balance, decimal deposit, decimal limitdeposit, double percent)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqliteCommand("INSERT INTO BusinessCard (ClientId, CardNumber, Balance, Deposit, LimitDeposit, Percent) VALUES (@clientid, @cardnumber, @balance, @deposit, @limitdeposit, @percent)", connection);

                command.Parameters.AddWithValue("@clientid", clientid);
                command.Parameters.AddWithValue("@cardnumber", cardnumber);
                command.Parameters.AddWithValue("@balance", balance);
                command.Parameters.AddWithValue("@deposit", deposit);
                command.Parameters.AddWithValue("@limitdeposit", limitdeposit);
                command.Parameters.AddWithValue("@percent", percent);

                await command.ExecuteNonQueryAsync();
            }
        }

        //-------------------[Удаление бизнес карты]-------------------//
        public async static Task DeleteBusinessCard(int idcard)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqliteCommand("DELETE FROM BusinessCard WHERE Id = @idcard", connection);
                command.Parameters.AddWithValue("@idcard", idcard);

                await command.ExecuteNonQueryAsync();
            }
        }

        //-------------------[Пополнение баланса для бизнес карты]-------------------//
        public async static Task<bool> ReplenishBalanceBusinessCard(int id, decimal new_balance, decimal current_balance, decimal current_deposit, decimal current_limit, double current_percent)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    UPDATE BusinessCard
                    SET Balance = @new_balance
                    WHERE Id = @id
                    AND Balance = @current_balance
                    AND Deposit = @current_deposit
                    AND LimitDeposit = @current_limitDeposit
                    AND Percent = @current_percent";
            command.Parameters.AddWithValue("new_balance", new_balance);
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("current_balance", current_balance);
            command.Parameters.AddWithValue("current_deposit", current_deposit);
            command.Parameters.AddWithValue("current_limitDeposit", current_limit);
            command.Parameters.AddWithValue("current_percent", current_percent);

            var reader = Convert.ToInt32(await command.ExecuteNonQueryAsync());

            return reader > 0;
        }

        //-------------------[Оплата для бизнес карты]-------------------//
        public async static Task<bool> PayBusinessCard(int id, decimal new_balance, decimal current_balance,
                                               decimal current_deposit, decimal current_limitDeposit, double current_percent)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    UPDATE BusinessCard
                    SET Balance = @new_balance
                    WHERE Id = @id
                    AND Balance = @current_balance
                    AND Deposit = @current_deposit
                    AND LimitDeposit = @current_limitDeposit
                    AND Percent = @current_percent";
            command.Parameters.AddWithValue("@new_balance", new_balance);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@current_balance", current_balance);
            command.Parameters.AddWithValue("@current_deposit", current_deposit);
            command.Parameters.AddWithValue("@current_limitDeposit", current_limitDeposit);
            command.Parameters.AddWithValue("@current_percent", current_percent);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        //-------------------[Пополнение депозита для бизнес карты]-------------------//
        public async static Task<bool> ReplenishDepositBusinessCard(int id, decimal new_deposit, decimal current_balance,
                                               decimal current_deposit, decimal current_limitDeposit, double current_percent)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    UPDATE BusinessCard
                    SET Deposit = @new_deposit
                    WHERE Id = @id
                    AND Balance = @current_balance
                    AND Deposit = @current_deposit
                    AND LimitDeposit = @current_limitDeposit
                    AND Percent = @current_percent";
            command.Parameters.AddWithValue("@new_deposit", new_deposit);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@current_balance", current_balance);
            command.Parameters.AddWithValue("@current_deposit", current_deposit);
            command.Parameters.AddWithValue("@current_limitDeposit", current_limitDeposit);
            command.Parameters.AddWithValue("@current_percent", current_percent);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        #endregion


        #region Запросы

        //-------------------[Запрос на изменение лимита депозита (U)]-------------------//
        public async static Task<bool> U_RequestChangeLimitDeposit(int clientId, string defcard, string typeCard, int idCard, string message, decimal newdeposit)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (оба действия зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                // Действие 1: Проверяем статус карты
                await using var cmdSelect = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdSelect.Transaction = transaction;
                cmdSelect.CommandText = @"
                    SELECT 1
                    FROM UsuallyCard
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

        //-------------------[Запрос на изменение лимита депозита (D)]-------------------//
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

        //-------------------[Запрос на изменение лимита депозита (C)]-------------------//
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

        //-------------------[Запрос на изменение лимита депозита (B)]-------------------//
        public async static Task<bool> B_RequestChangeLimitDeposit(int clientId, string defcard, string typeCard, int idCard,
                    string message, decimal newdeposit, decimal newBalance, decimal currentBalance)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (оба действия зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                // Действие 1: Списываем деньги + првоеряем
                await using var cmdUpdateBalance = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdUpdateBalance.Transaction = transaction;
                cmdUpdateBalance.CommandText = @"
                    UPDATE BusinessCard
                    SET Balance = @newBalance
                    WHERE Id = @idCard
                    AND Balance = @currentBalance
                    AND Status = 1";
                cmdUpdateBalance.Parameters.AddWithValue("newBalance", newBalance);
                cmdUpdateBalance.Parameters.AddWithValue("idCard", idCard);
                cmdUpdateBalance.Parameters.AddWithValue("currentBalance", currentBalance);

                // ExecuteNonQueryAsync вернет количество найденных строк (должна быть одна строка)
                var updateBalance = await cmdUpdateBalance.ExecuteNonQueryAsync();

                if (updateBalance == 0) throw new Exception("Произошла непредвиденная ошибка! Пожалуйста, повторите попытку позже.");

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

                // Если запрос не записался - отменяем
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

        //-------------------[Одобрено (Изменить лимит депозита) + закрыть Request (U)]-------------------//
        public async static Task U_ChangeLimitDepositAndAnswerRequest(decimal newLimit, int idCard, string answer, string comment, DateTime dateAnswer, int idRequest,
            int clientId, string title)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (оба действия зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                // Действие 1: Меняем лимит депозита
                await using var cmdUpdateDeposit = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdUpdateDeposit.Transaction = transaction;
                cmdUpdateDeposit.CommandText = @"
                    UPDATE UsuallyCard
                    SET LimitDeposit = @newLimit
                    WHERE Id = @idCard
                    AND Status = 1";
                cmdUpdateDeposit.Parameters.AddWithValue("newLimit", newLimit);
                cmdUpdateDeposit.Parameters.AddWithValue("idCard", idCard);

                // ExecuteNonQueryAsync вернет количество вставленных строк
                int updateDepositRows = await cmdUpdateDeposit.ExecuteNonQueryAsync();

                // Если депозит не сменился (по какой-то причине)
                if (updateDepositRows == 0) throw new Exception("Произошла непредвиденная ошибка. Пожалуйста, повторите попытку позже.");

                // Действие 2: Отправляем ответ на запрос
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

                // Действие 3: Отправляем уведомление клиенту
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

        //-------------------[Одобрено (Изменить лимит депозита) + закрыть Request (D)]-------------------//
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
                // Действие 1: Меняем лимит депозита
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

                // Действие 2: Отправляем ответ на запрос
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

                // Действие 3: Отправляем уведомление клиенту
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

        //-------------------[Одобрено (Изменить лимит депозита) + закрыть Request (C)]-------------------//
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
                // Действие 1: Меняем лимит депозита
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

                // Действие 2: Отправляем ответ на запрос
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

                // Действие 3: Отправляем уведомление клиенту
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

        //-------------------[Одобрено (Изменить лимит депозита) + закрыть Request (B)]-------------------//
        public async static Task B_ChangeLimitDepositAndAnswerRequest(decimal newLimit, int idCard, string answer, string comment, DateTime dateAnswer, int idRequest,
            int clientId, string title)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (оба действия зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                // Действие 1: Меняем лимит депозита
                await using var cmdUpdateDeposit = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdUpdateDeposit.Transaction = transaction;
                cmdUpdateDeposit.CommandText = @"
                    UPDATE BusinessCard
                    SET LimitDeposit = @newLimit
                    WHERE Id = @idCard
                    AND Status = 1";
                cmdUpdateDeposit.Parameters.AddWithValue("newLimit", newLimit);
                cmdUpdateDeposit.Parameters.AddWithValue("idCard", idCard);

                // ExecuteNonQueryAsync вернет количество вставленных строк
                int updateDepositRows = await cmdUpdateDeposit.ExecuteNonQueryAsync();

                // Если депозит не сменился (по какой-то причине)
                if (updateDepositRows == 0) throw new Exception("Произошла непредвиденная ошибка. Пожалуйста, повторите попытку позже.");

                // Действие 2: Отправляем ответ на запрос
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

                // Действие 3: Отправляем уведомление клиенту
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

        #region Запрос на закрытие карты (U)
        public async static Task U_RequestCloseCard(int clientId, string defcard, string typeCard, int idCard, string message,
                                decimal current_balance, decimal current_deposit, decimal current_limit)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (команды зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {                
                // Действие 1: Создаем заявку в таблице Request
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

                // Действие 2: Меняем статус карты в таблице UsuallyCard
                await using var cmdUpdate = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdUpdate.Transaction = transaction;
                cmdUpdate.CommandText = @"
                    UPDATE UsuallyCard 
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
                cmdUpdate.Parameters.AddWithValue("reason", $"Заявка на закрытие карты (номер {defcard})");

                int updateRows = await cmdUpdate.ExecuteNonQueryAsync();

                // Если карта изменилась, фиксируем транзакцию. Если нет — отменяем всё.
                if (updateRows == 0) throw new Exception("Произошла непредвиденная ошибка! Пожалуйста, повторите попытку позже.");

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

        #region Запрос на закрытие карты (D)
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
                cmdUpdate.Parameters.AddWithValue("reason", $"Заявка на закрытие карты");

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

        #region Запрос на закрытие карты (C)
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
                cmdUpdate.Parameters.AddWithValue("reason", $"Заявка на закрытие карты");

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

        //-------------------[Запрос на открытие карты]-------------------//
        public async static Task RequestOpenCard(int clientId, string typeCard, string message)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (оба действия зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                // Действие 1: Создаем заявку в таблице Request
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

                // Если такая активная заявка уже была (вставка не произошла), прерываем процесс
                if (insertRows == 0) throw new Exception("Вы уже отправляли заявку такого типа! Дождитесь ее рассмотрения.");

                // Действие 2: Считаем количество открытых обычных карт клиента
                await using var cmdCount = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdCount.Transaction = transaction;
                cmdCount.CommandText = @"
                    SELECT COUNT(*)
                    FROM UsuallyCard 
                    WHERE ClientId = @clientid";
                cmdCount.Parameters.AddWithValue("clientid", clientId);

                var countUsually = Convert.ToInt32(await cmdCount.ExecuteScalarAsync());

                // Действие 3: Проверяем совпадение количества
                if (countUsually != Session.CurrentClient.UsuallyCard.Count) throw new Exception("Возникла непредвиденная ошибка! Пожалуйста, повторите попытку позже.");
                if (countUsually == 3) throw new Exception("У Вас максимальное количество открытых обычных карт!");

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

        //-------------------[Поиск всех открытых запросов клиента]-------------------//
        public readonly struct StructRequestClient(List<Request> request)
        {
            public readonly List<Request> Request = request;
        }
        public async static Task<StructRequestClient> SearchRequestClient(int clientid)
        {
            // Создаем пустой список (в него будем "складывать" все найденные открытые запросы)
            List<Request> request = [];

            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    SELECT Id, Type, DefCard, TypeCard, Timestamp, NewDeposit
                    FROM Request
                    WHERE ClientId = @clientid
                    AND Done = 0";
            command.Parameters.AddWithValue("clientid", clientid);

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                int id = reader.GetInt32(0);
                int type = reader.GetInt32(1);
                string defcard = reader.IsDBNull(2) ? "" : reader.GetString(2);
                string typeCard = reader.GetString(3);
                DateTime timestamp = reader.GetDateTime(4);
                decimal new_deposit = reader.IsDBNull(5) ? 0m : reader.GetDecimal(5);

                request.Add(new Request(id, type, defcard, typeCard, timestamp, new_deposit));
            }

            // Отдаем структуре полученный список из открытых запросов клиента
            return new StructRequestClient(request);
        }

        //-------------------[Поиск открытых запросов (для админ панели)]-------------------//
        public readonly struct StructRequestAdmin(List<RequestAdmin> request)
        {
            public readonly List<RequestAdmin> Request = request;
        }
        public async static Task<StructRequestAdmin> SearchRequestAdmin()
        {
            List<RequestAdmin> request = [];

            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Сортируем по дате (по возрастанию) и берем первые 10 открытых
            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    SELECT Id, ClientId, Type, DefCard, TypeCard, IdCard, Message, Timestamp, NewDeposit
                    FROM Request
                    WHERE Done = 0
                    AND AdminAnswer IS NULL
                    ORDER BY Timestamp ASC
                    LIMIT 10";

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                int id = reader.GetInt32(0);
                int clientId = reader.GetInt32(1);
                int type = reader.GetInt32(2);
                string defcard = reader.IsDBNull(3) ? "" : reader.GetString(3);
                string typeCard = reader.GetString(4);
                int idCard = reader.IsDBNull(5) ? -1 : reader.GetInt32(5);                
                string message = reader.GetString(6);
                DateTime timestamp = reader.GetDateTime(7);
                decimal new_deposit = reader.IsDBNull(8) ? 0m : reader.GetDecimal(8);

                request.Add(new RequestAdmin(id, type, defcard, typeCard, timestamp, new_deposit, clientId, message, idCard));
            }

            return new StructRequestAdmin(request);
        }

        //-------------------[Поиск принятых запросов запросов (для админ панели)]-------------------//
        public readonly struct StructMyRequestAdmin(List<RequestAdmin> request)
        {
            public readonly List<RequestAdmin> MyRequest = request;
        }
        public async static Task<StructMyRequestAdmin> SearchMyRequestAdmin(int adminAnswer)
        {
            List<RequestAdmin> request = [];

            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Сортируем по дате (по возрастанию) и берем первые 10 открытых
            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    SELECT Id, ClientId, Type, DefCard, TypeCard, IdCard, Message, Timestamp, NewDeposit
                    FROM Request
                    WHERE Done = 0
                    AND AdminAnswer = @adminAnswer";
            command.Parameters.AddWithValue("adminAnswer", adminAnswer);

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                int id = reader.GetInt32(0);
                int clientId = reader.GetInt32(1);
                int type = reader.GetInt32(2);
                string defcard = reader.IsDBNull(3) ? "-1" : reader.GetString(3);
                string typeCard = reader.GetString(4);
                int idCard = reader.IsDBNull(5) ? -1 : reader.GetInt32(5);
                string message = reader.GetString(6);
                DateTime timestamp = reader.GetDateTime(7);
                decimal new_deposit = reader.IsDBNull(8) ? 0m : reader.GetDecimal(8);

                request.Add(new RequestAdmin(id, type, defcard, typeCard, timestamp, new_deposit, clientId, message, idCard));
            }

            return new StructMyRequestAdmin(request);
        }

        #region [Принять запрос (админ панель)
        public async static Task<bool> ApplyRequest(int idRequest, int adminAnswer)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = @"
                    UPDATE Request
                    SET AdminAnswer = @adminAnswer
                    WHERE Id = @idRequest
                    AND Done = 0
                    AND Answer IS NULL
                    AND Comment IS NULL
                    AND AdminAnswer IS NULL
                    AND DateAnswer IS NULL";
            command.Parameters.AddWithValue("adminAnswer", adminAnswer);
            command.Parameters.AddWithValue("idRequest", idRequest);

            var reader = Convert.ToInt32(await command.ExecuteNonQueryAsync());

            return reader > 0;
        }
        #endregion

        #region [Отказано (Ответить на запрос - Изменить депозит (U, D, C))
        public async static Task UDC_RefusalCLD(int idRequest, string answer, string comment, DateTime dateAnswer,
            int clientId, string title, string message)
        {
            var builder = new MySqlConnectionStringBuilder { Server = Server, UserID = UserID, Password = Password, Database = Database, AllowPublicKeyRetrieval = true };

            await using var connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            // Открываем транзакцию (оба действия зависят друг от друга)
            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                // Действие 1: Обновляем запрос (даем ответ)
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

                // Действие 2: Отправляем уведомление клиенту
                await using var notifications = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                notifications.Transaction = transaction;
                notifications.CommandText = @"
                    INSERT INTO Notifications (ClientId, Title, Message)
                    VALUES (@clientId, @title, @message)";
                notifications.Parameters.AddWithValue("clientId", clientId);
                notifications.Parameters.AddWithValue("title", title);
                notifications.Parameters.AddWithValue("message", message);

                var addNoti = await notifications.ExecuteNonQueryAsync();

                if (addNoti == 0) throw new Exception("Произошла непредвиденная ошибка. Пожалуйста, повторите попытку позже.");

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

        #region Отказано (Ответить на запрос - Закрыть карту (U))
        public async static Task U_RefusalClose(int idRequest, string answer, string comment, DateTime dateAnswer,
            int clientId, string title, string message, int idCard)
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
                    UPDATE UsuallyCard
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
                notifications.Parameters.AddWithValue("message", message);

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

        #region Отказано (Ответить на запрос - Закрыть карту (D))
        public async static Task D_RefusalClose(int idRequest, string answer, string comment, DateTime dateAnswer,
            int clientId, string title, string message, int idCard)
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
                notifications.Parameters.AddWithValue("message", message);

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

        #region Отказано (Ответить на запрос - Изменить депозит (B))
        public async static Task B_AnswerRequestAndNotificationCLD(int idRequest, string answer, string comment, DateTime dateAnswer,
            int clientId, string title, string message, int idCard, decimal delta)
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
                notifications.Parameters.AddWithValue("message", message);

                var addNoti = await notifications.ExecuteNonQueryAsync();

                if (addNoti == 0) throw new Exception("Произошла непредвиденная ошибка. Пожалуйста, повторите попытку позже.");
                #endregion

                #region Действие 3: Узнаем баланс карты
                await using var cmdSelect = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdSelect.Transaction = transaction;
                cmdSelect.CommandText = @"
                    SELECT Balance
                    FROM BusinessCard
                    WHERE ClientId = @clientid
                    AND Id = @idCard";
                cmdSelect.Parameters.AddWithValue("clientid", clientId);
                cmdSelect.Parameters.AddWithValue("idCard", idCard);

                decimal currentBalance = -1;

                await using (var reader = await cmdSelect.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        currentBalance = reader.GetDecimal(0);
                    }
                }

                // Если не нашел карту — отменяем
                if (currentBalance == -1) throw new Exception("Произошла непредвиденная ошибка! Пожалуйста, повторите попытку позже.");

                // Еслив все хорошо считаем новый баланс
                decimal newBalance = currentBalance + delta;
                #endregion

                #region Действие 4: Меняем баланс (+ проверяем)
                await using var cmdUpdate = connection.CreateCommand();

                // ТРАНЗАКЦИЯ ОБЯЗАТЕЛЬНО ДОЛЖНА БЫТЬ
                cmdUpdate.Transaction = transaction;
                cmdUpdate.CommandText = @"
                    UPDATE BusinessCard
                    SET Balance = @newBalance
                    WHERE Id = @idCard
                    AND Balance = @currentBalance";
                cmdUpdate.Parameters.AddWithValue("newBalance", newBalance);
                cmdUpdate.Parameters.AddWithValue("idCard", idCard);
                cmdUpdate.Parameters.AddWithValue("currentBalance", currentBalance);

                var updateRows = await cmdUpdate.ExecuteNonQueryAsync();

                // Если ответ не записался в БД (по какой-то причине)
                if (updateRows == 0) throw new Exception("Произошла непредвиденная ошибка! Пожалуйста, повторите попытку позже.");
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

        //-------------------[Уведомление]-------------------//
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



        //--------------------------------------------------//




        public static void ToPayDebt(int id, decimal new_debt) // Погашение задолженности для кредитной карты
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "UPDATE CreditCard SET Debt = @debt WHERE Id = @id";
                command.Parameters.AddWithValue("@debt", new_debt);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
        }

        public static void DeleteClient(int clientid)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Client WHERE Id = @id";
                command.Parameters.AddWithValue("@id", clientid);

                int count_change = command.ExecuteNonQuery();

                if (count_change > 0)
                {
                    Console.WriteLine($"Успешно: Клиент с ID: {clientid} удалён из базы данных!");
                }
                else throw new Exception($"Ошибка: Клиент с ID: {clientid} не найден! Повторите попытку.");
            }
        }
    }
}
