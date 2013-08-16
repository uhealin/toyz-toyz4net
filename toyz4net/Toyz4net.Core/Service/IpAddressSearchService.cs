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
    [System.Web.Services.WebServiceBindingAttribute(Name = "IpAddressSearchWebServiceSoap", Namespace = "http://WebXml.com.cn/")]
    public partial class IpAddressSearchService : System.Web.Services.Protocols.SoapHttpClientProtocol 
    {

         /// <remarks/>
    public IpAddressSearchService() {
        this.Url = "http://webservice.webxml.com.cn/WebServices/IpAddressSearchWebService.asmx";
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://WebXml.com.cn/getCountryCityByIp", RequestNamespace="http://WebXml.com.cn/", ResponseNamespace="http://WebXml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string[] getCountryCityByIp(string theIpAddress) {
        object[] results = this.Invoke("getCountryCityByIp", new object[] {
                    theIpAddress});
        return ((string[])(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegingetCountryCityByIp(string theIpAddress, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getCountryCityByIp", new object[] {
                    theIpAddress}, callback, asyncState);
    }
    
    /// <remarks/>
    public string[] EndgetCountryCityByIp(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string[])(results[0]));
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://WebXml.com.cn/getGeoIPContext", RequestNamespace="http://WebXml.com.cn/", ResponseNamespace="http://WebXml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string[] getGeoIPContext() {
        object[] results = this.Invoke("getGeoIPContext", new object[0]);
        return ((string[])(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegingetGeoIPContext(System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getGeoIPContext", new object[0], callback, asyncState);
    }
    
    /// <remarks/>
    public string[] EndgetGeoIPContext(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string[])(results[0]));
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://WebXml.com.cn/getVersionTime", RequestNamespace="http://WebXml.com.cn/", ResponseNamespace="http://WebXml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string getVersionTime() {
        object[] results = this.Invoke("getVersionTime", new object[0]);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegingetVersionTime(System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getVersionTime", new object[0], callback, asyncState);
    }
    
    /// <remarks/>
    public string EndgetVersionTime(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }

    }
}
