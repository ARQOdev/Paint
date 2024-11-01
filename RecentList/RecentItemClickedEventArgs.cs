using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecentList
{
    public class RecentItemClickedEventArgs : EventArgs
    {
        public string FilePath { get; set; }

        public RecentItemClickedEventArgs()
        {
            FilePath = "";
        }

        public RecentItemClickedEventArgs(string file_path)
        {
            FilePath = file_path;
        }
    }
}
