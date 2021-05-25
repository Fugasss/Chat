using System;
using System.Threading;
using System.Windows;
using Chat.CustomControls;
using Chat.Scripts;
using ServerClientLibrary.Packets;

namespace Chat
{
    public partial class MainWindow : Window
    {

        private readonly Client m_Client;
        private readonly string m_UserName;

        public MainWindow(string ip, int port, string username)
        {
            InitializeComponent();

            m_UserName = username;
            m_Client = new Client(ip, port, username);

            Client.OnMessageReceive += HandleMessage;
        }


        private void SendMessageButtonClick(object sender, RoutedEventArgs e)
        {
            var message = SendTextBox.Text;

            var userMessage = UserMessage.CreateInstance();

            userMessage.Type = PacketType.UserMessage;
            userMessage.Name = m_UserName;
            userMessage.Message = message;

            ShowMessage(userMessage, true);

            m_Client.SendMessage(userMessage);
        }

        private void HandleMessage(IMessage message)
        {
            switch (message)
            {
                case UserMessage or ServerMessage:
                    ShowMessage(message, false);
                    break;

                case WelcomeMessage:
                    AddUserToList(message);
                    break;
            }
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

        private void AddUserToList(IMessage message)
        {
            Dispatcher.BeginInvoke(new ThreadStart(() =>
            {
                var newUser = new UserNameListItem();

                newUser.Content = ((WelcomeMessage)message).UserName;

                UsersList.Items.Add(newUser);
            }));
        }
    }
}
