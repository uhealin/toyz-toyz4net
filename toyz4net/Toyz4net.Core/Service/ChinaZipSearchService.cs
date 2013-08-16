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
    [System.Web.Services.WebServiceBindingAttribute(Name = "ChinaZipSearchWebServiceSoap", Namespace = "http://WebXml.com.cn/")]
    public partial class ChinaZipSearchService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

         /// <remarks/>
    public ChinaZipSearchService() {
        this.Url = "http://webservice.webxml.com.cn/WebServices/ChinaZipSearchWebService.asmx";
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
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://WebXml.com.cn/getSupportProvinceCity", RequestNamespace="http://WebXml.com.cn/", ResponseNamespace="http://WebXml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public System.Data.DataSet getSupportProvinceCity() {
        object[] results = this.Invoke("getSupportProvinceCity", new object[0]);
        return ((System.Data.DataSet)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegingetSupportProvinceCity(System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getSupportProvinceCity", new object[0], callback, asyncState);
    }
    
    /// <remarks/>
    public System.Data.DataSet EndgetSupportProvinceCity(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((System.Data.DataSet)(results[0]));
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://WebXml.com.cn/getAddressByZipCode", RequestNamespace="http://WebXml.com.cn/", ResponseNamespace="http://WebXml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public System.Data.DataSet getAddressByZipCode(string theZipCode, string userID) {
        object[] results = this.Invoke("getAddressByZipCode", new object[] {
                    theZipCode,
                    userID});
        return ((System.Data.DataSet)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegingetAddressByZipCode(string theZipCode, string userID, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getAddressByZipCode", new object[] {
                    theZipCode,
                    userID}, callback, asyncState);
    }
    
    /// <remarks/>
    public System.Data.DataSet EndgetAddressByZipCode(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((System.Data.DataSet)(results[0]));
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://WebXml.com.cn/getZipCodeByAddress", RequestNamespace="http://WebXml.com.cn/", ResponseNamespace="http://WebXml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public System.Data.DataSet getZipCodeByAddress(string theProvinceName, string theCityName, string theAddress, string userID) {
        object[] results = this.Invoke("getZipCodeByAddress", new object[] {
                    theProvinceName,
                    theCityName,
                    theAddress,
                    userID});
        return ((System.Data.DataSet)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegingetZipCodeByAddress(string theProvinceName, string theCityName, string theAddress, string userID, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getZipCodeByAddress", new object[] {
                    theProvinceName,
                    theCityName,
                    theAddress,
                    userID}, callback, asyncState);
    }
    
    /// <remarks/>
    public System.Data.DataSet EndgetZipCodeByAddress(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((System.Data.DataSet)(results[0]));
    }

    }
}
