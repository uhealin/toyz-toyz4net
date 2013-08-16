using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using NHibernate;
using NHibernate.Criterion;
using System.Reflection;
using Toyz4net.Core.Util;
using Toyz4net.Core.Model;


namespace ZDSL.Biz
{
    public abstract class BaseZdBiz
    {
        private static string CONNECT_STRING_DEFAULT = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=ZDSLDB;Integrated Security=True";
        private static string DB_TYPE_DEFAULT = "MSSQL2005";
        private static string PATH_JS_CAHCE = "Cache/";

           private static Configuration CFG_DEFAULT;
           
           private static ISessionFactory SESSION_FACTORY;
           protected ISession session;
           
        
           private static void CreateCfg(){
               if (CFG_DEFAULT == null) {
                   CFG_DEFAULT = new Configuration();
                   //CFG_DEFAULT.Configure("MSSQL.cfg.xml");
                   
                   Assembly ass= Assembly.Load("ZDSL.Biz");
                   string connectString = GetDbConnectionString();
                    
                   string cfgName = string.Format("ZDSL.Biz.Cfg.{0}.cfg.xml",GetDbType());
          
                    CFG_DEFAULT
                   .Configure(ass, cfgName)
                   .SetProperty("connection.connection_string",connectString);
                   
               }
           }

           private static void CreateSessionFactory() {
               if (SESSION_FACTORY == null) {
                   CreateCfg();
                   SESSION_FACTORY = CFG_DEFAULT.BuildSessionFactory();
               }
           }

           public static ISession CreateSession() {
               CreateSessionFactory();
               return SESSION_FACTORY.OpenSession();
           }

           public static ICriteria CreateCriteria<T>()
           {
               
               return CreateSession().CreateCriteria(typeof(T));
           }

           public static ICriteria CreateCriteria<T>(PagerObject pager)
           {
               ICriteria icr = CreateCriteria<T>();
               icr.SetFirstResult(pager.startIndex);
               icr.SetMaxResults(pager.size);
               return icr;
           }

           public static IQuery CreateQuery(string hql) {
               return CreateSession().CreateQuery(hql);
           }

           public static ISQLQuery CreateSQLQuery(string sql) {
               return CreateSession().CreateSQLQuery(sql);
           }

           public static IQuery GetNameQuery(string name)
           {
               return CreateSession().GetNamedQuery(name);
           }


           protected BaseZdBiz(){
               CreateSessionFactory();
             
           }


           public static  JsResultObject Save(BaseModel  obj)
           {
               return Save(obj, "");
           }

           public static JsResultObject Save(BaseModel obj, string proName)
           {
               JsResultObject result = new JsResultObject();
               using (ISession ise = DataBiz.CreateSession())
               {
                   try
                   {
                       ITransaction itx = ise.BeginTransaction();
                       ise.Save(obj);
                       itx.Commit();
                       result = ResultUtil.SaveSuccess(1, proName);
                       result.rowNum += 1;
                   }
                   catch (Exception ex)
                   {
                       result = ResultUtil.SaveError("",ex.InnerException.ToString());
                   }

               }
               return result;

           }

           public static JsResultObject SaveOrUpdate(BaseModel obj, string proName)
           {
               JsResultObject result = new JsResultObject();
               using (ISession ise = DataBiz.CreateSession())
               {
                   try
                   {
                       ITransaction itx = ise.BeginTransaction();
                       ise.SaveOrUpdate(obj);
                       itx.Commit();
                       result = ResultUtil.SaveSuccess(1, proName);
                       result.rowNum += 1;
                   }
                   catch (Exception ex)
                   {
                       result = ResultUtil.SaveError("", ex.InnerException.ToString());
                   }

               }
               return result;

           }

           public static JsResultObject Update(BaseModel  obj, string proName)
           {
               JsResultObject result = new JsResultObject();
               using (ISession ise = DataBiz.CreateSession())
               {
                   try
                   {
                       ITransaction itx = ise.BeginTransaction();
                       ise.Update(obj);
                       itx.Commit();
                       result = ResultUtil.SaveSuccess(1, proName);
                       result.rowNum += 1;
                   }
                   catch (Exception ex)
                   {
                       result = ResultUtil.SaveError("", ex.InnerException.ToString());
                   }

               }
               return result;

           }

           public static JsResultObject Delete(BaseModel model,string proName) {
                JsResultObject result = new JsResultObject();
               int re = 0;
               using (ISession ise = DataBiz.CreateSession())
               {
                   try
                   {
                       ITransaction itx = ise.BeginTransaction();
                       ise.Delete(model);
                       re++;
                       itx.Commit();
                       result = ResultUtil.RemoveSuccess(re, proName);
                       result.rowNum += 1;
                   }
                   catch (Exception ex)
                   {
                       result = ResultUtil.RemoveError("",ex.InnerException.ToString());
                   }
               }
               return result;
           }

           public static JsResultObject Activate(BaseModel model, string proName)
           {
               model.status = BaseModel.STATUS_ACTIVATE;
               return Update(model, proName);
           }

           public static JsResultObject Deactivate(BaseModel model, string proName)
           {
               model.status = BaseModel.STATUS_DEACTIVATE;
               return Update(model, proName);
           }

           public static JsResultObject Remove<T>(string[] ids)
           {
               return Remove<T>(ids, "");
           }

           public static JsResultObject Remove<T>(string[] arrayIds, string proName)
           {
               JsResultObject result = new JsResultObject();
               int re = 0;
               using (ISession ise = DataBiz.CreateSession())
               {
                   try
                   {
                       ITransaction itx = ise.BeginTransaction();
                      
                       foreach (object id in arrayIds)
                       {
                           try
                           {
                               object temp = ise.Load(typeof(T), id);
                               ise.Delete(temp);
                               re++;
                               result.rowNum += 1;
                           }
                           catch (Exception ex) { }
                       }
                       itx.Commit();
                       result = ResultUtil.RemoveSuccess(re, proName);
                   }
                   catch (Exception ex)
                   {
                       result = ResultUtil.RemoveError("", ex.InnerException.ToString());
                   }
               }
               return result;
           }


           public static JsResultObject Remove<T>(int[] ids)
           {
               return Remove<T>(ids, "");
           }

           public static JsResultObject Remove<T>(int[] arrayIds, string proName)
           {
               JsResultObject result = new JsResultObject();
               int re = 0;
               using (ISession ise = DataBiz.CreateSession())
               {
                   try
                   {
                       ITransaction itx = ise.BeginTransaction();

                       foreach (object id in arrayIds)
                       {
                           try
                           {
                               object temp = ise.Load(typeof(T), id);
                               ise.Delete(temp);
                               re++;
                               result.rowNum += 1;
                           }
                           catch (Exception ex) { }
                       }
                       itx.Commit();
                       result = ResultUtil.RemoveSuccess(re, proName);
                   }
                   catch (Exception ex)
                   {
                       result = ResultUtil.RemoveError("", ex.InnerException.ToString());
                   }
               }
               return result;
           }

          

           public static T Load<T>(object pk) {
               T obj = default(T);
               using (ISession ise = CreateSession())
               {
                   try
                   {
                       obj = ise.Load<T>(pk);
                   }
                   catch (Exception ex)
                   {
                       return default(T);
                   }
               }
               return obj;
           }

           public static T Load<T>(params ICriterion[] conditions) {
               IList<T> list = List<T>(conditions);
               if (list.Count > 0)
               {
                   return list[0];
               }
               else {
                   return default(T);
               }
           }

           public static IList<T> List<T>(params ICriterion[] conditions) { 
              ICriteria icr=BaseZdBiz.CreateCriteria<T>();
              foreach (ICriterion condition in conditions) {
                  icr.Add(condition);
              }
              IList<T> list = icr.List<T>();
              return list;
           }

           public static int Count(ICriteria icr,string column) {
               int count = 0;
               try
               {
                   count = icr.SetProjection(Projections.Count(column)).UniqueResult<int>();
               }
               catch (Exception ex) { }
               return count;
           }

           public static int CountDistinct(ICriteria icr, string column)
           {
               int count = 0;
               try
               {
                   count = icr.SetProjection(Projections.CountDistinct(column)).UniqueResult<int>();
               }
               catch (Exception ex) { }
               return count;
           }


           protected void OpenSession() {
               session = SESSION_FACTORY.OpenSession();
           }

           protected void CloseSession() {
               session.Clear();
               session.Close();
           }



           public static string GetDbType() {
               return ObjectUtil.Parse(System.Configuration.ConfigurationManager.AppSettings["ZDSL.Biz.db.type"], DB_TYPE_DEFAULT);
           }

           public static string GetDbConnectionString() {
               if (System.Configuration.ConfigurationManager.ConnectionStrings["ZDSL.Biz.db.connectionString"] == null) {
                   return CONNECT_STRING_DEFAULT;
               }
               return ObjectUtil.Parse(System.Configuration.ConfigurationManager.ConnectionStrings["ZDSL.Biz.db.connectionString"].ConnectionString
                       , CONNECT_STRING_DEFAULT);
           }


           public static string GetPathJsCache() {
               return ObjectUtil.Parse(System.Configuration.ConfigurationManager.AppSettings["ZDSL.Biz.path.jsCache"], PATH_JS_CAHCE);

           }


           public static string GetPathImg()
           {
               return ObjectUtil.Parse(System.Configuration.ConfigurationManager.AppSettings["ZDSL.Biz.path.image"], PATH_JS_CAHCE);

           }
    }
}
