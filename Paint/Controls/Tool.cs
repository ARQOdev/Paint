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

    public enum ToolType
    {
        None = 0,
        Pencil,
        PaintBucket,
        Eraser,
        Dropper,
        Text,
        Rectangle
    }

    public partial class Tool : UserControl
    {
        public delegate void ToolClickedHandler(object sender, EventArgs e);
        public event ToolClickedHandler? ToolClicked = null;

        public Image DisplayImage { get; private set; }
        public string ToolName { get; private set; }
        public bool Selected { get; set; }
        public Color SelectionColor { get; set; } = ColorTranslator.FromHtml("#c9e0f7");
        public Color HoverColor { get; set; } = ColorTranslator.FromHtml("#e8eff7");
        public Color BorderColor { get; set; } = ColorTranslator.FromHtml("#62a2e4");
        public ToolType ToolType { get; private set; } = ToolType.None;

        public Tool()
        {
            InitializeComponent();
        }

        public Tool(Image image, string name, ToolType tool_type) : this()
        {
            DisplayImage = image;
            ToolName = name;
            ToolType = tool_type;
        }

        private void Tool_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //g.FillRectangle(Brushes.Red, this.ClientRectangle);

            using (Brush brush = new SolidBrush(Selected ? SelectionColor : this.ClientRectangle.Contains(PointToClient(Cursor.Position)) ? HoverColor : BackColor))
            using (Pen pen = new Pen(Selected ? BorderColor : BackColor, 2))
            {
                //g.DrawImage(DisplayImage, new Rectangle((this.Width - DisplayImage.Width) / 2, (this.Height - DisplayImage.Height) / 2, DisplayImage.Width, DisplayImage.Height));
                g.FillRectangle(brush, this.ClientRectangle);
                g.DrawImage(DisplayImage, this.ClientRectangle);
                g.DrawRectangle(pen, this.ClientRectangle);
            }
        }

        private void Tool_Click(object sender, EventArgs e)
        {
            ToolClicked?.Invoke(this, e);
        }
    }
}
