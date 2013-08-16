using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSONSharp;
using JSONSharp.Collections;
using JSONSharp.Values;
using System.Collections;
using NHibernate.Mapping;

namespace Toyz4net.Core.Util
{
    public static   class EasyuiUtil
    {

        public static string ToComboboxData(Dictionary<string, string> dict) {
            StringBuilder sbr = new StringBuilder("");
            foreach (string key in dict.Keys) {
                sbr
                .Append("{")
                .Append(string.Format("text:'{0}',value:'{1}'",dict[key],key))
                .Append("},")
                ;
            }
            return sbr.ToString().TrimEnd(',');
        }

        public static string ToFormatter(Dictionary<string, string> dict) {
            StringBuilder sbr = new StringBuilder("");
            sbr.Append("function(value, rowData, rowIndex){");
            foreach (string key in dict.Keys)
            {
                sbr
                .Append("{")
                .Append(string.Format("text:{0},value:{1}", dict[key], key))
                .Append("},")
                ;
            }
            sbr.Append("}");
            return sbr.ToString().TrimEnd(',');
        }

    }

    public class TreeNodeObject
    {

        public string id { get; set; }
        public string text { get; set; }
        public string iconCls { get; set; }
        public string state { get; set; }

        public Dictionary<string, object> attributes = new Dictionary<string, object>();

        public IList<TreeNodeObject> children { get; set; }

    }


    public class DatagridObject
    {
        private DatagridObject() {
            this.total = 0;
            this.rows = new ArrayList();
        }


  



        public int total {private set; get; }
        public IList rows {private set; get; }

        public JSONObjectCollection toJSONObjectCollection() {
            JSONObjectCollection json = new JSONObjectCollection();
            json.Add(new JSONStringValue("total"), new JSONNumberValue(this.total));
            json.Add(new JSONStringValue("rows"), JsonUtil.toJSONArrayCollection(this.rows));
            return json;
        }


        public static DatagridObject NewIntanst() {
            DatagridObject datagrid = new DatagridObject();
            datagrid.total = 0;
            datagrid.rows = new ArrayList();
            return datagrid;
        }

        public static DatagridObject ToDatagridObject(IList rows)
        {
            DatagridObject datagrid = new DatagridObject();
            datagrid.total = rows.Count;
            datagrid.rows = rows;
            return datagrid;
        }

        public static DatagridObject ToDatagridObject<T>(IList<T> rows)
        {
            DatagridObject datagrid = new DatagridObject();
            datagrid.total = rows.Count;
            datagrid.rows = rows as IList;
            return datagrid;
        }


        public static DatagridObject ToDatagridObject(IList rows, int total)
        {
            DatagridObject datagrid = new DatagridObject();
            datagrid.total = total;
            datagrid.rows = rows;
            return datagrid;
        }

        public static DatagridObject ToDatagridObject<T>(IList<T> rows, int total)
        {
            DatagridObject datagrid = new DatagridObject();
            datagrid.total = total;
            if (rows != null) {
                datagrid.rows = rows as IList;
            }
            return datagrid;
        }


        public static DatagridObject ToDatagridObject<T>(PageList<T> pagerList)
        {
            DatagridObject datagrid = new DatagridObject();
            if (pagerList != null)
            {
                datagrid.total = pagerList.total;
                datagrid.rows = pagerList as IList;
            }
            return datagrid;
        }

    }
}
