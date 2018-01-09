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
    public partial class InternetOption : Form
    {
        String adresa;
        public Font font;
        public Color forecolor, backcolor;
        public InternetOption(String adresa)
        {
            this.adresa = adresa;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            homepage.Text = "about:blank";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            homepage.Text = adresa;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FontDialog dlg = new FontDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
                font = dlg.Font;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
                forecolor = c.Color;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
                backcolor = c.Color;
        }

        private void InternetOption_Load(object sender, EventArgs e)
        {

        }


    }
}
