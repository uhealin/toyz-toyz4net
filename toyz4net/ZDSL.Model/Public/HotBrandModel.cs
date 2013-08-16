using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Model;

namespace ZDSL.Model.Public
{
    public  class HotBrandModel :BaseHotModel
    {

        public string id { get; set; }
        public string name { get; set; }


        public override object getPk()
        {
            return id;
        }

        public override void setPk(object obj)
        {
            id = obj.ToString();
        }
    }
}
