using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint.Controls
{
    public partial class ColorPaleteItem : UserControl
    {
        public Color Color { get; set; }
        public bool Selected { get; set; }
        public static ColorPaleteItem? CurrentColor { get; set; }

        public ColorPaleteItem()
        {
            InitializeComponent();
            this.Color = Color.White;
            this.Width = 24;
            this.Height = 24;
        }

        private void ColorPaleteItem_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            using (Brush brush = new SolidBrush(this.Color))
            using (Pen pen1 = new Pen(SystemColors.ControlDark))
            using (Pen pen2 = new Pen(SystemColors.Control))
            {
                graphics.FillRectangle(brush, this.ClientRectangle);
                graphics.DrawRectangle(pen1, 0, 0, this.Width - 1, this.Height - 1);
                graphics.DrawRectangle(pen2, 1, 1, this.Width - 3, this.Height - 3);
            }
        }

        private void ColorPaleteItem_Click(object sender, EventArgs e)
        {
            if (CurrentColor != null)
            {
                CurrentColor.Selected = false;
                CurrentColor.CurrentColorChanged();
            }
            Selected = true;
            CurrentColor = this;

            using (Graphics graphics = this.CreateGraphics())
            using (Pen pen = new Pen(SystemColors.ControlDark))
            {
                int circle_size = 5;
                int circle_x = (this.Width - circle_size) / 2;
                int circle_y = (this.Height - circle_size) / 2;
                graphics.DrawEllipse(pen, circle_x, circle_y, circle_size, circle_size);
            }
        }

        private void CurrentColorChanged()
        {
            using (Graphics graphics = CurrentColor!.CreateGraphics())
            using (Brush brush = new SolidBrush(this.Color))
            using (Pen pen1 = new Pen(SystemColors.ControlDark))
            using (Pen pen2 = new Pen(SystemColors.Control))
            {
                graphics.FillRectangle(brush, this.ClientRectangle);
                graphics.DrawRectangle(pen1, 0, 0, this.Width - 1, this.Height - 1);
                graphics.DrawRectangle(pen2, 1, 1, this.Width - 3, this.Height - 3);
            }



        }

    }
}
