using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Data
{
    public class ComboboxViewMode<T>: ViewModel
    {
        private ObservableCollection<T> bindingDataCollection = new ObservableCollection<T>();
        public ObservableCollection<T> BindingDataCollection
        {
            get { return bindingDataCollection; }
            set
            {
                bindingDataCollection = value;
                OnPropertyChanged();
            }
        }

        private T selectedData = default(T);
        public T SelectedData
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
