using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace E_Shuttlerun.Model
{
    public class ListCapaianModel : DependencyObject
    {
        public int NO { get; set; }
        public string NO_TEST { get; set; }
        public string NAMA { get; set; }
        public string HASIL { get; set; }        
        public string TESTOR { get; set; }
        public string WAKTU_SELESAI { get; set; }
        public string Color { get; set; }
    }
}
