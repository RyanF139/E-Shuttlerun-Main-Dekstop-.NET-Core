using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace E_ShuttleRun.Model
{
    public class ListTestorModel : DependencyObject
    {
        public string Id { get; set; }
        public int No { get; set; }
        public string Nama { get; set; }
        public string NRP { get; set; }
    }
}
