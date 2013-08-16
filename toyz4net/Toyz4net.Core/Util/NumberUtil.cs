using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toyz4net.Core.Util
{
    public  class NumberUtil
    {

        public static string Format(object f,int precision){
            string pattern = "";
            if (precision > 0)
            {
                pattern = "{0:000.";
            }
            else {
                pattern = "{0:000";
            }
             
            for(int p=0;p<precision;p++){
                pattern+="0";
            }
            pattern += "}";
            return string.Format(pattern,f);
        }


        //解析字符串，返回一个IToyzDateTimeObjcet接口的对象
        public static IToyzObjcet ParseToyz(string regexp) {
            IToyzObjcet toyz = null;
            if(string.IsNullOrEmpty(regexp))return toyz;
           
            //判断查询前序
            if (regexp.StartsWith("between@"))
            {

                string query = regexp.Split('@')[1];    //找出查询条件
                string[] subQuerys = query.Split(',');   //拆分查询条件，获取参数数组
                if (subQuerys.Length == 2)
                {
                    string subQuery0 = subQuerys[0];
                    string subQuery1 = subQuerys[1];
                    if (ObjectUtil.IsNumberQuery(subQuery0) && ObjectUtil.IsNumberQuery(subQuery1))
                    {
                        int min = int.Parse(subQuery0.Trim('(', ')'));
                        int max = int.Parse(subQuery1.Trim('(', ')'));
                        toyz = new ToyzNumberRangeObject(min, max);
                    }
                }
            }
            else if (regexp.StartsWith("gt@") || regexp.StartsWith("lt@"))
            {
                string query = regexp.Split('@')[1];    //找出查询条件
                string[] subQuerys = query.Split(',');   //拆分查询条件，获取参数数组
                if (subQuerys.Length ==1)
                {
                    string subQuery0 = subQuerys[0];
                    //string subQuery1 = subQuerys[1];
                    if (ObjectUtil.IsNumberQuery(subQuery0))
                    {
                        int min = regexp.StartsWith("lt@")?int.MinValue:int.Parse(subQuery0.Trim('(', ')'));
                        int max = regexp.StartsWith("gt@") ? int.MaxValue:int.Parse(subQuery0.Trim('(', ')'));
                        toyz = new ToyzNumberRangeObject(min, max);
                    }
                }
            }


            return toyz;

        }
    }


    public class ToyzNumberRangeObject:IToyzObjcet{
        public int min { get;private set; }
        public int max { get;private set; }

    

        public ToyzNumberRangeObject(int min, int max) {
            this.min = min;
            this.max = max;
        }

        public bool isBetween(int num) {
            return num >= min && num <= max;
        }

        #region IToyzObjcet 成员

        public NHibernate.Criterion.ICriterion createCriterion(string proName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
