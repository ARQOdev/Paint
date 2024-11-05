using MyPaint.Controls;
using MyPaint.Dialogs;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;
using MyPaint.Helpers;
using RecentList;

namespace MyPaint
{
    public partial class MainForm : Form
    {
        private int default_width = 800;
        private int default_height = 600;
        private int pen_size = 3;
        bool drawing = false;
        private string resizing = "";
        private Point prev_point;
        private Bitmap canvas_bitmap;
        private Graphics canvas_graphics;
        private Rectangle resize_rectangle = new Rectangle(-1, -1, -1, -1);

        private OpenFileDialog ofd;
        private SaveFileDialog sfd;
        private string image_path = "";
        System.Drawing.Imaging.ImageFormat image_format = System.Drawing.Imaging.ImageFormat.Png;

        public MainForm()
        {
            InitializeComponent();
            canvas_bitmap = new Bitmap(default_width, default_height);
            InitCanvasGraphics();

            SetDoubleBuffering(pbCanvas, true);

            ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            ofd.Filter = "PNG Files|*.png|JPG Files|*.jpg|BMP Files|*.bmp|All Files|*.*";
            ofd.RestoreDirectory = true;

            sfd = new SaveFileDialog();
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            sfd.Filter = "PNG Files|*.png|JPG Files|*.jpg|BMP Files|*.bmp";
            sfd.RestoreDirectory = true;

            CursorManager.CreateCursor(ToolType.Pencil, pen_size);

            RecentList.RecentList.Init(Assembly.GetExecutingAssembly().GetName().Name!, menuRecent);

            Application.ApplicationExit += OnApplicationExit;
            RecentList.RecentList.RecentItemClicked += RecentList_RecentItemClicked;
        }

        private void RecentList_RecentItemClicked(object sender, RecentItemClickedEventArgs e)
        {
            image_path = e.FilePath;
            switch (Path.GetExtension(image_path))
            {
                case "jpg":
                case "jpeg":
                    image_format = System.Drawing.Imaging.ImageFormat.Jpeg;
                    break;
                case "bmp":
                    image_format = System.Drawing.Imaging.ImageFormat.Bmp;
                    break;
                default:
                    image_format = System.Drawing.Imaging.ImageFormat.Png;
                    break;
            }
            canvas_bitmap.Dispose();
            canvas_bitmap = (Bitmap)Bitmap.FromFile(image_path);
            InitCanvasGraphics(false);

            RecentList.RecentList.MoveToHead(image_path);
        }

        private void InitCanvasGraphics(bool clear = true)
        {
            canvas_graphics = Graphics.FromImage(canvas_bitmap);
            if (clear)
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

            if (new Rectangle(new Point(0, 0), canvas_bitmap.Size).Contains(new Point(e.X, e.Y)))
            {
                drawing = true;
            }

            prev_point = e.Location;

            switch (LeftToolBar.ActiveTool.ToolType)
            {
                case ToolType.Pencil:
                    {
                        Color color = UserPalete.PaleteForeColor;
                        if (e.Button == MouseButtons.Right)
                            color = UserPalete.PaleteBackColor;
                        using (Brush brush = new SolidBrush(color))
                        {
                            Rectangle circle_rectangle = new Rectangle(e.X - pen_size / 2, e.Y - pen_size / 2, pen_size, pen_size);
                            canvas_graphics.FillEllipse(brush, circle_rectangle);
                        }
                    }
                    break;
                case ToolType.Eraser:
                    {
                        Color color = UserPalete.PaleteBackColor;
                        using (Brush brush = new SolidBrush(color))
                        {
                            Rectangle circle_rectangle = new Rectangle(e.X - pen_size / 2, e.Y - pen_size / 2, pen_size, pen_size);
                            canvas_graphics.FillEllipse(brush, circle_rectangle);
                        }
                    }
                    break;
                case ToolType.Dropper:
                    {
                        if (e.Button == MouseButtons.Left)
                        {
                            UserPalete.PaleteForeColor = canvas_bitmap.GetPixel(e.X, e.Y);
                        }
                        else if (e.Button == MouseButtons.Right)
                        {
                            UserPalete.PaleteBackColor = canvas_bitmap.GetPixel(e.X, e.Y);
                        }
                    }
                    break;
                case ToolType.PaintBucket:
                    {
                        Color color = UserPalete.PaleteForeColor;
                        if (e.Button == MouseButtons.Right)
                            color = UserPalete.PaleteBackColor;

                        Queue<Point> q = new Queue<Point>();
                        q.Enqueue(e.Location);
                        Fill(q, color);
                    }
                    break;
                default: break;
            }
        }
        
        // შესაცვლელია GetPixel & SetPixel
        private void Fill(Queue<Point> q, Color color)
        {
            int width = canvas_bitmap.Width;
            int height = canvas_bitmap.Height;

            bool[,] visited = new bool[width, height];
            Point start = q.Peek();
            visited[start.X, start.Y] = true;

            Point[] directions = { new Point(0, -1), new Point(0, 1), new Point(-1, 0), new Point(1, 0) };

            while (q.Count() > 0)
            {
                Point current = q.Dequeue();

                foreach (Point dir in directions)
                {
                    Point neighbor = new Point(current.X + dir.X, current.Y + dir.Y);
                    if (InBorder(neighbor) && !visited[neighbor.X, neighbor.Y] && SameColor(current, neighbor))
                    {
                        q.Enqueue(neighbor);
                        visited[neighbor.X, neighbor.Y] = true;
                    }
                }

                canvas_bitmap.SetPixel(current.X, current.Y, color);
            }
        }

        private bool SameColor(Point a, Point b)
        {
            return canvas_bitmap.GetPixel(a.X, a.Y) == canvas_bitmap.GetPixel(b.X, b.Y);
        }

        public bool InBorder(Point pixel)
        {
            return pixel.X >= 0 && pixel.X < canvas_bitmap.Width && pixel.Y >= 0 && pixel.Y < canvas_bitmap.Height;
        }

        private void pbCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            Rectangle bitmap_rectangle = new Rectangle(0, 0, canvas_bitmap.Width, canvas_bitmap.Height);
            if (bitmap_rectangle.Contains(e.Location))
            {
                pbCanvas.Cursor = CursorManager.CurrentCursor;
            }
            else
            {
                pbCanvas.Cursor = Cursors.Default;
            }

            if (drawing)
            {
                switch(LeftToolBar.ActiveTool.ToolType)
                {
                    case ToolType.Pencil:
                        {
                            Color color = UserPalete.PaleteForeColor;
                            if (e.Button == MouseButtons.Right)
                                color = UserPalete.PaleteBackColor;

                            using (Pen pen = new Pen(color, pen_size))
                            using (Brush brush = new SolidBrush(color))
                            {
                                canvas_graphics.DrawLine(pen, prev_point, e.Location);
                                Rectangle circle_rectangle = new Rectangle(e.X - pen_size / 2, e.Y - pen_size / 2, pen_size, pen_size);
                                canvas_graphics.FillEllipse(brush, circle_rectangle);
                            }
                            prev_point = e.Location;
                            pbCanvas.Invalidate();
                        }
                        break;
                    case ToolType.Eraser:
                        {
                            Color color = UserPalete.PaleteBackColor;
                            
                            using (Pen pen = new Pen(color, pen_size))
                            using (Brush brush = new SolidBrush(color))
                            {
                                canvas_graphics.DrawLine(pen, prev_point, e.Location);
                                Rectangle circle_rectangle = new Rectangle(e.X - pen_size / 2, e.Y - pen_size / 2, pen_size, pen_size);
                                canvas_graphics.FillEllipse(brush, circle_rectangle);
                            }
                            prev_point = e.Location;
                            pbCanvas.Invalidate();

                            // e.Button == MouseButtons.Right
                        }
                        break;
                    case ToolType.Dropper:
                        break;

                    default: break;
                }

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
                else if (!bitmap_rectangle.Contains(e.Location))
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

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (LeftToolBar.ActiveTool.ToolType != ToolType.Pencil && LeftToolBar.ActiveTool.ToolType != ToolType.Eraser)
                return;

            if (e.KeyCode == Keys.OemCloseBrackets)
            {
                if (pen_size == 100)
                    pen_size = 1;
                else
                    pen_size++;

                CursorManager.CreateCursor(ToolType.Pencil, pen_size);
                Rectangle bitmap_rectangle = new Rectangle(0, 0, canvas_bitmap.Width, canvas_bitmap.Height);
                if (bitmap_rectangle.Contains(pbCanvas.PointToClient(Cursor.Position)))
                {
                    pbCanvas.Cursor = CursorManager.CurrentCursor;
                }

                e.Handled = true;
            }
            else if (e.KeyCode == Keys.OemOpenBrackets)
            {
                if (pen_size == 1)
                    pen_size = 100;
                else
                    pen_size--;

                CursorManager.CreateCursor(ToolType.Pencil, pen_size);
                Rectangle bitmap_rectangle = new Rectangle(0, 0, canvas_bitmap.Width, canvas_bitmap.Height);
                if (bitmap_rectangle.Contains(pbCanvas.PointToClient(Cursor.Position)))
                {
                    pbCanvas.Cursor = CursorManager.CurrentCursor;
                }

                e.Handled = true;
            }
        }

        private void menuOpen_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pen_size = 3;
                CursorManager.CreateCursor(ToolType.Pencil, pen_size);

                image_path = ofd.FileName;
                switch (Path.GetExtension(image_path))
                {
                    case "jpg":
                    case "jpeg":
                        image_format = System.Drawing.Imaging.ImageFormat.Jpeg;
                        break;
                    case "bmp":
                        image_format = System.Drawing.Imaging.ImageFormat.Bmp;
                        break;
                    default:
                        image_format = System.Drawing.Imaging.ImageFormat.Png;
                        break;
                }
                ofd.InitialDirectory = Path.GetDirectoryName(image_path);
                canvas_bitmap.Dispose();
                canvas_bitmap = (Bitmap)Bitmap.FromFile(image_path);
                InitCanvasGraphics(false);

                RecentList.RecentList.AddFile(ofd.FileName);

            }
        }

        private void menuSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(image_path))
            {
                canvas_bitmap.Save(image_path, image_format);
                return;
            }

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                switch (sfd.FilterIndex)
                {
                    case 1:
                        image_format = System.Drawing.Imaging.ImageFormat.Jpeg;
                        break;
                    case 2:
                        image_format = System.Drawing.Imaging.ImageFormat.Bmp;
                        break;
                    default:
                        image_format = System.Drawing.Imaging.ImageFormat.Png;
                        break;

                }
                image_path = sfd.FileName;
                canvas_bitmap.Save(image_path, image_format);
            }
        }

        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                switch (sfd.FilterIndex)
                {
                    case 1:
                        image_format = System.Drawing.Imaging.ImageFormat.Jpeg;
                        break;
                    case 2:
                        image_format = System.Drawing.Imaging.ImageFormat.Bmp;
                        break;
                    default:
                        image_format = System.Drawing.Imaging.ImageFormat.Png;
                        break;
                }
                image_path = sfd.FileName;
                canvas_bitmap.Save(image_path, image_format);
            }
        }

        private void menuNewFile_Click(object sender, EventArgs e)
        {
            pen_size = 3;
            CursorManager.CreateCursor(ToolType.Pencil, pen_size);

            UserPalete.ResetPalete();
            canvas_graphics.Clear(UserPalete.PaleteBackColor);
            pbCanvas.Invalidate();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            menuNewFile.PerformClick();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            menuOpen.PerformClick();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            menuSave.PerformClick();
        }

        private void menuClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnApplicationExit(object? sender, EventArgs e)
        {
            RecentList.RecentList.Save();
        }
    }
}
