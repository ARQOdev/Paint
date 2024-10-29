using Paint.Controls;
using Paint.Dialogs;
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
        private string resizing = "";
        private Point prev_point;
        private Bitmap canvas_bitmap;
        private Graphics canvas_graphics;
        private Rectangle resize_rectangle = new Rectangle(-1, -1, -1, -1);

        public MainForm()
        {
            InitializeComponent();
            canvas_bitmap = new Bitmap(default_width, default_height);
            InitCanvasGraphics();

            SetDoubleBuffering(pbCanvas, true);
        }

        private void InitCanvasGraphics()
        {
            canvas_graphics = Graphics.FromImage(canvas_bitmap);
            canvas_graphics.Clear(UserPalete.PaleteBackColor);

            // ხატვის ხარისხის დაყენება
            canvas_graphics.SmoothingMode = SmoothingMode.AntiAlias;
            canvas_graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            canvas_graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            canvas_graphics.CompositingMode = CompositingMode.SourceOver;
            canvas_graphics.CompositingQuality = CompositingQuality.HighQuality;

            pbCanvas.Image = canvas_bitmap;
        }

        /* რთავს ან თიშავს Double Buffer-ს PictureBox-ში */
        private static void SetDoubleBuffering(PictureBox picture_box, bool value)
        {
            /*
                1. PropertyInfo ინახავს რაიმე ტიპში/კლასში მოცემული წევრების ინფოს/Properties რომლებზეც შეგვეძლება მანიპულირება
                2. GetProperty იგებს რომელიმე კონკრეტული პარამეტრის ინფორმაციას, მოცემულ მაგალითში - private bool DoubleBuffered
                3. იმის გამო რომ Double buffered პრივატი წევრი 'PictureBox' კლასისა, საჭიროა მივუთითოთ - BindingFlags.NonPublic
                4. ასევე უნდა დავაზუსტოთ არის თუ არა Property რის შეცვლასაც ვცდილობთ სტატიკური წევრი, რადგან არ არის ვუთითებთ - BindingFlags.Instance (სტატიკ ცვლადის შემთხვევაში - .Static)
            */

            // Type tp = typeof(Panel); - Type ინახავს ტიპებსა და კლასებს. typeof(...) აბრუნებს გადაცემული ობიექტის ტიპს/კლასს
            // PropertyInfo pi = tp.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            var propertyInfo = typeof(PictureBox).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance); // ^

            propertyInfo?.SetValue(picture_box, value, null);
            //if (propertyInfo != null) ^
            //    propertyInfo.SetValue(picture_box, value, null);
        }
        private void pbCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            Rectangle corner_rectangle = new Rectangle(canvas_bitmap.Width + 1, canvas_bitmap.Height + 1, 7, 7);
            Rectangle right_rectangle = new Rectangle(canvas_bitmap.Width + 1, (canvas_bitmap.Height - 7) / 2, 7, 7);
            Rectangle bottom_rectangle = new Rectangle((canvas_bitmap.Width - 7) / 2, canvas_bitmap.Height + 1, 7, 7);
            if (corner_rectangle.Contains(e.Location))
            {
                resizing = "corner";
                return;
            }
            else if (right_rectangle.Contains(e.Location))
            {
                resizing = "right";
                return;
            }
            else if (bottom_rectangle.Contains(e.Location))
            {
                resizing = "bottom";
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

            Rectangle corner_rectangle = new Rectangle(canvas_bitmap.Width + 1, canvas_bitmap.Height + 1, 7, 7);
            Rectangle right_rectangle = new Rectangle(canvas_bitmap.Width + 1, (canvas_bitmap.Height - 7) / 2, 7, 7);
            Rectangle bottom_rectangle = new Rectangle((canvas_bitmap.Width - 7) / 2, canvas_bitmap.Height + 1, 7, 7);

            if (resizing == "")
            {
                if (corner_rectangle.Contains(e.Location))
                    pbCanvas.Cursor = Cursors.SizeNWSE;
                else if (right_rectangle.Contains(e.Location))
                    pbCanvas.Cursor = Cursors.SizeWE;
                else if (bottom_rectangle.Contains(e.Location))
                    pbCanvas.Cursor = Cursors.SizeNS;
                else
                    pbCanvas.Cursor = Cursors.Default;
            }

            switch (resizing)
            {
                case "corner":
                    resize_rectangle.Width = e.X;
                    resize_rectangle.Height = e.Y;
                    pbCanvas.Invalidate();
                    break;

                case "right":
                    resize_rectangle.Width = e.X;
                    resize_rectangle.Height = canvas_bitmap.Height;
                    pbCanvas.Invalidate();
                    break;

                case "bottom":
                    resize_rectangle.Width = canvas_bitmap.Width;
                    resize_rectangle.Height = e.Y;
                    pbCanvas.Invalidate();
                    break;

                default:
                    break;
            }
        }

        private void pbCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false;

            switch (resizing)
            {
                case "corner":
                    {
                        Bitmap temp = (Bitmap)canvas_bitmap.Clone();
                        canvas_bitmap.Dispose();
                        canvas_bitmap = new Bitmap(e.X, e.Y);
                        InitCanvasGraphics();
                        canvas_graphics.DrawImage(temp, new Point(0, 0));
                        temp.Dispose();
                    }
                    break;

                case "right":
                    {
                        Bitmap temp = (Bitmap)canvas_bitmap.Clone();
                        int height = canvas_bitmap.Height;
                        canvas_bitmap.Dispose();
                        canvas_bitmap = new Bitmap(e.X, height);
                        InitCanvasGraphics();
                        canvas_graphics.DrawImage(temp, new Point(0, 0));
                        temp.Dispose();
                    }
                    break;

                case "bottom":
                    {
                        Bitmap temp = (Bitmap)canvas_bitmap.Clone();
                        int width = canvas_bitmap.Width;
                        canvas_bitmap.Dispose();
                        canvas_bitmap = new Bitmap(width, e.Y);
                        InitCanvasGraphics();
                        canvas_graphics.DrawImage(temp, new Point(0, 0));
                        temp.Dispose();
                    }
                    break;

                default:
                    break;
            }

            resize_rectangle = new Rectangle(-1, -1, -1, -1);
            pbCanvas.Invalidate();
            resizing = "";
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

            if (resizing != "")
            {
                using (Pen pen = new Pen(Color.Gray))
                {
                    pen.DashStyle = DashStyle.Dot;
                    graphics.DrawRectangle(pen, resize_rectangle);
                }
            }

        }

        private void menuResize_Click(object sender, EventArgs e)
        {
            ResizeForm form = new ResizeForm(canvas_bitmap.Size);
            if (form.ShowDialog() == DialogResult.OK)
            {
                Size size = form.GetNewSize();
                Bitmap temp = (Bitmap)canvas_bitmap.Clone();
                canvas_bitmap.Dispose();
                canvas_bitmap = new Bitmap(size.Width, size.Height);
                InitCanvasGraphics();
                canvas_graphics.DrawImage(temp, new Point(0, 0));
                temp.Dispose();
            }
        }
    }
}
