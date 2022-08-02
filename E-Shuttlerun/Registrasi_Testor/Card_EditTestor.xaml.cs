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

namespace E_Shuttlerun.Registrasi_Testor
{
    /// <summary>
    /// Interaction logic for Card_EditTestor.xaml
    /// </summary>
    public partial class Card_EditTestor : UserControl        
    {

        private static readonly HttpClient client = new HttpClient();

        public string urlStandalone = System.Configuration.ConfigurationManager.AppSettings["SERVER_API_LOCAL"];
        public string urlIntegrated = System.Configuration.ConfigurationManager.AppSettings["SERVER_API"];
        public string url;
        public string routetestor = "/api/v2/garjas/testor/";
        string mode;


        string idTestor;        

        public ViewRegistrasiTestor _viewRegistrasiTestor;        
        public Card_EditTestor(ViewRegistrasiTestor viewRegistrasiTestor)
        {
            InitializeComponent();
            _viewRegistrasiTestor = viewRegistrasiTestor;

            idTestor = _viewRegistrasiTestor.idtestor.ToString();
            textBoxNamaTestor.Text = _viewRegistrasiTestor.namatestor.ToString();
            textBoxNRP.Text = _viewRegistrasiTestor.nrptestor.ToString(); 
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

        private void btn_EditTEstor_Click(object sender, RoutedEventArgs e)
        {

            if (textBoxNamaTestor.Text == "" || textBoxNRP.Text == "")
            {
                WarningEdit.Text = "Harap Isi Nama dan NRP !!";
            }
            else 
            {
                string message = "Are you sure you want to edit a Testor ?";
                if (MessageBox.Show(message, "Message", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    EditTestor();
                }                         
            }
            
        }

        private void btn_BatalEdit_Click(object sender, RoutedEventArgs e)
        {
            
            string message = "are you sure you want cancel to edit ?";
            if (MessageBox.Show(message, "Message", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _viewRegistrasiTestor.CallCardRegistrasiTestor();
            }
        }

        private async void EditTestor()
        {
            try
            {                
                var values = new Dictionary<string, string>
                {
                    { "nama", textBoxNamaTestor.Text},
                    { "nrp",  textBoxNRP.Text },                    
                };


                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync(url + routetestor + idTestor, content);
                var responseString = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(responseString);

                JObject stuff = JObject.Parse(responseString);
                string status = stuff["status"].ToString();
                string message = stuff["message"].ToString();

                if (status == "Success")
                {
                    _viewRegistrasiTestor.ShowMessage(message);
                    _viewRegistrasiTestor.CallCardRegistrasiTestor();
                    _viewRegistrasiTestor.GetTestor();
                }
                else
                {
                    MessageBox.Show("Tidak ada Data");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
    }
}
