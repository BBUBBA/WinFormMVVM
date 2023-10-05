using System.Windows.Forms;
using WindowsFormsApp1.MVVM;

namespace WindowsFormsApp1.ViewModels.Base
{
    internal class BaseViewModel : BindableObject
    {
        protected Control view;

        public BaseViewModel(Control _view)
        {
            view = _view;
        }
    }
}
