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

namespace E_Shuttlerun.Registrasi_Testor
{
    /// <summary>
    /// Interaction logic for ViewRegistrasiTestor.xaml
    /// </summary>
    public partial class ViewRegistrasiTestor : UserControl
    {        
        public ObservableCollection<ListTestorModel> DataTestor { get; } = new ObservableCollection<ListTestorModel>();
        private static readonly HttpClient client = new HttpClient();

        public string urlStandalone = System.Configuration.ConfigurationManager.AppSettings["SERVER_API_LOCAL"];
        public string urlIntegrated = System.Configuration.ConfigurationManager.AppSettings["SERVER_API"];
        public string url;
        public string routeListTestor = "/api/v2/garjas/testor?subjenis_test=SHUTTLE RUN&seleksiid=";
        public string routeDeleteTestor = "/api/v2/garjas/testor/";
        string mode;

        public string idtestor;
        public string namatestor;
        public string nrptestor;
        public string idSeleksi;

        public Main_App.MenuBar _menubar;
        public ViewRegistrasiTestor(Main_App.MenuBar menubar)
        {
            InitializeComponent();
            DataContext = this;
            _menubar = menubar;

            idSeleksi = _menubar.seleksiid.ToString();
            mode = _menubar.mode.ToString();

            ValidateUrl();
            GetTestor();
            CallCardRegistrasiTestor();
            
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

        private void btn_Kembali_RegistrasiTestor_Click(object sender, RoutedEventArgs e)
        {
            _menubar.CallMainRegistrasi();
        }

        private void DataGridColumnHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ListTestorModel model = btn.DataContext as ListTestorModel;
            idtestor = model.Id.ToString();
            namatestor = model.Nama.ToString();
            nrptestor = model.NRP.ToString();


            CallCardEditTestor();
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ListTestorModel model = btn.DataContext as ListTestorModel;

            idtestor = model.Id.ToString();
            namatestor = model.Nama.ToString();
            nrptestor = model.NRP.ToString();

            string message = "Are you sure you want to Remove a Testor ?";
            if (MessageBox.Show(message, "Message", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var response = await client.DeleteAsync(url + routeDeleteTestor + idtestor);
                var responstring = await response.Content.ReadAsStringAsync();
                Console.Write(responstring);
                GetTestor();

                ShowMessage("Testor was deleted successfully!");
            }
            
        }

        public async void GetTestor()
        {
            try
            {

                string GET_Data_Testor = url + routeListTestor + idSeleksi;

                Console.WriteLine(GET_Data_Testor);

                HttpResponseMessage response = await client.GetAsync(GET_Data_Testor);
                response.EnsureSuccessStatusCode();

                var responseString = await client.GetStringAsync(GET_Data_Testor);
                JObject stuff = JObject.Parse(responseString);

                JArray data = (JArray)stuff["data"];

                int i = 0;

                DataTestor.Clear();
                foreach (JObject obj in data)
                {
                    //Console.Write(obj);
                    i = i + 1;
                    DataTestor.Add(new ListTestorModel
                    {
                        Id = obj["id"].ToString(),
                        Nama = obj["nama"].ToString(),
                        NRP = obj["nrp"].ToString(),
                        No = i
                    });
                }
            }
            catch (HttpRequestException e)
            {

            }
        }

        public void CallCardRegistrasiTestor()
        {
            Card_RegistrasiTestor card_RegistrasiTestor = new Card_RegistrasiTestor(this);
            PanelTambahEditTestor.Children.Clear();
            PanelTambahEditTestor.Children.Add(card_RegistrasiTestor);
        }
                
        private void CallCardEditTestor()
        {
            Card_EditTestor card_EditTestor = new Card_EditTestor(this);
            PanelTambahEditTestor.Children.Clear();
            PanelTambahEditTestor.Children.Add(card_EditTestor);
        }

        public void ShowMessage(string message)
        {
            DialogMessageDaftarTestor dialogMessage = new DialogMessageDaftarTestor(message);
            dialogMessage.Show();
        }
    }
}
