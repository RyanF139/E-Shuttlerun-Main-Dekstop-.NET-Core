using E_Shuttlerun.Model;
using E_ShuttleRun.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace E_Shuttlerun.Data_Grid
{
    /// <summary>
    /// Interaction logic for dgv_NilaiTerbaik.xaml
    /// </summary>
    public partial class dgv_NilaiTerbaik : UserControl
    {
        public ObservableCollection<ListCapaianModel> DaftarTerbaik { get; } = new ObservableCollection<ListCapaianModel>();
        private static readonly HttpClient client = new HttpClient();

        string urlStandalone = System.Configuration.ConfigurationManager.AppSettings["SERVER_API_LOCAL"];
        string urlIntegrated = System.Configuration.ConfigurationManager.AppSettings["SERVER_API"];
        string url;
        string routeSeleksiId = "/api/v2/garjas/nilai/ranking?seleksiid=";
        string routeSubjenisTest = "&subjenis_test=SHUTTLE RUN";
        string mode;

        string SeleksiId;


        Dashboard.MainDashboard _mainDashboard;
        
        public dgv_NilaiTerbaik(Dashboard.MainDashboard mainDashboard)
        {
            InitializeComponent();
            DataContext = this;
            _mainDashboard = mainDashboard;
            

            mode = _mainDashboard._menuBar.mode.ToString();
            SeleksiId = _mainDashboard.IdSeleksi.ToString();

            ValidateUrl();
            GetDataNilaiTerbaik();
        }

        private void ValidateUrl()
        {
            if ( mode == "Integrated" )
            {
                url = urlIntegrated;
            }
            else if (mode == "Standalone" )
            {
                url = urlStandalone;
            }

        }

        public async void GetDataNilaiTerbaik()
        {
            try
            {                
                string GET_Daftar_Rangking = url + routeSeleksiId + SeleksiId + routeSubjenisTest;

                HttpResponseMessage response = await client.GetAsync(GET_Daftar_Rangking);
                response.EnsureSuccessStatusCode();

                var responseString = await client.GetStringAsync(GET_Daftar_Rangking);
                JObject stuff = JObject.Parse(responseString);

                Console.Write(stuff);

                JArray data = (JArray)stuff["data"];
                Console.Write(data);

                DaftarTerbaik.Clear();
                foreach (JObject obj in data)
                {
                    Console.Write(obj);

                    DaftarTerbaik.Add(new ListCapaianModel
                    {
                        NO_TEST = obj["id"].ToString(),
                        NAMA = obj["nama"].ToString(),
                        HASIL = obj["hasil"].ToString(),
                        TESTOR = obj["testor"].ToString(),
                    });

                    for (int i = 0; i < DaftarTerbaik.Count; i++)
                    {
                        DaftarTerbaik[i].NO = i + 1;
                        Console.Write(DaftarTerbaik[i].NO);
                    }
                }
            }
            catch (HttpRequestException e)
            {

            }

        }
    }    
}
