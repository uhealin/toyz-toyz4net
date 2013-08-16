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
    [System.Web.Services.WebServiceBindingAttribute(Name = "WeatherWebServiceSoap", Namespace = "http://WebXml.com.cn/")]
    public partial class WeatherService : System.Web.Services.Protocols.SoapHttpClientProtocol 
    {

         /// <remarks/>
    public WeatherService() {
        this.Url = "http://webservice.webxml.com.cn/WebServices/WeatherWebService.asmx";
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://WebXml.com.cn/getSupportCity", RequestNamespace="http://WebXml.com.cn/", ResponseNamespace="http://WebXml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string[] getSupportCity(string byProvinceName) {
        object[] results = this.Invoke("getSupportCity", new object[] {
                    byProvinceName});
        return ((string[])(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegingetSupportCity(string byProvinceName, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getSupportCity", new object[] {
                    byProvinceName}, callback, asyncState);
    }
    
    /// <remarks/>
    public string[] EndgetSupportCity(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string[])(results[0]));
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://WebXml.com.cn/getSupportProvince", RequestNamespace="http://WebXml.com.cn/", ResponseNamespace="http://WebXml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string[] getSupportProvince() {
        object[] results = this.Invoke("getSupportProvince", new object[0]);
        return ((string[])(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegingetSupportProvince(System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getSupportProvince", new object[0], callback, asyncState);
    }
    
    /// <remarks/>
    public string[] EndgetSupportProvince(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string[])(results[0]));
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://WebXml.com.cn/getSupportDataSet", RequestNamespace="http://WebXml.com.cn/", ResponseNamespace="http://WebXml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public System.Data.DataSet getSupportDataSet() {
        object[] results = this.Invoke("getSupportDataSet", new object[0]);
        return ((System.Data.DataSet)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegingetSupportDataSet(System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getSupportDataSet", new object[0], callback, asyncState);
    }
    
    /// <remarks/>
    public System.Data.DataSet EndgetSupportDataSet(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((System.Data.DataSet)(results[0]));
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://WebXml.com.cn/getWeatherbyCityName", RequestNamespace="http://WebXml.com.cn/", ResponseNamespace="http://WebXml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string[] getWeatherbyCityName(string theCityName) {
        object[] results = this.Invoke("getWeatherbyCityName", new object[] {
                    theCityName});
        return ((string[])(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegingetWeatherbyCityName(string theCityName, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getWeatherbyCityName", new object[] {
                    theCityName}, callback, asyncState);
    }
    
    /// <remarks/>
    public string[] EndgetWeatherbyCityName(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string[])(results[0]));
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://WebXml.com.cn/getWeatherbyCityNamePro", RequestNamespace="http://WebXml.com.cn/", ResponseNamespace="http://WebXml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string[] getWeatherbyCityNamePro(string theCityName, string theUserID) {
        object[] results = this.Invoke("getWeatherbyCityNamePro", new object[] {
                    theCityName,
                    theUserID});
        return ((string[])(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegingetWeatherbyCityNamePro(string theCityName, string theUserID, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getWeatherbyCityNamePro", new object[] {
                    theCityName,
                    theUserID}, callback, asyncState);
    }
    
    /// <remarks/>
    public string[] EndgetWeatherbyCityNamePro(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string[])(results[0]));
    }

    }


    public class WeatherServiceObject{
        
         
    }
}
