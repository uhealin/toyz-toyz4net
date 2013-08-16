using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ZDSL.Model.Data;
using ZDSL.Biz;
using Toyz4net.Core.App;
using NHibernate;
using NHibernate.Criterion;
using ZDSL.Biz;
using Toyz4net.Core.Util;

namespace ZDSL.App
{
    public partial class ImageLocalForm : Form
    {
        private bool isStop;
        private IList<HotelModel> _hotels;

        public ImageLocalForm()
        {
            InitializeComponent();
        }

        private void btnSelectRootPath_Click(object sender, EventArgs e)
        {
            FolderDialog folder = new FolderDialog();
            if (folder.DisplayDialog() == DialogResult.OK)
            {
                tbRootPath.Text = folder.Path;
            }
            else
            {
                MessageBox.Show("你没有选择目录");
            }
        }

        private void btnImageLocal_Click(object sender, EventArgs e)
        {
            isStop = false;
            this.tsslResult.Text = "开始导入。。。";
            Thread t = new Thread(new ThreadStart(startImageLocal));
            t.Start();

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.isStop = true;
        }

        public void startImageLocal() {
            string basePath = tbRootPath.Text.Trim();
            if (string.IsNullOrEmpty(basePath)) {
                MessageBox.Show("请先设置图片路径");
                return;
            }
            DataBiz dataBiz = DataBiz.GetInstant();
            ICriteria icr = BaseZdBiz.CreateCriteria<HotelImageModel>();
            IList<HotelImageModel> images=icr.List<HotelImageModel>();
            JsResultObject re = new JsResultObject();
            foreach (HotelImageModel image in images) {
                if (isStop) break;
                JsResultObject tempRe = dataBiz.LocalHotelImage(basePath, image,false);
                re.rowNum += tempRe.rowNum;
              if (image.title.Contains("外观")) {
                  tempRe=dataBiz.LocalHotelImage(basePath, image, true );
                  re.rowNum += tempRe.rowNum;
              }
              this.tsslResult.Text = string.Format("({0}/{1}){2}:{3}", re.rowNum, images.Count, image.title, tempRe.msg);      
            }

        }



    }
}
