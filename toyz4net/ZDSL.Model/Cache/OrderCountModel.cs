using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Model;

namespace ZDSL.Model.Cache
{
    public  class OrderCountModel :BaseModel
    {

        public string id{get;set;}
        public int allCount{get;set;}
        public int newCount{get;set;}
        public int failCount{get;set;}
        public int cancelCount{get;set;}
        public int dealCount{get;set;}
        public int zdSuccessCount{get;set;}
        public int elongSuccessCount{get;set;}
        public int commentedCount{get;set;}

        public override object getPk()
        {
            throw new NotImplementedException();
        }

        public override void setPk(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
