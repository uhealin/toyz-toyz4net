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
[System.Web.Services.WebServiceBindingAttribute(Name="ValidateCodeWebServiceSoap", Namespace="http://WebXml.com.cn/")]
    public partial class ValidateCodeService : System.Web.Services.Protocols.SoapHttpClientProtocol 
    {

         /// <remarks/>
    public ValidateCodeService() {
        this.Url = "http://webservice.webxml.com.cn/WebServices/ValidateCodeWebService.asmx";
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://WebXml.com.cn/smallValidateImage", RequestNamespace="http://WebXml.com.cn/", ResponseNamespace="http://WebXml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public void smallValidateImage(string byString) {
        this.Invoke("smallValidateImage", new object[] {
                    byString});
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginsmallValidateImage(string byString, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("smallValidateImage", new object[] {
                    byString}, callback, asyncState);
    }
    
    /// <remarks/>
    public void EndsmallValidateImage(System.IAsyncResult asyncResult) {
        this.EndInvoke(asyncResult);
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://WebXml.com.cn/smallValidateByte", RequestNamespace="http://WebXml.com.cn/", ResponseNamespace="http://WebXml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
    public byte[] smallValidateByte(string byString) {
        object[] results = this.Invoke("smallValidateByte", new object[] {
                    byString});
        return ((byte[])(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginsmallValidateByte(string byString, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("smallValidateByte", new object[] {
                    byString}, callback, asyncState);
    }
    
    /// <remarks/>
    public byte[] EndsmallValidateByte(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((byte[])(results[0]));
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://WebXml.com.cn/enValidateImage", RequestNamespace="http://WebXml.com.cn/", ResponseNamespace="http://WebXml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public void enValidateImage(string byString) {
        this.Invoke("enValidateImage", new object[] {
                    byString});
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginenValidateImage(string byString, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("enValidateImage", new object[] {
                    byString}, callback, asyncState);
    }
    
    /// <remarks/>
    public void EndenValidateImage(System.IAsyncResult asyncResult) {
        this.EndInvoke(asyncResult);
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://WebXml.com.cn/enValidateByte", RequestNamespace="http://WebXml.com.cn/", ResponseNamespace="http://WebXml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
    public byte[] enValidateByte(string byString) {
        object[] results = this.Invoke("enValidateByte", new object[] {
                    byString});
        return ((byte[])(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginenValidateByte(string byString, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("enValidateByte", new object[] {
                    byString}, callback, asyncState);
    }
    
    /// <remarks/>
    public byte[] EndenValidateByte(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((byte[])(results[0]));
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://WebXml.com.cn/cnValidateImage", RequestNamespace="http://WebXml.com.cn/", ResponseNamespace="http://WebXml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public void cnValidateImage(string byString) {
        this.Invoke("cnValidateImage", new object[] {
                    byString});
    }
    
    /// <remarks/>
    public System.IAsyncResult BegincnValidateImage(string byString, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("cnValidateImage", new object[] {
                    byString}, callback, asyncState);
    }
    
    /// <remarks/>
    public void EndcnValidateImage(System.IAsyncResult asyncResult) {
        this.EndInvoke(asyncResult);
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://WebXml.com.cn/cnValidateByte", RequestNamespace="http://WebXml.com.cn/", ResponseNamespace="http://WebXml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
    public byte[] cnValidateByte(string byString) {
        object[] results = this.Invoke("cnValidateByte", new object[] {
                    byString});
        return ((byte[])(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegincnValidateByte(string byString, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("cnValidateByte", new object[] {
                    byString}, callback, asyncState);
    }
    
    /// <remarks/>
    public byte[] EndcnValidateByte(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((byte[])(results[0]));
    }


    }
}
