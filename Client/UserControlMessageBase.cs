using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Chat
{
    public  class UserControlMessageBase : UserControl
    {
        public virtual string Text { get; set; }
        public virtual string Time { get; set; }
        public virtual string UserName { get; set; }
    }
}
