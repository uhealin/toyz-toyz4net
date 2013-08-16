using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Toyz4net.Core.Util
{
    public  class StringUtil
    {

        public static string Ignore(string value, int len) {
            if (string.IsNullOrEmpty(value)) {
                return "";
            }
            else if (value.Length <= len) {
                return value;
            }else {
                return value.Substring(0, len);
            }
        }


        public static IList<int> ToIntArray(string[] valArray) {

            IList<int> intArray = new List<int>();
            foreach (string val in valArray) {
                try
                {
                    intArray.Add(Convert.ToInt32(val));
                }
                catch (Exception ex) { }
            }
            return intArray;
        }

        public static string UnionArray(string[] vals,char spar) {

            string returnVal = "";
            foreach (string val in vals) {
                returnVal += val + spar;
            }
            return returnVal.TrimEnd(spar);
        
        }


        public static string UrlDecode(string str) {
            return HttpUtility.UrlDecode(str);
        }


        public static bool IsIn(string value, string[] values) {
            bool isIn = false;
            foreach (string tv in values) {
                if (tv == value) {
                    isIn = true;
                    break;
                }
            }
            return isIn;
        }

        public static bool IsNotIn(string value, string[] values) {
            return !IsIn(value, values);
        }
    }
}
