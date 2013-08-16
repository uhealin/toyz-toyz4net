using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using System.Runtime.Serialization;
using Toyz4net.Core.Util;
using System.Threading;

namespace Toyz4net.Core.Model
{
    [Serializable]
    public abstract class BaseModel:IQueryAble
    {

        public static string IND_Y = "Y";
        public static string IND_N = "N";

        public static string STATUS_DELETE = "del";
        public static string STATUS_DEACTIVATE = "dct";
        public static string STATUS_ACTIVATE = "act";
        

        public string status { get; set; } 

        public BaseModel() {
            this.status = STATUS_ACTIVATE;
        }

        public object createPk() {
            int rangdomNum=new Random().Next(1000,9999);
            string pk = string.Format("{0}{1}"
                , DateTime.Now.ToString("yyyyMMddHHmmss")
                ,rangdomNum.ToString()
            );
            Thread.Sleep(50);
            return pk;
        }

        public abstract object getPk();

        public abstract void setPk(object obj);

        public static ICriterion StatusEqActivate()
        {
            return Restrictions.Eq("status", STATUS_ACTIVATE);
        }

        public static ICriterion StatusEqDeactivate()
        {
            return Restrictions.Eq("status", STATUS_DEACTIVATE);
        }

        public static ICriterion StatusEqDelete()
        {
            return Restrictions.Eq("status", STATUS_DELETE);
        } 

        #region IQueryAble 成员

        public  void  setOrderBy(ref ICriteria icr)
        {
            
        }

        #endregion
    }




    public interface IQueryAble {
        void setOrderBy(ref ICriteria icr);
    }


}
