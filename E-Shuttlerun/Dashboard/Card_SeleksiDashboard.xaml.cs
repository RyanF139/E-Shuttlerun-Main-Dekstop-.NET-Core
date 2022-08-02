using E_ShuttleRun.Model;
using System;
using System.Collections.Generic;
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

namespace E_Shuttlerun.Dashboard
{
    /// <summary>
    /// Interaction logic for Card_SeleksiDashboard.xaml
    /// </summary>
    public partial class Card_SeleksiDashboard : UserControl
    {
        public MainDashboard _mainDashboardl;
        public PilihSeleksiModel _pilihSeleksiModel;
        public Card_SeleksiDashboard(MainDashboard mainDashboardl, PilihSeleksiModel model)
        {
            InitializeComponent();            
            DataContext = model;
            _mainDashboardl = mainDashboardl;

            RoutedEventArgs newEventArgs = new RoutedEventArgs(Button.ClickEvent);
            btn_ViewDashboard.RaiseEvent(newEventArgs);

        }

        private void btn_ViewDashboard_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            PilihSeleksiModel model = btn.DataContext as PilihSeleksiModel;

            string idSeleksi = model.id;        
            
            _mainDashboardl.CallDashboardInfo(idSeleksi);
            _mainDashboardl.CallGridNilaiTerbaik();
        }
    }
}
