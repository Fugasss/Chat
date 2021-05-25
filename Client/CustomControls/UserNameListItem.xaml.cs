using System.Windows.Controls;

namespace Chat.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для UserNameListItem.xaml
    /// </summary>
    public partial class UserNameListItem : UserControl
    {
        public UserNameListItem()
        {
            InitializeComponent();
        }

        public new string Content
        {
            get => UserName.Content.ToString();
            set => UserName.Content = value;
        }
    }
}
