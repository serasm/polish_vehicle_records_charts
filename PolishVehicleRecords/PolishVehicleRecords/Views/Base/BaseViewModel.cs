using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace PolishVehicleRecords.Views.Base
{
    public class BaseViewModel : INotifyPropertyChanged, INotifyCollectionChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public virtual void OnCollectionChanged(NotifyCollectionChangedAction action, [CallerMemberName]string propertyName = null)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, propertyName));
        }
    }
}
