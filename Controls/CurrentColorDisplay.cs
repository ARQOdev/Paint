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
    public partial class CurrentColorDisplay : UserControl
    {
        public CurrentColorDisplay()
        {
            InitializeComponent();
        }

        public CurrentColorDisplay(int width, int height) : this()
        {
            this.Width = width;
            this.Height = height;
        }

        private void CurrentColorDisplay_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            using (Pen pen1 = new Pen(SystemColors.ControlDark))
            using (Pen pen2 = new Pen(SystemColors.Control))
            {
                if (ColorPaleteItem.CurrentColor != null)
                {
                    Color color = ColorPaleteItem.CurrentColor.Color;
                    using (Brush brush = new SolidBrush(color))
                    {
                        graphics.FillRectangle(brush, this.ClientRectangle);
                    }
                }
                graphics.DrawRectangle(pen1, 0, 0, this.Width - 1, this.Height - 1);
                graphics.DrawRectangle(pen2, 1, 1, this.Width - 3, this.Height - 3);
            }
        }
    }
}
