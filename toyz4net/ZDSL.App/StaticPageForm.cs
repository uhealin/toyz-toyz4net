using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Toyz4net.Core.App;
using Toyz4net.Core.Util;
using NHibernate;
using NHibernate.Criterion;
using ZDSL.Biz;
using ZDSL.Model.Data;
using System.IO;
using System.Threading;

namespace ZDSL.App
{
    public partial class StaticPageForm : Form
    {

        private bool isStop;

        public StaticPageForm()
        {
            InitializeComponent();
            tbPathIndex.Text = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.Home.Index"];
            tbPathHotel.Text = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.Hotel.Index"];
            tbCityHotel.Text = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.Hotel.City"];
            tbDHotel.Text = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.Hotel.D"];
            tbClHotel.Text = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.Hotel.Cl"];
            tbPathBrand.Text = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.Brand.Index"];
            tbPathNews.Text = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.News.Index"];
            tbPathExhi.Text = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.Exhi.Index"];
            tbPathComment.Text = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.Comment.Index"];
            tbPathHotelDetail.Text = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.Hotel.Detail"];
            tbPathBrandDetail.Text = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.Brand.BrandHotel"];
            tbPathNewsDetail.Text = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.News.NewsHotel"];
            tbPathExhiDetail.Text = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.Exhi.ExhiHotel"];

        }

        private void btnSelectRootPath_Click(object sender, EventArgs e)
        {
            FolderDialog folder = new FolderDialog();
            if (folder.DisplayDialog() == DialogResult.OK)
            {
                tbRootPath.Text = folder.Path;
            }
            else {
                MessageBox.Show("你没有选择目录");
            }
        }

        /*
        private void btnSelectScriptPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                tbScriptPath.Text = file.FileName;
            }
            else {
                MessageBox.Show("你没有选择目录");
            }
        }

        private void btnGenScript_Click(object sender, EventArgs e)
        {
             string scriptPath=tbScriptPath.Text.Trim();
             FileUtil.FileDel(scriptPath );
             String scriptContext = this.getScript();
             if (FileUtil.WriteFile(scriptPath, scriptContext))
             {
                 MessageBox.Show(string.Format("写入文件成功:{0}", scriptPath));
             }
             else {
                 MessageBox.Show(string.Format("写入文件失败:{0}", scriptPath));
             }
        }
        */

        private string getScript() {

            StringBuilder sbr = new StringBuilder("");

            sbr.Append(getSubScript("/Public/Home/Index",tbPathIndex.Text));
            sbr.Append(getSubScript("/Public/Hotel/Index", tbPathHotel.Text));
            sbr.Append(getSubScript("/Public/Comment/Index", tbPathComment.Text));
            sbr.Append(getSubScript("/Public/Brand/Index", tbPathBrand.Text));
            sbr.Append(getSubScript("/Public/News/Index", tbPathNews.Text));
            sbr.Append(getSubScript("/Public/Exhi/Index", tbPathExhi.Text));

            ICriteria icr = BaseZdBiz.CreateCriteria<HotelModel>();
            IList<HotelModel> hotels = icr.List<HotelModel>();
            foreach (HotelModel hotel in hotels) {
                sbr.Append(getSubScript(
                  string.Format("/Public/Hotel/Detail?hotelId={0}",hotel.hotelId)
                  ,string.Format(tbPathHotelDetail.Text,hotel.hotelId)
                ));
            }

            icr = BaseZdBiz.CreateCriteria<BrandModel>();
            IList<BrandModel> brands = icr.List<BrandModel>();
            foreach (BrandModel brand in brands) {
                sbr.Append(getSubScript(
                   string.Format("/Public/Brand/BrandHotel?brandId={0}", brand.brandID)
                  , string.Format(tbPathBrandDetail.Text , brand.brandID)
                  ));
            }

        

            return sbr.ToString();
        
        }

        private string getSubScript(string url, string file) {
            return string.Format("[{3}{0}]\r\n URL={3}{0}\r\n File={4}{1} \r\n Time={2} \r\n\r\n",

                url.Trim(),
                file.Trim(),
                60,
                tbWebRootPath.Text.Trim(),
                tbRootPath.Text.Trim()
            );
        }

        private int genPageFile(string url,string file){
            string webPath = string.Format("{0}{1}", this.tbWebRootPath.Text.Trim(), url.Trim());
            string filePath = string.Format("{0}{1}", this.tbRootPath.Text.Trim(), file);
            string context="";
            try
            {
             context = NetClientUtil.doGet(webPath);
            }catch(Exception ex){
             return 0;
            }
            FileInfo fileInfo = new FileInfo(filePath);

            string dirPath = fileInfo.Directory.ToString();
            FileUtil.CreateFolder(dirPath);
                if (FileUtil.WriteFile(filePath, context))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            
          
           
        }

        private void btnGenPageFile_Click(object sender, EventArgs e)
        {
            this.tsslResult.Text = "开始导入。。。";
            Thread t = new Thread(new ThreadStart(StartGenPageFile));
            t.Start();

        }


        public void StartGenPageFile() {


            isStop = false;
            int count = 0;
            GeoBiz geoBiz = GeoBiz.GetInstant();
            if (cbHomeIndex.Checked)
            {
                count += genPageFile("/Public/Home/Index", tbPathIndex.Text);
            }
            if (cbHotelIndex.Checked)
            {
                count += genPageFile("/Public/Hotel/Index", tbPathHotel.Text);
            }
            if (cbCommentIndex.Checked)
            {
                count += genPageFile("/Public/Comment/Index", tbPathComment.Text);
            }
            if (cbBrandIndex.Checked)
            {
                count += genPageFile("/Public/Brand/Index", tbPathBrand.Text);
            }
            if (cbNewsIndex.Checked)
            {
                count += genPageFile("/Public/News/Index", tbPathNews.Text);
            }
            if (cbExhiIndex.Checked)
            {
                count += genPageFile("/Public/Exhi/Index", tbPathExhi.Text);
            }

            ICriteria icr = BaseZdBiz.CreateCriteria<HotelModel>();
            if (cbHotelDetail.Checked)
            {
                
                IList<HotelModel> hotels = icr.List<HotelModel>();
                count = 0;
                foreach (HotelModel hotel in hotels)
                {
                    if (isStop) break;
                    count += genPageFile(
                      string.Format("/Public/Hotel/Detail?hotelId={0}", hotel.hotelId)
                      , string.Format(tbPathHotelDetail.Text, hotel.hotelId)
                    );
                    this.tsslResult.Text = string.Format("({0}/{1}),{2}:{3} 生成中...", count, hotels.Count, hotel.hotelId, hotel.hotelName);
                }
            }

            if (cbBrandDetail.Checked)
            {
                icr = BaseZdBiz.CreateCriteria<BrandModel>();
                IList<BrandModel> brands = icr.List<BrandModel>();
                count = 0;
                foreach (BrandModel brand in brands)
                {
                    if (isStop) break;
                    count += genPageFile(
                       string.Format("/Public/Brand/BrandHotel?brandId={0}", brand.brandID)
                      , string.Format(tbPathBrandDetail.Text, brand.brandID)
                      );
                    this.tsslResult.Text = string.Format("({0}/{1}),{2}:{3} 生成中...", count, brands.Count, brand.brandID, brand.brandName);
                }
            }

            if (cbNewsHotel.Checked)
            {
                icr = BaseZdBiz.CreateCriteria<NewsModel>();
                IList<NewsModel> newss = icr.List<NewsModel>();
                count = 0;
                foreach (NewsModel news in newss)
                {
                    if (isStop) break;
                    count += genPageFile(
                       string.Format("/Public/News/NewsHotel?newsId={0}", news.id)
                      , string.Format(tbPathNewsDetail.Text, news.id)
                      );
                    this.tsslResult.Text = string.Format("({0}/{1}),{2}:{3} 生成中...", count, newss.Count,news.id , news.title);
                }
            }

            if (cbExhiHotel.Checked)
            {
                icr = BaseZdBiz.CreateCriteria<ExhiModel>();
                IList<ExhiModel> exhis = icr.List<ExhiModel>();
                count = 0;
                foreach (ExhiModel exhi in exhis)
                {
                    if (isStop) break;
                    count += genPageFile(
                       string.Format("/Public/Exhi/ExhiHotel?exhiId={0}", exhi.id)
                      , string.Format(tbPathExhiDetail.Text, exhi.id)
                      );
                    this.tsslResult.Text = string.Format("({0}/{1}),{2}:{3} 生成中...", count, exhis.Count, exhi.id, exhi.name);
                }
            }


           
            if (cbCityHotel.Checked) {
                IList<GeoModel> geos = BaseZdBiz.List<GeoModel>();
                foreach (GeoModel geo in geos)
                {
                    if (isStop) break;
                    count += genPageFile(
                       string.Format("/Public/Hotel/Search?cityName={0}", geo.cityName)
                      , string.Format(tbCityHotel.Text.Trim(), geo.cityName,geo.cityCode)
                      );
                    this.tsslResult.Text = string.Format("({0}/{1}),{2}:{3} 生成中...", count, geos.Count, geo.cityCode, geo.cityName);

                    if (cbClHotel.Checked) {
                        IList<GeoCommercialLocationModel> geoCls = geoBiz.GetGeoCls(geo.id);
                        int c = 0;
                        foreach (GeoCommercialLocationModel geoCl in geoCls) {
                            if (isStop) break;
                            
                            c += genPageFile(
                               string.Format("/Public/Hotel/Search?cityName={0}&geoClId={1}", geo.cityName,geoCl.locationId)
                              , string.Format( tbClHotel.Text.Trim(), geo.cityCode,geoCl.locationId)
                              );
                            this.tsslResult.Text = string.Format("({0}/{1}),{2}:{3}:{4} 生成中...", c, geoCls.Count, geo.cityCode, geo.cityName, geoCl.name);
                        }
                    }

                    if (cbDHotel.Checked)
                    {
                        IList<GeoDistrictsModel> geoDs = geoBiz.GetGeoDs(geo.id);
                        int c = 0;
                        foreach (GeoDistrictsModel geoD in geoDs)
                        {
                            if (isStop) break;
                            c += genPageFile(
                               string.Format("/Public/Hotel/Search?cityName={0}&geoDId={1}", geo.cityName, geoD.locationId)
                              , string.Format(tbDHotel.Text.Trim(), geo.cityCode,geoD.locationId)
                              );
                            this.tsslResult.Text = string.Format("({0}/{1}),{2}:{3}:{4} 生成中...", c, geoDs.Count, geo.cityCode, geo.cityName, geoD.name);
                        }
                    }
                }
            }

  



            this.tsslResult.Text =string.Format( "{0}完成",DateTime.Now.ToString());
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            isStop = true;
        }

    }
}
