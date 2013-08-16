using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using Toyz4net.Core.NorthBoundAPIService;

namespace Toyz4net.Core.Service
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("WebServiceStudio", "0.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "NorthBoundAPIServiceSoap", Namespace = "http://elong.com/NorthBoundAPI/")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Response))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Request))]

    public partial class ELongApiService : System.Web.Services.Protocols.SoapHttpClientProtocol 
    {

        public static string USER_TEST = System.Configuration.ConfigurationManager.AppSettings["elong.user.test"];
        public static string PWD_TEST = System.Configuration.ConfigurationManager.AppSettings["elong.pwd.test"];
        public static string USER_PUBLIC = System.Configuration.ConfigurationManager.AppSettings["elong.user.public"];
        public static string PWD_PUBLIC = System.Configuration.ConfigurationManager.AppSettings["elong.pwd.public"];

        public static string URL_PUBLIC = System.Configuration.ConfigurationManager.AppSettings["elong.url.public"];
        public static string URL_TEST = System.Configuration.ConfigurationManager.AppSettings["elong.url.test"];

        public static string CREDIT_NUMBER_TEST = "4033910000000000";

        public static string RESULT_CODE_SUCCESS = "0";
        public static string RESULT_CODE_ERROR = "1";

        private static ELongApiService Instant;
        private static readonly object lockHelper = new object();

        private ELongApiService(string url) {
            this.Url = url;
        }

        public static string ROOM_IVN_STATUE_CODE_NORMAL="0";
        public static string ROOM_IVN_STATUE_CODE_SOMEDAY_FULL = "1";
        public static string ROOM_IVN_STATUE_CODE_ALL_FULL = "2";

        public static string GetTextRoomInvStatusCode(string code) {
            string text = "未知状态:"+code;   //0-正常 1-部分日期满房，2-全部满房
            if (code == ROOM_IVN_STATUE_CODE_ALL_FULL) {
                text = "全部满房";
            }
            else if (code == ROOM_IVN_STATUE_CODE_NORMAL)
            {
                text = "正常";
            }
            else if(code==ROOM_IVN_STATUE_CODE_SOMEDAY_FULL) {
                text = "部分日期满房";
            }
            return text;
        }

        public static ELongApiService GetTestInstant()
        {

            if (Instant == null)
            {
                lock (lockHelper)
                {
                    if (Instant == null)
                    {
                        Instant = new ELongApiService(URL_TEST);
                    }
                }

            }
            return Instant;
        }

        public static ELongApiService GetPublicInstant()
        {

            if (Instant == null)
            {
                lock (lockHelper)
                {
                    if (Instant == null)
                    {
                        Instant = new ELongApiService(URL_PUBLIC);
                    }
                }

            }
            return Instant;
        }


        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://elong.com/NorthBoundAPI/GetHotelList", RequestNamespace = "http://elong.com/NorthBoundAPI/", ResponseNamespace = "http://elong.com/NorthBoundAPI/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public GetHotelListResponse GetHotelList(GetHotelListRequest GetHotelListRequest)
        {
            object[] results = this.Invoke("GetHotelList", new object[] {
                    GetHotelListRequest});
            return ((GetHotelListResponse)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetHotelList(GetHotelListRequest GetHotelListRequest, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetHotelList", new object[] {
                    GetHotelListRequest}, callback, asyncState);
        }

        /// <remarks/>
        public GetHotelListResponse EndGetHotelList(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((GetHotelListResponse)(results[0]));
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://elong.com/NorthBoundAPI/GetHotelBaseInfoList", RequestNamespace = "http://elong.com/NorthBoundAPI/", ResponseNamespace = "http://elong.com/NorthBoundAPI/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public GetHotelBaseInfoListResponse GetHotelBaseInfoList(GetHotelBaseInfoListRequest GetHotelListRequest)
        {
            object[] results = this.Invoke("GetHotelBaseInfoList", new object[] {
                    GetHotelListRequest});
            return ((GetHotelBaseInfoListResponse)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetHotelBaseInfoList(GetHotelBaseInfoListRequest GetHotelListRequest, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetHotelBaseInfoList", new object[] {
                    GetHotelListRequest}, callback, asyncState);
        }

        /// <remarks/>
        public GetHotelBaseInfoListResponse EndGetHotelBaseInfoList(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((GetHotelBaseInfoListResponse)(results[0]));
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://elong.com/NorthBoundAPI/SubmitHotelOrder", RequestNamespace = "http://elong.com/NorthBoundAPI/", ResponseNamespace = "http://elong.com/NorthBoundAPI/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public SubmitHotelOrderResponse SubmitHotelOrder(SubmitHotelOrderRequest SubmitHotelOrderRequest)
        {
            object[] results = this.Invoke("SubmitHotelOrder", new object[] {
                    SubmitHotelOrderRequest});
            return ((SubmitHotelOrderResponse)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginSubmitHotelOrder(SubmitHotelOrderRequest SubmitHotelOrderRequest, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("SubmitHotelOrder", new object[] {
                    SubmitHotelOrderRequest}, callback, asyncState);
        }

        /// <remarks/>
        public SubmitHotelOrderResponse EndSubmitHotelOrder(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((SubmitHotelOrderResponse)(results[0]));
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://elong.com/NorthBoundAPI/GetHotelOrderList", RequestNamespace = "http://elong.com/NorthBoundAPI/", ResponseNamespace = "http://elong.com/NorthBoundAPI/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public GetHotelOrderListResponse GetHotelOrderList(GetHotelOrderListRequest GetHotelOrderListRequest)
        {
            object[] results = this.Invoke("GetHotelOrderList", new object[] {
                    GetHotelOrderListRequest});
            return ((GetHotelOrderListResponse)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetHotelOrderList(GetHotelOrderListRequest GetHotelOrderListRequest, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetHotelOrderList", new object[] {
                    GetHotelOrderListRequest}, callback, asyncState);
        }

        /// <remarks/>
        public GetHotelOrderListResponse EndGetHotelOrderList(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((GetHotelOrderListResponse)(results[0]));
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://elong.com/NorthBoundAPI/GetHotelOrderDetailById", RequestNamespace = "http://elong.com/NorthBoundAPI/", ResponseNamespace = "http://elong.com/NorthBoundAPI/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public GetHotelOrderDetailByOrderIdResponse GetHotelOrderDetailById(GetHotelOrderDetailByOrderIdRequest GetHotelOrderDetailByIdRequest)
        {
            object[] results = this.Invoke("GetHotelOrderDetailById", new object[] {
                    GetHotelOrderDetailByIdRequest});
            return ((GetHotelOrderDetailByOrderIdResponse)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetHotelOrderDetailById(GetHotelOrderDetailByOrderIdRequest GetHotelOrderDetailByIdRequest, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetHotelOrderDetailById", new object[] {
                    GetHotelOrderDetailByIdRequest}, callback, asyncState);
        }

        /// <remarks/>
        public GetHotelOrderDetailByOrderIdResponse EndGetHotelOrderDetailById(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((GetHotelOrderDetailByOrderIdResponse)(results[0]));
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://elong.com/NorthBoundAPI/GetHotelOrderListById", RequestNamespace = "http://elong.com/NorthBoundAPI/", ResponseNamespace = "http://elong.com/NorthBoundAPI/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public GetHotelOrderListByIdResponse GetHotelOrderListById(GetHotelOrderListByIdRequest GetHotelOrderListByIdRequest)
        {
            object[] results = this.Invoke("GetHotelOrderListById", new object[] {
                    GetHotelOrderListByIdRequest});
            return ((GetHotelOrderListByIdResponse)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetHotelOrderListById(GetHotelOrderListByIdRequest GetHotelOrderListByIdRequest, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetHotelOrderListById", new object[] {
                    GetHotelOrderListByIdRequest}, callback, asyncState);
        }

        /// <remarks/>
        public GetHotelOrderListByIdResponse EndGetHotelOrderListById(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((GetHotelOrderListByIdResponse)(results[0]));
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://elong.com/NorthBoundAPI/CancelHotelOrderById", RequestNamespace = "http://elong.com/NorthBoundAPI/", ResponseNamespace = "http://elong.com/NorthBoundAPI/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public CancelHotelOrderByIdResponse CancelHotelOrderById(CancelHotelOrderByIdRequest CancelHotelOrderByIdRequest)
        {
            object[] results = this.Invoke("CancelHotelOrderById", new object[] {
                    CancelHotelOrderByIdRequest});
            return ((CancelHotelOrderByIdResponse)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginCancelHotelOrderById(CancelHotelOrderByIdRequest CancelHotelOrderByIdRequest, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("CancelHotelOrderById", new object[] {
                    CancelHotelOrderByIdRequest}, callback, asyncState);
        }

        /// <remarks/>
        public CancelHotelOrderByIdResponse EndCancelHotelOrderById(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((CancelHotelOrderByIdResponse)(results[0]));
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://elong.com/NorthBoundAPI/InstantConfirm", RequestNamespace = "http://elong.com/NorthBoundAPI/", ResponseNamespace = "http://elong.com/NorthBoundAPI/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public GetInstantConfirmResponse InstantConfirm(GetInstantConfirmRequest request)
        {
            object[] results = this.Invoke("InstantConfirm", new object[] {
                    request});
            return ((GetInstantConfirmResponse)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginInstantConfirm(GetInstantConfirmRequest request, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("InstantConfirm", new object[] {
                    request}, callback, asyncState);
        }

        /// <remarks/>
        public GetInstantConfirmResponse EndInstantConfirm(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((GetInstantConfirmResponse)(results[0]));
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://elong.com/NorthBoundAPI/Login", RequestNamespace = "http://elong.com/NorthBoundAPI/", ResponseNamespace = "http://elong.com/NorthBoundAPI/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public LoginResponse Login(LoginRequest loginRequest)
        {
            object[] results = this.Invoke("Login", new object[] {
                    loginRequest});
            return ((LoginResponse)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginLogin(LoginRequest loginRequest, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Login", new object[] {
                    loginRequest}, callback, asyncState);
        }

        /// <remarks/>
        public LoginResponse EndLogin(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((LoginResponse)(results[0]));
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://elong.com/NorthBoundAPI/GetHotelProductVouch", RequestNamespace = "http://elong.com/NorthBoundAPI/", ResponseNamespace = "http://elong.com/NorthBoundAPI/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public GetHotelProductVouchForAPIResponse GetHotelProductVouch(GetHotelProductVouchForAPIRequest request)
        {
            object[] results = this.Invoke("GetHotelProductVouch", new object[] {
                    request});
            return ((GetHotelProductVouchForAPIResponse)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetHotelProductVouch(GetHotelProductVouchForAPIRequest request, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetHotelProductVouch", new object[] {
                    request}, callback, asyncState);
        }

        /// <remarks/>
        public GetHotelProductVouchForAPIResponse EndGetHotelProductVouch(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((GetHotelProductVouchForAPIResponse)(results[0]));
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://elong.com/NorthBoundAPI/GetHotelInventory", RequestNamespace = "http://elong.com/NorthBoundAPI/", ResponseNamespace = "http://elong.com/NorthBoundAPI/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public GetHotelInventoryResponse GetHotelInventory(GetHotelInventoryRequest request)
        {
            object[] results = this.Invoke("GetHotelInventory", new object[] {
                    request});
            return ((GetHotelInventoryResponse)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetHotelInventory(GetHotelInventoryRequest request, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetHotelInventory", new object[] {
                    request}, callback, asyncState);
        }

        /// <remarks/>
        public GetHotelInventoryResponse EndGetHotelInventory(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((GetHotelInventoryResponse)(results[0]));
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://elong.com/NorthBoundAPI/GetHotelRoomPriceInfo", RequestNamespace = "http://elong.com/NorthBoundAPI/", ResponseNamespace = "http://elong.com/NorthBoundAPI/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public GetHotelRoomPriceResponse GetHotelRoomPriceInfo(GetHotelRoomPriceRequest request)
        {
            object[] results = this.Invoke("GetHotelRoomPriceInfo", new object[] {
                    request});
            return ((GetHotelRoomPriceResponse)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetHotelRoomPriceInfo(GetHotelRoomPriceRequest request, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetHotelRoomPriceInfo", new object[] {
                    request}, callback, asyncState);
        }

        /// <remarks/>
        public GetHotelRoomPriceResponse EndGetHotelRoomPriceInfo(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((GetHotelRoomPriceResponse)(results[0]));
        }
    }
}
