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

namespace E_Shuttlerun.Dashboard
{
    /// <summary>
    /// Interaction logic for MainDashboard.xaml
    /// </summary>
    public partial class MainDashboard : UserControl
    {
        private static readonly HttpClient client = new HttpClient();

        string urlStandalone = System.Configuration.ConfigurationManager.AppSettings["SERVER_API_LOCAL"];
        string urlIntegrated = System.Configuration.ConfigurationManager.AppSettings["SERVER_API"];
        string url;
        string mode;
        string route = "/seleksi";

        public string IdSeleksi;

        public Main_App.MenuBar _menuBar;
        public MainDashboard(Main_App.MenuBar menuBar)
        {
            InitializeComponent();                     
            _menuBar = menuBar;

            mode = _menuBar.mode.ToString();

            ValidateUrl();
            GetSeleksi();            
            
        }

        private void ValidateUrl()
        {
            if ( mode == "Integrated" )
            {
                url = urlIntegrated;
            }
            else if ( mode == "Standalone" )
            {
                url = urlStandalone;
            }
        }

        public void CallDashboardInfo(String _IdSeleksi)
        {
            IdSeleksi = _IdSeleksi.ToString(); //From Card Seleksi Dahboard

            Card_DashboardInfo card_DashboardInfo = new Card_DashboardInfo(this);
            PanelInfoDashboard.Children.Clear();
            PanelInfoDashboard.Children.Add(card_DashboardInfo);
        }

        public void CallGridNilaiTerbaik()
        {
            Data_Grid.dgv_NilaiTerbaik dgvNilaiTerbaik = new Data_Grid.dgv_NilaiTerbaik(this);
            PanelNilaiTerbaik.Children.Clear();
            PanelNilaiTerbaik.Children.Add(dgvNilaiTerbaik);
        }
        
        private async void GetSeleksi()
        {
            try
            {                
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
                    PanelSeleksiDashboard.Children.Add(new Card_SeleksiDashboard(this, new PilihSeleksiModel
                    {
                        id = obj["id"].ToString(),
                        status = obj["status"].ToString(),
                        nama = obj["nama"].ToString(),
                        waktu_mulai = obj["tanggal_mulai"].ToString() + " " + "S.d" + " " + obj["tanggal_selesai"]
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
