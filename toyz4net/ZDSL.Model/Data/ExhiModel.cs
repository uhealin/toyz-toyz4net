using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Model;

namespace ZDSL.Model.Data
{
    public  class ExhiModel: BaseModel
    {
        public static string EXHI_STATUS_DEPLOY = "deploy";
        public static string EXHI_STATUS_UNDEPLOY = "undeploy";

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

        public string name
        {
            get;
            set;
        }

        public string geoFk
        {
            get;
            set;
        }

        public string msg
        {
            get;
            set;
        }

        public DateTime startDate
        {
            get;
            set;
        }

        public DateTime endDate
        {
            get;
            set;
        }

        public string exhiStatus { get; set; }

        public string hotelIdArray { get; set; }

        public string address { get; set; }

        public string busName { get; set; }

        public string siteName { get; set; }
   }




    public class ExhiRefHotelModel : BaseModel
    {

        public string id { get; set; }
        public string exhiId { get; set; }
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