using E_Shuttlerun.Model;
using E_ShuttleRun.Model;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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

namespace E_Shuttlerun.Daftar_Capaian
{
    /// <summary>
    /// Interaction logic for ViewDaftarCapaian.xaml
    /// </summary>
    public partial class ViewDaftarCapaian : UserControl
    {
        private int numberOfRecPerPage;
        //To check the paging direction according to use selection.
        private enum PagingMode { First = 1, Next = 2, Previous = 3, Last = 4, PageCountChange = 5 };
        

        public ObservableCollection<ListCapaianModel> DataCapaian { get; } = new ObservableCollection<ListCapaianModel>();
        private static readonly HttpClient client = new HttpClient();

        string urlStandalone = System.Configuration.ConfigurationManager.AppSettings["SERVER_API_LOCAL"];
        string urlIntegrated = System.Configuration.ConfigurationManager.AppSettings["SERVER_API"];
        string url;
        string mode;

        //string routecapaian = "/peserta/capaian/";
        string seleksiid;
        string namaseleksi;

        public string search;
        public int totalData, lastPage, PageIndex, perPage;

        public Main_App.MenuBar _menubar;
        public ViewDaftarCapaian(Main_App.MenuBar menubar)
        {
            InitializeComponent();
            DataContext = this;
            _menubar = menubar;

            seleksiid = _menubar.seleksiid.ToString();
            namaseleksi = _menubar.namaseleksi.ToString();
            titleLabel.Text = namaseleksi;
            mode = _menubar.mode.ToString();


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


            ValidateUrl();
            getDataCapaian();
           
        }

        private void ValidateUrl()
        {
            if ( mode == "Integrated")
            {
                url = urlIntegrated;
            }
            else if ( mode == "Standalone")
            {
                url = urlStandalone;
            }
        }

        private async void getDataCapaian()
        {
            try
            {               
                string route = "/api/v2/garjas/nilai/list?seleksiid=" + seleksiid + "&subjenis_test=SHUTTLE" + " " + "RUN&page=" + PageIndex.ToString() + "&per_page=" + perPage.ToString()+ "&search=";

                if (search != null)
                {
                    route = route + search.ToString();
                }


                string GET_Daftar_Capaian = url + route;

                Console.WriteLine(GET_Daftar_Capaian);

                HttpResponseMessage response = await client.GetAsync(GET_Daftar_Capaian);
                response.EnsureSuccessStatusCode();

                var responseString = await client.GetStringAsync(GET_Daftar_Capaian);
                JObject stuff = JObject.Parse(responseString);

                perPage = int.Parse(stuff["per_page"].ToString());
                totalData = int.Parse(stuff["total_data"].ToString());
                lastPage = int.Parse(stuff["total_page"].ToString());

                //decimal lastpage = totalData / perPage;
                //lastPage = (int)Math.Ceiling(lastpage);

                JArray data = (JArray)stuff["data"];


                DataCapaian.Clear();
                foreach (JObject obj in data)
                {
                    //Console.Write(obj);

                    DataCapaian.Add(new ListCapaianModel
                    {
                        NO_TEST = obj["id"].ToString(),
                        NAMA = obj["nama"].ToString(),
                        HASIL = obj["hasil"].ToString(),                        
                        TESTOR = obj["testor"].ToString(),
                        WAKTU_SELESAI = obj["waktu_selesai"].ToString(),
                    });

                    for (int i = 0; i < DataCapaian.Count; i++)
                    {
                        DataCapaian[i].NO = i + 1;
                        Console.Write(DataCapaian[i].NO);
                    }
                }
            }
            catch (HttpRequestException e)
            {

            }

        }

        public void export(string FileName)
        {
            Microsoft.Office.Interop.Excel.Application excel = null;
            Microsoft.Office.Interop.Excel.Workbook wb = null;
            object missing = Type.Missing;
            Microsoft.Office.Interop.Excel.Worksheet ws = null;
            Microsoft.Office.Interop.Excel.Range rng = null;

            // collection of DataGrid Items
            var dtExcelDataTable = dgvCapaian;
            excel = new Microsoft.Office.Interop.Excel.Application();
            wb = excel.Workbooks.Add();
            ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
            ws.Columns.AutoFit();
            ws.Columns.EntireColumn.ColumnWidth = 25;

            // Header row
            for (int Idx = 0; Idx < dtExcelDataTable.Columns.Count; Idx++)
            {
                ws.Range["A1"].Offset[0, Idx].Value = dtExcelDataTable.Columns[Idx].Header;
                ws.Range["A1"].Offset[0, Idx].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                ws.Range["A1"].Offset[0, Idx].Cells.Font.Bold = true;
            }

            // Data Rows
            ListCapaianModel attendance;
            List<String> data = new List<String>();
            for (int Idx = 0; Idx < dtExcelDataTable.Items.Count; Idx++)
            {
                attendance = dtExcelDataTable.Items[Idx] as ListCapaianModel;
                data.Add(attendance.NO.ToString());
                data.Add(attendance.NO_TEST);
                data.Add(attendance.NAMA);
                data.Add(attendance.HASIL);                
                data.Add(attendance.TESTOR);
                data.Add(attendance.WAKTU_SELESAI);
                for (int col = 0; col < dtExcelDataTable.Columns.Count; col++)
                {
                    ws.Range["A2"].Offset[Idx, col].Resize[1].Value = data[col];

                    ws.Range["A2"].Offset[Idx, col].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    ws.Range["A2"].Offset[Idx, 1].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                }
                data.Clear();
            }

            //excel.Visible = false;
            //wb.Activate();
            excel.Visible = false;
            wb.SaveAs(FileName);
            wb.Close();

            Process proc = new Process();
            proc.StartInfo.FileName = FileName;
            proc.Start();
        }

        private void btn_Kembali_DaftarCapaian_Click(object sender, RoutedEventArgs e)
        {
            _menubar.CallMainDaftarCapaian();
        }

        private void btn_DownloadDaftarCapaian_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog filename = new SaveFileDialog();
                filename.Filter = "Comma-separated values file (*.xlsx)|*.xlsx";
                filename.FileName = "Rekap Hasil ShutlleRun " + namaseleksi + " " + DateTime.Now.ToString("yyyyMMddhhmm");
                if (filename.ShowDialog() == true)
                {
                    FileInfo fi = new FileInfo(filename.FileName);
                    export(filename.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Previous_Click(object sender, RoutedEventArgs e)
        {
            if (PageIndex != 1)
            {
                PageIndex -= 1;
                getDataCapaian();
            }
            else
            {
                MessageBox.Show("tidak ada data lagi");
            }
        }

        private void cbNumberOfRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbNumberOfRecords.Text != "")
            {
                string text = (sender as ComboBox).SelectedItem as string;
                perPage = int.Parse(text);
                getDataCapaian();
            }
        }

        private void CommentTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            PageIndex = 1;
            search = CommentTextBox.Text;
            getDataCapaian();
        }

        private void btn_First_Click(object sender, RoutedEventArgs e)
        {
            PageIndex = 1;
            getDataCapaian();
        }

        private void btn_Next_Click(object sender, RoutedEventArgs e)
        {        

            if ((DataCapaian.Count * PageIndex) < totalData && PageIndex != lastPage)
            {
                PageIndex += 1;                
                getDataCapaian();
            }
            else
            {
                MessageBox.Show("tidak ada data lagi");
            }
        }

        private void btn_Last_Click(object sender, RoutedEventArgs e)
        {
            PageIndex = lastPage;
            getDataCapaian();
        }
    }
}
