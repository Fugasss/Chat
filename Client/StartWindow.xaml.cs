using Chat.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chat
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void IpValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex(@"[^0-9.]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TryConnectOnClick(object sender, RoutedEventArgs e)
        {   
            var ip = IpBox.Text;
            var correctPort = int.TryParse(PortBox.Text, out var port);

            var available = Client.ServerAvailable(ip, port) && correctPort;

            if (available)
            {
                var mainWindow = new MainWindow(ip, port, UsernameBox.Text);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                ErrorLabel.Visibility = Visibility.Visible;
            }
        }
    }
}