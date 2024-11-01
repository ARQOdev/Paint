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
    public partial class ToolBar : UserControl
    {
        public List<Tool> Tools = new List<Tool>();

        public ToolBar()
        {
            InitializeComponent();

            Tool pencil = new Tool(Properties.Resources.pencil_32x32, "ფანქარი");
            Tool paint_bucket = new Tool(Properties.Resources.paintBucket_32x32, "საღებავის ქილა");
            Tool eraser = new Tool(Properties.Resources.eraser_32x32, "საშლელი");
            Tool dropper = new Tool(Properties.Resources.dropper_32x32, "პიპეტი");

            AddTool(pencil);
            AddTool(paint_bucket);
            AddTool(eraser);
            AddTool(dropper);

            pencil.Selected = true;
            ArrangeTools();
        }

        private void ArrangeTools()
        {
            int padding = 3;
            System.Diagnostics.Debug.WriteLine(this.Size);
            for (int i = 0; i < Tools.Count; i++)
            {
                Tools[i].Location = new Point((this.Width - Tools[i].Width) / 2, i * Tools[i].Height + padding * (i + 1));
                Tools[i].Invalidate();
            }
        }

        public void AddTool(Tool tool)
        {
            Tools.Add(tool);
            tool.Size = new Size(32, 32);
            this.Controls.Add(tool);
        }

    }
}
