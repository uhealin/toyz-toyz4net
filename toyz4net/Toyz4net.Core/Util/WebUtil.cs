using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections;
using System.Web.SessionState;
using System.Web.Mvc;
using System.Reflection;
using Toyz4net.Core.Plugin;
using System.Net;

namespace Toyz4net.Core.Util
{
     public static class WebUtil
    {

         public static string JQ_DOCUMENT_READY_START = "$(document).ready(function(){";
         public static string JQ_DOCUMENT_READY_END = "});  //$(document).ready";

         public static string GetWebRootPath() {
             /*
             string AppPath = "";
             HttpContext HttpCurrent = HttpContext.Current;
             HttpRequest Req;
             if (HttpCurrent == null) return "";
             
             Req = HttpCurrent.Request;
             string UrlAuthority = Req.Url.GetLeftPart(UriPartial.Authority);
             if (Req.ApplicationPath == null || Req.ApplicationPath == "/")
             //直接安装在   Web   站点   
             {
                 AppPath = UrlAuthority;
             }
             else
             {
                 //安装在虚拟子目录下   
                 AppPath = UrlAuthority + Req.ApplicationPath;
             }
             return AppPath.TrimEnd('/','\\');
              */
             string rootPath = "";
             if (HttpContext.Current == null) return "";

             HttpRequest req = HttpContext.Current.Request;
             rootPath = string.Format("{0}://{1}{2}{3}"
                 ,req.Url.Scheme
                 ,req.Url.Host
                 ,req.Url.Port==80?"" : ":"+req.Url.Port
                 ,req.ApplicationPath
                 );
             return rootPath.TrimEnd('/','\\');
         }


         public static int IsNullOrEmpty(string[] paramNames) {
             if (HttpContext.Current == null) return ObjectUtil.RESULT_ALL_NULL;

             HttpRequest req = HttpContext.Current.Request;
             IList<string> vals = new List<string>();
             foreach (string paramName in paramNames) {
                 vals.Add(req.Params[paramName]);
             }
             return ObjectUtil.IsAllNullOrEmpty(vals.ToArray<string>());
         }

         public static HttpSessionState GetSession() {
             if (HttpContext.Current == null) return null;
             return HttpContext.Current.Session;
         }

         public static HttpRequest GetRequest() {
             if (HttpContext.Current == null) return null;
             return HttpContext.Current.Request;
            
         }

         public static T GetSessionAttr<T>(string key) {
             HttpSessionState session = GetSession();
             object obj= session[key];
             if (obj == null)
             { return default(T); }
             else {
                 return (T)obj;
             }

         }


         public static bool SetSessionAttr(string key, object val) {

             bool isOverwrite = GetSessionAttr<object>(key) != null;
             GetSession()[key] = val;
             return isOverwrite;
         }


         public static T GetRequestAttr<T>(string key) {
             HttpRequest req = GetRequest();
             object obj = req[key];
             if (obj == null)
             { return default(T); }
             else
             {
                 return (T)obj;
             }
         }

         public static string GetParamAttr(string key,string defaluVal) {
             HttpRequest req = GetRequest();
             return ObjectUtil.Parse(req.Params[key], defaluVal);
         }


         public static T Eval<T>(T obj, string prefix, string subfix)
         {
             HttpRequest req=GetRequest();
             if(req==null){return obj;}
             return ObjectUtil.Eval<T>(obj,req.Params,prefix,subfix);
         }


         public static string EncodeUTF8(string val){
             if (string.IsNullOrEmpty(val)) {
                 return "";
             }
             return HttpUtility.UrlEncode(val, Encoding.UTF8);
         }

         public static bool IsPost() {
             HttpRequest req = GetRequest();
             return "post" == req.RequestType.ToLower();
         }

         public static bool IsGet()
         {
             HttpRequest req = GetRequest();
             return "get" == req.RequestType.ToLower();
         }


         public static byte[] GenValidateImg(int length,out string vcode) {
             ValidateCode vCode = new ValidateCode();
             vcode = vCode.CreateValidateCode(length);
             byte[] bs = vCode.CreateValidateGraphic(vcode);
             WebUtil.SetSessionAttr(typeof(ValidateCode).Name, vcode);
             return bs;
         }


         public static JsResultObject DoValidateCode(string vcode) {
             JsResultObject re = new JsResultObject();
             re.code = JsResultObject.CODE_ERROR;
             string code = WebUtil.GetSessionAttr<string>(typeof(ValidateCode).Name);
             if (string.IsNullOrEmpty(vcode)) {
                 re.msg = "请输入验证码";
             }
             else if (string.IsNullOrEmpty(code)) {
                 re.msg = "验证码不存在";
             }
             else if (vcode != code)
             {
                 re.msg = "验证码匹配错误，请重新输入";
             }
             else {
                 re.code = JsResultObject.CODE_SUCCESS;
                 re.msg = "验证成功";
             }

             return re;
         }


          
    }
}
