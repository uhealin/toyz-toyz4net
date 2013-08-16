using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Model;

namespace ZDSL.Model.Public
{
    public  class FrontPageModel :BaseModel
    {

        public  string id{get;set;}
        public  string hotelHotCityNameArray{get;set;}
        public  string homeHotCityNameArray{get;set;}
        public string homeHotBrandNameArray { get; set; }
        public  string searchBrandNameArray{get;set;}

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
