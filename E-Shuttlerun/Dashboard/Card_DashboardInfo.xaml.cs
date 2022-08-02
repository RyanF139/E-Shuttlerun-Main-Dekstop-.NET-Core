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
    /// Interaction logic for Card_DashboardInfo.xaml
    /// </summary>
    public partial class Card_DashboardInfo : UserControl
    {
        private static readonly HttpClient client = new HttpClient();

        string urlStandAlone = System.Configuration.ConfigurationManager.AppSettings["SERVER_API_LOCAL"];
        string urlIntegrated = System.Configuration.ConfigurationManager.AppSettings["SERVER_API"];
        string url;
        string routeSeleksiID = "/api/v2/garjas/dashboard?seleksiid=";
        string routeSubjenisTes = "&subjenis_test=SHUTTLE RUN";
        string SeleksiId;
        string mode;
        

        MainDashboard _mainDashboard;
        public Card_DashboardInfo(MainDashboard mainDashboard)
        {
            InitializeComponent();
            _mainDashboard = mainDashboard;

            mode = _mainDashboard._menuBar.mode.ToString();
            SeleksiId = _mainDashboard.IdSeleksi.ToString();

            ValidateUrl();
            GetDashboard();
        }

        private void ValidateUrl()
        {
            if ( mode == "Integrated" )
            {
                url = urlIntegrated;
            }
            else if ( mode == "Standalone" )
            {
                url = urlStandAlone;
            }
        }

        public async void GetDashboard()
        {
            try
            {
                
                string GET_Dashboard = url + routeSeleksiID + SeleksiId + routeSubjenisTes;

                Console.WriteLine(GET_Dashboard);

                HttpResponseMessage response = await client.GetAsync(GET_Dashboard);
                response.EnsureSuccessStatusCode();

                var responseString = await client.GetStringAsync(GET_Dashboard);
                //MessageBox.Show(responseString.ToString());
                JObject stuff = JObject.Parse(responseString);

                if (stuff["status"].ToString() == "Success")
                {
                    JObject data = JObject.Parse(stuff["data"].ToString());
                    LabelJumlahPeserta.Text = data["jumlah_peserta"].ToString();
                    LabelJumlahAntrian.Text = data["antrian"].ToString();
                    LabelSelesai.Text = data["selesai"].ToString();

                }

            }
            catch (HttpRequestException e)
            {

            }
        }
    }
}
