using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Panels.extend_control
{
    public class DMLinkButton : DMSystemButton
    {
        public bool DMDisplayLine
        {
            get { return (bool)GetValue(DMDisplayLineProperty); }
            set { SetValue(DMDisplayLineProperty, value); }
        }
        public static readonly DependencyProperty DMDisplayLineProperty =
            DependencyProperty.Register("DMDisplayLine", typeof(bool), typeof(DMLinkButton), new PropertyMetadata(true));
    }
}
