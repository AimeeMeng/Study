using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExceptionForm
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //error();
            ThreadError();
        }

        public void error()
        {
            throw new Exception("test");
        }

        public void ThreadError()
        {
            Thread thread = new Thread(new ThreadStart(ThreadStartMethord));
            thread.Start();
            Thread thread1 = new Thread(new ThreadStart(ThreadStartMethord));
            thread1.Start();
        }

        public void ThreadStartMethord()
        {
            try
            {
                throw new Exception("ThreadError");
            }
            catch (Exception ex) {
                this.BeginInvoke((Action)delegate
                {
                    throw ex;
                });
            }  
        }


    }
}
