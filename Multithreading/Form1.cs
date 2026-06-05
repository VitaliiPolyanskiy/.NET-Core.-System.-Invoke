using System;
using System.Threading;
using System.Windows.Forms;

namespace Multithreading
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private delegate void InWork(int a);

        private void ThreadFunk()
        {
            try
            {
                // Створення анонімних делегатів
                Action Act1 = () =>
                {
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = 230;
                    progressBar1.Value = 0;
                    button1.Enabled = false;
                };

                Action Act2 = ()=>
                {
                    button1.Enabled = true;
                };

                InWork IW = a =>
                {
                    progressBar1.Value = a;
                };

                // Виконує вказаний делегат у тому потоці, якому належить базовий дескриптор вікна елемента управління.
                Invoke(Act1);

                for (int i = 0; i < 230; i++)
                {
                    Thread.Sleep(50);
                    // Виконує вказаний делегат у тому потоці, якому належить базовий дескриптор вікна елемента управління.
                    Invoke(IW, i);
                    //progressBar1.Value = i;
                }
                Invoke(Act2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Створення делегату функції, в якій працюватиме новий потік
            ThreadStart MethodThread = new ThreadStart(ThreadFunk);
            // Створення об'єкта потоку
            Thread thread = new Thread(MethodThread);
            thread.IsBackground = true;
            // Старт потоку
            thread.Start();
        }
    }
}
