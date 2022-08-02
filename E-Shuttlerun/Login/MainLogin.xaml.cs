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

namespace E_Shuttlerun.Login
{
    /// <summary>
    /// Interaction logic for MainLogin.xaml
    /// </summary>
    public partial class MainLogin : UserControl
    {
        public string mode;
        public string status_user; // From Login & Change Password
        public string nama_user; // From Login & Change Password

        public MainWindow _main;        
        public MainLogin(MainWindow main)
        {
            InitializeComponent();
            DataContext = this;
            _main = main;
            CallCardLogin();
            ToggleStandalone.IsChecked= true;                                  
        }

        public void CallCardLogin()
        {
            Card_Login card_Login = new Card_Login(this);
            PanelLogin.Children.Clear();
            PanelLogin.Children.Add(card_Login);
        }

        public void callapp(String _status_user, String _nama_user)
        {
            status_user = _status_user.ToString(); // From Login & Change Password
            nama_user = _nama_user.ToString(); // From Login & Change Password
            
            _main.CallMainApp(status_user, nama_user, mode);
        }
        
        public void ValidateMode()
        {            
            if (toggleIntegrated.IsChecked == true)
            {
                
                mode = "Integrated";
                lblStatusMode.Text = mode;

            }
            else if (ToggleStandalone.IsChecked == true)
            {
                mode = "Standalone";
                lblStatusMode.Text = mode;
            }
        }
        

        public void toggleIntegrated_Checked(object sender, RoutedEventArgs e)
        {
            ToggleStandalone.IsChecked = false;
            toggleIntegrated.IsEnabled = true;
            
            ValidateMode();            
           
        }

        public void ToggleStandalone_Checked(object sender, RoutedEventArgs e)
        {
            toggleIntegrated.IsChecked = false;
            ToggleStandalone.IsEnabled = true;
            
            ValidateMode();
        }
    }
}
