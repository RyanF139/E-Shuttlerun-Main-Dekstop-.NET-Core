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
    /// Interaction logic for Form_TambahSeleksi.xaml
    /// </summary>
    public partial class Form_TambahSeleksi : UserControl
    {

        private static readonly HttpClient client = new HttpClient();

        string url = System.Configuration.ConfigurationManager.AppSettings["SERVER_API_LOCAL"];
        string route = "/seleksi";


        Main_App.MenuBar _menuBar;
        public Form_TambahSeleksi(Main_App.MenuBar menuBar)
        {
            InitializeComponent();
            _menuBar = menuBar;
        }

        private void btn_Tambah_Click(object sender, RoutedEventArgs e)
        {
            if (
                textNamaSeleksi.Text == "" || textJenisPeserta.Text == "" ||
                textPemilik.Text == "" || textTanggalmulai.Text == "" ||
                textTanggalAkhir.Text == "" || textSprintId.Text == "" ||
                textAngkatanId.Text == "") 
            {
                MessageBox.Show("Pastikan semua telah di isi ");
            }                
            else
            {
                string message = "are you sure you want to add this selection ?";
                if (MessageBox.Show(message, "Message", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    TambahSeleksi();
                }
                
            }
        }

        private void btn_ClearForm_Click(object sender, RoutedEventArgs e)
        {
            string message = "are you sure you want clear this form ?";
            if (MessageBox.Show(message, "Message", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                textNamaSeleksi.Text = "";
                textJenisPeserta.Text = "";
                textPemilik.Text = "";
                textTanggalmulai.Text = "";
                textTanggalAkhir.Text = "";
                textSprintId.Text = "";
                textAngkatanId.Text = "";
            }

            
        }

        private void btn_Batal_Click(object sender, RoutedEventArgs e)
        {
            string message = "are you sure you want cancel to add this selection ?";
            if (MessageBox.Show(message, "Message", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _menuBar.CallMainListSeleksi();
            }
             
        }

       /* private void ValidasiTanggalMulai()
        {
            int tanggalmulai =(textTanggalmulai.Text);
            int tanggalakhir = Int32.Parse(textTanggalAkhir.Text);
            if (tanggalakhir <= tanggalmulai || tanggalmulai >= tanggalakhir)
            {
                MessageBox.Show("Moho periksa kembali tanggal mulai dan akhir seleksi");
            }
            else 
            {
                TambahSeleksi();
            }
        }*/

        public async void TambahSeleksi()
        {
            try
            {


               
                var values = new Dictionary<string, string>
                {
                    { "nama", textNamaSeleksi.Text },
                    { "jenis_peserta", textJenisPeserta.Text },
                    { "pemilik", textPemilik.Text },
                    { "tanggal_mulai", textTanggalmulai.Text },
                    { "tanggal_selesai", textTanggalAkhir.Text },
                    { "sprintid", textSprintId.Text },
                    { "angkatanid", textAngkatanId.Text },                    
                };


                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync(url + route, content);
                var responseString = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(responseString);

                JObject stuff = JObject.Parse(responseString);
                
                string message = stuff["message"].ToString();
                MessageBox.Show(message);
                _menuBar.CallMainListSeleksi();
                
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
