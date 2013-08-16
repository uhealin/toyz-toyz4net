using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Runtime.Serialization.Json;
using System.Collections.Specialized;
using System.Collections;
using JSONSharp;
using JSONSharp.Collections;
using JSONSharp.Values;
using NHibernate.Criterion;

namespace Toyz4net.Core.Util
{
    public static  class ObjectUtil
    {

        public static int RESULT_ALL_NULL = 0;
        public static int RESULT_SOME_NULL = 1;
        public static int RESULT_NO_NULL = 2;

        public static int ParseInt(string val, int defaultVal) {
            int returnVal = defaultVal;
            try
            {
                returnVal = Int32.Parse(val);
            }
            catch (Exception ex) {
                returnVal = defaultVal;
            }

            return returnVal;
        }



        public static double ParseDouble(string val, double defaultVal)
        {
            double returnVal = defaultVal;
            try
            {
                returnVal = Double.Parse(val);
            }
            catch (Exception ex)
            {
                returnVal = defaultVal;
            }

            return returnVal;
        }

        public static float  ParseFloat(string val, float defaultVal)
        {
            float returnVal = defaultVal;
            try
            {
                returnVal = float.Parse(val);
            }
            catch (Exception ex)
            {
                returnVal = defaultVal;
            }

            return returnVal;
        }

        public static DateTime ParseDateTime(string val, DateTime defaultVal) {
            DateTime returnVal = defaultVal;
            try
            {
                DateTime.Parse(val);
            }
            catch (Exception ex) {
                returnVal = defaultVal;
            }
            return returnVal;
        }

        public static string Parse(object val, string defaultVal) {
            if (val == null)
            {
                return defaultVal;
            }
            else {
                return val.ToString();
            }
        }



        public static T Eval<T>(T obj, XmlNode node,XmlNamespaceManager xnm, string prefix,string subfix)
        {
            Type type = obj.GetType();
            FieldInfo[] fields = GetAllFields(type);
            foreach (FieldInfo field in fields) {
                string query = prefix + GetSimpleName(field) + subfix;
                try
                {
                    string value = null;
                    if (xnm != null)
                    {
                        value = node.SelectSingleNode(query, xnm).InnerText;
                    }
                    else {
                        value = node.SelectSingleNode(query).InnerText;
                    }
                    field.SetValue(obj, value);
                }
                catch (Exception ex) { }
            }
            return obj;
        }


        public static FieldInfo[] GetAllFields(Type type) {
            IList<FieldInfo> fields = new List<FieldInfo>();
            Type baseType = type;
            while (baseType != typeof(Object)&&baseType != typeof(object)) {
                  FieldInfo[] tempFields = baseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                  foreach (FieldInfo tempField in tempFields) {
                      fields.Add(tempField);
                  }
                  baseType = baseType.BaseType;
            }

            return fields.ToArray<FieldInfo>();
        }



        public static T Eval<T>(T obj, NameValueCollection nvc, string prefix, string subfix)
        {
            Type type = obj.GetType();
            FieldInfo[] fields = GetAllFields(type);
            foreach (FieldInfo field in fields)
            {
                string query = prefix + GetSimpleName(field) + subfix;
                try
                {
                    string value = nvc[query];
                    if(value==null)continue;
                    Eval(ref obj, field, value);
                }
                catch (Exception ex) { }
            }
            return obj;
        }



        public static void Eval<T>(ref T obj, FieldInfo field, string value)
        {
            Type type = field.FieldType;
            object setValue = null;
            if (type == typeof(String))
            {
                setValue = value;
            }
            else if (type == typeof(Double) || type == typeof(double))
            {
                setValue = Double.Parse(value);
            }
            else if (type == typeof(float) )
            {
                setValue = float.Parse(value);
            }
            else if (type == typeof(Int32) || type == typeof(int))
            {
                setValue = Int32.Parse(value);
            }
            else if (type == typeof(DateTime))
            {
                setValue = DateTimeUtil.ParseDateTime(value);
            }

            field.SetValue(obj, setValue);
        }

        public static string GetSimpleName(FieldInfo field) {
            string sn = field.Name;
            int startIndex = sn.IndexOf('<')+1;
            int endIndex = sn.IndexOf('>')-startIndex;
            sn = sn.Substring(startIndex,endIndex);
            return sn;
        }






        public static bool IsEqual(object obj1, object obj2) {

            if (obj1 == null && obj2 == null) {
                return true;
            }
            else if ((obj1 == null && obj2 != null) || (obj1 != null && obj2 == null))
            {
                return false;
            }
            else {
                return obj1.Equals(obj2);
            }
            
        }

        public static bool IsNotEqual(object obj1, object obj2) {
            return !IsEqual(obj1, obj2);
        }


        public static bool IsList<T>(object obj)  {
            if(obj==null)return false;
            return  obj is IList
                    || obj is ArrayList
                    || obj is List<T> 
                    || obj is LinkedList<T>
                ;
        }

        public static bool IsDictionary<K, V>(object obj) 
        {
            if (obj == null) return false;
            Type type = obj.GetType();
            return typeof(Dictionary<K, V>).Equals(type);
        }


        public static int IsAllNullOrEmpty(string[] strs) {
            int re = 0;
            int noNullCount = 0;
            foreach (string str in strs) {
                if (!string.IsNullOrEmpty(str)) {

                    noNullCount++;
                }
            }
            if (noNullCount == 0) {
                re=RESULT_ALL_NULL;
            }
            else if (noNullCount == strs.Length)
            {
                re=RESULT_NO_NULL;
            }
            else {
                re = RESULT_SOME_NULL;
            }
            return re;
        }

        public static string ToUnionString(object[] array,char sparChar) {
             StringBuilder sbr =new StringBuilder("");
            if (array != null) {
                foreach (object obj in array) {
                    sbr.Append(obj.ToString()).Append(sparChar);
                }
            }
            return sbr.ToString().Trim(sparChar);
        }


        //判断是否日期查询格式
        public static bool IsDateQuery(string val)
        {
            return (val.StartsWith("[") && val.EndsWith("]"));
        }

        //判断是否数字查询格式
        public static bool IsNumberQuery(string val)
        {
            return (val.StartsWith("(") && val.EndsWith(")"));
        }



        public static IList<string> GetProList<T>(IList<T> list, string proName) {
            IList<string> vals = new List<string>();
            FieldInfo[] fields = GetAllFields(typeof(T));
            FieldInfo field = null;
            foreach (FieldInfo tempField in fields) {
                if (proName == GetSimpleName(tempField)) {
                    field = tempField;
                    break;
                 }
            }
            if (field == null) return vals;
            foreach(object obj in list){
                object val = field.GetValue(obj);
                if (val == null) continue;
                vals.Add(val.ToString());
            }
            return vals;
        }

    }

    public interface IToyzObjcet
    {

        ICriterion createCriterion(string proName);
    }
}
