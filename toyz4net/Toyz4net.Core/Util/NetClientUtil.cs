using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Toyz4net.Core.Util
{
    public class NetClientUtil
    {

        public static string doGet(string url) {
              
              return doGet(url,Encoding.UTF8);
        } 

        public static string doGet(string url,Encoding encoding){
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader sr = new StreamReader(response.GetResponseStream(),encoding);
            string context = sr.ReadToEnd();
            sr.Close();
            return context;
        }


        public static string doPost(string url,string param)
        {

            return doPost(url,param ,Encoding.UTF8);
        }

        public static string doPost(string url,string param, Encoding encoding)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentLength = param.Length;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            

            byte[] postdata = encoding.GetBytes(param);
            Stream postStream = request.GetRequestStream();
            postStream.Write(postdata,0,postdata.Length);
            StreamReader sr = new StreamReader(postStream, encoding);
            string context = sr.ReadToEnd();
            sr.Close();
            return context;
        }

    }
}
