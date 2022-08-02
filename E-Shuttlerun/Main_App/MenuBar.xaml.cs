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

namespace E_Shuttlerun.Main_App
{
    /// <summary>
    /// Interaction logic for MenuBar.xaml
    /// </summary>
    public partial class MenuBar : UserControl
    {
        MainApp _mainapp;        

        public string status_user; // From Main App
        public string nama_user; // From Main App
        public string mode;

        public string seleksiid;
        public string namaseleksi;
        public MenuBar(MainApp mainapp)
        {
            InitializeComponent();
            _mainapp = mainapp;            

            //Hide Menu
            CardDashboard.Visibility = Visibility.Hidden;
            CardRun.Visibility = Visibility.Hidden;
            CardRegistrasi.Visibility = Visibility.Hidden;
            CardDaftarCapaian.Visibility = Visibility.Hidden;
            CardListSelski.Visibility = Visibility.Hidden;

            status_user = _mainapp.status_user.ToString(); //status dari mainapp
            nama_user = _mainapp.nama_user.ToString(); //namaTestor dari mainapp
            mode = _mainapp.mode.ToString(); // From Main App
           
            ValidasiAksesUser();

            RoutedEventArgs newEventArgs = new RoutedEventArgs(Button.ClickEvent);
            btn_Dashboard.RaiseEvent(newEventArgs);

        }

        private void btn_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            killProcessTrack();

            _mainapp.labelNameForm.Text = "Dashboard";

            btn_Dashboard.IsEnabled = false;
            CardDashboard.Background = new SolidColorBrush(Colors.WhiteSmoke);
            IconViewDashboard.Foreground = new SolidColorBrush(Colors.DimGray);
            LabelDashboard.Foreground = new SolidColorBrush(Colors.DimGray);
            
            btn_Run_Normal();
            btn_Registrasi_Normal();
            btn_DaftarCapaian_Normal();
            btn_ListSeleksiNormal();

            CallMainDashboard();

        }
        
        private void btn_Run_Click(object sender, RoutedEventArgs e)
        {
            killProcessTrack();

            _mainapp.labelNameForm.Text = "Run Activity";

            btn_Run.IsEnabled = false;
            CardRun.Background = new SolidColorBrush(Colors.WhiteSmoke);
            IconRun.Foreground = new SolidColorBrush(Colors.DimGray);
            LabelRun.Foreground = new SolidColorBrush(Colors.DimGray);

            btn_Dashboard_Normal();
            btn_Registrasi_Normal();
            btn_DaftarCapaian_Normal();
            btn_ListSeleksiNormal();

            CallMainRun();

        }

        private void btn_Registrasi_Click(object sender, RoutedEventArgs e)
        {
            killProcessTrack();

            _mainapp.labelNameForm.Text = "Registrasi Testor";

            btn_Registrasi.IsEnabled = false;
            CardRegistrasi.Background = new SolidColorBrush(Colors.WhiteSmoke);
            IconRegistrasi.Foreground = new SolidColorBrush(Colors.DimGray);
            LabelRegistrasi.Foreground = new SolidColorBrush(Colors.DimGray);

            btn_Dashboard_Normal();
            btn_Run_Normal();
            btn_DaftarCapaian_Normal();
            btn_ListSeleksiNormal();

            CallMainRegistrasi();

        }

        private void btn_DaftarCapaian_Click(object sender, RoutedEventArgs e)
        {
            killProcessTrack();

            _mainapp.labelNameForm.Text = "Daftar Capaian";

            btn_DaftarCapaian.IsEnabled = false;
            CardDaftarCapaian.Background = new SolidColorBrush(Colors.WhiteSmoke);
            IconDaftarCapaian.Foreground = new SolidColorBrush(Colors.DimGray);
            LabelDaftarCapaian.Foreground = new SolidColorBrush(Colors.DimGray);

            btn_Dashboard_Normal();
            btn_Run_Normal();
            btn_Registrasi_Normal();
            btn_ListSeleksiNormal();

            CallMainDaftarCapaian();
        }

        private void btn_ListSeleksi_Click(object sender, RoutedEventArgs e)
        {
            killProcessTrack();

            _mainapp.labelNameForm.Text = "List Seleksi";

            btn_ListSeleksi.IsEnabled = false;
            CardListSelski.Background = new SolidColorBrush(Colors.WhiteSmoke);
            IconListSeleksi.Foreground = new SolidColorBrush (Colors.DimGray);
            LabelListSeleksi.Foreground = new SolidColorBrush(Colors.DimGray);

            btn_Dashboard_Normal();
            btn_Run_Normal();
            btn_Registrasi_Normal();
            btn_DaftarCapaian_Normal();

            CallMainListSeleksi();
        }

        private void btn_Dashboard_Normal()
        {
            btn_Dashboard.IsEnabled = true;
            CardDashboard.Background = new SolidColorBrush(Colors.Transparent);
            IconViewDashboard.Foreground = new SolidColorBrush(Colors.White);
            LabelDashboard.Foreground = new SolidColorBrush(Colors.White);
        }

        private void btn_Run_Normal()
        {
            btn_Run.IsEnabled = true;
            CardRun.Background = new SolidColorBrush(Colors.Transparent);
            IconRun.Foreground = new SolidColorBrush(Colors.White);
            LabelRun.Foreground = new SolidColorBrush(Colors.White);
        }

        private void btn_Registrasi_Normal()
        {
            btn_Registrasi.IsEnabled = true;
            CardRegistrasi.Background = new SolidColorBrush(Colors.Transparent);
            IconRegistrasi.Foreground = new SolidColorBrush(Colors.White);
            LabelRegistrasi.Foreground = new SolidColorBrush(Colors.White);
        }
       
        private void btn_DaftarCapaian_Normal()
        {
            btn_DaftarCapaian.IsEnabled = true;
            CardDaftarCapaian.Background = new SolidColorBrush(Colors.Transparent);
            IconDaftarCapaian.Foreground = new SolidColorBrush(Colors.White);
            LabelDaftarCapaian.Foreground = new SolidColorBrush(Colors.White);
        }

        private void btn_ListSeleksiNormal()
        {
            btn_ListSeleksi.IsEnabled = true;
            CardListSelski.Background = new SolidColorBrush(Colors.Transparent);
            IconListSeleksi.Foreground = new SolidColorBrush (Colors.White);
            LabelListSeleksi.Foreground = new SolidColorBrush(Colors.White);
        }

        public void ValidasiAksesUser()
        {
            if (status_user == "ADMIN")
            {
                CardDashboard.Visibility = Visibility.Visible;
                CardRun.Visibility = Visibility.Visible;
                CardRegistrasi.Visibility = Visibility.Visible;
                CardDaftarCapaian.Visibility = Visibility.Visible;
                CardListSelski.Visibility = Visibility.Visible;
            }
            else if (status_user == "PANITIA")
            {
                CardDashboard.Visibility = Visibility.Visible;
                CardRun.Visibility = Visibility.Visible;
                CardRegistrasi.Visibility = Visibility.Visible;
                CardDaftarCapaian.Visibility = Visibility.Visible;
                CardListSelski.Visibility = Visibility.Hidden;
            }
            else if (status_user == "TESTOR")
            {
                CardDashboard.Visibility = Visibility.Visible;
                CardRun.Visibility = Visibility.Visible;
                CardRegistrasi.Visibility = Visibility.Hidden;
                CardDaftarCapaian.Visibility = Visibility.Hidden;
                CardListSelski.Visibility = Visibility.Hidden;
            }
        }

        public void CallMainDashboard()
        {
            Dashboard.MainDashboard maindashboard = new Dashboard.MainDashboard(this);
            _mainapp.PanelMainApp.Children.Clear();
            _mainapp.PanelMainApp.Children.Add(maindashboard);
        }

        public void CallMainRun()
        {
            Run.MainRun mainrun = new Run.MainRun(this);
            _mainapp.PanelMainApp.Children.Clear();
            _mainapp.PanelMainApp.Children.Add(mainrun);
        }

        public void CallMainRegistrasi()
        {
            Registrasi_Testor.MainRegistrasi mainRegistrasi = new Registrasi_Testor.MainRegistrasi(this);
            _mainapp.PanelMainApp.Children.Clear();
            _mainapp.PanelMainApp.Children.Add(mainRegistrasi);
        }

        public void CallMainDaftarCapaian()
        {
            Daftar_Capaian.MainDaftarCapaian mainDaftarCapaian = new Daftar_Capaian.MainDaftarCapaian(this);
            _mainapp.PanelMainApp.Children.Clear();
            _mainapp.PanelMainApp.Children.Add(mainDaftarCapaian);
        }

        public void CallMainListSeleksi()
        {
            List_Seleksi.MainListSeleksi mainListSeleksi = new List_Seleksi.MainListSeleksi(this);
            _mainapp.PanelMainApp.Children.Clear();
            _mainapp.PanelMainApp.Children.Add(mainListSeleksi);
        }

        public void CallFormSeleksi()
        {
            List_Seleksi.Form_TambahSeleksi form_TambahSeleksi = new List_Seleksi.Form_TambahSeleksi(this);
            _mainapp.PanelMainApp.Children.Clear();
            _mainapp.PanelMainApp.Children.Add(form_TambahSeleksi);
        }


        
        public void CallFormEditSeleksi(String id)
        {
            seleksiid = id;
            List_Seleksi.Form_EditSeleksi form_EditSeleksi = new List_Seleksi.Form_EditSeleksi(this);
            _mainapp.PanelMainApp.Children.Clear();
            _mainapp.PanelMainApp.Children.Add(form_EditSeleksi);
        }

        public void CallViewSeleksi(String id, String nama)            
        {
            seleksiid=id;
            namaseleksi = nama;
            List_Seleksi.ViewSeleksi viewSeleksi = new List_Seleksi.ViewSeleksi(this);
            _mainapp.PanelMainApp.Children.Clear();
            _mainapp.PanelMainApp.Children.Add(viewSeleksi);
        }

        public void CallViewDaftarCapaian(String _id_seleksi, String _nama_seleksi)
        {
            seleksiid = _id_seleksi;
            namaseleksi = _nama_seleksi;
            Daftar_Capaian.ViewDaftarCapaian viewDaftarCapaian = new Daftar_Capaian.ViewDaftarCapaian(this);
            _mainapp.PanelMainApp.Children.Clear();
            _mainapp.PanelMainApp.Children.Add(viewDaftarCapaian);
        }

        public void CallViewRegistrasiTestor(String id, String nama)
        {
            seleksiid = id;
            namaseleksi = nama;
            Registrasi_Testor.ViewRegistrasiTestor viewRegistrasiTestor = new Registrasi_Testor.ViewRegistrasiTestor(this);
            _mainapp.PanelMainApp.Children.Clear();
            _mainapp.PanelMainApp.Children.Add(viewRegistrasiTestor);
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
