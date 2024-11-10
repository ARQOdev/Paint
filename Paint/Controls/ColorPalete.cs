using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPaint.Controls
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
            set
            {
                ForeColorItem.Color = value;
                ForeColorItem.Invalidate();
            }
        }
        public Color PaleteBackColor
        {
            get
            {
                return BackColorItem.Color;
            }
            set
            {
                BackColorItem.Color = value;
                BackColorItem.Invalidate();
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
                ColorTranslator.FromHtml("#000000"),
                ColorTranslator.FromHtml("#FFFFFF"),
                ColorTranslator.FromHtml("#FFFFFF"),
                ColorTranslator.FromHtml("#7F7F7F"),
                ColorTranslator.FromHtml("#C8BFE7"),
                ColorTranslator.FromHtml("#FFFFFF"),
                ColorTranslator.FromHtml("#880014"),
                ColorTranslator.FromHtml("#B97858"),
                ColorTranslator.FromHtml("#FFFFFF"),
                ColorTranslator.FromHtml("#FF7D27"),
                ColorTranslator.FromHtml("#FFCA0C"),
                ColorTranslator.FromHtml("#FFFFFF"),
                ColorTranslator.FromHtml("#FFF200"),
                ColorTranslator.FromHtml("#EEE5B2"), //
                ColorTranslator.FromHtml("#FFFFFF"),
                ColorTranslator.FromHtml("#22B14D"),
                ColorTranslator.FromHtml("#B4E61D"),
                ColorTranslator.FromHtml("#FFFFFF"),
                ColorTranslator.FromHtml("#00A2E8"),
                ColorTranslator.FromHtml("#99D9EB"),
                ColorTranslator.FromHtml("#FFFFFF"),
                ColorTranslator.FromHtml("#3E47CB"),
                ColorTranslator.FromHtml("#7092BE"),
                ColorTranslator.FromHtml("#FFFFFF"),
                ColorTranslator.FromHtml("#A249A4"),
                ColorTranslator.FromHtml("#ED1C23"),
                ColorTranslator.FromHtml("#FFFFFF"),
                ColorTranslator.FromHtml("#FFAEC9"),
                ColorTranslator.FromHtml("#C2C3C4"), //
                ColorTranslator.FromHtml("#FFFFFF")
            };
            ItemCount = 30;
            int j = 0;
            /* ზემოდან ქვემოთ
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
            */

            for (int i = 0; i < ItemCount; i++)
            {
                if (j == 3)
                    j = 0;

                ColorPaleteItem item = new ColorPaleteItem();
                item.Size = new Size(24, 24);
                if (i < colors.Count)
                {
                    item.Color = colors[i];
                }
                this.Controls.Add(item);
                item.Location = new Point(j * item.Width + j, BackColorItem.Bottom + 6 * gap + i / 3 * item.Height + i / 3);
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

        public void ResetPalete()
        {
            ForeColorItem.Color = Color.Black;
            BackColorItem.Color = Color.White;
            ForeColorItem.Invalidate();
            BackColorItem.Invalidate();
        }

    }
}
