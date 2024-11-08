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
        public Dictionary<ToolType, Tool> ToolsDictionary = new Dictionary<ToolType, Tool>();
        public Tool ActiveTool { get; private set; }

        public ToolBar()
        {
            InitializeComponent();

            Tool pencil = new Tool(Properties.Resources.pencil_32x32, "ფანქარი", ToolType.Pencil);
            Tool paint_bucket = new Tool(Properties.Resources.paintBucket_32x32, "საღებავის ქილა", ToolType.PaintBucket);
            Tool eraser = new Tool(Properties.Resources.eraser_32x32, "საშლელი", ToolType.Eraser);
            Tool dropper = new Tool(Properties.Resources.dropper_32x32, "პიპეტი", ToolType.Dropper);
            Tool rectangle = new Tool(Properties.Resources.dropper_32x32, "ოთხკუთხედი", ToolType.Rectangle);

            AddTool(pencil);
            AddTool(paint_bucket);
            AddTool(eraser);
            AddTool(dropper);
            AddTool(rectangle);

            ActiveTool = pencil;
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
                Tools[i].ToolClicked += ToolBar_ToolClicked;
            }
        }

        private void ToolBar_ToolClicked(object sender, EventArgs e)
        {
            ActiveTool.Selected = false;
            ActiveTool.Invalidate();
            ActiveTool = (Tool)sender;
            ActiveTool.Selected = true;
            ActiveTool.Invalidate();

            switch(ActiveTool.ToolType)
            {
                case ToolType.Pencil:
                    MyPaint.Helpers.CursorManager.CreateCursor(ToolType.Pencil, 3);
                    break;
                case ToolType.Eraser:
                    MyPaint.Helpers.CursorManager.CreateCursor(ToolType.Eraser, 3);
                    break;
                case ToolType.PaintBucket:
                    MyPaint.Helpers.CursorManager.CreateCursor(ToolType.PaintBucket, 0);
                    break;
                case ToolType.Dropper:
                    MyPaint.Helpers.CursorManager.CreateCursor(ToolType.Dropper, 0);
                    break;

                default:
                    MyPaint.Helpers.CursorManager.CreateCursor(ToolType.Pencil, 1);
                    break;
            }
        }

        public void AddTool(Tool tool)
        {
            Tools.Add(tool);
            ToolsDictionary.Add(tool.ToolType, tool);
            tool.Size = new Size(32, 32);
            this.Controls.Add(tool);
        }

        public void SetActiveTool(ToolType tool_type)
        {
            ActiveTool.Selected = false;
            ActiveTool.Invalidate();
            ActiveTool = ToolsDictionary[tool_type];
            ActiveTool.Selected = true;
            ActiveTool.Invalidate();
        }
    }
}
