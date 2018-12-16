using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp1.Panels.extend_control
{


    /// <summary>
    /// BitFieldSelector.xaml 的交互逻辑
    /// </summary>
    public partial class BitFieldSelector : UserControl
    {
        public BitFieldSelector()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 当前控件可以容纳最大的bit位数
        /// </summary>
        public const int MAX_BIT_COUNT = 31;

        #region Dependency Properties

        /// <summary>
        /// 数据标题
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(BitFieldSelector), new FrameworkPropertyMetadata(null, TitlePropertyChangedCallback));

        private static void TitlePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var bitFieldSelector = dependencyObject as BitFieldSelector;
            if (bitFieldSelector == null) return;
            bitFieldSelector.tbx_title.Text = bitFieldSelector.Title;
        }


        /// <summary>
        /// 位选择数据源
        /// </summary>
        public IEnumerable<string> ItemSource
        {
            get { return (IEnumerable<string>)GetValue(ItemSourceProperty); }
            set
            {
                SetValue(ItemSourceProperty, value);
            }
        }

        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemSource", typeof(IEnumerable<string>), typeof(BitFieldSelector), new FrameworkPropertyMetadata(null, ItemSourcePropertyChangedCallback));

        private static void ItemSourcePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var bitFieldSelector = dependencyObject as BitFieldSelector;
            if (bitFieldSelector == null) return;
            if (bitFieldSelector.ItemSource.Count() > MAX_BIT_COUNT) throw new Exception($"too many selector,max is {MAX_BIT_COUNT}");
            bitFieldSelector.BitValue = 0;
            bitFieldSelector.sp_appendBit.Children.Clear();
            foreach (var eItem in bitFieldSelector.ItemSource)
            {
                var lb = new Label();
                lb.Content = eItem;
                lb.MouseDown += new MouseButtonEventHandler(bitFieldSelector.Label_MouseDown);
                bitFieldSelector.sp_appendBit.Children.Add(lb);
            }

            //bitFieldSelector.SetBitUIAsBitValue();
        }

        ///// <summary>
        ///// 当前的选择值
        ///// </summary>
        //public int BitValue
        //{
        //    get { return (int)GetValue(BitValueProperty); }
        //    set { SetValue(BitValueProperty, value); }
        //}

        // 目前使用依赖属性会出异常，原因未明，可能因为该属性和Itemsource联动，先屏蔽
        //public static readonly DependencyProperty BitValueProperty =
        //    DependencyProperty.Register("BitValue", typeof(object), typeof(BitFieldSelector), new FrameworkPropertyMetadata(null, BitValuePropertyChangedCallback));

        //private static void BitValuePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        //{
        //    //var bitFieldSelector = dependencyObject as BitFieldSelector;
        //    //if (bitFieldSelector == null) return;
        //}

        #endregion

        private int m_bitValue = 0;

        /// <summary>
        /// 获取/设置已经设定的位选择值
        /// </summary>
        public int BitValue
        {
            get { return m_bitValue; }
            set
            {
                m_bitValue = value;
                SetBitUIAsBitValue();
            }
        }

        private void SetBitUIAsBitValue()
        {            
            var bitVal = this.m_bitValue;
            if (0 == bitVal) return;
            if (bitVal > int.MaxValue) throw new Exception($"非法的 BitValue值 {bitVal}，最大不超 ${int.MaxValue}");
            var lbList = sp_appendBit.Children;
            if (null == lbList || 0 == lbList.Count) return;
            for (var i = 0; i < MAX_BIT_COUNT; i++)
            {
                var counter = 1 << i;
                if (counter > bitVal) return;
                if (i >= lbList.Count) return;
                var lbCur = lbList[i] as Label;
                if (null == lbCur) return;
                if ((bitVal & counter) > 0)
                {
                    SetBitUISelected(lbCur, true);
                }
                else
                {
                    SetBitUISelected(lbCur, false);
                }
            }
        }


        private void SetBitUISelected(Label lb,bool isSelected = false)
        {
            if(isSelected)
            {
                lb.Background = new SolidColorBrush(Colors.Blue);
            }
            else
            {
                lb.Background = new SolidColorBrush(Colors.Gray);
            }
        }



        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var lb = sender as Label;

            if (null == lb) return;

            var selText = lb.Content.ToString();

            var i = 0;

            foreach (var eStr in ItemSource)
            {
                if (eStr == selText) break;
                i++;
            }

            if (i == ItemSource.Count()) throw new Exception("BitFieldSelector 找不到选择的对应值！");

            if (((m_bitValue >> i) & 1) == 1)
            {
                // 已选中，再点击是取消
                //lb.Background = new SolidColorBrush(Colors.Gray);
                SetBitUISelected(lb, false);
                m_bitValue &= (~(0x01 << i));
            }
            else
            {
                // 未选中，点击选中
                //lb.Background = new SolidColorBrush(Colors.Blue);
                SetBitUISelected(lb, true);
                m_bitValue |= (0x01 << i);
            }
        }
    }
}
