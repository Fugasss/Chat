namespace Chat.CustomControls
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
