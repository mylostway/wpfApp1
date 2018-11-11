using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL_OA.Data;

namespace WpfApp1.Data
{
    public class ComboboxViewMode<T>: ViewModel
    {
        public ComboboxViewMode()
        {
            var enumList = EnumHelper.GetEnumInfoListOnName<T>();
            bindingDataCollection.AddRange(enumList);
            selectedData = null;
        }

        private ObservableCollection<EnumInfo> bindingDataCollection = new ObservableCollection<EnumInfo>();
        public ObservableCollection<EnumInfo> BindingDataCollection
        {
            get { return bindingDataCollection; }
            set
            {
                bindingDataCollection = value;
                OnPropertyChanged();
            }
        }

        private EnumInfo selectedData = default(EnumInfo);
        public EnumInfo SelectedData
        {
            get { return selectedData; }
            set
            {
                selectedData = value;
                OnPropertyChanged();
            }
        }
    }
}
