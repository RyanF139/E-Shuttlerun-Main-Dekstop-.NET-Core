using E_ShuttleRun.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    /// Interaction logic for Card_Seleksi.xaml
    /// </summary>
    public partial class Card_SeleksiRun : UserControl
    {
        string Run_Activity_Standalone = System.Configuration.ConfigurationManager.AppSettings["PathRunActivity_Local"];
        string Run_Activity_Integrated = System.Configuration.ConfigurationManager.AppSettings["PathRunActivity"];
        string Run_Activity;

        string nama_testor;  // From Menu Bar
        string mode;  // From Menu Bar
        string seleksiid; // From Model

        public MainRun _mainrun;
        public PilihSeleksiModel _pilihSeleksiModel;
        
        public Card_SeleksiRun(MainRun mainrun, PilihSeleksiModel model)
        {
            InitializeComponent();
            DataContext = model;
            _mainrun = mainrun;
            _pilihSeleksiModel = model;

            nama_testor = _mainrun._menuBar.nama_user;
            mode = _mainrun._menuBar.mode;
            seleksiid = _pilihSeleksiModel.id.ToString();
            ValidateRun();
        }

        private void ValidateRun()
        {
            if (mode == "Integrated")
            {
                Run_Activity = Run_Activity_Integrated;
            }
            else if (mode == "Standalone")
            {
                Run_Activity = Run_Activity_Standalone;
            }
        }

        private void btn_ViewRunActivity_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            PilihSeleksiModel model = btn.DataContext as PilihSeleksiModel;
            string seleksi_id = model.id;
           
            string argument = String.Format("{0} \"{1}\"", nama_testor, seleksiid);
            Console.WriteLine(argument);
            Process p = new Process();
            p.StartInfo.FileName = Run_Activity;
            p.StartInfo.Arguments = argument;
            p.Start();
        }
    }
}
