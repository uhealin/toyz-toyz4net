using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Toyz4net.Core.Util;

namespace Toyz4net.Core.Client
{
     public abstract  class BaseClient
    {
         
         protected static Dictionary<string, XmlDocument> CACHE_DOC = new Dictionary<string, XmlDocument>();


         protected static XmlDocument GetDoc(string path)
         {
             XmlDocument doc = new XmlDocument();
             if (CACHE_DOC.ContainsKey(path))
             {
                 doc = CACHE_DOC[path];
             }
             else
             {
                 string context = NetClientUtil.doGet(path);
                 doc.LoadXml(context);
                 CACHE_DOC.Add(path, doc);
             }
             return doc;
         }

    }
}
