using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace E_ShuttleRun.Model
{
    public class PilihSeleksiModel : DependencyObject
    {
        public string id { get; set; }
        public string status { get; set; }
        public string nama { get; set; }
        public string waktu_mulai { get; set; }
    }
}
