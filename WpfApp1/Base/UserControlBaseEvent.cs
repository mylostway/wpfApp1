using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp1.Base
{
    public class UIControlEventArgs : EventArgs
    {

    }

    public delegate void UIControlBaseEvent(UserControl sender,UIControlEventArgs args);


}
