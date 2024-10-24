using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint.Controls
{
    public partial class ColorPalete : UserControl
    {
        public int ItemCount { get; set; }
        public Color ForeColor { get; set; }
        public Color BackColor { get; set; }


        private List<ColorPaleteItem> palete;
        private List<Color> colors;
        public ColorPalete()
        {
            InitializeComponent();
            palete = new List<ColorPaleteItem>();
            colors = new List<Color>() {
                Color.Black,
                Color.White,
                Color.Red,
                Color.Green,
                Color.Blue,
                Color.Yellow,
                Color.Orange,
                Color.Pink,
                Color.Purple,
                Color.Brown
            };
            ItemCount = 10;
            for (int i = 0; i < ItemCount; i++)
            {
                ColorPaleteItem item = new ColorPaleteItem();
                item.Size = new Size(24, 24);
                if (i < colors.Count)
                    item.Color = colors[i];
                this.Controls.Add(item);
                item.Location = new Point(0, i * item.Height + i);
                palete.Add(item);
            }
        }

        private void ColorPalete_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
