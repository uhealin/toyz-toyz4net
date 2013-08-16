using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toyz4net.Core.Model
{

    public interface ICacheAble {

        

         void setTimeout(double timeout);
         bool isTimeout();
         void renewal();
    }

    public abstract class BaseCacheModel:BaseModel,ICacheAble
    {
        public readonly static double TIMEOUT_DEFAULT = 100000;

        public BaseCacheModel()
        {

            this.timeout = TIMEOUT_DEFAULT;
            this.createTime = DateTime.Now;
        }

        public BaseCacheModel(double timeout)
        {

            this.timeout = timeout;
            this.createTime = DateTime.Now;
        }

        public DateTime createTime { get; private set; }
        public double timeout { get; set; }


        public void setTimeout(double timeout)
        {
            this.timeout = timeout;
        }

        public bool isTimeout()
        {
            return (createTime.AddMilliseconds(timeout).Millisecond) >= (DateTime.Now.Millisecond);
        }

        public void renewal() {
            this.createTime = DateTime.Now;
        }

        
    }


}
