using ReaLTaiizor.Controls;
using ReaLTaiizor.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankApplication
{
    public partial class AdminPanel : MetroForm
    {
        Admin CurrentAdmin;
        public AdminPanel()
        {
            InitializeComponent();
            DoubleBuffered = true;

            CurrentAdmin = Session.CurrentAdmin;
        }

        //--------------------[Кнопка "Закрыть форму"]---------------------//
        private void CloseAdmPanelClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //--------------------[Кнопка "Обновить"]---------------------//
        private async void UpdatePageAdminMenu_Click(object sender, EventArgs e)
        {
            try
            {
                //// Списки запросов
                //var check = await DataBase.CheckPasswordAndOutDataAdmin(CurrentAdmin.Login);
                //var find_my_request = DataBase.SearchMyRequestAdmin(check.id);
                //var find_request = DataBase.SearchRequestAdmin();

                //await Task.WhenAll(find_request, find_my_request);

                //List<RequestAdminFull> request_full = [];

                //Admin CurrentAdminUpdate = new(
                //    check.id,
                //    check.login,
                //    check.password,
                //    check.secureCode,
                //    check.status,
                //    (await find_my_request).MyRequest,
                //    (await find_request).Request,
                //    request_full);


                //// Передаем нашего обновленного админа
                //Session.CurrentAdmin = CurrentAdminUpdate;

                ////Enabled = false;

                //PanelOpenRequest.Controls.Clear();
                //PanelOpenRequest.Visible = false;

                //Close();
                //AdminPanel adminPanel = new();
                //await Task.Delay(1000);
                //adminPanel.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //--------------------[Кнопка "Открытые запросы"]---------------------//
        private void OpenRequestButton_Click(object sender, EventArgs e)
        {
            try
            {
                //if (PanelOpenRequest.Visible == true) return;
                //else
                //{
                //    PanelMyRequest.Visible = false;
                //    PanelMyRequest.Controls.Clear();

                //    // Временно отключаем логику разметки панели, чтобы она не перерисовывалась при каждой кнопке
                //    PanelOpenRequest.SuspendLayout();

                //    int x_point = 40;
                //    int y_point = 30;
                //    string message = "";

                //    var request = CurrentAdmin.RequestAdmin;                    

                //    for (int i = 0; i < request.Count; i++)
                //    {
                //        if (request[i].Type == 1) message = $"ID запроса: {request[i].Id}\nТип запроса: Изменение депозита";
                //        if (request[i].Type == 2) message = $"ID запроса: {request[i].Id}\nТип запроса: Закрыть карту";
                //        if (request[i].Type == 3) message = $"ID запроса: {request[i].Id}\nТип запроса: Открытьк карту";

                //        var RequestList = new CyberButton
                //        {
                //            Width = 750,
                //            Height = 100,
                //            Location = new Point(x_point, y_point),
                //            RoundingInt = 20,

                //            Font = new Font("Segoe UI", 16.0f),
                //            TextButton = message,

                //            // Важно: сохраняем все данные запроса (i-тый элемент в списке) в свойство Tag
                //            Tag = (i, request[i]),

                //            Background_WidthPen = 2,
                //            ColorBackground_Pen = Color.LightSlateGray,
                //            ColorBackground = Color.DimGray,
                //            ForeColor = Color.Tan,
                //        };

                //        // Важно: подписываем кнопку на общее событие клика
                //        RequestList.Click += RequestList_Click;

                //        // Добавляем кнопку в панель
                //        PanelOpenRequest.Controls.Add(RequestList);                        

                //        y_point += 120;
                //    }

                //    // Включаем разметку обратно и заставляем панель плавно отобразить всё разом
                //    PanelOpenRequest.ResumeLayout(true);
                //}

                //PanelOpenRequest.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //--------------------[При нажатии на тикет]---------------------//
        private void RequestList_Click(object sender, EventArgs e)
        {
            // Распознаем, какая именно кнопка вызвала это событие
            var clickedButton = (Control)sender;

            // Достаем из её свойства Tag данные запроса (i-тый элемент в списке), который мы туда положили в цикле
            var (index, currentRequest) = ((int, RequestAdmin))clickedButton.Tag;

            PanelOpenRequest.Visible = false;
            PanelOpenRequest.Controls.Clear();

            RequestExplore request = new();

            request.ShowDialog();
        }

        //--------------------[Кнопка "Мои запросы"]---------------------//
        private void MyRequestButton_Click(object sender, EventArgs e)
        {
            try
            {
                //if (PanelMyRequest.Visible == true) return;
                //else
                //{
                //    PanelOpenRequest.Visible = false;
                //    PanelOpenRequest.Controls.Clear();

                //    PanelMyRequest.SuspendLayout();

                //    int x_point = 85;
                //    int y_point = 30;
                //    string message = "";

                //    var myRequest = CurrentAdmin.MyRequestAdmin;                                       

                //    for (int i = 0; i < myRequest.Count; i++)
                //    {
                //        if (myRequest[i].Type == 1) message = $"ID запроса: {myRequest[i].Id}\nТип запроса: Изменение депозита";
                //        if (myRequest[i].Type == 2) message = $"ID запроса: {myRequest[i].Id}\nТип запроса: Закрыть карту";
                //        if (myRequest[i].Type == 3) message = $"ID запроса: {myRequest[i].Id}\nТип запроса: Открытьк карту";

                //        var MyRequestList = new CyberButton
                //        {
                //            Width = 650,
                //            Height = 100,
                //            Location = new Point(x_point, y_point),
                //            RoundingInt = 20,

                //            Font = new Font("Segoe UI", 16.0f),
                //            TextButton = message,

                //            Tag = (i, myRequest[i]),

                //            Background_WidthPen = 2,
                //            ColorBackground_Pen = Color.LightSlateGray,
                //            ColorBackground = Color.DimGray,
                //            ForeColor = Color.Tan,
                //        };

                //        MyRequestList.Click += MyRequestList_Click;

                //        PanelMyRequest.Controls.Add(MyRequestList);

                //        y_point += 120;
                //    }

                //    PanelMyRequest.ResumeLayout(true);
                //}

                //PanelMyRequest.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //--------------------[При нажатии на один из моих запросов]---------------------//
        private void MyRequestList_Click(object sender, EventArgs e)
        {
            // Распознаем, какая именно кнопка вызвала это событие
            var clickedButton = (Control)sender;

            // Достаем из её свойства Tag данные запроса (i-тый элемент в списке), который мы туда положили в цикле
            var (index, currentMyRequest) = ((int, RequestAdmin))clickedButton.Tag;

            PanelMyRequest.Visible = false;
            PanelMyRequest.Controls.Clear();

            MyRequest myRequest = new(index, currentMyRequest);

            myRequest.ShowDialog();
        }
    }
}
