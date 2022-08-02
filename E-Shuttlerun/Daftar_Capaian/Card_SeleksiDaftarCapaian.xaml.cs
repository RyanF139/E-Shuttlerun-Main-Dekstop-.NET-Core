using E_ShuttleRun.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace E_Shuttlerun.Daftar_Capaian
{
    /// <summary>
    /// Interaction logic for Card_SeleksiDaftarCapaian.xaml
    /// </summary>
    public partial class Card_SeleksiDaftarCapaian : UserControl
    {
        public MainDaftarCapaian _mainDaftarCapaian;
        public PilihSeleksiModel _pilihSeleksiModel;
        public Card_SeleksiDaftarCapaian(MainDaftarCapaian mainDaftarCapaian, PilihSeleksiModel model)
        {
            InitializeComponent();
            DataContext = model;
            _mainDaftarCapaian = mainDaftarCapaian;
            _pilihSeleksiModel = model;
            
        }

        private void btn_ViewDaftarCapaian_Click(object sender, RoutedEventArgs e)
        {
            string nama_seleksi = _pilihSeleksiModel.nama.ToString();
            string id_seleksi = _pilihSeleksiModel.id.ToString();            
            _mainDaftarCapaian._menubar.CallViewDaftarCapaian(id_seleksi, nama_seleksi);
        }
    }
}
