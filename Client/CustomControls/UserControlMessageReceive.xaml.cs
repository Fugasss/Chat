namespace Chat.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для UserControlMessageReceive.xaml
    /// </summary>
    public partial class UserControlMessageReceive : UserControlMessageBase
    {
        public UserControlMessageReceive()
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
