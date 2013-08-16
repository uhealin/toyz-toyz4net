using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toyz4net.Core.Util
{
    public   static  class ResultUtil
    {

        public static JsResultObject SaveSuccess(int num,string recordName) {
            string patternMsg="已保存 {0}条 {1}数据";
            JsResultObject result = new JsResultObject();
            result.code = JsResultObject.CODE_SUCCESS;
            result.title = "操作成功";
            result.msg = string.Format(patternMsg, num, recordName);
            return result;
        }

        public static JsResultObject SaveError(string recordName,string errorMsg)
        {
            string patternMsg = "未保存 {0}数据。 原因:{1}";
            JsResultObject result = new JsResultObject();
            result.code = JsResultObject.CODE_ERROR;
            result.title = "操作失败";
            result.msg = string.Format(patternMsg,recordName,errorMsg);
            result.icon = JsResultObject.ICON_ERROR;
            return result;
        }

        public static JsResultObject RemoveSuccess(int num, string recordName)
        {
            string patternMsg = "成功删除 {0}条 {1}数据";
            JsResultObject result = new JsResultObject();
            result.code = JsResultObject.CODE_SUCCESS;
            result.title = "操作成功";
            result.msg = string.Format(patternMsg, num, recordName);
            return result;
        }

        public static JsResultObject RemoveError(string recordName, string errorMsg)
        {
            string patternMsg = "未保存 {0}数据。 原因:{1}";
            JsResultObject result = new JsResultObject();
            result.code = JsResultObject.CODE_ERROR;
            result.title = "操作失败";
            result.msg = string.Format(patternMsg, recordName, errorMsg);
            result.icon = JsResultObject.ICON_ERROR;
            return result;
        }

    }


    public class JsResultObject {

        public static int CODE_SUCCESS = 0;
        public static int CODE_ALERT = 1;
        public static int CODE_ERROR = 2;

        public static string ICON_INFO = "info";
        public static string ICON_ERROR = "error";
        public static string ICON_QUESTION = "question";
        public static string ICON_WARNING = "warning";

        public static string ACTION_SHOW = "show";
        public static string ACTION_CONFIRM = "confirm";
        public static string ACTION_ALERT = "alert";
        public static string ACTION_PROMPT = "prompt";


        public JsResultObject() {
            this.code = CODE_SUCCESS;
            this.msg = "";
            this.title = "";
            this.action = ACTION_SHOW;
            this.icon = ICON_INFO;
            this.attrs = new Dictionary<string, object>();
            this.rowNum = 0;
            this.traceStack = new Dictionary<string, string>();
        }

        public int code { get; set; }
        public string msg { get; set; }
        public string title { get; set; }
        public string action { get; set; }
        public string icon { get; set; }
        public int rowNum { get; set; }


        public Dictionary<string,object> attrs { get; set; }

        public Dictionary<string, string> traceStack { get; set; }
    }


    public class CrudResultObjct :JsResultObject{ 
        
    }
}
