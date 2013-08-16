using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using Toyz4net.Core.Util;

namespace Toyz4net.Core.Service
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("WebServiceStudio", "0.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "MobileCodeWSSoap", Namespace = "http://WebXml.com.cn/")]
    public partial class MobileCodeService : System.Web.Services.Protocols.SoapHttpClientProtocol 
    {


        /// <remarks/>
        public MobileCodeService()
        {
        this.Url = "http://webservice.webxml.com.cn/WebServices/MobileCodeWS.asmx";
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://WebXml.com.cn/getMobileCodeInfo", RequestNamespace="http://WebXml.com.cn/", ResponseNamespace="http://WebXml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string getMobileCodeInfo(string mobileCode, string userID) {
        object[] results = this.Invoke("getMobileCodeInfo", new object[] {
                    mobileCode,
                    userID});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegingetMobileCodeInfo(string mobileCode, string userID, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getMobileCodeInfo", new object[] {
                    mobileCode,
                    userID}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndgetMobileCodeInfo(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://WebXml.com.cn/getDatabaseInfo", RequestNamespace="http://WebXml.com.cn/", ResponseNamespace="http://WebXml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string[] getDatabaseInfo() {
        object[] results = this.Invoke("getDatabaseInfo", new object[0]);
        return ((string[])(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegingetDatabaseInfo(System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getDatabaseInfo", new object[0], callback, asyncState);
    }
    
    /// <remarks/>
    public string[] EndgetDatabaseInfo(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string[])(results[0]));
    }


    }


    public class MobileCodeObject{


        //String(0) 到 String(4)：省份，城市，城市代码，城市图片名称，最后更新时间。String(5) 到 String(11)：当天的 气温，概况，风向和风力，天气趋势开始图片名称(以下称：图标一)，天气趋势结束图片名称(以下称：图标二)，现在的天气实况，天气和生活指数。String(12) 到 String(16)：第二天的 气温，概况，风向和风力，图标一，图标二。String(17) 到 String(21)：第三天的 气温，概况，风向和风力，图标一，图标二。String(22) 被查询的城市或地区的介绍


        public string provinceName { get; set; }
        public string cityName { get; set; }
        public string cityCode { get; set; }
        public string cityImage { get; set; }
        public DateTime lastModifyDate { get; set; }

        public string todayTemperature { get; set; }
        public string todayBaseInfo { get; set; }
        public string todayWind { get; set; }
        public string todayDrift { get; set; }
        public string todayDriftStartImage { get; set; }
        public string todayDriftEndImage { get; set; }
        public string todayAllInfo { get; set; }
        public string todayTip { get; set; }

        public string tomorrowTemperature { get; set; }
        public string tomorrowBaseInfo { get; set; }
        public string tomorrowWind { get; set; }
        public string tomorrowDrift { get; set; }
        public string tomorrowDriftStartImage { get; set; }
        public string tomorrowDriftEndImage { get; set; }

        public string aftertomorrowTemperature { get; set; }
        public string aftertomorrowBaseInfo { get; set; }
        public string aftertomorrowWind { get; set; }
        public string aftertomorrowDrift { get; set; }
        public string aftertomorrowDriftStartImage { get; set; }
        public string aftertomorrowDriftEndImage { get; set; }

        public string cityDescp { get; set; }

        public MobileCodeObject(string[] res) {
            int index=0;
            this.provinceName = res[index++];
            this.cityName = res[index++];
            this.cityCode = res[index++];
            this.cityImage = res[index++];
            this.lastModifyDate = DateTimeUtil.ParseDateTime(res[index++]);

            this.todayTemperature = res[index++];
            this.todayBaseInfo = res[index++];
            this.todayWind = res[index++];
            this.todayDrift = res[index++];
            this.todayDriftStartImage = res[index++];
            this.todayDriftEndImage = res[index++];
            this.todayAllInfo = res[index++];
            this.todayTip = res[index++];


            this.tomorrowTemperature = res[index++];
            this.tomorrowBaseInfo = res[index++];
            this.tomorrowWind = res[index++];
            this.tomorrowDrift = res[index++];
            this.tomorrowDriftStartImage = res[index++];
            this.tomorrowDriftEndImage = res[index++];



            this.aftertomorrowTemperature = res[index++];
            this.aftertomorrowBaseInfo = res[index++];
            this.aftertomorrowWind = res[index++];
            this.aftertomorrowDrift = res[index++];
            this.aftertomorrowDriftStartImage = res[index++];
            this.aftertomorrowDriftEndImage = res[index++];

            this.cityDescp = res[index++];



        }

    }
}
