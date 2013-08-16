using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZDSL.Model.Cache
{
    public class RewardCountModel
    {

        /*
                  public static string REBATE_STATUS_REBATED="rebated";
         public static string REBATE_STATUS_REQUIRE = "require";
         public static string REBATE_STATUS_UNREBATED = "unrebated";
         public static string REBATE_STATUS_CANCEL = "cancel";
         */
        public string id { get; set; }
        public int allCount { get; set; }
        public int rebatedCount { get; set; }
        public int requireCount { get; set; }
        public int unrebateCount { get; set; }
        public int cancelCount { get; set; }
    }
}
