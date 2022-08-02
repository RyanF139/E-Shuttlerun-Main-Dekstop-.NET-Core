using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace E_ShuttleRun.Model
{


    public class ListPesertaModel : DependencyObject
    {
        public int NO { get; set; }
        public string NAMA { get; set; }
        public string NRP { get; set; }
        public string PANGKAT { get; set; }
        public string JABATAN { get; set; }
        public string KORPS { get; set; }
        public string TANGGAL_LAHIR { get; set; }
        public string Color { get; set; }
    }
}
