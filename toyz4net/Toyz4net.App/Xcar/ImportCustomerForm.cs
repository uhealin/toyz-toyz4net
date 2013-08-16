using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Toyz4net.Core.Util;
using NHibernate;
using NHibernate.Criterion;
using Microsoft.Office.Interop.Excel;
using System.Threading;

namespace Toyz4net.App.Xcar
{
    public partial class ImportCustomerForm : Form
    {
        public ImportCustomerForm()
        {
            InitializeComponent();

           
        }

        Thread _thread;
        private void btnSelectFilePath_Click(object sender, EventArgs e)
        {
            DialogResult dr= this.ofdImportFile.ShowDialog();
            this.txtFilePath.Text = this.ofdImportFile.FileName;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (_thread == null)
            {
                _thread = new Thread(new ThreadStart(importCustomer));
                _thread.Start();
            }
            else {
                _thread.Abort();
                _thread = null;
            }
        }

 

        public void importCustomer() {
            ExcelHelper eh = new ExcelHelper(this.txtFilePath.Text);
            String name = eh.GetSheetNames().ElementAt(0);
            DataSet ds = eh.ExcelToDataSet(name);
            System.Data.DataTable dt = ds.Tables[0];
            Dictionary<string, IList<ShopModel>> dictCityShops = new Dictionary<string, IList<ShopModel>>();

            ICriteria icr = NHibernateHelper.CreateCriteria<DictModel>();
            icr.Add(Restrictions.Eq("type", DictModel.Type.drive_cs.ToString()));
            IList<DictModel> drivercss = icr.List<DictModel>();

            icr = NHibernateHelper.CreateCriteria<DictModel>();
            icr.Add(Restrictions.Eq("type", DictModel.Type.buy_time.ToString()));
            IList<DictModel> buytimes = icr.List<DictModel>();
            Random random = new Random();
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                tsslState.Text = string.Format("数据导入({0}/{1})", ++i, dt.Rows.Count);
                CustomerModel customer = new CustomerModel();
                customer.name = dr[0].ToString();
                customer.mobile = dr[1].ToString();
                customer.city = dr[2].ToString();

                String region = dr[3].ToString();



                if (!string.IsNullOrEmpty(customer.city))
                {
                    IList<ShopModel> shops = null;
                    if (dictCityShops.ContainsKey(customer.city))
                    {
                        shops = dictCityShops[customer.city];
                    }
                    else
                    {
                        icr = NHibernateHelper.CreateCriteria<ShopModel>();
                        icr.Add(Restrictions.Like("city", "%" + customer.city + "%"));
                        shops = icr.List<ShopModel>();
                        dictCityShops.Add(customer.city, shops);
                    }
                    if (shops.Count > 0)
                    {
                        customer.province = shops[0].province;
                      
                        if (!string.IsNullOrEmpty(region))
                        {
                            icr = NHibernateHelper.CreateCriteria<ShopModel>();
                            icr.Add(Restrictions.Like("city", "%" + customer.city + "%"));
                            icr.Add(Restrictions.Or(Restrictions.Like("smallRegion", "%" + region + "%"), Restrictions.Like("address", "%" + region + "%")));
                            IList<ShopModel> shops1 = icr.List<ShopModel>();
                            if (shops1.Count > 0)
                            {
                                customer.shop = shops1[random.Next(0, shops1.Count - 1)].name;
                            }
                            else
                            {
                                customer.shop = shops[random.Next(0, shops.Count - 1)].name;
                            }
                        }
                        else
                        {

                            customer.shop = shops[random.Next(0, shops.Count - 1)].name;
                        }
                        
                    }

                    customer.buytime = buytimes[random.Next(0, buytimes.Count - 1)].value;
                    customer.drivecs = drivercss[random.Next(0, drivercss.Count - 1)].value;
                    customer.uploadInd = "N";
                }

                customer.createPk();
                JsResultObject re = Toyz4net.Core.Util.NHibernateHelper.SaveOrUpdate(customer, "");
                String msg = re.msg;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExcelHelper eh = new ExcelHelper(this.txtFilePath.Text);
            String name = eh.GetSheetNames().ElementAt(0);
            DataSet ds = eh.ExcelToDataSet(name);
            System.Data.DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                ShopModel shop = new  ShopModel();
                shop.bigRegion = dr[2].ToString();
                shop.smallRegion = dr[2].ToString();
                shop.province=  dr[3].ToString();
                shop.city = dr[4].ToString();
                shop.name = dr[5].ToString();
                shop.id = dr[6].ToString();
                shop.address = dr[7].ToString();
                shop.brand = dr[8].ToString();
                JsResultObject re = Toyz4net.Core.Util.NHibernateHelper.SaveOrUpdate(shop, "");
                String msg = re.msg;
            }
        }
    }
}
