namespace Paint
{
    public partial class MainForm : Form
    {
        private bool drawing = false;
        private Point prev_point;
        private Bitmap canvas_bitmap;
        private Graphics canvas_graphics;

        public MainForm()
        {
            InitializeComponent();
            canvas_bitmap = new Bitmap(800, 600);
            canvas_graphics = Graphics.FromImage(canvas_bitmap);
            canvas_graphics.Clear(Color.White);
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
                using (Pen pen = new Pen(Color.Black, 2))
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
    }
}
