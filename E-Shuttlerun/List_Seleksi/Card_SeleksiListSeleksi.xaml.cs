using E_ShuttleRun.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace E_Shuttlerun.List_Seleksi
{
    /// <summary>
    /// Interaction logic for Card_SeleksiListSeleksi.xaml
    /// </summary>
    public partial class Card_SeleksiListSeleksi : UserControl
    {
        private static readonly HttpClient client = new HttpClient();

        MainListSeleksi _mainListseleksi;
        PilihSeleksiModel _pilihSeleksiModel;
        public Card_SeleksiListSeleksi(MainListSeleksi mainListseleksi, PilihSeleksiModel model)
        {
            InitializeComponent();
            DataContext = model;
            _mainListseleksi = mainListseleksi;
            _pilihSeleksiModel = model;
        }

        private void btn_EditSeleksi_Click(object sender, RoutedEventArgs e)
        {
            string id = _pilihSeleksiModel.id.ToString();
            _mainListseleksi._menubar.CallFormEditSeleksi(id);
        }

        private void btn_HapusSeleksi_Click(object sender, RoutedEventArgs e)
        {
            string message = "are you sure you want to delete this selection ?";
            if (MessageBox.Show(message, "Message", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                HapusSeleksi();
            }
            
        }

        private void btn_ViewSeleksi_Click(object sender, RoutedEventArgs e)
        {
            string id = _pilihSeleksiModel.id.ToString();
            string nama = _pilihSeleksiModel.nama.ToString();
            _mainListseleksi._menubar.CallViewSeleksi(id,nama);
        }

        public async void HapusSeleksi()
        {
            string id = _pilihSeleksiModel.id.ToString();            

            string url = System.Configuration.ConfigurationManager.AppSettings["SERVER_API_LOCAL"];
            string route = "/seleksi/" + id;
            var response = await client.DeleteAsync(url + route);
            var responstring = await response.Content.ReadAsStringAsync();
            Console.Write(responstring);
            _mainListseleksi._menubar.CallMainListSeleksi();           
        }
        
    }
}
