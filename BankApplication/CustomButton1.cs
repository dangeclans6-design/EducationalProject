using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankApplication
{
    public class CustomButton1 : Control
    {
        private readonly StringFormat SF = new();
        private bool MouseEntered = false;
        private bool MousePressed = false;
        private bool roundingEnable = false;

        [Category("Appearance")]
        [Description("Вкл/Выкл закругление объекта")]
        [DefaultValue(false)]
        public bool RoundingEnabled
        {
            get { return roundingEnable; }
            set
            {
                roundingEnable = value;
                Refresh();
            }
        }

        private int roundingPercent = 100;
        [DisplayName("Rounding [%]")]
        [DefaultValue(100)]
        [Description("Указывает радиус закругления объекта в прцентном соотношении")]
        public int RoundingPercent
        {
            get { return roundingPercent; }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    roundingPercent = value;

                    Refresh();
                }
            }
        }

        public CustomButton1()
        {
            // Оптимизация кнопки
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);

            // Перерисовка кнопки
            DoubleBuffered = true;

            Size = new Size(30, 100);

            BackColor = Color.Tomato;

            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // С помощью Graphics происходит вся отрисовка
            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.HighQuality;

            graph.Clear(Parent.BackColor);

            Rectangle rect = new(0, 0, Width - 1, Height - 1);

            // Закругления
            float roundingValue = 0;
            if (RoundingEnabled && RoundingPercent > 0)
            {
                roundingValue = Height / 100F * RoundingPercent;
            }
            GraphicsPath rectPath = Drawer.RoundedRectalgle(rect, roundingValue);

            // Основной прямоугольник (Фон)
            graph.DrawPath(new Pen(BackColor), rectPath);
            graph.FillPath(new SolidBrush(BackColor), rectPath);

            graph.SetClip(rectPath);

            // Стандартное рисование прямоугольника при клике
            if (MouseEntered)
            {
                graph.DrawRectangle(new Pen(Color.FromArgb(60, Color.White)), rect);
                graph.FillRectangle(new SolidBrush(Color.FromArgb(60, Color.White)), rect);
            }

            if (MousePressed)
            {
                graph.DrawRectangle(new Pen(Color.FromArgb(30, Color.Black)), rect);
                graph.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.Black)), rect);
            }

            graph.DrawString(Text, Font, new SolidBrush(ForeColor), rect, SF);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            MouseEntered = true;

            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            MouseEntered = false;

            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            MousePressed = true;

            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            MousePressed = false;

            Invalidate();
        }
    }
}
