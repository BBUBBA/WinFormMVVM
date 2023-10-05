using WindowsFormsApp1.MVVM;

namespace WindowsFormsApp1.Models
{
    internal class TestModel : BindableObject
    {

        public int FireCnt;

        string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

    }
}
