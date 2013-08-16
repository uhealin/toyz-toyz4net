using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;
using Toyz4net.Core.Util;

namespace Toyz4net.App.Xcar
{
    public abstract class Model
    {
        public string  id { get; set; }

        public void createPk()
        {
            int rangdomNum = new Random().Next(1000, 9999);
            string pk = string.Format("{0}{1}"
                , DateTime.Now.ToString("yyyyMMddHHmmss")
                , rangdomNum.ToString()
            );
            id = pk;
            Thread.Sleep(10);
            
        }

    }

    public class CustomerModel : Model
    {

        public string name { get; set; }
        public string mobile { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string shop { get; set; }
        public string buytime { get; set; }
        public string drivecs { get; set; }
        public string uploadInd { get; set; }
        public DateTime uploadTime { get; set; }
        public string remark { get; set; }

        public JsResultObject doSubmit()
        {


            string action = "http://topic.xcar.com.cn/201207/xxy/data/form/save1.php";

            string indata =

               "userid=name&passwd=password&SESSION_TIMEOUT=40";



            indata = string.Format("name={0}&mobile={1}&province={2}&city={3}&other={4}&buy_time={6}&drive_cs={5}",
                Gobal.GB2312(this.name.Trim())
                , Gobal.GB2312(this.mobile.Trim())
                , Gobal.GB2312(this.province.Trim())
                , Gobal.GB2312(this.city.Trim())
                , Gobal.GB2312(this.shop.Trim())
                , Gobal.GB2312(this.drivecs.Trim())
                , Gobal.GB2312(this.buytime.Trim())
                );

            //indata = "name=54&mobile=13569875579&province=%BC%AA%C1%D6&city=%CB%C4%C6%BD&other=%CB%C4%C6%BD%C9%EA%B4%EF&buy_time=1%D4%C2%7E3%D4%C2&drive_cs=%D6%D0%CE%E7+%2812%3A00+-+13%3A00%29";

            string outdata = "";

            CookieContainer myCookieContainer = new CookieContainer();

            //新建一个CookieContainer来存放Cookie集合 

            HttpWebRequest myHttpWebRequest;



            myHttpWebRequest = (HttpWebRequest)WebRequest.Create(action);

            //新建一个HttpWebRequest 
            myHttpWebRequest.Referer = "http://topic.xcar.com.cn/201207/xxy/";
            myHttpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:14.0) Gecko/20100101 Firefox/14.0.1";
            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";

            myHttpWebRequest.ContentLength = indata.Length;

            myHttpWebRequest.Method = "POST";



            myHttpWebRequest.CookieContainer = myCookieContainer;

            //设置HttpWebRequest的CookieContainer为刚才建立的那个myCookieContainer 

            Stream myRequestStream = myHttpWebRequest.GetRequestStream();

            StreamWriter myStreamWriter =

              new StreamWriter(myRequestStream);

            myStreamWriter.Write(indata);

            //把数据写入HttpWebRequest的Request流 


            myStreamWriter.Close();

            myRequestStream.Close();

            //关闭打开对象 

            HttpWebResponse myHttpWebResponse =

              (HttpWebResponse)myHttpWebRequest.GetResponse();

            //新建一个HttpWebResponse 

            myHttpWebResponse.Cookies =

              myCookieContainer.GetCookies(myHttpWebRequest.RequestUri);

            //获取一个包含url的Cookie集合的CookieCollection 

            Stream myResponseStream = myHttpWebResponse.GetResponseStream();

            StreamReader myStreamReader =

              new StreamReader(myResponseStream, Encoding.GetEncoding("gb2312"));

            outdata = myStreamReader.ReadToEnd();

            //把数据从HttpWebResponse的Response流中读出 

            myStreamReader.Close();

            myResponseStream.Close();

            JsResultObject re = new JsResultObject();
            this.uploadTime = DateTime.Now;
            if (outdata.Contains("提交成功"))
            {
                re.code = JsResultObject.CODE_SUCCESS;
                re.msg = "提交成功";
                this.uploadInd = "Y";
                
                this.remark = re.msg;
            }
            else
            {
                re.code = JsResultObject.CODE_ERROR;
                re.msg = outdata; ;
                this.remark = outdata;
            }
            return re;

        }
    }

    public class ShopModel : Model {
        public string name { get; set; }
        public string city { get; set; }
        public string brand { get; set; }
        public string province { get; set; }
        public string address { get; set; }
        public string bigRegion { get; set; }
        public string smallRegion { get; set; }
    }

    public class DictModel  {

        public enum Type { 
         drive_cs,buy_time
        }
        public int id { get; set; }
        public string type { get; set; }
        public string text { get; set; }
        public string value { get; set; }
    }

}
