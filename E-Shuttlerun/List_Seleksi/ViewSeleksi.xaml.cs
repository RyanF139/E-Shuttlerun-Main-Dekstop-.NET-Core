using E_ShuttleRun.Model;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
    /// Interaction logic for ViewSeleksi.xaml
    /// </summary>
    public partial class ViewSeleksi : UserControl
    {
        private int numberOfRecPerPage;
        //To check the paging direction according to use selection.
        private enum PagingMode { First = 1, Next = 2, Previous = 3, Last = 4, PageCountChange = 5 };

        

        string url = System.Configuration.ConfigurationManager.AppSettings["SERVER_API_LOCAL"];        
        string routeUploadPeserta = "/api/v2/garjas/peserta/upload";        

        public ObservableCollection<ListPesertaModel> DataPeserta { get; } = new ObservableCollection<ListPesertaModel>();
        private static readonly HttpClient client = new HttpClient();

        string idseleksi;
        string path;

        public string search;
        public int totalData, lastPage, PageIndex, perPage;

        Main_App.MenuBar _MenuBar;
        public ViewSeleksi(Main_App.MenuBar menuBar)
        {
            InitializeComponent();
            DataContext=this;
            _MenuBar = menuBar;

            idseleksi = _MenuBar.seleksiid.ToString();
            titleLabel.Text = _MenuBar.namaseleksi.ToString();

            cbNumberOfRecords.Items.Add("5");
            cbNumberOfRecords.Items.Add("10");
            cbNumberOfRecords.Items.Add("15");
            cbNumberOfRecords.Items.Add("20");
            cbNumberOfRecords.Items.Add("25");
            cbNumberOfRecords.Items.Add("30");

            search = null;
            lastPage = 1;
            PageIndex = 1;
            perPage = int.Parse(cbNumberOfRecords.Text);

            getDataPeserta();

        }        
        private async void getDataPeserta()
        {

            try
            {               
                string route = "/api/v2/garjas/list/peserta?seleksiid=" + idseleksi + "&subjenis_test=SHUTTLE" + " " + "RUN&page=" + PageIndex.ToString() + "&per_page=" + perPage.ToString() + "&search=";

                if (search != null)
                {
                    route = route + search.ToString();
                }


                string GET_List_Peserta = url + route;

                Console.WriteLine(GET_List_Peserta);

                HttpResponseMessage response = await client.GetAsync(GET_List_Peserta);
                response.EnsureSuccessStatusCode();

                var responseString = await client.GetStringAsync(GET_List_Peserta);
                JObject stuff = JObject.Parse(responseString);

                perPage = int.Parse(stuff["per_page"].ToString());
                totalData = int.Parse(stuff["total_data"].ToString());
                lastPage = int.Parse(stuff["total_page"].ToString());
                

                JArray data = (JArray)stuff["data"];


                DataPeserta.Clear();                
                foreach (JObject obj in data)
                {
                    //Console.Write(obj);

                    DataPeserta.Add(new ListPesertaModel
                    {
                        NAMA = obj["nama"].ToString(),
                        NRP = obj["nrp"].ToString(),
                        PANGKAT = obj["pangkat"].ToString(),
                        JABATAN = obj["jabatan"].ToString(),
                        KORPS = obj["korps"].ToString(),
                        TANGGAL_LAHIR = obj["tanggal_lahir"].ToString()
                    }); ;

                    for (int i = 0; i < DataPeserta.Count; i++)
                    {
                        DataPeserta[i].NO = i + 1;
                        Console.Write(DataPeserta[i].NO);
                    }
                }
            }
            catch (HttpRequestException e)
            {

            }

        }


        private void btn_Kembali_Click(object sender, RoutedEventArgs e)
        {
            _MenuBar.CallMainListSeleksi();
        }

        private void btn_ImportPeserta_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            path = dialog.FileName;
            UploadList();
            
        }

        public async void UploadList()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (var content = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture)))
                    {
                        FileInfo info = new FileInfo(path);
                        byte[] image = File.ReadAllBytes(info.FullName);
                        string name = info.Name;

                        var streamContent = new StreamContent(info.OpenRead());
                        streamContent.Headers.Add("Content-Type", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        var seleksi = new StringContent(idseleksi);
                        content.Add(seleksi, "seleksi_id");
                        content.Add(streamContent , "file", name );

                        //MessageBox.Show(idseleksi);                        
                        Console.WriteLine(content);

                        using (
                            var message =
                            await client.PostAsync(url+routeUploadPeserta, content))                            
                        {
                            var ret = await message.Content.ReadAsStringAsync();
                            Console.WriteLine(ret);
                            
                        }
                    }

                }
                getDataPeserta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error upload file", MessageBoxButton.OK, MessageBoxImage.Error);
            }

         }

        private void btn_Previous_Click(object sender, RoutedEventArgs e)
        {
            if (PageIndex != 1)
            {
                PageIndex -= 1;
                getDataPeserta();
            }
            else
            {
                MessageBox.Show("tidak ada data lagi");
            }
        }

        private void btn_First_Click(object sender, RoutedEventArgs e)
        {
            PageIndex = 1;
            getDataPeserta();
        }

        private void cbNumberOfRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbNumberOfRecords.Text != "")
            {
                string text = (sender as ComboBox).SelectedItem as string;
                perPage = int.Parse(text);
                getDataPeserta();
            }
        }

        private void btn_Next_Click(object sender, RoutedEventArgs e)
        {
            if ((DataPeserta.Count * PageIndex) < totalData && PageIndex != lastPage)
            {
                PageIndex += 1;
                getDataPeserta(); ;
            }
            else
            {
                MessageBox.Show("tidak ada data lagi");
            }
        }

        private void btn_Last_Click(object sender, RoutedEventArgs e)
        {
            PageIndex = lastPage;
            getDataPeserta();
        }
        private void Search_KeyUp(object sender, KeyEventArgs e)
        {
            PageIndex = 1;
            search = Search.Text;
            getDataPeserta();
        }
                                        

        

        
        
    }
}
