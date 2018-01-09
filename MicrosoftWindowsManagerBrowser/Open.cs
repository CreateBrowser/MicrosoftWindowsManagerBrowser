// Mohammed Osama Mohammed
//Windows now Browser 2018
//Email : mohmamed.osama914@gmail.com
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MicrosoftWindowsManagerBrowser
{
    public partial class Open : Form
    {
        WebBrowser wb;

        public Open(WebBrowser wb)
        {
            this.wb = wb;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            wb.Navigate(textBox1.Text);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "Text Files(*.txt)|*.txt|Html file(*.html)|*.html|AllFiles|*.*";
            if (o.ShowDialog() == DialogResult.OK)
                textBox1.Text = o.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                wb.Navigate(textBox1.Text);
                this.Close();
            }
        }

        private void Open_Load(object sender, EventArgs e)
        {

        }
    }
}
