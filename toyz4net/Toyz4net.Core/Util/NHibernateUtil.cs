using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using System.Reflection;
using NHibernate;
using Toyz4net.Core.Model;
using NHibernate.Criterion;

namespace Toyz4net.Core.Util
{
   public  class NHibernateHelper
    {

           private static Configuration CFG_DEFAULT;
           private static ISessionFactory SESSION_FACTORY;
           protected ISession session;
           
        
           public static void CreateCfg(Assembly ass,string cfgName ){
                   //CFG_DEFAULT.Configure("MSSQL.cfg.xml");     
                    CFG_DEFAULT = new Configuration();
                    CFG_DEFAULT
                   .Configure(ass, cfgName);
               
           }

           public static void CreateCfg(Assembly ass, string cfgName,string connectString)
           {
               //CFG_DEFAULT.Configure("MSSQL.cfg.xml");     
               CFG_DEFAULT = new Configuration();
               CFG_DEFAULT
              .Configure(ass, cfgName)
              .SetProperty("connection.connection_string", connectString);
              ;

           }

           public static void CreateSessionFactory() {
                 SESSION_FACTORY = CFG_DEFAULT.BuildSessionFactory();
               
           }

           public static ISession CreateSession() {
              
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


          


           public static  JsResultObject Save(object  obj)
           {
               return Save(obj, "");
           }

           public static JsResultObject Save(object obj, string proName)
           {
               JsResultObject result = new JsResultObject();
               using (ISession ise = CreateSession())
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

           public static JsResultObject SaveOrUpdate(object obj, string proName)
           {
               JsResultObject result = new JsResultObject();
               using (ISession ise = CreateSession())
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

           public static JsResultObject Update(object  obj, string proName)
           {
               JsResultObject result = new JsResultObject();
               using (ISession ise = CreateSession())
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

           public static JsResultObject Delete(object model,string proName) {
                JsResultObject result = new JsResultObject();
               int re = 0;
               using (ISession ise = CreateSession())
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
               using (ISession ise = CreateSession())
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
               using (ISession ise = CreateSession())
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
              ICriteria icr=CreateCriteria<T>();
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


    }
}
