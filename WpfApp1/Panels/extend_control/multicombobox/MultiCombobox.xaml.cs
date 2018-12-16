using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WpfApp1.Data;

namespace WpfApp1.Panels.extend_control
{
    public class MultipleCheckboxModel : INotifyPropertyChanged
    {
        public MultipleCheckboxModel() { }

        public MultipleCheckboxModel(int id = 0,string title = "",bool isSelected = false)
        {
            Id = id;
            Title = title;
            IsSelected = isSelected;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        private bool _isSelected;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                //NotifyPropertyChanged(x => x.IsSelected);
                //PropertyChanged?.Invoke(this, null);
            }
        }
    }

    /// <summary>
    /// MultiCombobox.xaml 的交互逻辑
    /// </summary>
    public partial class MultiCombobox : UserControl
    {
        public MultiCombobox()
        {
            InitializeComponent();
        }

        #region Dependency Properties

        public static readonly DependencyProperty MWidthProperty =
            DependencyProperty.Register("MWidth", typeof(object), typeof(MultiCombobox), new FrameworkPropertyMetadata(null, MWidthPropertyChangedCallback));

        public double MWidth
        {
            get { return (double)GetValue(MWidthProperty); }
            set
            {
                SetValue(MWidthProperty, value);
            }
        }

        private static void MWidthPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var multioleCheckbox = dependencyObject as MultiCombobox;
            if (multioleCheckbox == null) return;
            multioleCheckbox.CheckableCombo.Width = multioleCheckbox.MWidth;
        }


        public IEnumerable<MultipleCheckboxModel> ItemsSource
        {
            get { return (IEnumerable<MultipleCheckboxModel>)GetValue(ItemsSourceProperty); }
            set
            {
                SetValue(ItemsSourceProperty, value);
                SetText();
            }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(object), typeof(MultiCombobox), new FrameworkPropertyMetadata(null, ItemsSourcePropertyChangedCallback));

        private static void ItemsSourcePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var multioleCheckbox = dependencyObject as MultiCombobox;
            if (multioleCheckbox == null) return;
            multioleCheckbox.CheckableCombo.ItemsSource = multioleCheckbox.ItemsSource;
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MultiCombobox), new FrameworkPropertyMetadata("", TextPropertyChangedCallback));

        private static void TextPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var multioleCheckbox = dependencyObject as MultiCombobox;
            if (multioleCheckbox == null) return;
        }

        public string DefaultText
        {
            get { return (string)GetValue(DefaultTextProperty); }
            set { SetValue(DefaultTextProperty, value); }
        }

        public static readonly DependencyProperty DefaultTextProperty =
            DependencyProperty.Register("DefaultText", typeof(string), typeof(MultiCombobox), new UIPropertyMetadata(string.Empty));

        #endregion


        #region Event

        private void Checkbox_OnClick(object sender, RoutedEventArgs e)
        {
            var checkbox = sender as CheckBox;
            if (checkbox == null) return;
            if ((string)checkbox.Content == "All")
            {
                Text = "";
                if (checkbox.IsChecked != null && checkbox.IsChecked.Value)
                {
                    foreach(var eItem in ItemsSource)
                    {
                        eItem.IsSelected = true;
                        Text = "All";
                    }
                    //ItemsSource.ForEach(x =>
                    //{
                    //    x.IsSelected = true;
                    //    Text = "All";
                    //});
                }
                else
                {
                    foreach (var eItem in ItemsSource)
                    {
                        eItem.IsSelected = false;
                        Text = "None";
                    }
                    //ItemsSource.ForEach(x =>
                    //{
                    //    x.IsSelected = false;
                    //    Text = "None";
                    //});
                }
            }
            else
            {
                SetText();
            }

        }

        #endregion



        #region Private Method

        private void SetText()
        {
            Text = "";
            var all = ItemsSource.FirstOrDefault(x => x.Title == "All");
            foreach (var item in ItemsSource)
            {


                if (item.IsSelected && item.Title != "All")
                {
                    Text += item.Title + ",";
                }
                else if (all != null)
                {
                    if (all.IsSelected)
                        all.IsSelected = false;
                }
            }

            Text = string.IsNullOrEmpty(Text) ? DefaultText : Text.TrimEnd(new[] { ',' });
        }

        #endregion
    }
}
