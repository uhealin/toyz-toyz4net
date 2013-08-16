using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;
using Toyz4net.Core.Util;

namespace Toyz4net.Core.Model
{
    public abstract class BaseAdapter
    {

        public static string XML_NULL = "xml_null";
        public static string XML_BLANK = "xml_blank";

        public void from(XmlNode node, XmlNamespaceManager xnm, string prefix, string subfix)
        {
            Type type = this.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

            foreach (FieldInfo field in fields)
            {
                string query = prefix +ObjectUtil.GetSimpleName(field) + subfix;
                try
                {
                    string value = null;
                    XmlNode tempNode;
                    if (xnm != null)
                    {
                        tempNode = node.SelectSingleNode(query, xnm);
                    }
                    else
                    {
                        tempNode = node.SelectSingleNode(query);
                    }
                    if (tempNode == null) {
                        field.SetValue(this,XML_NULL);
                        continue;
                    }
                    value = tempNode.InnerText;
                    field.SetValue(this, value);
                }
                catch (Exception ex) { }
            }
            
        }

        public XmlNode toXmlNode() {
            return null;
        }
    }
}
