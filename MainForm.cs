using Paint.Controls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Reflection;
using System.Windows.Forms;


namespace Paint
{
    public partial class MainForm : Form
    {
        private int default_width = 800;
        private int default_height = 600;
        private bool drawing = false;
        private bool resizing = false;
        private Point prev_point;
        private Bitmap canvas_bitmap;
        private Graphics canvas_graphics;
        private Rectangle resize_rectangle = new Rectangle(-1, -1, -1, -1);
        public MainForm()
        {
            InitializeComponent();
            canvas_bitmap = new Bitmap(default_width, default_height);
            canvas_graphics = Graphics.FromImage(canvas_bitmap);
            canvas_graphics.Clear(Color.White);
            // ხატვის ხარისხის დაყენება
            canvas_graphics.SmoothingMode = SmoothingMode.AntiAlias;
            canvas_graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            canvas_graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            canvas_graphics.CompositingMode = CompositingMode.SourceOver;
            canvas_graphics.CompositingQuality = CompositingQuality.HighQuality;

            pbCanvas.Image = canvas_bitmap;

            SetDoubleBuffering(pbCanvas, true);
        }

        // rtavs an tishavs Double Buffers gadacemul panelshi
        private static void SetDoubleBuffering(PictureBox picture_box, bool value)
        {
            // Type tp = typeof(Panel); - inaxavs 'Panel'-is tips/klass

            /*
                1. PropertyInfo inaxavs raime tipshi/klashi mocemuli wevrebis informacias romelzec shegvedzleba manipulireba
                2. GetProperty igebs romelime konkretuli parametris informacias, mocemul magalitshi - private bool DoubleBuffered
                3. imis gamo rom Double buffered privatuli wevria 'Panel' klasis sachiroa mivutitot - BindingFlags.NonPublic
                4. aseve unda davazustot aris tu ara is statikuri wevri, radgan araris mas vutitebt - BindingFlags.Instance (statikuris shemtxvevashi - .Static)
            */
            // PropertyInfo pi = tp.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);

            var propertyInfo = typeof(PictureBox).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance); // zemotxsenebuli asec sheileba chaiweros

            if (propertyInfo != null)
            {
                propertyInfo.SetValue(picture_box, value, null);
            }
            // propertyInfo?.SetValue(panel, value, null); ^
        }
        private void pbCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            Rectangle rectangle = new Rectangle(canvas_bitmap.Width + 1, canvas_bitmap.Height + 1, 7, 7);
            if (rectangle.Contains(e.Location))
            {
                resizing = true;
                return;
            }

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

                return;
            }

            Rectangle rectangle = new Rectangle(canvas_bitmap.Width + 1, canvas_bitmap.Height + 1, 7, 7);
            if (rectangle.Contains(e.Location))
                pbCanvas.Cursor = Cursors.SizeNWSE;
            else
                pbCanvas.Cursor = Cursors.Default;
            
            if (resizing)
            {
                resize_rectangle.Width = e.X;
                resize_rectangle.Height = e.Y;
                pbCanvas.Invalidate();
            }
        }

        private void pbCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false;
            resize_rectangle = new Rectangle(-1, -1, -1, -1);
            pbCanvas.Invalidate();
            resizing = false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            canvas_graphics.Dispose();
            canvas_bitmap.Dispose();
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            graphics.FillRectangle(Brushes.White, canvas_bitmap.Width + 1, canvas_bitmap.Height + 1, 7, 7);
            graphics.DrawRectangle(Pens.Black, canvas_bitmap.Width + 1, canvas_bitmap.Height + 1, 7, 7);

            graphics.FillRectangle(Brushes.White, canvas_bitmap.Width + 1, (canvas_bitmap.Height - 7) / 2, 7, 7);
            graphics.DrawRectangle(Pens.Black, canvas_bitmap.Width + 1, (canvas_bitmap.Height - 7) / 2, 7, 7);

            graphics.FillRectangle(Brushes.White, (canvas_bitmap.Width - 7) / 2, canvas_bitmap.Height + 1, 7, 7);
            graphics.DrawRectangle(Pens.Black, (canvas_bitmap.Width - 7) / 2, canvas_bitmap.Height + 1, 7, 7);

            if (resizing)
            {
                using (Pen pen = new Pen(Color.Gray))
                {
                    pen.DashStyle = DashStyle.Dot;
                    graphics.DrawRectangle(pen, resize_rectangle);
                }
            }

        }

        private void pbCanvas_MouseHover(object sender, EventArgs e)
        {
            
        }
    }
}
