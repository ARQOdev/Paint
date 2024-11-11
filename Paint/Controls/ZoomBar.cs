using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPaint.Controls
{
    public partial class ZoomBar : UserControl
    {
        private float _value;
        public float Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value < 0.1f)
                    _value = 0.1f;
                else if (value > 8.0f)
                    _value = 8.0f;
                else
                    _value = value;
            }
        }

        public ZoomBar()
        {
            Value = 1;
            InitializeComponent();

            RoundButton zoom_out_button = new RoundButton(Icon.Minus);
            zoom_out_button.Size = new Size(23, 23);
            pnlZoomOut.Controls.Add(zoom_out_button);
            zoom_out_button.Location = new Point((pnlZoomOut.Width - zoom_out_button.Width) / 2, (pnlZoomOut.Height - zoom_out_button.Height) / 2);

            RoundButton zoom_in_button = new RoundButton(Icon.Plus);
            zoom_in_button.Size = new Size(23, 23);
            pnlZoomIn.Controls.Add(zoom_in_button);
            zoom_in_button.Location = new Point((pnlZoomIn.Width - zoom_in_button.Width) / 2, (pnlZoomIn.Height - zoom_in_button.Height) / 2);

            zoom_out_button.Click += ZoomOut_Click;
            zoom_in_button.Click += ZoomIn_Click;

        }

        private void ZoomIn_Click(object? sender, EventArgs e)
        {
            if (Value < 1)
                Value = Value + 0.1f;
            else
                Value = Value + 1.0f;

            lblValue.Text = String.Format("{0}%", (int)(Value * 100));
        }

        private void ZoomOut_Click(object? sender, EventArgs e)
        {
            if (Value <= 1)
                Value = Value - 0.1f;
            else
                Value = Value - 1.0f;

            lblValue.Text = String.Format("{0}%", (int)(Value * 100));
        }

        private void pnlSlider_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            using (Pen pen = new Pen(Color.LightGray, 2))
            {
                graphics.DrawLine(pen, 0, pnlSlider.Height / 2, pnlSlider.Width, pnlSlider.Height / 2);
                System.Diagnostics.Debug.WriteLine(pnlSlider.Size);
            }

            using (Pen pen = new Pen(Color.Blue, 1))
            using (Brush brush = new SolidBrush(Color.Blue))
            {
                GraphicsPath graphics_path = new GraphicsPath();
                graphics_path.AddLines(new Point[] { new Point(62, 9), new Point(62, 21), new Point(65, 25), new Point(68, 21), new Point(68, 9), new Point(62, 9) });
                graphics.DrawPath(pen, graphics_path);
                graphics.FillPath(brush, graphics_path);
            }
        }
    }
}
