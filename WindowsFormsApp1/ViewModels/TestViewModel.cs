using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.ViewModels.Base;

namespace WindowsFormsApp1.ViewModels
{
    internal class TestViewModel : BaseViewModel, IDisposable
    {

        public BindingSource ModelBS = new BindingSource();

        TestModel _model;
        public TestModel Model
        {
            get => _model;
            private set
            {
                ModelBS.DataSource = _model = value;
            }
        }

        int _count = 0;
        public int Count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }

        public TestViewModel(Control _view) : base(_view)
        {
            Model = new TestModel();
            Model.Name = "Test";
        }

        public void ModelFire()
        {
#if false
            Model.FireCnt++;
            Model.Name = "Fire " + Model.FireCnt.ToString();
#else

            Task.Run(() =>
            {
                //view.BeginInvoke(new Action(() =>
                //{
                Model.FireCnt++;
                Model.Name = "Fire " + Model.FireCnt.ToString();
                //}));

            });
#endif
        }

        public void ChangeModel()
        {
            Model = new TestModel();
            Model.Name = "New Model";
        }

        public void Dispose()
        {
            ModelBS?.Dispose();
            ModelBS = null;
        }
    }
}
