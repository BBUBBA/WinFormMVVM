using System;
using System.Windows.Forms;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.ViewModels;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        TestViewModel vm;

        public Form1()
        {
            InitializeComponent();
            vm = new TestViewModel(this);
            Load += Form1_Load;
            FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            vm?.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Bind(vm.ModelBS, nameof(TestModel.Name));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            vm.ModelFire();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            vm.ChangeModel();
        }

    }
}
