using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Model;

namespace ZDSL.Model.Data
{
    public class NewsModel:BaseModel
    {

        public static string NEWS_STATUS_DEPLOY = "deploy";
        public static string NEWS_STATUS_UNDEPLOY = "undeploy";


        public override object getPk()
        {
            return this.id;
        }

        public override void setPk(object pk)
        {
            this.id = pk.ToString();
        }

        public string id
        {
            get;
            set;
        }

        public string title
        {
            get;
            set;
        }

        public string context
        {
            get;
            set;
        }

        public DateTime deployDate
        {
            get;
            set;
        }

        public DateTime modifyDate
        {
            get;
            set;
        }

        public string deployerId
        {
            get;
            set;
        }

        public string type { get; set; }

        public string newsStatus
        {
            get;
            set;
        }

        public string hotelIdArray { get; set; }

    }


    public class NewsRefHotelModel :BaseModel{

        public string id { get; set; }
        public string newsId { get; set; }
        public string hotelId { get; set; }

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
