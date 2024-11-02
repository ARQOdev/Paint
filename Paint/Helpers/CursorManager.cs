using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint.Helpers
{
    public static class CursorManager
    {
        public static Cursor? CurrentCursor { get; private set; }

        public static void CreateCursor(Controls.ToolType tool_type, int size)
        {
            CurrentCursor?.Dispose();
            switch(tool_type)
            {
                case Controls.ToolType.Pencil:
                case Controls.ToolType.Eraser:
                    {
                        size += size % 2 == 0 ? 1 : 0;
                        Bitmap cursor_bitmap = size > 15 ? new Bitmap(size + 2, size + 2) : new Bitmap(17, 17);
                        using (Graphics cursor_graphics = Graphics.FromImage(cursor_bitmap))
                        using (Pen pen = new Pen(Color.Black, 1))
                        {
                            cursor_graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            float half = cursor_bitmap.Height / 2;
                            cursor_graphics.DrawLine(pen, half, half - 4, half, half - 7);
                            cursor_graphics.DrawLine(pen, half, half + 4, half, half + 7);
                            cursor_graphics.DrawLine(pen, half - 4, half, half - 7, half);
                            cursor_graphics.DrawLine(pen, half + 4, half, half + 7, half);
                            cursor_graphics.FillEllipse(Brushes.Black, new RectangleF(half, half, 1, 1));
                            cursor_graphics.DrawEllipse(pen, new RectangleF(half - size / 2, half - size / 2, size, size));
                        }

                        IntPtr ptr = cursor_bitmap.GetHicon();
                        Cursor cursor = new Cursor(ptr);
                        cursor_bitmap.Dispose();

                        CurrentCursor = cursor;
                        break;
                    }
                case Controls.ToolType.PaintBucket:
                    CurrentCursor = new Cursor(new System.IO.MemoryStream(Properties.Resources.paintBucket));
                    break;
                case Controls.ToolType.Dropper:
                    CurrentCursor = new Cursor(new System.IO.MemoryStream(Properties.Resources.dropper));
                    break;
                default: break;
            }
        }
    }
}
