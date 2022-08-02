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

namespace E_Shuttlerun
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public string status_user;
        public string nama_user;
        public string mode;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CallLogin();            
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        public void CallLogin()
        {
            Login.MainLogin mainLogin = new Login.MainLogin(this);
            PanelMainPanel.Children.Clear();
            PanelMainPanel.Children.Add(mainLogin);
            
        }

        public void CallMainApp(String _status_user, String _nama_user, String _mode)
        {
            status_user = _status_user.ToString(); // From Main Login
            nama_user = _nama_user.ToString(); // From Main Login
            mode = _mode.ToString();            
            Main_App.MainApp mainApp = new Main_App.MainApp(this);
            PanelMainPanel.Children.Clear();
            PanelMainPanel.Children.Add(mainApp);
        }
    }
}
