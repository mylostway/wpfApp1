using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.Base
{
    public class AttachedPropertyManager
    {
        /*
        public static readonly DependencyProperty AttachedData =
            DependencyProperty.RegisterAttached("attached", typeof(string), typeof(AttachedPropertyManager), new PropertyMetadata("", OnAttachedChange));
            */

        public static readonly DependencyProperty AttachedData =
            DependencyProperty.RegisterAttached("attached", typeof(string), typeof(AttachedPropertyManager), new PropertyMetadata(""));

        private static void OnAttachedChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var element = obj as UIElement;
            if (element != null)
            {
                //
            }
        }
    }
}
