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

namespace E_Shuttlerun.Run
{
    /// <summary>
    /// Interaction logic for MainRun.xaml
    /// </summary>
    public partial class MainRun : UserControl
    {

        private static readonly HttpClient client = new HttpClient();

        string urlStandalone = System.Configuration.ConfigurationManager.AppSettings["SERVER_API_LOCAL"];
        string urlIntegrated = System.Configuration.ConfigurationManager.AppSettings["SERVER_API"];
        string url;
        string route = "/seleksi";
        string mode;

        public Main_App.MenuBar _menuBar;
        public MainRun(Main_App.MenuBar menuBar)
        {
            InitializeComponent();            
            _menuBar = menuBar;
            mode = _menuBar.mode.ToString() ;            
            ValidateUrl();
            GetSeleksi();
        }

        private void ValidateUrl()
        {
            mode = _menuBar.mode;
            if (mode == "Integrated")
            {
                url = urlIntegrated;
            }
            else if (mode == "Standalone")
            {
                url = urlStandalone;
            }
        }

        public async void GetSeleksi()
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
                    PanelMainRun.Children.Add(new Card_SeleksiRun(this, new PilihSeleksiModel
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
