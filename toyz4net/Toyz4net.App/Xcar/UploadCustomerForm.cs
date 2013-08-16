using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NHibernate;
using NHibernate.Criterion;
using Toyz4net.Core.Util;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace Toyz4net.App.Xcar
{
    public partial class UploadCustomerForm : Form
    {

        private CustomerModel _customer;

        public UploadCustomerForm()
        {
            InitializeComponent();

            string sql = "select DISTINCT(province) from xcar_shop ";
            ISQLQuery isql=NHibernateHelper.CreateSQLQuery(sql);
            var ds = isql.List<string>();
   
            foreach(var item in ds){
                this.cobProvince.Items.Add(item);
            }

            sql = "select text from xcar_buytime ";
             isql = NHibernateHelper.CreateSQLQuery(sql);
             ds = isql.List<string>();

            foreach (var item in ds)
            {
                this.cobBuytime.Items.Add(item);
            }

            sql = "select text from xcar_drivercs ";
            isql = NHibernateHelper.CreateSQLQuery(sql);
            ds = isql.List<string>();

            foreach (var item in ds)
            {
                this.cobDrivercs.Items.Add(item);
            }
            this.cobDrivercs.SelectedIndex = 0;
            this.cobBuytime.SelectedIndex = 0;

            reflashDgvCustomer();
            updateGroupBox();
            timer1.Stop();
        }

        private void cobProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            string province = this.cobProvince.Text;
            string sql = "select DISTINCT(city) from xcar_shop where province=?";
            ISQLQuery isql = NHibernateHelper.CreateSQLQuery(sql);
            isql.SetString(0, province);
            var ds = isql.List<string>();
            this.cobCity.Items.Clear();
            foreach (var item in ds)
            {
                this.cobCity.Items.Add(item);
            }
            this.cobCity.SelectedIndex = 0;
        }

        private void cobCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            string city = this.cobCity.Text;
            string sql = "select name from xcar_shop where city=?";
            ISQLQuery isql = NHibernateHelper.CreateSQLQuery(sql);
            isql.SetString(0, city);
            var ds = isql.List<string>();
            this.cobShop.Items.Clear();
            foreach (var item in ds)
            {
                this.cobShop.Items.Add(item);
            }
            this.cobShop.SelectedIndex = 0;
        }

        private void cobShop_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cobBuytime_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cobDrivercs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtMobile.Text = string.Empty;
            txtName.Text = string.Empty;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            CustomerModel customer = _customer;

            if (_customer.uploadInd == "Y") {
              DialogResult dr=  MessageBox.Show("确认重新提交吗?", "", MessageBoxButtons.YesNo);
              if (dr != DialogResult.Yes) {
                  return;
              }
            }
            customer.city = cobCity.Text;
            customer.province = cobProvince.Text;
            customer.shop = cobShop.Text;
            customer.drivecs = cobDrivercs.Text;
            customer.buytime = cobBuytime.Text;
            JsResultObject re = customer.doSubmit();
           
                tsslStatue.Text = re.msg;
                
           
            re = NHibernateHelper.Update(customer, "客户");
            reflashDgvCustomer();
            updateGroupBox();
        }

        private void UploadCustomerForm_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“dataDataSet.xcar_customer”中。您可以根据需要移动或移除它。
           // this.xcar_customerTableAdapter.Fill(this.dataDataSet.xcar_customer);

        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = dgvCustomer.Rows[e.RowIndex];

                _customer = new CustomerModel();
                string id = row.Cells[0].Value.ToString();
                _customer = NHibernateHelper.Load<CustomerModel>(id);
                if (_customer != null)
                {
                    updateCustomerUI();
                }
            }
            catch (Exception ex) { return; }
        }

        private void updateCustomerUI() {
            txtMobile.Text = _customer.mobile;
            txtName.Text = _customer.name;
            cobBuytime.Text = _customer.buytime;
            cobDrivercs.Text = _customer.drivecs;
            cobProvince.Text = _customer.province;
            cobCity.Text = _customer.city;
            cobShop.Text = _customer.shop;
        }

        private void reflashDgvCustomer() {
            SQLiteConnection conn = new SQLiteConnection(Gobal.LOCAL_DB_CS);
            conn.Open();
            DataSet ds = new DataSet();

            SQLiteDataAdapter sdr;  
  
            string strSql = "select * from xcar_customer order by upload_ind desc,upload_time desc"; 
            sdr = new SQLiteDataAdapter(strSql, conn);

            ds.Clear();

            sdr.Fill(ds, "a");

            conn.Close();

            //this.dataGridView1.DataSource = null;  
            this.dgvCustomer.DataSource = ds.Tables[0];
           
        }

        private void updateGroupBox() { 
          string sql="select count(*) from xcar_customer where upload_time like ? and upload_ind='Y'";
          ISQLQuery isql = NHibernateHelper.CreateSQLQuery(sql);
          isql.SetString(0, DateTime.Now.ToString("yyyy-MM-dd") + "%");
          //int count = (int)isql.UniqueResult();
          groupBox1.Text = string.Format("今天已成功上传 {0} 个客户", isql.UniqueResult().ToString());
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            DataGridViewRow row=null;
            int c = 0;
            do
            {
               c= r.Next(c, dgvCustomer.Rows.Count - 1);
                row = dgvCustomer.Rows[c];
                
            } while (row.Cells["colUploadInd"].Value.ToString() == "Y");
            string id = row.Cells["colId"].Value.ToString();
            _customer = NHibernateHelper.Load<CustomerModel>(id);
            updateCustomerUI();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            btnNext_Click(null, null);
            btnSubmit_Click(null, null);
            Random r=new Random();
            int c=r.Next(120,600);
            tsslStatue.Text = string.Format("最后上传时间:{0},{1}秒后再次上传",DateTime.Now.ToLongTimeString(),c);
            timer1.Interval = c * 1000;
            
        }

        private void btnTimer_Click(object sender, EventArgs e)
        {
            if (btnTimer.Text == "开始")
            {
                btnTimer.Text = "停止";
                timer1.Start();
            }
            else {
                btnTimer.Text = "开始";
                timer1.Stop();
            }
        }
    }
}
