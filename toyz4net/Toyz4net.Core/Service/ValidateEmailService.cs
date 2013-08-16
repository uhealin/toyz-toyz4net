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
    [System.Web.Services.WebServiceBindingAttribute(Name = "ValidateEmailWebServiceSoap", Namespace = "http://WebXml.com.cn/")]
    public partial class ValidateEmailService : System.Web.Services.Protocols.SoapHttpClientProtocol 
    {
       //
        public static byte RESULT_REVALIDATE = 0;  //0 = 请重新验证；
        public static byte RESULT_RIGHTFUL = 1;  //1 = 邮件地址合法；
        public static byte RESULT_DOMAINS_RIGHTFUL = 2;  //2 = 只是域名正确；
        public static byte RESULT_UNKNOW_ERROR = 3;  // 3 = 一个未知错误；
        public static byte RESULT_NO_SERVER = 4;  //4 = 邮件服务器没有找到；
        public static byte RESULT_FROMATE_ERROR = 5;  //5 = 电子邮件地址错误；
        public static byte RESULT_VALIDATE_OVERFLOW = 6;  // 6 = 免费用户验证超过数量（50次/24小时）；
        public static byte RESULT_BUSSINESS_USER_ERROR = 7;  //7 = 商业用户不能通过验证；

         /// <remarks/>
    public ValidateEmailService() {
        this.Url = "http://webservice.webxml.com.cn/WebServices/ValidateEmailWebService.asmx";
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://WebXml.com.cn/ValidateEmailAddress", RequestNamespace="http://WebXml.com.cn/", ResponseNamespace="http://WebXml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public byte ValidateEmailAddress(string theEmail) {
        object[] results = this.Invoke("ValidateEmailAddress", new object[] {
                    theEmail});
        return ((byte)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginValidateEmailAddress(string theEmail, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("ValidateEmailAddress", new object[] {
                    theEmail}, callback, asyncState);
    }
    
    /// <remarks/>
    public byte EndValidateEmailAddress(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((byte)(results[0]));
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://WebXml.com.cn/ValidateEmailAddressPro", RequestNamespace="http://WebXml.com.cn/", ResponseNamespace="http://WebXml.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public byte ValidateEmailAddressPro(string theEmail, int theEmailPort) {
        object[] results = this.Invoke("ValidateEmailAddressPro", new object[] {
                    theEmail,
                    theEmailPort});
        return ((byte)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginValidateEmailAddressPro(string theEmail, int theEmailPort, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("ValidateEmailAddressPro", new object[] {
                    theEmail,
                    theEmailPort}, callback, asyncState);
    }
    
    /// <remarks/>
    public byte EndValidateEmailAddressPro(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((byte)(results[0]));
    }


    }
}
