using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint.Controls
{

    public enum Icon
    {
        None,
        Minus,
        Plus
    }

    public class RoundButton : Button
    {
        private Icon ButtonIcon { get; set; }
        private int IconSize { get; set; } = 10;
        private bool MousePressed { get; set; } = false;

        public RoundButton() : base()
        {
            UpdateRegion();
            ButtonIcon = Icon.None;
        }

        public RoundButton(Icon icon) : this()
        {
            ButtonIcon = icon;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateRegion();
        }

        private void UpdateRegion()
        {
            GraphicsPath graphics_path = new GraphicsPath();
            graphics_path.AddEllipse(0, 0, this.Width - 1, this.Height - 1);
            this.Region = new Region(graphics_path);
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (GraphicsPath graphics_path = new GraphicsPath())
            using (Brush brush = new SolidBrush(this.BackColor))
            {
                graphics_path.AddEllipse(0, 0, this.Width - 1, this.Height - 1);
                using (PathGradientBrush gradient_brush = new PathGradientBrush(graphics_path))
                {
                    gradient_brush.CenterColor = Color.White;
                    gradient_brush.SurroundColors = new Color[] { Color.LightGray };
                    graphics.FillEllipse(gradient_brush, 0, 0, this.Width - 1, this.Height - 1);
                }
            }

            if (MousePressed)
            {
                using (Pen pen_white = new Pen(Color.White, 2))
                using (Pen pen_gray = new Pen(Color.Gray, 2)) 
                {
                    //graphics.DrawEllipse(pen_white, 1, 1, this.Width - 3, this.Height - 3);
                    graphics.FillEllipse(Brushes.LightGray, 2, 2, this.Width - 4, this.Height - 4);
                    graphics.DrawEllipse(pen_gray, 2, 2, this.Width - 4, this.Height - 4);
                }
            }
            else
            {
                using (GraphicsPath graphics_path = new GraphicsPath())
                {
                    graphics_path.AddEllipse(0, 0, this.Width - 1, this.Height - 1);
                    using (PathGradientBrush gradient_brush = new PathGradientBrush(graphics_path))
                    {
                        gradient_brush.CenterColor = Color.White;
                        gradient_brush.SurroundColors = new Color[] { Color.LightGray };
                        graphics.FillEllipse(gradient_brush, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }

                using (Pen pen = new Pen(Color.Gray, 2))
                {
                    graphics.DrawEllipse(pen, 1, 1, this.Width - 3, this.Height - 3);
                }
            }


            using (Pen pen = new Pen(this.ForeColor, 2))
            {
                graphics.DrawLine(pen, (this.Width - this.IconSize) / 2, (this.Height - 0) / 2, (this.Width + this.IconSize) / 2, (this.Height - 0) / 2);
                if (this.ButtonIcon == Icon.Plus)
                {
                    graphics.DrawLine(pen, (this.Width - 0) / 2, (this.Height - this.IconSize) / 2, (this.Width - 0) / 2, (this.Height + this.IconSize) / 2);
                }
            }

            // TextRenderer.DrawText(graphics, this.Text, this.Font, new Rectangle(1, 1, this.Width - 3, this.Height - 3), this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            MousePressed = true;
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            MousePressed = false;
            this.Invalidate();
        }

    }
}
