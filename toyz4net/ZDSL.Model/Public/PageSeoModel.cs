using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Model;

namespace ZDSL.Model.Public
{
    public  class PageSeoModel:BaseModel
    {

        public string id{get;set;}
        public string homeIndexTitle{get;set;}
        public string homeIndexDesc{get;set;}
        public string homeIndexKeywords{get;set;}
        public string hotelIndexTitle{get;set;}
        public string hotelIndexDesc{get;set;}
        public string hotelIndexKeywords{get;set;}
        public string hotelDetailTitle{get;set;}
        public string hotelDetailDesc{get;set;}
        public string hotelDetailKeywords{get;set;}
        public string brandIndexTitle{get;set;}
        public string brandIndexDesc{get;set;}
        public string brandIndexKeywords{get;set;}
        public string brandHotelTitle{get;set;}
        public string brandHotelDesc{get;set;}
        public string brandHotelKeywords{get;set;}
        public string newsIndexTitle{get;set;}
        public string newsIndexDesc{get;set;}
        public string newsIndexKeywords{get;set;}
        public string newsHotelTitle{get;set;}
        public string newsHotelDesc{get;set;}
        public string newsHotelKeywords{get;set;}
        public string exhiIndexTitle{get;set;}
        public string exhiIndexDesc{get;set;}
        public string exhiIndexKeywords{get;set;}
        public string exhiHotelTitle{get;set;}
        public string exhiHotelDesc{get;set;}
        public string exhiHotelKeywords{get;set;}
        public string cityTitle{get;set;}
        public string cityDesc{get;set;}
        public string cityKeywords{get;set;}
        public string cityDTitle{get;set;}
        public string cityDDesc{get;set;}
        public string cityDKeywords{get;set;}
        public string cityClTitle{get;set;}
        public string cityClDesc{get;set;}
        public string cityClKeywords{get;set;}
        public string hotelSearchTitle{get;set;}
        public string hotelSearchDesc{get;set;}
        public string hotelSearchKeywords{get;set;}
        public string commentIndexTitle { get; set; }
        public string commentIndexDesc { get; set; }
        public string commentIndexKeywords { get; set; }


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
