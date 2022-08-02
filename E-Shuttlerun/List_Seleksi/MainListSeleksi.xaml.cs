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
    /// Interaction logic for MainListSeleksi.xaml
    /// </summary>
    public partial class MainListSeleksi : UserControl
    {

        private static readonly HttpClient client = new HttpClient();

        string url = System.Configuration.ConfigurationManager.AppSettings["SERVER_API_LOCAL"];
        string route_seleksi_all = "/seleksi";
        string route_seleksi_started = "/seleksi/started";
        string route_seleksi_done = "/seleksi/done";

        public Main_App.MenuBar _menubar;
        public MainListSeleksi(Main_App.MenuBar menubar)
        {
            InitializeComponent();
            GetSeleksi();
            _menubar = menubar;
        }

        private void btn_Tambah_Seleksi_Click(object sender, RoutedEventArgs e)
        {
            _menubar.CallFormSeleksi();
        }

        public async void GetSeleksi()
        {
            try
            {
                string route = route_seleksi_started;
                string GET_ListSeleksi = url + route;
                HttpResponseMessage response = await client.GetAsync(GET_ListSeleksi);
                response.EnsureSuccessStatusCode();

                var responseString = await client.GetStringAsync(GET_ListSeleksi);
                JObject stuff = JObject.Parse(responseString);

                Console.Write(stuff);

                JArray data = (JArray)stuff["data"];
                Console.Write(data);


                foreach (JObject obj in data)
                {
                    PanelMainListSeleksi.Children.Add(new Card_SeleksiListSeleksi(this, new PilihSeleksiModel
                    {
                        id = obj["id"].ToString(),
                        status = obj["status"].ToString(),
                        nama = obj["nama"].ToString(),
                        waktu_mulai = ((DateTime)obj["tanggal_mulai"]) + " " + "S.d" + " " + obj["tanggal_selesai"]
                    }));
                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show("Tidak Ada Seleksi");
            }


        }
    }
}
