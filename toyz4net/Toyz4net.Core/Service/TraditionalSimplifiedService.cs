using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    [System.Web.Services.WebServiceBindingAttribute(Name = "TraditionalSimplifiedWebServiceSoap", Namespace = "http://webxml.com.cn/")]
    public partial class TraditionalSimplifiedService : System.Web.Services.Protocols.SoapHttpClientProtocol 
    {


        /// <remarks/>
    public TraditionalSimplifiedService() {
        this.Url = "http://webservice.webxml.com.cn/WebServices/TraditionalSimplifiedWebService.asmx";
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://webxml.com.cn/toSimplifiedChinese", RequestNamespace="http://webxml.com.cn/", ResponseNamespace="http://webxml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string toSimplifiedChinese(string sText) {
        object[] results = this.Invoke("toSimplifiedChinese", new object[] {
                    sText});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegintoSimplifiedChinese(string sText, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("toSimplifiedChinese", new object[] {
                    sText}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndtoSimplifiedChinese(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://webxml.com.cn/toTraditionalChinese", RequestNamespace="http://webxml.com.cn/", ResponseNamespace="http://webxml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string toTraditionalChinese(string sText) {
        object[] results = this.Invoke("toTraditionalChinese", new object[] {
                    sText});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegintoTraditionalChinese(string sText, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("toTraditionalChinese", new object[] {
                    sText}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndtoTraditionalChinese(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }

    }
}
