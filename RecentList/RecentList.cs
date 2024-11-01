using System.IO.Enumeration;
using System.Security.Cryptography.X509Certificates;

namespace RecentList
{
    public static class RecentList
    {
        private static string FileName = "RecentList.txt";
        private static string FolderPath = "";
        private static int ItemCount = 10;
        private static ToolStripMenuItem? RecentMenu { get; set; } = null;

        public static List<string> Files { get; private set; }

        public delegate void RecentItemClickedDelegate(object sender, RecentItemClickedEventArgs e);
        public static event RecentItemClickedDelegate? RecentItemClicked = null;

        static RecentList()
        {
            Files = new List<string>();
        }

        public static void Init(string app_name, ToolStripMenuItem recent_menu, int item_count = 10)
        {
            RecentMenu = recent_menu;
            FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            FolderPath = Path.Combine(FolderPath, app_name);
            
            if (Path.Exists(FolderPath) == false)
            {
                Directory.CreateDirectory(FolderPath);
                return;
            }

            if (!Path.Exists(Path.Combine(FolderPath, FileName)))
                return;

            using (StreamReader reader = File.OpenText(Path.Combine(FolderPath, FileName)))
            {
                string? line = reader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    Files.Add(line);
                    ToolStripMenuItem item = new ToolStripMenuItem(Path.GetFileName(line));
                    item.ToolTipText = line;
                    RecentMenu.DropDownItems.Add(item);
                    item.Click += Item_Click;

                    line = reader.ReadLine();
                }
            }
        }

        private static void Item_Click(object? sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender!;
            RecentItemClicked?.Invoke(sender, new RecentItemClickedEventArgs(item.ToolTipText!));
        }

        public static void Save()
        {
            File.WriteAllLines(Path.Combine(FolderPath, FileName), Files);
        }

        public static void AddFile(string file_path)
        {
            Files.Insert(0, file_path);
            ToolStripMenuItem item = new ToolStripMenuItem(Path.GetFileName(file_path));
            item.ToolTipText = file_path;
            RecentMenu?.DropDownItems.Insert(0, item);
            if (Files.Count > ItemCount)
            {
                Files.RemoveAt(Files.Count - 1);
                ToolStripMenuItem last_item = (ToolStripMenuItem)RecentMenu!.DropDownItems[RecentMenu.DropDownItems.Count - 1];
                RecentMenu.DropDownItems.Remove(last_item);
                last_item.Dispose();
            }
        }

        public static void RemoveFile(string file_path)
        {
            Files.Remove(file_path);
            ToolStripMenuItem? item = null;
            foreach (ToolStripMenuItem temp in RecentMenu!.DropDownItems)
            {
                if (temp.ToolTipText == file_path)
                {
                    item = temp;
                    break;
                }
            }
            RecentMenu.DropDownItems.Remove(item!);
            item?.Dispose();
        }

        public static void MoveToHead(string file_path)
        {
            RemoveFile(file_path);
            AddFile(file_path);
        }

        public static void Clear()
        {
            Files.Clear();
        }
    }
}