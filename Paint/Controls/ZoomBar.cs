using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        private decimal _value;
        private bool zoom = false;
        private int[] positions = new int[17];
        private decimal[] values = {0.1m, 0.2m, 0.3m, 0.4m, 0.5m, 0.6m, 0.7m, 0.8m, 0.9m, 1.0m, 2.0m, 3.0m, 4.0m, 5.0m, 6.0m, 7.0m, 8.0m };
        private int current_position = 9;

        public delegate void ValueChangedDelegate(object sender, EventArgs e);
        public event ValueChangedDelegate ValueChanged;

        public decimal Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value < 0.1m)
                {
                    _value = 0.1m;
                    current_position = 0;
                }
                else if (value > 8.0m)
                {
                    _value = 8.0m;
                    current_position = 16;
                }
                else
                {
                    if (value <= 1.0m)
                    {
                        _value = value - (value % 0.1m);
                    }
                    else
                    {
                        _value = value - (value % 1m);
                    }
                }

                ValueChanged?.Invoke(this, new EventArgs());
                pnlSlider.Invalidate();
            }
        }

        public ZoomBar()
        {
            InitializeComponent();
            Value = 1.0m;

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

            this.pnlSlider_Resize(this, new EventArgs());
        }

        private void ZoomIn_Click(object? sender, EventArgs e)
        {
            if (Value < 1)
                Value = Value + 0.1m;
            else
                Value = Value + 1.0m;

            lblValue.Text = String.Format("{0}%", (int)(Value * 100));
        }

        private void ZoomOut_Click(object? sender, EventArgs e)
        {
            if (Value <= 1)
                Value = Value - 0.1m;
            else
                Value = Value - 1.0m;

            lblValue.Text = String.Format("{0}%", (int)(Value * 100));
        }

        private void pnlSlider_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            using (Pen pen = new Pen(Color.LightGray, 2))
            {
                graphics.DrawLine(pen, 0, pnlSlider.Height / 2, pnlSlider.Width, pnlSlider.Height / 2);
            }

            using (Pen pen = new Pen(Color.Blue, 1))
            using (Brush brush = new SolidBrush(Color.Blue))
            using (GraphicsPath Slider = new GraphicsPath())
            {
                int x = positions[current_position];
                Slider.AddLines(new Point[] { new Point(x - 3, 9), new Point(x - 3, 21), new Point(x, 25), new Point(x + 3, 21), new Point(x + 3, 9), new Point(x - 3, 9) });
                graphics.DrawPath(pen, Slider);
                graphics.FillPath(brush, Slider);
            }
        }

        private void pnlSlider_MouseDown(object sender, MouseEventArgs e)
        {
            int x = positions[current_position];
            Rectangle slider_rectangle = new Rectangle(x - 3, 9, 7, 16);
            if (slider_rectangle.Contains(e.Location))
                zoom = true;
        }

        private void pnlSlider_MouseMove(object sender, MouseEventArgs e)
        {
            if (!zoom)
                return;

            SetPosition(e.X);
        }

        private void SetPosition(int x)
        {
            int diff = this.Width;
            int new_position = current_position;
            for (int i = 0; i < 17; i++)
            {
                if (diff > Math.Abs(x - positions[i]))
                {
                    diff = Math.Abs(x - positions[i]);
                    new_position = i;
                }
            }

            if (new_position != current_position)
            {
                current_position = new_position;
                Value = values[new_position];
                lblValue.Text = String.Format("{0}%", (int)(Value * 100));
            }

        }

        private void pnlSlider_MouseUp(object sender, MouseEventArgs e)
        {
            zoom = false;
        }

        private void pnlSlider_Resize(object sender, EventArgs e)
        {
            int size = pnlSlider.Width - 6;
            int gap = size / 16;
            for (int i = 0; i < 17; i++)
            {
                positions[i] = i * gap + 3;
            }
            pnlSlider.Invalidate();
        }
    }
}
