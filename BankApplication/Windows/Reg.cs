using ReaLTaiizor.Controls;
using ReaLTaiizor.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks; // "Требование к логину:", ReaLTaiizor.Enum.Poison.ToolTipIcon.Info
using System.Windows.Forms;

namespace BankApplication
{
    public partial class Reg : MetroForm
    {
        //---------[Для логина]----------------//
        readonly ToolTip loginTip = new() { ToolTipTitle = "Требование к логину:", ToolTipIcon = ToolTipIcon.Info };
        readonly string messege_login = "- Только латинские буквы и цифры\n" +
                               "- Длина от 4 до 12 символов\n" +
                               "- Без пробелов и спецсимволов";

        //---------[Для пароля]----------------//
        readonly ToolTip passwordTip = new() { ToolTipTitle = "Требование к паролю:", ToolTipIcon = ToolTipIcon.Info };
        readonly string messege_password = "- Только латинские буквы и цифры\n" +
                                  "- Длина от 6 до 18 символов\n" +
                                  "- Без пробелов и спецсимволов\n" +
                                  "- Разрешенные символы: - | _ | . | ! | +";

        //---------[Для Имени, Фамилии, Отчества]----------------//
        readonly ToolTip nameTip = new() { ToolTipTitle = "Требование к заполнению:", ToolTipIcon = ToolTipIcon.Info };
        readonly string messege_name = "- Только буквы";

        //---------[Для номера телефона]----------------//
        readonly ToolTip numberPhoneTip = new() { ToolTipTitle = "Требование к заполнению:", ToolTipIcon = ToolTipIcon.Info };
        readonly string messege_numberPhone = "- Формат:\n7 XXX XXX XX XX\n8 XXX XXX XX XX";

        //---------[Для почты]----------------//
        readonly ToolTip mailTip = new() { ToolTipTitle = "Требование к заполнению:", ToolTipIcon = ToolTipIcon.Info };
        readonly string messege_mail = "- Формат: example@mail.ru";

        //-----------------------------[Дни в месяцах]------------------------------------//
        readonly List<int> day30 = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30]; // 30 дней
        readonly List<int> day31 = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31]; // 31 день
        readonly List<int> feb_day_vis = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29]; // Февраль високосный
        readonly List<int> feb_day_nevis = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28]; // Февраль невисокосный

        //----------------------[Переменные дяля хранения года, месяца и дня рождения]-----------------------//
        string year_str; // Переменная для запоминания выбранного года рождения
        string mounth_str; // Переменная для запоминания выбранного месяца рождения
        string day_str; // Переменная для запоминания выбранного дня рождения
        //-----------------------------------------------------------------//   

        // Конструктор
        public Reg()
        {
            InitializeComponent();

            BoxMounth.Enabled = false;
            BoxDay.Enabled = false;

            //
            name3_reg.Enabled = false;
        }

        // Методы
        //--------------------[Закрытие формы]---------------------//
        private void CloseRegClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //--------------------[Кнопка "Назад"]---------------------//
        private void BackRegClick(object sender, EventArgs e)
        {
            // Закрываем окно при нажатии на кнопку назад
            Hide();

            // Открываем Window1 (возвращаемся назад)
            StartWindow window1 = new();

            window1.Show();
        }

        private void Reg_FormClosed(object sender, FormClosedEventArgs e) // При закрытии формы приложение выключается
        {
            Application.Exit();
        }
        //-----------------------[Подсказки]-------------------------------//
        private void LoginReg_Enter(object sender, EventArgs e) { loginTip.Show(messege_login, login_reg, 0, login_reg.Height); }
        private void LoginReg_Leave(object sender, EventArgs e) { loginTip.Hide(login_reg); }

        private void PasswordReg_Enter(object sender, EventArgs e) { passwordTip.Show(messege_password, password_reg, 0, password_reg.Height); }
        private void PasswordReg_Leave(object sender, EventArgs e) { passwordTip.Hide(password_reg); }

        private void Name1Reg_Enter(object sender, EventArgs e) { nameTip.Show(messege_name, name1_reg, 0, name1_reg.Height); }
        private void Name1Reg_Leave(object sender, EventArgs e) { nameTip.Hide(name1_reg); }

        private void Name2Reg_Enter(object sender, EventArgs e) { nameTip.Show(messege_name, name2_reg, 0, name2_reg.Height); }
        private void Name2Reg_Leave(object sender, EventArgs e) { nameTip.Hide(name2_reg); }

        private void Name3Reg_Enter(object sender, EventArgs e) { nameTip.Show(messege_name, name3_reg, 0, name3_reg.Height); }
        private void Name3Reg_Leave(object sender, EventArgs e) { nameTip.Hide(name3_reg); }

        private void NumberPhone_Enter(object sender, EventArgs e) { numberPhoneTip.Show(messege_numberPhone, number_reg, 0, number_reg.Height); }
        private void NumberPhone_Leave(object sender, EventArgs e) { numberPhoneTip.Hide(number_reg); }

        private void Mail_Enter(object sender, EventArgs e) { mailTip.Show(messege_mail, mail_reg, 0, mail_reg.Height); }
        private void Mail_Leave(object sender, EventArgs e) { mailTip.Hide(mail_reg); }

        //--------------------[Кнопка "Продолжить" в меню регистарции]---------------------//
        private async void GoRegClick(object sender, EventArgs e)
        {
            string login = login_reg.Text.Trim();
            string password = password_reg.Text.Trim();
            string name1 = name1_reg.Text.Trim();
            string name2 = name2_reg.Text.Trim();
            string name3 = name3_reg.Text.Trim();
            string numberPhone = number_reg.Text.Trim();
            string mail = mail_reg.Text.Trim();

            try
            {
                // Проверки введенных значений
                await TryReg.TryLoginReg(login); // Проверка логина (асинхронная так как есть обращение к БД)
                TryReg.TryPasswordReg(password); // Проверка пароля
                TryReg.TryNameReg(name1); // Проверка Имени
                TryReg.TryNameReg(name2); // Проверка Фамилии
                TryReg.TryNumberReg(numberPhone); // Проверка номера телефона
                await TryReg.TryMailReg(mail); // Проверка почты (асинхронная так как есть обращение к БД)
                TryReg.TryDateBirthReg(year_str, mounth_str, day_str);

                // Проверяем есть ли отчество
                if (name3_reg.Enabled)
                {
                    // Проверка Отчества
                    TryReg.TryNameReg(name3);

                    // Если все хорошо - добавляет клиента в БД (с фамилией)
                    await DataBase.AddClient(login, password, name1, name2, name3, $"{day_str}/{mounth_str}/{year_str}", numberPhone, mail);

                    Auth auth = new();

                    Hide(); // Скрывает текущее окно
                    MessageBox.Show("Вы успешно зарегестрирвоались\nСейчас вы попадете в меню авторизации!", "Регистрация"); // Сообщение об успешной регистрации
                    auth.Show(); // Открывает окно авторизации
                }
                else
                {
                    // Добавляет клиента в БД без фамилии
                    await DataBase.AddClient(login, password, name1, name2, "", $"{day_str}/{mounth_str}/{year_str}", numberPhone, mail);

                    Auth auth = new();

                    Hide(); // Скрывает текущее окно
                    MessageBox.Show("Вы успешно зарегестрирвоались\nСейчас вы попадете в меню авторизации!", "Регистрация"); // Сообщение об успешной регистрации
                    auth.Show(); // Открывает окно авторизации
                }

                // Отправляем сообщение о регистрации в лог
                int id_client = await DataBase.CheckMaxId();
                string messegeForLog = $"Пользователь с ID: {id_client} зарегистрировался.";
                await DataBase.AddLogs(id_client, "INFO", messegeForLog, "Окно регистрации");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //--------------------[Выбор года]---------------------//
        private void BoxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // При выходе из списка запоминает выбранный год
                year_str = BoxYear.SelectedItem.ToString();

                // Включает выбор месяца только после того как выбрали год
                BoxMounth.Enabled = !string.IsNullOrWhiteSpace(year_str);

                // Если год переизбрали, лучше временно сбросить и заблокировать дни
                BoxDay.Enabled = false;
                BoxDay.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //--------------------[Выбор месяца]---------------------//
        private void BoxMounth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // При выходе из списка запоминает выбранное число
                mounth_str = BoxMounth.SelectedItem.ToString();

                // Включает выбор дня только после того как выбрали месяц
                BoxDay.Enabled = !string.IsNullOrWhiteSpace(mounth_str);

                //--------------------[Выбор дня]---------------------//
                int year = int.Parse(year_str);

                // Проверяем високосный ли год (True/False)
                bool isLeapYear = (year % 4 == 0);

                // Настраиваем дни в зависимости от месяца
                switch (mounth_str)
                {
                    case "Февраль":
                        BoxDay.DataSource = isLeapYear ? feb_day_vis : feb_day_nevis;
                        break;
                    case "Апрель":
                    case "Июнь":
                    case "Сентябрь":
                    case "Ноябрь":
                        BoxDay.DataSource = day30;
                        break;
                    default:
                        BoxDay.DataSource = day31;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //--------------------[Выбор дня]---------------------//
        private void BoxDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            day_str = BoxDay.SelectedItem.ToString(); // При выходе из списка запоминает выбранное число
        }

        //--------------------[Переключатель Отчества]---------------------//
        private void Name3SwitchSwitchedChanged(object sender)
        {
            if (name3_switch.Switched) { name3_reg.Enabled = true; }
            else { name3_reg.Enabled = false; }
        }
    }
}
