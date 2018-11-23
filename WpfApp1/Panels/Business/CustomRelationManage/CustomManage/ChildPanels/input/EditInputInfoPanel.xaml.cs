using System;
using System.Collections.Generic;
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
using WL_OA.Data.entity;

namespace WpfApp1.Panels.Business.CustomRelationManage
{
    /// <summary>
    /// EditInputInfo.xaml 的交互逻辑
    /// </summary>
    public partial class EditInputInfoPanel : UserControl
    {
        public EditInputInfoPanel()
        {
            InitializeComponent();

            EditInputInfoEntity.FinputTime = DateTime.Now;

            this.DataContext = EditInputInfoEntity;
        }

        public CustomerInputInfoEntity EditInputInfoEntity { get; set; } = new CustomerInputInfoEntity();

        public CustomerInputInfoEntity GetEditInfo()
        {
            return EditInputInfoEntity;
        }

        public void Init(CustomerInputInfoEntity editInfo)
        {
            if (null == editInfo) return;
            this.EditInputInfoEntity = editInfo;
            this.DataContext = EditInputInfoEntity;
        }
    }
}
