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

namespace Chat
{
    /// <summary>
    /// Логика взаимодействия для UserControlMessageSend.xaml
    /// </summary>
    public partial class UserControlMessageSend : UserControlMessageBase
    {
        public UserControlMessageSend()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get => TextBox.Text;
            set => TextBox.Text = value;
        }

        public override string Time
        {
            get => TimeBox.Text;
            set => TimeBox.Text = value;
        }

        public override string UserName
        {
            get => NameBox.Text;
            set => NameBox.Text = value;
        }
    }
}
