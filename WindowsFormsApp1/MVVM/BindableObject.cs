using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace WindowsFormsApp1.MVVM
{
    public abstract class BindableObject : INotifyPropertyChanged
    {
        private static SynchronizationContext GuiContext = SynchronizationContext.Current;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                GuiContext.Post(s => handler(this, new PropertyChangedEventArgs(propertyName)), null);
        }

        protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
