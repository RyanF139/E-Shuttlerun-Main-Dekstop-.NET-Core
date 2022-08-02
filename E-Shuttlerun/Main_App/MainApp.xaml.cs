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

namespace E_Shuttlerun.Main_App
{
    /// <summary>
    /// Interaction logic for MainApp.xaml
    /// </summary>
    public partial class MainApp : UserControl
    {
        public MainWindow _main;
        public string status_user; // from Main Windows
        public string nama_user;
        public string mode;
        public MainApp(MainWindow main)
        {
            InitializeComponent();
            _main = main;        
                        
            status_user = _main.status_user.ToString(); 
            nama_user = _main.nama_user.ToString(); 
            mode = _main.mode.ToString() ;
            LabelUser.Content = status_user;
            CallMenuBar();
           
        }

        private void btn_LogOut_Click(object sender, RoutedEventArgs e)
        {
            
            killProcessTrack();
            if (MessageBox.Show("Are you sure you want to log out ?", "Log Out Message", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _main.CallLogin();
            }
            
        }

        private void btn_ConfigReader_Click(object sender, RoutedEventArgs e)
        {

        }

        public void CallMenuBar()
        {            
            MenuBar menuBar = new MenuBar(this);
            PanelMenu.Children.Clear();
            PanelMenu.Children.Add(menuBar);
        }

        public void killProcessTrack()
        {
            Process[] workers = Process.GetProcessesByName("Track_ShuttleRun");
            foreach (Process worker in workers)
            {
                worker.Kill();
                worker.WaitForExit();
                worker.Dispose();
            }
        }
    }
}
