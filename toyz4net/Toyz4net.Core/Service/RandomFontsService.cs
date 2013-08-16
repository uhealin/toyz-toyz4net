using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Toyz4net.Core.Service
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("WebServiceStudio", "0.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "RandomFontsWebServiceSoap", Namespace = "http://WebXml.com.cn/")]
    public partial class RandomFontsService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        /// <remarks/>
        public RandomFontsService()
        {
            this.Url = "http://webservice.webxml.com.cn/WebServices/RandomFontsWebService.asmx";
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://WebXml.com.cn/getChineseFonts", RequestNamespace = "http://WebXml.com.cn/", ResponseNamespace = "http://WebXml.com.cn/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string[] getChineseFonts(int byFontsLength)
        {
            object[] results = this.Invoke("getChineseFonts", new object[] {
                    byFontsLength});
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegingetChineseFonts(int byFontsLength, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("getChineseFonts", new object[] {
                    byFontsLength}, callback, asyncState);
        }

        /// <remarks/>
        public string[] EndgetChineseFonts(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://WebXml.com.cn/getCharFonts", RequestNamespace = "http://WebXml.com.cn/", ResponseNamespace = "http://WebXml.com.cn/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string[] getCharFonts(int byFontsLength)
        {
            object[] results = this.Invoke("getCharFonts", new object[] {
                    byFontsLength});
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegingetCharFonts(int byFontsLength, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("getCharFonts", new object[] {
                    byFontsLength}, callback, asyncState);
        }

        /// <remarks/>
        public string[] EndgetCharFonts(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }
    }

}
