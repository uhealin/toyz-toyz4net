using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Util;
using ZDSL.Model.Admin;
using ZDSL.Model.Cache;
using NHibernate;
using NHibernate.Criterion;
using Toyz4net.Core.Model;
using System.Web;
using System.Web.SessionState;

namespace ZDSL.Biz
{
    public class AdminBiz :BaseZdBiz
    {

        private AdminBiz() { }


        public  static string SESSION_KEY_USER = typeof(UserModel).FullName;
        public static string SESSION_KEY_LOGIN_MSG = "LoginMsg";

        private static TableCountModel CACHE_TABLE_COUNT; 
        private static AdminBiz Instant;
        private static readonly object lockHelper = new object();

        public static AdminBiz GetInstant() {

            if (Instant == null)
            {
                lock (lockHelper) {
                    if (Instant == null)
                    {
                        Instant = new AdminBiz();
                    }
                }
                
            }
            return Instant;
        }

        public IList<TreeNodeObject> createTree(IList<MenuModel> menus, string parentId)
        {
           
           return MenuModel.getTreeNode(menus,parentId);
        }

        public TableCountModel getTableCount() {
            TableCountModel tableCount = CACHE_TABLE_COUNT;
            if (tableCount == null||tableCount.isTimeout())
            {
                IQuery iquery = GetNameQuery("procUpdateTableCount");
                iquery.ExecuteUpdate();
                this.OpenSession();
                tableCount = this.session.Load<TableCountModel>("system");
                tableCount.setTimeout(300000);
                this.CloseSession();
            }
            return tableCount;
   
        }


        public bool isAuthority() {

            return WebUtil.GetSessionAttr<UserModel>(SESSION_KEY_USER) != null;
        }

        public JsResultObject login(string id,string pwd) {
            JsResultObject re = new JsResultObject();
            ICriteria icr = CreateCriteria<UserModel>();
            icr.Add(Restrictions.And(Restrictions.Eq("id", id), Restrictions.Eq("pwd", pwd)));
            IList<UserModel> users = new List<UserModel>();
            try
            {
                users = icr.List<UserModel>();
                if (users.Count == 1)
                {
                    UserModel tempUser = users[0];
                    re.code = JsResultObject.CODE_SUCCESS;
                    re.title = "登陆成功";
                    re.msg = string.Format("欢迎用户 {0} 进入捷达商务管理系统,登陆系统时间为{1}", tempUser.name,DateTime.Now.ToString(DateTimeUtil.PATTERN_DB_DATETIME));
                    WebUtil.SetSessionAttr(SESSION_KEY_USER, tempUser);
                    WebUtil.SetSessionAttr(SESSION_KEY_LOGIN_MSG, re.msg);
                }
                else if(users.Count<0) { 
                   throw new Exception("登陆帐号或密码错误");
                }
                else if (users.Count > 1) {
                    throw new Exception(string.Format( "存在{0}个同名的用户",users.Count));
                }
            }
            catch (Exception ex) {
                re.code = JsResultObject.CODE_ERROR;
                re.title = "登陆失败";
                re.msg = string.Format("失败原因:{0}",ex.Message);
            }
           
            return re;
        }

        

        public static ConfigModel GetCurrConfig() {
            ConfigModel config= BaseZdBiz.Load<ConfigModel>("default");
            if (config == null) {
                config = new ConfigModel();
                config.id = "default";
            }
            return config;
        } 

    }
}
