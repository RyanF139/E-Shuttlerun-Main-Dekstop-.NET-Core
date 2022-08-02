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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace E_Shuttlerun.Registrasi_Testor
{
    /// <summary>
    /// Interaction logic for DialogMessageDaftarTesor.xaml
    /// </summary>
    public partial class DialogMessageDaftarTestor : Window
    {
        public DialogMessageDaftarTestor(String message)
        {
            InitializeComponent();
            TextMessage.Text = message;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Timer();
        }

        private void Timer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += timerClose_Tick;
            timer.Start();
        }
        private void btn_CloseMessageTestor_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void timerClose_Tick(object sender, EventArgs e)
        {
            Close();
        }       


    }
}
