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

namespace E_Shuttlerun.Registrasi_Testor
{
    /// <summary>
    /// Interaction logic for Card_RegistrasiTestor.xaml
    /// </summary>
    public partial class Card_RegistrasiTestor : UserControl
    {

        private static readonly HttpClient client = new HttpClient();

        

        public string urlStandalone = System.Configuration.ConfigurationManager.AppSettings["SERVER_API_LOCAL"];
        public string urlIntegrated = System.Configuration.ConfigurationManager.AppSettings["SERVER_API"];
        public string url;
        public string routetestor = "/api/v2/garjas/testor";
        string mode;

        string SeleksiID;

        ViewRegistrasiTestor _viewRegistrasiTestor;
        public Card_RegistrasiTestor(ViewRegistrasiTestor viewRegistrasiTestor)
        {
            InitializeComponent();
            _viewRegistrasiTestor = viewRegistrasiTestor;

            SeleksiID = _viewRegistrasiTestor.idSeleksi.ToString();
            mode = _viewRegistrasiTestor._menubar.mode.ToString();
            ValidateUrl();
        }

        private void ValidateUrl()
        {
            if (mode == "Integrated")
            {
                url = urlIntegrated;
            }
            else if (mode == "Standalone")
            {
                url = urlStandalone;
            }
        }

        private void btn_TambahTestor_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxNamaTestor.Text == "" || textBoxNRP.Text == "")
            {
                warningregistrasi.Text = " Harap Isi Nama dan NRP !! ";
            }
            else
            {
                string message = "Are you sure you want to add " + textBoxNamaTestor.Text + " as a Testor ?";
                if (MessageBox.Show(message, "Message", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    TambahPeserta();
                }
                else 
                {
                    textBoxNamaTestor.Clear();
                    textBoxNRP.Clear();
                }
                            
            }
        }

        private async void TambahPeserta()
        {
            try
            {
                var values = new Dictionary<string, string>
                {
                    { "seleksiid", SeleksiID },
                    { "subjenis_test", "SHUTTLE RUN" },
                    { "nama", textBoxNamaTestor.Text },
                    { "nrp", textBoxNRP.Text },                    
                };


                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync(url + routetestor, content);
                var responseString = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(responseString);

                JObject stuff = JObject.Parse(responseString);
                string status = stuff["status"].ToString();
                string message = stuff["message"].ToString();                
                _viewRegistrasiTestor.ShowMessage(message);
                _viewRegistrasiTestor.GetTestor();
                textBoxNamaTestor.Text = "";
                textBoxNRP.Text = "";

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
