using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;


namespace ZDSL.Biz
{
    public class LogBiz<T> :BaseZdBiz
    {

        private ILog logger;

        private LogBiz() {
            logger = log4net.LogManager.GetLogger(typeof(T));
            
        }


        
    }
}
