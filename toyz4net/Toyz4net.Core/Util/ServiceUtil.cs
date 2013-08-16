using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Service;

namespace Toyz4net.Core.Util
{
    public  class ServiceUtil
    {
        public static JsResultObject ValidateEmail(string email)
        {

            // 0 = 请重新验证；1 = 邮件地址合法；2 = 只是域名正确；3 = 一个未知错误；4 = 邮件服务器没有找到；5 = 电子邮件地址错误；6 = 免费用户验证超过数量（50次/24小时）；
            JsResultObject re = new JsResultObject();
            ValidateEmailService service = new ValidateEmailService();
            byte b = service.ValidateEmailAddress(email);
             re.title = "Email验证失败";
             re.code = JsResultObject.CODE_ERROR;
            if (b == ValidateEmailService.RESULT_RIGHTFUL)
            {
                re.code = JsResultObject.CODE_SUCCESS;
                re.title = "Email验证成功";
                re.msg = string.Format("{0}:有效", email);
            }
            else if(b== ValidateEmailService.RESULT_BUSSINESS_USER_ERROR)
            {
                
                re.msg = string.Format("{0}:商业用户不能通过验证", email);
            }
            else if (b == ValidateEmailService.RESULT_DOMAINS_RIGHTFUL)
            {
                
                re.msg = string.Format("{0}:只是域名正确", email);
            }
            else if (b == ValidateEmailService.RESULT_FROMATE_ERROR)
            {

                re.msg = string.Format("{0}:电子邮件地址错误", email);
            }
            else if (b == ValidateEmailService.RESULT_NO_SERVER)
            {

                re.msg = string.Format("{0}:邮件服务器没有找到", email);
            }
            else if (b == ValidateEmailService.RESULT_REVALIDATE)
            {

                re.msg = string.Format("{0}:请重新验证", email);
            }
            else if (b == ValidateEmailService.RESULT_UNKNOW_ERROR)
            {
                
                re.msg = string.Format("{0}:未知错误", email);
            }
            else if (b == ValidateEmailService.RESULT_VALIDATE_OVERFLOW)
            {

                re.msg = string.Format("{0}:免费用户验证超过数量（50次/24小时）", email);
            }
            return re;
        }
        

        public static JsResultObject ValidateMoblie(string mobile) {

            JsResultObject re = new JsResultObject();
            MobileCodeService service = new MobileCodeService();
            string[] res= service.getMobileCodeInfo(mobile,"").Split('：');

            if (res.Length==2)
            {
                re.code = JsResultObject.CODE_SUCCESS;
                re.msg = res[1];
            }
            else {
                re.code = JsResultObject.CODE_ERROR;
                re.msg = string.Format("{0}为无效手机号码", mobile);
            }
            return re;
        }
    }
}
