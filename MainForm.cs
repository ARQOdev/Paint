using Paint.Controls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;


namespace Paint
{
    public partial class MainForm : Form
    {
        private bool drawing = false;
        private Point prev_point;
        private Bitmap canvas_bitmap;
        private Graphics canvas_graphics;
        public CurrentColorDisplay main_color_display = new CurrentColorDisplay(50, 50);

        public MainForm()
        {
            InitializeComponent();
            canvas_bitmap = new Bitmap(800, 600);
            canvas_graphics = Graphics.FromImage(canvas_bitmap);
            canvas_graphics.Clear(Color.White);
            canvas_graphics.SmoothingMode = SmoothingMode.AntiAlias;
            canvas_graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            canvas_graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            canvas_graphics.CompositingMode = CompositingMode.SourceOver;
            canvas_graphics.CompositingQuality = CompositingQuality.HighQuality;

            pbCanvas.Image = canvas_bitmap;
        }

        private void pbCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            drawing = true;
            prev_point = e.Location;
        }

        private void pbCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                Color color = UserPalete.PaleteForeColor;
                if (e.Button == MouseButtons.Right)
                    color = UserPalete.PaleteBackColor;
                using (Pen pen = new Pen(color, 2))
                {
                    canvas_graphics.DrawLine(pen, prev_point, e.Location);
                }
                prev_point = e.Location;
                pbCanvas.Invalidate();
            }
        }

        private void pbCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            canvas_graphics.Dispose();
            canvas_bitmap.Dispose();
        }

        private void pnlColorPalete_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            main_color_display.Location = new Point(19, 1);
            pnlColorPalete.Controls.Add(main_color_display);
        }
    }
}
