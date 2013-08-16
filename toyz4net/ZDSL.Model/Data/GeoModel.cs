using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Util;
using Toyz4net.Core.Client;
using Toyz4net.Core.Model;

namespace ZDSL.Model.Data
{
    public class GeoModel:BaseModel
    {
        public string id { get; set; }	//	Geoid,从1开始，依次加1
        public string country { get; set; }	//	国家
        public string provinceName { get; set; }	//	省份名称
        public string provinceId { get; set; }	//	省份ID
        public string cityName { get; set; }	//	城市名称
        public string cityCode { get; set; }	//	城市ID
        public int properties { get; set; }	//	该城市下的有效酒店个数 
        public string url { get; set; }	//	该酒店在www.elong.com网站上的搜索列表URL
        public string seoKeyword { get; set; }
        public string recInd { get; set; }
        public int recLevel { get; set; }

        public IList<GeoCommercialLocationModel> geoCls { get; set; }
        public IList<GeoDistrictsModel> geoDs { get; set; }
        public IList<GeoLandmarkLocationModel> geoLls { get; set; }

        public override object getPk()
        {
            return this.id;
        }

        public override void setPk(object pk)
        {
            this.id = pk.ToString();
        }


        public void from(ELongGeoAdapter geo)
        {
            this.id = geo.id;
            this.country = geo.country;
            this.provinceName = geo.provinceName;
            this.provinceId = geo.provinceId;
            this.cityCode = geo.cityCode;
            this.cityName = geo.cityName;
            this.properties = ObjectUtil.ParseInt(geo.properties,0);
            this.url = geo.url;
        }
    }


    public abstract class GeoLocationModel:BaseModel
    {
        public string id { get; set; }	//ID
        public string name { get; set; }	 //名称
        public string geoFk { get; set; }
        public string locationId { get; set; }

        public override object getPk()
        {
            return this.id;
        }

        public override void setPk(object pk)
        {
            this.id = pk.ToString();
        }

        public void from(ELongGeoLocationAdapter adapter)
        {
            this.id = string.Format("{0}::{1}", adapter.exGeoId, adapter.id);
            this.name = adapter.name;
            this.locationId = adapter.id;
            this.geoFk = adapter.exGeoId;
        }
    }

    public class GeoDistrictsModel : GeoLocationModel { };
    public class GeoCommercialLocationModel : GeoLocationModel { };
    public class GeoLandmarkLocationModel : GeoLocationModel { };
}
