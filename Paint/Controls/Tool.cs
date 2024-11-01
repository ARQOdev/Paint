using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPaint.Controls {
    public partial class Tool : UserControl
    {
        public Image DisplayImage { get; private set; }
        public string ToolName { get; private set; }
        public bool Selected { get; set; }
        public Color SelectionColor { get; set; } = ColorTranslator.FromHtml("#c9e0f7");
        public Color HoverColor { get; set; } = ColorTranslator.FromHtml("#e8eff7");
        public Color BorderColor { get; set; } = ColorTranslator.FromHtml("#62a2e4");

        public Tool()
        {
            InitializeComponent();
        }

        public Tool(Image image, string name) : this()
        {
            DisplayImage = image;
            ToolName = name;
        }

        private void Tool_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //g.FillRectangle(Brushes.Red, this.ClientRectangle);

            using (Brush brush = new SolidBrush(Selected ? SelectionColor : this.ClientRectangle.Contains(PointToClient(Cursor.Position)) ? HoverColor : BackColor))
            using (Pen pen = new Pen(BorderColor, 3))
            {
                g.FillRectangle(brush, this.ClientRectangle);
                g.DrawImage(DisplayImage, new Rectangle((this.Width - DisplayImage.Width) / 2, (this.Height - DisplayImage.Height) / 2, DisplayImage.Width, DisplayImage.Height));
                //System.Diagnostics.Debug.WriteLine(DisplayImage.Size);
                //g.DrawImage(DisplayImage, this.ClientRectangle);
                g.DrawRectangle(pen, this.ClientRectangle);
            }
        }
    }
}
