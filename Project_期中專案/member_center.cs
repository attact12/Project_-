using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_期中專案
{
    public partial class member_center : Form
    {

        iSpanProjectEntities dbContext = new iSpanProjectEntities();

        //public string txt{set{ label2.Text = value; } }
        public string account2
        {
            get { return lab_showname.Text; }
            set { lab_showname.Text = value; }
        }
        public member_center()
        {
            InitializeComponent();

        }
        //public member_center(string acc)
        //{
        //    InitializeComponent();
        //      account2=acc;
        //}
        private void button1_Click(object sender, EventArgs e)
        {
            (new Form1()).ShowDialog();
            

        }
    }
}
