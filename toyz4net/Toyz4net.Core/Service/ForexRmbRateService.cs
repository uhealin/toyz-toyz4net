using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toyz4net.Core.Service
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("WebServiceStudio", "0.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "ForexRmbRateWebServiceSoap", Namespace = "http://webxml.com.cn/")]
    public partial class ForexRmbRateService : System.Web.Services.Protocols.SoapHttpClientProtocol 
    {

        /// <remarks/>
    public ForexRmbRateService() {
        this.Url = "http://webservice.webxml.com.cn/WebServices/ForexRmbRateWebService.asmx";
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://webxml.com.cn/getForexRmbRate", RequestNamespace="http://webxml.com.cn/", ResponseNamespace="http://webxml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public System.Data.DataSet getForexRmbRate() {
        object[] results = this.Invoke("getForexRmbRate", new object[0]);
        return ((System.Data.DataSet)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegingetForexRmbRate(System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getForexRmbRate", new object[0], callback, asyncState);
    }
    
    /// <remarks/>
    public System.Data.DataSet EndgetForexRmbRate(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((System.Data.DataSet)(results[0]));
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://webxml.com.cn/getForexRmbRatePro", RequestNamespace="http://webxml.com.cn/", ResponseNamespace="http://webxml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public System.Data.DataSet getForexRmbRatePro(string theUserID) {
        object[] results = this.Invoke("getForexRmbRatePro", new object[] {
                    theUserID});
        return ((System.Data.DataSet)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegingetForexRmbRatePro(string theUserID, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getForexRmbRatePro", new object[] {
                    theUserID}, callback, asyncState);
    }
    
    /// <remarks/>
    public System.Data.DataSet EndgetForexRmbRatePro(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((System.Data.DataSet)(results[0]));
    }

    }
}
