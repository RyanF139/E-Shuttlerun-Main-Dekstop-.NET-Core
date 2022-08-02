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
    /// Interaction logic for Form_EditSeleksi.xaml
    /// </summary>
    public partial class Form_EditSeleksi : UserControl
    {
        private static readonly HttpClient client = new HttpClient();

        public Main_App.MenuBar _menuBar;
        string url = System.Configuration.ConfigurationManager.AppSettings["SERVER_API_LOCAL"];
        string route = "/seleksi/";

        public Form_EditSeleksi(Main_App.MenuBar menuBar)
        {
            InitializeComponent();
            _menuBar = menuBar;

            GetEditSeleksi();
        }

        private void btn_Batal_Click(object sender, RoutedEventArgs e)
        {
            string message = "are you sure you want cancel to Edit this selection ?";
            if (MessageBox.Show(message, "Message", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _menuBar.CallMainListSeleksi();
            }
            
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {

            string message = "are you sure you want to Edit this selection ?";
            if (MessageBox.Show(message, "Message", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                EditSeleksi();
            }
           
        }


        public async void EditSeleksi()
        {
            try
            {
                string id = _menuBar.seleksiid.ToString();
                var values = new Dictionary<string, string>
                {
                    { "nama", textNamaSeleksi.Text },
                    { "jenis_peserta", textJenisPeserta.Text },
                    { "pemilik", textPemilik.Text },
                    { "tanggal_mulai", textTanggalmulai.Text },
                    { "tanggal_selesai", textTanggalAkhir.Text },
                    { "sprintid", textSprintId.Text },
                    { "angkatanid", textAngkatanId.Text }
                };


                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync(url + route+id, content);
                var responseString = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(responseString);

                JObject stuff = JObject.Parse(responseString);
                string status = stuff["status"].ToString();
                string message = stuff["message"].ToString();

                if (status == "Success")
                {
                    MessageBox.Show(message);
                    _menuBar.CallMainListSeleksi();
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

        public async void GetEditSeleksi()
        {
            try
            {
                
                

                string id = _menuBar.seleksiid.ToString();
                string GetSeleksi = url + route+id;

                Console.WriteLine(GetSeleksi);

                HttpResponseMessage response = await client.GetAsync(GetSeleksi);
                response.EnsureSuccessStatusCode();
                var responseString = await client.GetStringAsync(GetSeleksi);

                JObject stuff = JObject.Parse(responseString);

                if (stuff["status"].ToString() == "Success")
                {
                    JObject data = JObject.Parse(stuff["data"].ToString());

                    textNamaSeleksi.Text = data["nama"].ToString();
                    textJenisPeserta.Text = data["jenis_peserta"].ToString();
                    textPemilik.Text = data["pemilik"].ToString();
                    textTanggalmulai.Text = data["tanggal_mulai"].ToString();
                    textTanggalAkhir.Text = data["tanggal_selesai"].ToString();
                    textSprintId.Text = data["sprintid"].ToString();
                    textAngkatanId.Text = data["angkatanid"].ToString();
                    
                }
                else 
                {
                    MessageBox.Show("Tidak Ada Data");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
    }
}
