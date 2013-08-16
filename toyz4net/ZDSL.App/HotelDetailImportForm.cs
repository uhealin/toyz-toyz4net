using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZDSL.Model.Data;
using ZDSL.Biz;
using NHibernate;
using NHibernate.Criterion;
using Toyz4net.Core.Util;
using System.Threading;

namespace ZDSL.App
{
    public partial class HotelDetailImportForm : Form
    {

        private  bool isStop = false;
        private IList<HotelModel> _hotels;

       

        public HotelDetailImportForm()
        {
            InitializeComponent();
            this.nudMin.Value = 0;
            ICriteria icr = BaseZdBiz.CreateCriteria<HotelModel>();
            int count = BaseZdBiz.Count(icr,"hotelId");
            this.nudMax.Maximum = count;
            this.nudMin.Maximum = count;
            this.nudMax.Value = count;

        }



        private void btnImport_Click(object sender, EventArgs e)
        {

            this.tsslResult.Text = "开始导入。。。";
            this.lvImportHotels.Items.Clear();
            string keyword=tbKeyword.Text.Trim();
            ICriteria icr = BaseZdBiz.CreateCriteria<HotelModel>();
            icr.SetFirstResult(Convert.ToInt32(this.nudMin.Value));
            icr.SetMaxResults(Convert.ToInt32(this.nudMax.Value));
            icr.Add(Restrictions.Like("hotelName","%"+keyword+"%"));
            _hotels = icr.List<HotelModel>();
            DataBiz dataBiz = DataBiz.GetInstant();
            isStop = false;
           
            Thread t= new Thread(new ThreadStart(startImportHotelDetail));
            t.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            isStop = true;
        }

        public ListView getResultListView() {
            return this.lvImportHotels;
        }




        private void startImportHotelDetail()
        {

            DataBiz dataBiz = DataBiz.GetInstant();
            JsResultObject re = new JsResultObject();
            
            int i = 1;
            foreach (HotelModel hotel in _hotels)
            {
                if (isStop) break;

                if (this.lvImportHotels.Items.Count > 200) {
                    this.lvImportHotels.Items.Clear();
                }

                this.Update();
                
                ListViewItem lvi = null;
                try
                {
                    re.rowNum += dataBiz.ImportHotelDetail(hotel.hotelId, this.cbSubInfo.Checked).rowNum;
                    this.tsslResult.Text = string.Format("({0}/{1}){2}:{3}导入中....",i++ , _hotels.Count(),hotel.hotelId, hotel.hotelName);
                    if (re.rowNum >0)
                    {
                        lvi = new ListViewItem(new string[] { hotel.hotelId, hotel.hotelName, DateTime.Now.ToShortTimeString(), re.rowNum.ToString() });

                    }
                    else {
                        lvi = new ListViewItem(new string[] { hotel.hotelId, hotel.hotelName, DateTime.Now.ToShortTimeString(), "0" });
                    }
                }
                catch (Exception ex)
                {
                    lvi = new ListViewItem(new string[] { hotel.hotelId, hotel.hotelName, DateTime.Now.ToShortTimeString(), "0", ex.Message });

                }
                finally
                {
                    this.lvImportHotels.Items.Add(lvi);
                }

            }
           

        }
  
    }


 
}
