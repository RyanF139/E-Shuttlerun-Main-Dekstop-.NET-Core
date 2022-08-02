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
    /// Interaction logic for Card_NewPassword.xaml
    /// </summary>
    public partial class Card_NewPassword : UserControl
    {
        private static readonly HttpClient client = new HttpClient();

        string urlStandalone = System.Configuration.ConfigurationManager.AppSettings["SERVER_API_LOCAL"];
        string urlIntegrated = System.Configuration.ConfigurationManager.AppSettings["SERVER_API"];
        string url;
        string route = "/api/v2/change-password";

        string Mode;

        Card_Login _card_login;
        public Card_NewPassword(Card_Login card_login)
        {
            InitializeComponent();
            _card_login = card_login;
            Mode = _card_login._mainLogin.mode; //From Main Login
        }

        private void btn_UpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            if (textCurrentPassword.Password == "" || NewPassword.Password == "" || ConfirmNewPassword.Password == "")
            {
                MessageBox.Show(" Harap Masukan Password Lama/Password Baru");
            }
            else 
            {
                ValidateUrl();
                ChangePasswordLogin();
            }            
        }

        private void ValidateUrl()
        {
            Mode = _card_login._mainLogin.mode;
            if (Mode == "Integrated")
            {
                url = urlIntegrated;
            }
            else if (Mode == "Standalone")
            {
                url = urlStandalone;
            }
        }

        public async void ChangePasswordLogin()
        {
            try
            {
                string id = _card_login.id.ToString();
                var values = new Dictionary<string, string>
                {
                    { "user_id", id },
                    { "old_password", textCurrentPassword.Password },
                    { "new_password", ConfirmNewPassword.Password },
                };


                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync(url + route, content);
                var responseString = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(responseString);

                JObject stuff = JObject.Parse(responseString);
                Console.WriteLine(stuff);
                string status = stuff["status"].ToString();
                Console.WriteLine(status);
                string message = stuff["message"].ToString();                
                string status_users = _card_login.status_user.ToString();
                string nama_testor = _card_login.nama_user.ToString();
                Console.WriteLine(message,status_users);

                if (status == "Success")
                {
                    //_card_login._mainLogin.callapp(status_users, nama_testor);
                    _card_login._mainLogin.callapp(status_users, nama_testor);
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
