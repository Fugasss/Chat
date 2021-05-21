using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using Chat.Scripts;
using ServerClientLibrary.Packets;

namespace Chat
{
    public partial class MainWindow : Window
    {
        private const int Port = 80;
        private const string Ip = "127.0.0.1";

        private readonly Client m_Client;

        public MainWindow()
        {
            InitializeComponent();

            m_Client = new Client(Ip, Port);
            Client.OnMessageReceive += ShowMessage;
        }

        private void SendMessageButtonClick(object sender, RoutedEventArgs e)
        {
            const string userName = "You";

            var message = SendTextBox.Text;

            var userMessage = new UserMessage();
            userMessage.Type = PacketType.UserMessage;
            userMessage.Name = userName;
            userMessage.Message = message;

            ShowMessage(userMessage);

            m_Client.SendMessage(userMessage);
        }

        private void ShowMessage(IMessage message)
        {
            var userName = (message is UserMessage user) ? user.Name : "System";

            ShowMessage(message.Message, userName, DateTime.Now, false);
        }

        private void ShowMessage(string message, string userName, DateTime time, bool self)
        {
            UserControlMessageBase newMessage = self ? new UserControlMessageSend() : new UserControlMessageReceive();

            newMessage.UserName = userName;
            newMessage.Time = time.ToString("HH:mm MMM");
            newMessage.Text = message;
            newMessage.Padding = new Thickness(0, 5, 0, 5);

            Dispatcher.BeginInvoke(new ThreadStart(() => Messages.Children.Add(newMessage)));
        }
    }
}
