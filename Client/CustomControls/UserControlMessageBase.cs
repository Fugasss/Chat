using System.Windows.Controls;

namespace Chat.CustomControls
{
    public  class UserControlMessageBase : UserControl
    {
        public virtual string Text { get; set; }
        public virtual string Time { get; set; }
        public virtual string UserName { get; set; }
    }
}
