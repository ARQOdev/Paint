using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPaint.Controls
{
    public partial class ColorPaleteItem : UserControl
    {
        public delegate void ColorPaleteItemClickedHandler(object sender, EventArgs e);
        public event ColorPaleteItemClickedHandler? ItemClicked = null;
        public Color Color { get; set; }
        public bool Selected { get; set; }
        // public static ColorPaleteItem? CurrentColor { get; set; }

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
            ItemClicked?.Invoke(this, e);
        }
    }
}
