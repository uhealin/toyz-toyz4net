using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using System.Reflection;

namespace Toyz4net.Core.Util
{

    /*
     d ShortDatePattern 
D LongDatePattern 
f 完整日期和时间（长日期和短时间） 
F FullDateTimePattern（长日期和长时间） 
g 常规（短日期和短时间） 
G 常规（短日期和长时间） 
m、M MonthDayPattern 
r、R RFC1123Pattern 
s 使用当地时间的 SortableDateTimePattern（基于 ISO 8601） 
t ShortTimePattern 
T LongTimePattern 
u UniversalSortableDateTimePattern 用于显示通用时间的格式 
U 使用通用时间的完整日期和时间（长日期和长时间） 
y、Y YearMonthPattern 

下表列出了可被合并以构造自定义模式的模式。这些模式是区分大小写的；例如，识别“MM”，但不识别“mm”。如果自定义模式包含空白字符或用单引号括起来的字符，则输出字符串页也将包含这些字符。未定义为格式模式的一部分或未定义为格式字符的字符按其原义复制。

格式模式 说明 
d 月中的某一天。一位数的日期没有前导零。 
dd 月中的某一天。一位数的日期有一个前导零。 
ddd 周中某天的缩写名称，在 AbbreviatedDayNames 中定义。 
dddd 周中某天的完整名称，在 DayNames 中定义。 
M 月份数字。一位数的月份没有前导零。 
MM 月份数字。一位数的月份有一个前导零。 
MMM 月份的缩写名称，在 AbbreviatedMonthNames 中定义。 
MMMM 月份的完整名称，在 MonthNames 中定义。 
y 不包含纪元的年份。如果不包含纪元的年份小于 10，则显示不具有前导零的年份。 
yy 不包含纪元的年份。如果不包含纪元的年份小于 10，则显示具有前导零的年份。 
yyyy 包括纪元的四位数的年份。 
gg 时期或纪元。如果要设置格式的日期不具有关联的时期或纪元字符串，则忽略该模式。 
h 12 小时制的小时。一位数的小时数没有前导零。 
hh 12 小时制的小时。一位数的小时数有前导零。 
H 24 小时制的小时。一位数的小时数没有前导零。 
HH 24 小时制的小时。一位数的小时数有前导零。 
m 分钟。一位数的分钟数没有前导零。 
mm 分钟。一位数的分钟数有一个前导零。 
s 秒。一位数的秒数没有前导零。 
ss 秒。一位数的秒数有一个前导零。 
f 秒的小数精度为一位。其余数字被截断。 
ff 秒的小数精度为两位。其余数字被截断。 
fff 秒的小数精度为三位。其余数字被截断。 
ffff 秒的小数精度为四位。其余数字被截断。 
fffff 秒的小数精度为五位。其余数字被截断。 
ffffff 秒的小数精度为六位。其余数字被截断。 
fffffff 秒的小数精度为七位。其余数字被截断。 
t 在 AMDesignator 或 PMDesignator 中定义的 AM/PM 指示项的第一个字符（如果存在）。 
tt 在 AMDesignator 或 PMDesignator 中定义的 AM/PM 指示项（如果存在）。 
z 时区偏移量（“+”或“-”后面仅跟小时）。一位数的小时数没有前导零。例如，太平洋标准时间是“-8”。 
zz 时区偏移量（“+”或“-”后面仅跟小时）。一位数的小时数有前导零。例如，太平洋标准时间是“-08”。 
zzz 完整时区偏移量（“+”或“-”后面跟有小时和分钟）。一位数的小时数和分钟数有前导零。例如，太平洋标准时间是“-08:00”。 
: 在 TimeSeparator 中定义的默认时间分隔符。 
/ 在 DateSeparator 中定义的默认日期分隔符。 
% c 其中 c 是格式模式（如果单独使用）。如果格式模式与原义字符或其他格式模式合并，则可以省略“%”字符。 
\ c 其中 c 是任意字符。照原义显示字符。若要显示反斜杠字符，请使用“\\”。 
     */


    public static   class DateTimeUtil
    {

        private static Dictionary<string, IToyzObjcet> CACHE_TOYZ_DATETIME = new Dictionary<string, IToyzObjcet>();


        public static string PATTERN_DB_DATE = "yyyy-MM-dd";
        public static string PATTERN_DB_DATETIME = "yyyy-MM-dd HH:mm:ss";
        public static string PATTERN_DB_TIME = "HH:mm:ss";

        public static DateTime SYSTEM_DATE_MIN = new DateTime(1970, 1, 1);

        public static DateTime MSSQL_DATE_MIN = new DateTime(1753,1,2);


        public static long getTotalMilliseconds() {

            return  Convert.ToInt64(new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds);
        }

        public static DateTime ParseDateTime(string val) {
            DateTime returnDateTime = MSSQL_DATE_MIN;
            string[] patterns = new string[] {PATTERN_DB_DATE,PATTERN_DB_DATETIME,PATTERN_DB_TIME };
            foreach (string pattern in patterns) {
                returnDateTime = ParseDateTime(val, pattern);
                if (!returnDateTime.Equals(MSSQL_DATE_MIN)) {
                    break;
                }
            }
            return returnDateTime;
        }

        public static DateTime ParseDateTime(string val,string pattern)
        {
            DateTime returnDateTime = MSSQL_DATE_MIN;
            try
            {
                returnDateTime = DateTime.ParseExact(val, pattern, System.Threading.Thread.CurrentThread.CurrentCulture);
            }
            catch (Exception ex) { }
            return returnDateTime;
        }


        public static int CompareDay(DateTime dt1, DateTime dt2) {

            int re = 0;
            if (dt1.Year < dt2.Year) {
                return -1;
            }
            else if (dt1.Year > dt2.Year) {
                return 1;
            }

            int doy1=Convert.ToInt32(dt1.DayOfYear);
            int doy2=Convert.ToInt32(dt2.DayOfYear);
            if (doy1<doy2)
            {
                return -1;
            }
            else if (doy1>doy2)
            {
                return 1;
            }else{
                return 0;
            }


        
        }


        //解析字符串，返回一个IToyzDateTimeObjcet接口的对象
        public static IToyzObjcet ParseToyzDateTime(string val) { 
           IToyzObjcet toyz=null;

           if (CACHE_TOYZ_DATETIME.ContainsKey(val)) {
               return CACHE_TOYZ_DATETIME[val];
           }


           //判断查询前序
           if (val.StartsWith("month@"))
           {
               
               string query = val.Split('@')[1];    //找出查询条件
               string[] subQuerys = query.Split(',');   //拆分查询条件，获取参数数组
               if (subQuerys.Length == 1) {        //如果只带一个参数
                   string subQuery = subQuerys[0];
                   if (ObjectUtil.IsDateQuery(subQuery))        //如果第一个参数是日期
                   {
                       DateTime monthdatetime = DateTime.Parse(subQuery.Trim('[',']'));   // 格式转换之前要去掉"[]"
                       toyz = new ToyzMonthObject(monthdatetime);
                   }
               }
               else if (subQuerys.Length == 2)
               {
                   string subQuery0 = subQuerys[0];
                   string subQuery1 = subQuerys[1];
                   if (ObjectUtil.IsNumberQuery(subQuery0) && ObjectUtil.IsNumberQuery(subQuery1))
                   {
                       int year = int.Parse(subQuery0.Trim('(', ')'));
                       int month = int.Parse(subQuery1.Trim('(', ')'));
                       toyz = new ToyzMonthObject(year, month);
                   }
               }
           }
           else if (val.StartsWith("week@"))
           {
               string query = val.Split('@')[1];
               string[] subQuerys = query.Split(',');
               if (subQuerys.Length == 1)
               {        //如果只带一个参数
                   string subQuery = subQuerys[0];
                   if (ObjectUtil.IsDateQuery(subQuery))        //如果第一个参数是日期
                   {
                       DateTime weekdatetime = DateTime.Parse(subQuery.Trim('[', ']'));   // 格式转换之前要去掉"[]"
                       toyz = new ToyzMonthObject(weekdatetime);
                   }
               }
           }
           else if (val.StartsWith("range@"))
           {
               string query = val.Split('@')[1];
               string[] subQuerys = query.Split(',');
               if (subQuerys.Length == 2)
               {
                   string subQuery0 = subQuerys[0];
                   string subQuery1 = subQuerys[1];
                   if (ObjectUtil.IsDateQuery(subQuery0) && ObjectUtil.IsDateQuery(subQuery1))
                   {
                       DateTime begindate = DateTime.Parse(subQuery0.Trim('[', ']'));
                       DateTime enddate = DateTime.Parse(subQuery1.Trim('[', ']'));
                       toyz = new ToyzRangeDateObject(begindate, enddate);
                   }
                   else if (ObjectUtil.IsDateQuery(subQuery0) && ObjectUtil.IsNumberQuery(subQuery1))
                   {
                       DateTime begindate = DateTime.Parse(subQuery0.Trim('[', ']'));
                       int diff = int.Parse(subQuery1.Trim('(', ')'));
                       toyz = new ToyzRangeDateObject(begindate, diff);
                   }
               }
           }

           //MonthObject month = new MonthObject(3,3);
            
           //格式为 week@[yyyy-MM-dd]  初始化WeekObject(DateTime dt)

           //格式为 month@[yyyy-MM-dd]  初始化MonthObject(DateTime dt)

           //格式为 month@(yyyy),(mm)     初始化MonthObject(int year,int month)

          //格式为  range@[yyyy-MM-dd],[yyyy-MM-dd]  初始化RangeDateObject(DateTime dt,DateTime dt)

           //格式为  range@[yyyy-MM-dd],(diff)    初始化RangeDateObject(DateTime dt,int diff)

           if (toyz != null) {

               CACHE_TOYZ_DATETIME.Add(val, toyz);
           }
             
           return toyz;
        }

        public static void FixMSSQLNullDateTime<T>(ref T obj){
           FieldInfo[] fields=ObjectUtil.GetAllFields(obj.GetType());
           foreach (FieldInfo field in fields) {
               Type type = field.FieldType;
               object val = field.GetValue(obj);
               if (type == typeof(DateTime)
                &&(val==null || val.Equals(default(DateTime)) )   
                   ) {
                   field.SetValue(obj,MSSQL_DATE_MIN);
               }
           }
        }


        public static string FormateWeekDateCn(DateTime dt, string pattern) {
            string[] weekdays = { "日", "一", "二", "三", "四", "五", "六" };
            string week = weekdays[Convert.ToInt32(dt.DayOfWeek)];
            return string.Format(pattern,week);
        }

    }



    public class ToyzWeekObject :IToyzObjcet{

        private ToyzWeekObject() { }

        public ToyzWeekObject(DateTime date) {
            this.setWeekDays(date);
        }

        public DateTime mon { get;protected set; }
        public DateTime tue { get; protected set; }
        public DateTime wed { get; protected set; }
        public DateTime thur { get; protected set; }
        public DateTime fri { get; protected set; }
        public DateTime sat { get; protected set; }
        public DateTime sun { get; protected set; }

        public ToyzWeekObject getDiffWeek(int diff) {
            ToyzWeekObject week=this;
            if (diff < 0) {
                for (int i = 0; i < diff; i++) {
                    week = week.getLastWeek();
                }
            }
            else if (diff > 0) {
                for (int i = 0; i < diff; i++)
                {
                    week = week.getNextWeek();
                }
            }
            return week;
        }

        public ToyzWeekObject getNextWeek() {
            DateTime nextDt = this.sat.AddDays(1);
            ToyzWeekObject week = new ToyzWeekObject(nextDt);
            return week;
        }

        public ToyzWeekObject getLastWeek() {
            DateTime lastDt = this.sun.AddDays(-1);
            ToyzWeekObject week = new ToyzWeekObject(lastDt);
            return week;
        }

        public void setWeekDays(DateTime date)
        {
           int index= Convert.ToInt32(date.DayOfWeek);
           for (int i = 0; i < 7; i++) {
               int diff = i-index;
               DateTime dt = date.AddDays(diff);
               switch (i) {
                   case 0: { this.sun = dt; break; }
                   case 1: { this.mon = dt; break; }
                   case 2: { this.tue = dt; break; }
                   case 3: { this.wed = dt; break; }
                   case 4: { this.thur = dt; break; }
                   case 5: { this.fri = dt; break; }
                   case 6: { this.sat = dt; break; }
                   
               }
           }
      
        }


        #region IToyzDateTimeObjcet 成员

        public ICriterion createCriterion(string proName)
        {
            return Restrictions.Between(proName,this.sun,this.sat);
        }

        #endregion
    }

    public class ToyzMonthObject :ToyzRangeDateObject, IToyzObjcet {



        private ToyzMonthObject() { }

        //根据一个日期来获取整个月的日期days
        public ToyzMonthObject(DateTime dt)
        {
            int year=dt.Year;
            int month=dt.Month;
            int num = DateTime.DaysInMonth(year, month);
            DateTime tempDate = new DateTime(year, month, 1);
            for (int i = 0; i < num; i++)
            {
                 days.AddLast(tempDate.AddDays(i));  
            }
        }

  

        //根据年和月来获取整个月的日期days
        public ToyzMonthObject(int year, int month)
        {

            int num = DateTime.DaysInMonth(year, month);
            DateTime tempDate = new DateTime(year, month, 1);
            for (int i = 0; i < num; i++)
            {
                 days.AddLast(tempDate.AddDays(i));  
            }


        }
		

        //获取这个月特定的某一日
        public DateTime getDayInMonth(int dayNum)
        {
            
            return days.ElementAt(dayNum);

        }

    }


    public class ToyzSeasonObject : ToyzRangeDateObject,IToyzObjcet
    {

        protected LinkedList<ToyzMonthObject> months = new LinkedList<ToyzMonthObject>();


        public ToyzSeasonObject(DateTime dt) {
            int month = dt.Month;
           
        }

    }

    public class ToyzRangeDateObject :IToyzObjcet {

        public LinkedList<DateTime> days { get; private set; }

        protected ToyzRangeDateObject() {
            days = new LinkedList<DateTime>();
        }

        public ToyzRangeDateObject(DateTime beginTime,DateTime endTime) {
            days = new LinkedList<DateTime>();
            while (
               beginTime.CompareTo(endTime)<=0
            ) {
                days.AddLast(beginTime);
               beginTime= beginTime.AddDays(1);
            }
        }

        public ToyzRangeDateObject(DateTime beginTime, int num) {
            for (int i = 0; i < num; i++) {
                days.AddLast(beginTime.AddDays(1));
            }

        }


        //获取这个月的第一日
        public DateTime getFirstDay()
        {
            return days.First();
        
        }

        //获取这个月的最后一日
        public DateTime getLastDay()
        {
            return days.Last();
        }

         public DateTime getDiffDay(int diff)
        { 
            return days.ElementAt(diff);
        }


        #region IToyzDateTimeObjcet 成员

        public ICriterion createCriterion(string proName)
        {
            return Restrictions.Between(proName, days.First.Value, days.Last.Value);
        }

        #endregion
    }
}
