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

namespace E_Shuttlerun.Registrasi_Testor
{
    /// <summary>
    /// Interaction logic for Card_SeleksiRegistrasi.xaml
    /// </summary>
    public partial class Card_SeleksiRegistrasi : UserControl
    {
        public MainRegistrasi _mainRegistrasi;
        public PilihSeleksiModel _pilihSeleksiModel;
        public Card_SeleksiRegistrasi(MainRegistrasi mainRegistrasi, PilihSeleksiModel model)
        {
            InitializeComponent();
            DataContext = model;
            _mainRegistrasi = mainRegistrasi;
            _pilihSeleksiModel = model;
        }

        private void btn_RegistrasiTestor_Click(object sender, RoutedEventArgs e)
        {
            string id = _pilihSeleksiModel.id.ToString();
            string nama = _pilihSeleksiModel.nama.ToString();
            _mainRegistrasi._menubar.CallViewRegistrasiTestor(id,nama);
        }
    }
}
