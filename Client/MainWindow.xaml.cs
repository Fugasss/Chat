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

        private readonly Client m_Client;
        private readonly string m_UserName;

        public MainWindow(string ip, int port,string username)
        {
            InitializeComponent();

            m_UserName = username;
            m_Client = new Client(ip, port);

            Client.OnMessageReceive += ShowMessage;
        }


        private void SendMessageButtonClick(object sender, RoutedEventArgs e)
        {
            var message = SendTextBox.Text;

            var userMessage = new UserMessage();

            userMessage.Type = PacketType.UserMessage;
            userMessage.Name = m_UserName;
            userMessage.Message = message;

            ShowMessage(userMessage, true);

            m_Client.SendMessage(userMessage);
        }

        private void ShowMessage(IMessage message)
        {
            ShowMessage(message, false);
        }

        private void ShowMessage(IMessage message, bool self)
        {
            var userName = (message is UserMessage user) ? user.Name : "System";

            ShowMessage(message.Message, userName, DateTime.Now, self);
        }

        private void ShowMessage(string message, string userName, DateTime time, bool self)
        {
            Dispatcher.BeginInvoke(new ThreadStart(() =>
            {
                UserControlMessageBase newMessage;

                if (self)
                    newMessage = new UserControlMessageSend();
                else
                    newMessage = new UserControlMessageReceive();

                newMessage.UserName = userName;
                newMessage.Time = time.ToString("HH:mm MMM");
                newMessage.Text = message;
                newMessage.Padding = new Thickness(0, 5, 0, 5);

                Messages.Children.Add(newMessage);
            }));
        }
    }
}
