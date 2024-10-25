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
        public Color PaleteForeColor
        {
            get
            {
                return ForeColorItem.Color;
            }
        }
        public Color PaleteBackColor
        {
            get
            {
                return BackColorItem.Color;
            }
        }
        private ColorPaleteItem ForeColorItem { get; set; }
        private ColorPaleteItem BackColorItem { get; set; }

        private int border = 1;
        private int gap = 2;

        private List<ColorPaleteItem> palete;
        private List<Color> colors;
        public ColorPalete()
        {
            InitializeComponent();

            ForeColorItem = new ColorPaleteItem();
            ForeColorItem.Color = Color.Black;
            this.Controls.Add(ForeColorItem);
            ForeColorItem.Size = new Size(50, 50);
            ForeColorItem.Location = new Point((this.Width - ForeColorItem.Width) / 2, border);

            BackColorItem = new ColorPaleteItem();
            BackColorItem.Color = Color.White;
            this.Controls.Add(BackColorItem);
            BackColorItem.Size = new Size(40, 40);
            BackColorItem.Location = new Point((this.Width - BackColorItem.Width) / 2, ForeColorItem.Bottom + gap);

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
                Color.Brown,//
                Color.Black,
                Color.White,
                Color.Red,
                Color.Green,
                Color.Blue,
                Color.Yellow,
                Color.Orange,
                Color.Pink,
                Color.Purple,
                Color.Brown,//
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
            ItemCount = 30;
            int j = 0;
            for (int i = 0; i < ItemCount; i++)
            {
                if (j == 10)
                    j = 0;

                ColorPaleteItem item = new ColorPaleteItem();
                item.Size = new Size(24, 24);
                if (i < colors.Count)
                {
                    item.Color = colors[i];
                }
                this.Controls.Add(item);
                item.Location = new Point(i / 10 * item.Width + i / 10, BackColorItem.Bottom + 6*gap + j * item.Height + j);
                palete.Add(item);
                item.ItemClicked += PaleteItemClicked;
                j++;
            }
        }

        private void PaleteItemClicked(object sender, EventArgs e)
        {
            ColorPaleteItem item = (ColorPaleteItem)sender;
            MouseEventArgs args = (MouseEventArgs)e;
            if (args.Button == MouseButtons.Left)
            {
                ForeColorItem.Color = item.Color;
                ForeColorItem.Invalidate();
            }
            else if (args.Button == MouseButtons.Right)
            {
                BackColorItem.Color = item.Color;
                BackColorItem.Invalidate();
            }
        }
    }
}
