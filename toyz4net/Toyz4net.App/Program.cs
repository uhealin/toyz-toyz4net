using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Toyz4net.Core.Util;
using System.Reflection;
using System.Data.SqlClient;

namespace Toyz4net.App
{
    static class Program
    {


        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            Assembly ass = Assembly.Load("Toyz4net.App");
            String cfgName = "Toyz4net.App.SQLite.cfg.xml";
            NHibernateHelper.CreateCfg(ass, cfgName, Gobal.LOCAL_DB_CS);
            NHibernateHelper.CreateSessionFactory();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            

        }



 
        
    }

    public class Gobal {

        public static string LOCAL_DB_CS = string.Format("Data Source={0}\\local.db;Version=3", System.Environment.CurrentDirectory);

        public static string GB2312(String val)
        {
            return System.Web.HttpUtility.UrlEncode(val, System.Text.Encoding.GetEncoding("GB2312")).ToUpper().Replace("(", "%28").Replace(")", "%29");
        }


    }

}
