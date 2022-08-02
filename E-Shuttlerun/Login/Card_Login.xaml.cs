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

namespace E_Shuttlerun.Login
{
    /// <summary>
    /// Interaction logic for Card_Login.xaml
    /// </summary>
    public partial class Card_Login : UserControl
    {
        private static readonly HttpClient client = new HttpClient();
                        
        string urlStandalone = System.Configuration.ConfigurationManager.AppSettings["SERVER_API_LOCAL"];
        string urlIntegrated = System.Configuration.ConfigurationManager.AppSettings["SERVER_API"];
        string url ;
        string route = "/api/v2/login";

        public string id;
        public string status_user;
        public string nama_user;
        string mode; // from MainLogin

        public MainLogin _mainLogin;        
        public Card_Login(MainLogin mainLogin)
        {
            InitializeComponent();
            _mainLogin = mainLogin;
            mode = _mainLogin.mode; 
            
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            if (textUsername.Text == "" || textPassword.Password =="")
            {
                MessageBox.Show(" Harap Masukan Username/Password");
            }
            else
            {
                ValidateUrl();
                Login();
            }                                
        }

        private void ValidateUrl()
        {
            mode = _mainLogin.mode;
            if (mode == "Integrated")
            {
                url = urlIntegrated;
            }
            else if (mode == "Standalone")
            {
                url = urlStandalone;
            }
        }

        public void CallCardNewPassword()
        {
            Card_NewPassword card_newpassword = new Card_NewPassword(this);
            _mainLogin.PanelLogin.Children.Clear();
            _mainLogin.PanelLogin.Children.Add(card_newpassword);
        }

        public async void Login()
        {
            try
            {
                var values = new Dictionary<string, string>
            {
                { "username", textUsername.Text },
                { "password",textPassword.Password},
                { "subjenis_test", "SHUTTLE RUN" },
            };


                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync(url + route, content);
                var responseString = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(responseString);

                JObject stuff = JObject.Parse(responseString);

                string status = stuff["status"].ToString();
                string message = stuff["message"].ToString();

                if (status == "Success")
                {
                    JObject data = JObject.Parse(stuff["data"].ToString());

                    id = data["id"].ToString();
                    nama_user = data["nama"].ToString();
                    status_user = data["role"].ToString();                    
                    bool status_password = ((bool)data["status_password"]);
                    //Console.WriteLine(status_password);

                    if (status_user == "PANITIA" && status_password == true || status_user == "ADMIN" && status_password == true || status_user == "TESTOR" && status_password == true)
                    {
                        MessageBox.Show(message);
                        _mainLogin.callapp(status_user, nama_user);
                    }                   
                    else if (status_password == false)
                    {
                        MessageBox.Show(message + " Kamu pertama kali login silahkan ganti password");
                        CallCardNewPassword();
                        
                    }
                }
                else
                {
                    MessageBox.Show(message);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
