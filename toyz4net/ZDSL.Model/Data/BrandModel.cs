using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Util;
using Toyz4net.Core.Client;
using Toyz4net.Core.Model;
using NHibernate.Criterion;
using NHibernate;

namespace ZDSL.Model.Data
{
    public class BrandModel:BaseModel
    {

        public static string TYPE_CLASS = "class";
        public static string TYPE_ECONMY = "economy";


        private static Dictionary<string, string> DictType;

        public static Dictionary<string, string> GetDictType() {

            if (DictType == null) {

                DictType = new Dictionary<string, string>();
                DictType.Add(TYPE_CLASS, "品牌");
                DictType.Add(TYPE_ECONMY, "经济型");
            }
            return DictType;
        
        }

        public string brandID { get; set; }	//连锁品牌ID
        public string groupID { get; set; }	//	连锁品牌所属的酒店集团ID
        public string brandName { get; set; }	//	连锁酒店品牌中文简称
        public string brandNameLong { get; set; }	//	连锁酒店品牌中文全称
        public string brandFirstLetter { get; set; }	//	连锁品牌名称首字母
        public string brandPinYin { get; set; }
        public string picURL { get; set; }	//	连锁品牌图片地址的URL
        public string brandURL { get; set; }	//	连锁品牌网站的URL
        public int hotelCount { get; set; }	//	连锁品牌包含酒店数量
        public DateTime lastChangetime { get; set; }	//	最后的修改时间
        public string type { get; set; }
        public string recInd { get; set; }
        public int recLevel { get; set; }


        public override object getPk()
        {
            return this.brandID;
        }

        public override void setPk(object pk)
        {
            this.brandID = pk.ToString();
        }

        public void setOrderBy(ref ICriteria icr)
        {
            icr.AddOrder(Order.Desc("recInd"));
            icr.AddOrder(Order.Desc("recLevel"));
            icr.AddOrder(Order.Desc("picURL"));
            icr.AddOrder(Order.Desc("brandName"));

        }

        public void from(ELongBrandAdapter brand)
        {
            this.brandID = brand.brandID;
            this.groupID = brand.groupId;
            this.brandName = brand.brandName;
            this.brandNameLong = brand.brandNameLong;
            this.brandFirstLetter = brand.brandFirstletter;
            this.brandPinYin = brand.brandPinYin;
            this.picURL = brand.picURL;
            this.brandURL = brand.brandURL;
            this.hotelCount = ObjectUtil.ParseInt(brand.hotelCount, 0);
            this.lastChangetime = ELongStaticClient.ParseDateTime(brand.lastChangetime) ;
        }
    }
}
