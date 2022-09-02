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
    public partial class OverWriteCustDB : Form
    {
        iSpanProjectEntities dbindex = new iSpanProjectEntities();
        public string account2
        {
            get;
            set;
        }
        byte[] bytes;
        public OverWriteCustDB()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        internal void showdata(string account2)
        {
            MemberAccount list = new MemberAccount();
            var q = (from i in dbindex.MemberAccount
                     where i.MemberAcc == account2
                     select i).FirstOrDefault();

            txtAccount.Text = q.MemberAcc;
            txtPassworld.Text = q.MemberPw;
            //list.RegionID = cmbo_city.SelectedIndex + 1;
            //if (q.RegionID == 5)
            //{
            //    string a = "新北市";
            //    cmbo_city.Text = a;
            //}

            //cmb_are.SelectedIndex = q.RegionList.RegionID;
            txt_phon.Text = q.Phone;
            txt_mail.Text = q.Email;
            txt_backMail.Text = q.BackUpEmail;
            txtadd.Text = q.Address;
            txt_nickName.Text = q.NickName;
            txt_name.Text = q.Name;
            DTP_BIR.Value = q.Birthday;
            System.IO.MemoryStream ms = new System.IO.MemoryStream(q.MemPic);
            this.pic_box.Image = Image.FromStream(ms);
            txt_bio.Text = q.Bio;

            Application.DoEvents();
        }

        private void OverWriteCustDB_Load(object sender, EventArgs e)
        {
            showdata(account2);
            cbload();
        }
        private void cbload()
        {
            var q = from i in dbindex.CountryList
                    orderby i.CountryID ascending
                    select i.CountryName;
            var q1 = from i in dbindex.RegionList
                     where i.CountryID == 1
                     orderby i.RegionID ascending
                     select i.RegionName;

            cmbo_city.Text = q.FirstOrDefault();

            cmbo_city.Items.AddRange(q.ToArray());
            cmb_are.Items.AddRange(q1.ToArray());
            cmb_are.Text = cmb_are.Items[0].ToString();
        }

        private void cmbo_city_SelectedIndexChanged(object sender, EventArgs e)
        {
            var q = from i in dbindex.CountryList
                    where i.CountryName == cmbo_city.Text
                    orderby i.CountryID
                    select i.CountryID;
            var q1 = from i in dbindex.RegionList
                     where i.CountryID == q.FirstOrDefault()
                     orderby i.RegionID ascending
                     select i.RegionName;

            cmb_are.Items.Clear();
            cmb_are.Items.AddRange(q1.ToArray());
            cmb_are.Text = cmb_are.Items[0].ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MemberAccount list = new MemberAccount() { };

            list.MemberAcc = txtAccount.Text;
            list.MemberPw = txtPassworld.Text;
            list.TWorNOT = ckbox_yes.Checked;
            list.RegionID = cmbo_city.SelectedIndex + 1;
            list.Phone = txt_phon.Text;
            list.Email = txt_mail.Text;
            list.BackUpEmail = txt_backMail.Text;
            list.Address = txtadd.Text;
            list.NickName = txt_nickName.Text;
            list.Name = txt_name.Text;
            list.Birthday = DTP_BIR.Value;
            list.Bio = txt_bio.Text;
            list.MemPic = bytes;

            this.dbindex.MemberAccount.Add(list);
            this.dbindex.SaveChanges();
            MessageBox.Show("修改成功");
        }

        private void btn_pic_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pic_box.Image = Image.FromFile(this.openFileDialog1.FileName);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                this.pic_box.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                bytes = ms.GetBuffer();//將圖片轉成byt儲存起來
            }
        }
    }
}
