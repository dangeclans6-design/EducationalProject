using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace BankApplication
{
    public class Client(int id, string login, string password, string name1, string name2, string name3, string dateBirth, string numberPhone, string mail,
                        List<UsuallyCard> usuallyCard, List<DebitCard> debitCard, List<CreditCard> creditCard,
                        List<BusinessCard> businessCard, List<Request> openrequest, List<Notifications> notifications, List<Notifications> isReadNotifications)
    {
        // АвтоСвойства (Auto-Properties)
        //----------------------[Данные Клиента]------------------------//
        public int Id { get; set; } = id;
        public string Login { get; set; } = login;
        public string Password { get; set; } = password;
        public string Name1 { get; set; } = name1;
        public string Name2 { get; set; } = name2;
        public string Name3 { get; set; } = name3;
        public string DateBirth { get; set; } = dateBirth;
        public string NumberPhone { get; set; } = numberPhone;
        public string Mail { get; set; } = mail;

        //----------------------[Данные карт Клиента]------------------------//
        public List<UsuallyCard> UsuallyCard { get; set; } = usuallyCard;
        public List<DebitCard> DebitCard { get; set; } = debitCard;
        public List<CreditCard> CreditCard { get; set; } = creditCard;
        public List<BusinessCard> BusinessCard { get; set; } = businessCard;
        public List<Request> OpenRequest { get; set; } = openrequest;
        public List<Notifications> Notifications { get; set; } = notifications;
        public List<Notifications> IsReadNotifications { get; set; } = isReadNotifications;
    }

    public class Admin(int id, string login, string password, string secureCode, int status, List<RequestAdmin> myRequest, List<RequestAdmin> request, List<RequestAdminFull> requestFull)
    {
        // АвтоСвойства (Auto-Properties)
        //----------------------[Данные Администратора]------------------------//
        public int Id { get; set; } = id;
        public string Login { get; set; } = login;
        public string Password { get; set; } = password;
        public string SecureCode { get; set; } = secureCode;
        public int Status { get; set; } = status;

        //----------------------[Запросы]------------------------//
        public List<RequestAdmin> MyRequestAdmin { get; set; } = myRequest;
        public List<RequestAdmin> RequestAdmin { get; set; } = request;
        public List<RequestAdminFull> RequestAdminFull { get; set; } = requestFull;

        // Методы
        //----------------------[Принять запрос]------------------------//
        public async Task ApplyRequest(int idRequest, int indexRequest)
        {
            bool IsDbUpdate = await DataBase.ApplyRequest(idRequest, Id);

            if (IsDbUpdate)
            {
                MyRequestAdmin.Add(RequestAdmin[indexRequest]);
                RequestAdmin.RemoveAt(indexRequest);                
            }
            else throw new Exception($"Произошла непредвиденная ошибка!\nПожалуйста, обновите страницу или повторите попытку позже.");
        }

        //----------------------[Ответить на запрос]------------------------//
        public async Task AnswerRequest(int clientId, int idRequest, string answer, string comment, int indexMyRequest,
            int typeRequest, decimal newDeposit, int idCard, string typeCard, string newCardNumber, string cardNumber)
        {
            switch (typeRequest)
            {
                case 1: // Измененить депозита
                    {
                        DateTime dateAnswer = DateTime.Now;
                        string title = $"Ответ на запрос #{idRequest} (Изменение лимита депозита)";

                        switch (typeCard, answer)
                        {
                            case ("U", "Одобрено"):
                                await DataBase.U_ChangeLimitDepositAndAnswerRequest(newDeposit, idCard, answer, comment, dateAnswer, idRequest, clientId, title);
                                MyRequestAdmin.RemoveAt(indexMyRequest);
                                break;
                            case ("U", "Отказано"):
                                await DataBase.UDC_RefusalCLD(idRequest, answer, comment, dateAnswer, clientId, title, comment);
                                MyRequestAdmin.RemoveAt(indexMyRequest);
                                break;
                            case ("D", "Одобрено"):
                                await DataBase.D_ChangeLimitDepositAndAnswerRequest(newDeposit, idCard, answer, comment, dateAnswer, idRequest, clientId, title);
                                MyRequestAdmin.RemoveAt(indexMyRequest);
                                break;
                            case ("D", "Отказано"):
                                await DataBase.UDC_RefusalCLD(idRequest, answer, comment, dateAnswer, clientId, title, comment);
                                MyRequestAdmin.RemoveAt(indexMyRequest);
                                break;
                            case ("C", "Одобрено"):
                                await DataBase.C_ChangeLimitDepositAndAnswerRequest(newDeposit, idCard, answer, comment, dateAnswer, idRequest, clientId, title);
                                MyRequestAdmin.RemoveAt(indexMyRequest);
                                break;
                            case ("C", "Отказано"):
                                await DataBase.UDC_RefusalCLD(idRequest, answer, comment, dateAnswer, clientId, title, comment);
                                MyRequestAdmin.RemoveAt(indexMyRequest);
                                break;
                            case ("B", "Одобрено"):
                                await DataBase.B_ChangeLimitDepositAndAnswerRequest(newDeposit, idCard, answer, comment, dateAnswer, idRequest, clientId, title);
                                MyRequestAdmin.RemoveAt(indexMyRequest);
                                break;
                            case ("B", "Отказано"):
                                await DataBase.B_AnswerRequestAndNotificationCLD(idRequest, answer, comment, dateAnswer, clientId, title, comment, idCard, 400000); // 400000 - стоимость изменения лимита депозита для бизнес карты
                                MyRequestAdmin.RemoveAt(indexMyRequest);
                                break;
                        }

                        break;
                    }
                case 2: // Закрыть карту
                    {
                        DateTime dateAnswer = DateTime.Now;
                        string title = $"Ответ на запрос #{idRequest} (Закрытие карты)";

                        switch (typeCard, answer)
                        {
                            case ("U", "Одобрено"):
                                await DataBase.U_CloseAccept(idCard, answer, comment, dateAnswer, idRequest, typeCard, cardNumber, clientId, title);
                                MyRequestAdmin.RemoveAt(indexMyRequest);
                                break;
                            case ("U", "Отказано"):
                                await DataBase.U_RefusalClose(idRequest, answer, comment, dateAnswer, clientId, title, comment, idCard);
                                MyRequestAdmin.RemoveAt(indexMyRequest);
                                break;
                            case ("D", "Одобрено"):
                                await DataBase.D_CloseAccept(idCard, answer, comment, dateAnswer, idRequest, typeCard, cardNumber, clientId, title);
                                MyRequestAdmin.RemoveAt(indexMyRequest);
                                break;
                            case ("D", "Отказано"):
                                await DataBase.D_RefusalClose(idRequest, answer, comment, dateAnswer, clientId, title, comment, idCard);
                                MyRequestAdmin.RemoveAt(indexMyRequest);
                                break;
                            case ("C", "Одобрено"):
                                await DataBase.C_CloseAccept(idCard, answer, comment, dateAnswer, idRequest, typeCard, cardNumber, clientId, title);
                                MyRequestAdmin.RemoveAt(indexMyRequest);
                                break;
                            case ("C", "Отказано"):
                                await DataBase.C_RefusalClose(idRequest, answer, comment, dateAnswer, clientId, title, comment, idCard);
                                MyRequestAdmin.RemoveAt(indexMyRequest);
                                break;
                            case ("B", "Одобрено"):
                                await DataBase.B_CloseAccept(idCard, answer, comment, dateAnswer, idRequest, typeCard, cardNumber, clientId, title);
                                MyRequestAdmin.RemoveAt(indexMyRequest);
                                break;
                            case ("B", "Отказано"):
                                await DataBase.B_RefusalClose(idRequest, answer, comment, dateAnswer, clientId, title, comment, idCard);
                                MyRequestAdmin.RemoveAt(indexMyRequest);
                                break;
                        }

                        break;
                    }
                case 3: // Открыть карту
                    {
                        DateTime dateAnswer = DateTime.Now;

                        //if (typeCard == "U" & answer == "Одобрено")
                        //{
                        //    await DataBase.OpenCardUAndAnswerRequest(clientId, cardNumber, answer, comment, dateAnswer, idRequest);
                        //}
                        //if (typeCard == "U" & answer == "Отказано")
                        //{
                        //    await DataBase.AnswerRequest(idRequest, answer, comment, dateAnswer);
                        //}
                        //MyRequestAdmin.RemoveAt(indexMyRequest);
                        //await DataBase.AddNotification(clientId, $"Ответ по запросу #{idRequest}", $"{answer}. Комментарий:\n{comment}", dateAnswer);

                        break;
                    }
            }
        }
    }

    public static class Session
    {
        // Здесь будет храниться ОДИН экземпляр вошедшего клиента
        public static Client CurrentClient { get; set; }

        // Здесь будет храниться ОДИН экземпляр вошедшего администратора
        public static Admin CurrentAdmin { get; set; }
    }

    //-------------------------[Класс проверки введенных данных]----------------------------//
    internal class TryReg
    {
        static void Main()
        {
            try
            {
                TryNumberReg("890820256303");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        //-------------------------[Проверка логина при регистрации]----------------------------//
        public async static Task TryLoginReg(string login)
        {
            // Проверка на пустое поле
            if (string.IsNullOrWhiteSpace(login)) throw new Exception("Поле логина не может быть пустым!");

            // Провенрка на длинну
            if (login.Length < 4 || login.Length > 12) throw new Exception("Длина логина должна быть не менее 4 и не более 12 символов!");

            // Проверка на допустимые символы
            foreach (char symb in login)
            {
                if (char.IsLetterOrDigit(symb)) continue;
                else throw new Exception("Недопустимые символы в поле логина!");
            }

            // Проверка есть ли пользователь с таким логином в БД
            if (await DataBase.CheckLoginForReg(login)) throw new Exception("Пользователь с таким логином уже существует!");
        }

        //-------------------------[Проверка пароля при регистрации]----------------------------//
        public static void TryPasswordReg(string password)
        {
            // Проверка на пустое поле
            if (string.IsNullOrWhiteSpace(password)) throw new Exception("Поле пароля не может быть пустым!");

            // Провенрка на длинну
            if (password.Length < 6 || password.Length > 18) throw new Exception("Длина пароля должна быть не менее 6 и не более 18 символов!");

            // Проверка на допустимые символы
            foreach (char symb in password)
            {
                if (char.IsLetterOrDigit(symb) | (symb == '-') | (symb == '_') | (symb == '.') | (symb == '!') | (symb == '+')) continue;
                else throw new Exception("Недопустимые символы в поле пароля!");
            }
        }

        //-------------------------[Проверка Имени, Фамилии, Отчества при регистрации]----------------------------//
        public static void TryNameReg(string name1)
        {
            // Проверка на пустое поле
            if (string.IsNullOrWhiteSpace(name1)) throw new Exception("Заполните поля 'Имя' и 'Фамилия'.");

            // Провенрка на длинну
            if (name1.Length < 2 || name1.Length > 18) throw new Exception("В поле должно содержаться не менее 2 и не более 18 символов!");

            // Проверка на допустимые символы
            foreach (char symb in name1)
            {
                if (char.IsLetter(symb)) continue;
                else throw new Exception("Недопустимые символы в поле!");
            }

            // Проверка на первую заглавную букву
            char[] char_name1 = name1.ToCharArray();
            if (char.IsLower(char_name1[0])) throw new Exception("Первая буква должна быть заглавной!");
        }

        //-------------------------[Проверка номера телефона при регистрации]----------------------------//
        public static void TryNumberReg(string numberPhone)
        {
            // Проверка на пустое поле
            if (string.IsNullOrWhiteSpace(numberPhone)) throw new Exception("Заполните поле 'Номер телефона'.");

            // Провенрка на длинну
            if (numberPhone.Length != 11) throw new Exception("Номер должен содержать 11 цифр!");

            // Проверка на допустимые символы
            foreach (char symb in numberPhone)
            {
                if (char.IsDigit(symb)) continue;
                else throw new Exception("Недопустимые символы в поле для номера! Только цифры!");
            }

            // Проверка на первую цифру (7 или 8)
            char[] char_name1 = numberPhone.ToCharArray();
            if (char_name1[0] == '7' || char_name1[0] == '8') {  }
            else throw new Exception("Первая цифра должна быть 7 или 8!");
        }

        //-------------------------[Проверка почты при регистрации]----------------------------//
        public async static Task TryMailReg(string mail)
        {
            // Проверка на пустое поле
            if (string.IsNullOrWhiteSpace(mail)) throw new Exception("Заполните поле 'Почта'.");

            // Провенрка на длинну
            if (mail.Length < 4 || mail.Length > 20) throw new Exception("Длина почты должна быть не менее 4 и не более 20 символов!");

            // Проверка формата почты через Регулярное выражение
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(mail, emailPattern)) { throw new Exception("Неверный формат почты! Пример: example@mail.com"); }

            // Проверка есть ли пользователь с таким логином в БД
            if (await DataBase.CheckMailForReg(mail)) throw new Exception("Пользователь с такой почтой уже существует!");
        }

        //-------------------------[Проверка даты рождения при регистрации]----------------------------//
        public static void TryDateBirthReg(string year, string mounth, string day)
        {
            // Проверка на пустое поле
            if (string.IsNullOrWhiteSpace(year)) throw new Exception("Заполните поле 'Год'.");
            if (string.IsNullOrWhiteSpace(mounth)) throw new Exception("Заполните поле 'Месяц'.");
            if (string.IsNullOrWhiteSpace(day)) throw new Exception("Заполните поле 'День'.");
        }

        //-------------------------[Проверка введенного числа]----------------------------//
        public static decimal TryDecimal(string amount)
        {
            // Проверка на пустое поле
            if (string.IsNullOrWhiteSpace(amount)) throw new Exception("Поле не может быть пустым!");

            // Проверка на число
            if (!decimal.TryParse(amount, out decimal result)) throw new Exception("Введите число!");

            return result;
        }

        //-------------------------[Проверка введенного текста (для отправки запроса)]----------------------------//
        public static void TryString1000(string message)
        {
            // Проверка на пустое поле
            if (string.IsNullOrWhiteSpace(message)) throw new Exception("Поле не может быть пустым!");

            // Проверка на допустимые символы
            foreach (char symb in message)
            {
                if (char.IsLetterOrDigit(symb) || char.IsWhiteSpace(symb) || char.IsControl(symb) || (symb == '-') || (symb == ',') || (symb == ':') || (symb == '_') || (symb == '.') || (symb == '!') || (symb == '+') || (symb == ' ')) continue;
                else throw new Exception($"Недопустимый символ: '{symb}' (код в таблице: {(int)symb})");
            }

            // Провенрка на длинну
            if (message.Length < 10 || message.Length > 1000) throw new Exception("Длина сообщения должна быть не менее 10 и не более 1000 символов!");
        }

        //-------------------------[Проверка введенного текста]----------------------------//
        public static void TryString300(string message)
        {
            // Проверка на пустое поле
            if (string.IsNullOrWhiteSpace(message)) throw new Exception("Поле не может быть пустым!");

            // Проверка на допустимые символы
            foreach (char symb in message)
            {
                if (char.IsLetterOrDigit(symb) || char.IsWhiteSpace(symb) || char.IsControl(symb) || (symb == '-') || (symb == ',') || (symb == ':') ||  (symb == '_') || (symb == '.') || (symb == '!') || (symb == '+') || (symb == ' ')) continue;
                else throw new Exception($"Недопустимый символ: '{symb}' (код в таблице: {(int)symb})");
            }

            // Провенрка на длинну
            if (message.Length < 6 || message.Length > 300) throw new Exception("Длина комментария должна быть не менее 6 и не более 300 символов!");
        }

        //-------------------------[Генерация уникального номера карты (обычная)]----------------------------//
        public static string GenerateCardNumber(int clientId, string cardTypePrefix = "4000")
        {
            // Получаем 10-значный Timestamp (секунды с 1970 года)
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            // Форматируем ID клиента, чтобы он занимал строго 6 цифр
            string formattedClientId = clientId.ToString().PadLeft(6, '0');

            // Номер
            string cardNumber = $"{cardTypePrefix}{timestamp}{formattedClientId}";

            return cardNumber;
        }
    }
}
