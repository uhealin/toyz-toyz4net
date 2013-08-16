using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Model;

namespace ZDSL.Model.Public
{
    public  class AdSiderModel:BaseModel
    {


        public string id{get;set;}
        public string src{get;set;}
        public string alt{get;set;}
        public string title{get;set;}
        public int recLevel{get;set;}
        public string  href { get; set; }


        public override object getPk()
        {
            return this.id;
        }

        public override void setPk(object obj)
        {
            this.id = obj.ToString();
        }
    }
}
