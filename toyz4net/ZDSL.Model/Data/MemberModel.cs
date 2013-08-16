using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Model;

namespace ZDSL.Model.Data
{
     public class MemberModel: BaseModel
    {

         public static string EXCHANG_STATUS_WAIT="wait";
         public static string EXCHANG_STATUS_CASH = "cash";
         public static string EXCHANG_STATUS_MOBLIE = "moblie";



         public string id { get; set; }
         public  string pwd { get; set; }
         public string nickName { get; set; }
         public string email { get; set; }
        public string guestFk { get; set; }
        public string sinaWeibo { get; set; }
        public string moblie { get; set; }
        public string realName { get; set; }
        public int totalAmount { get; set; }
        public string exchangeStatus { get; set; }
        public string weiboUid { get; set; }
        public string qqUid { get; set; }
        public string renrenUid { get; set; }
        public string kaixinUid { get; set; }
        public string tencentWeiboUid { get; set; }
        public string rebateInd { get; set; }
        public string rebateMoblie { get; set; }
        public string remark { get; set; }

        public override object getPk()
        {
            return this.id;
        }

        public override void setPk(object pk)
        {
            this.id = pk.ToString();
        }
    }


     public class MemberRewardModel : BaseModel {

         public static string REBATE_STATUS_REBATED="rebated";
         public static string REBATE_STATUS_REQUIRE = "require";
         public static string REBATE_STATUS_UNREBATED = "unrebated";
         public static string REBATE_STATUS_CANCEL = "cancel";

          private static  Dictionary<string, string[]> DictRewardStatus ;

          public  static  Dictionary<string, string[]> GetDictRewardStatus() {
            
               if(DictRewardStatus==null){
                  DictRewardStatus=new Dictionary<string,string[]>();
                  DictRewardStatus.Add("未返还", new string[] { MemberRewardModel.REBATE_STATUS_UNREBATED });
                  DictRewardStatus.Add("处理中", new string[] { MemberRewardModel.REBATE_STATUS_REQUIRE });
                  DictRewardStatus.Add("已返还", new string[] { MemberRewardModel.REBATE_STATUS_REBATED });

                  DictRewardStatus.Add("取消", new string[] { MemberRewardModel.REBATE_STATUS_CANCEL });
               }
               return DictRewardStatus;
          }



         public static string BUS_TYPE_NEW_MEMBER = "newMember";
         public static string BUS_TYPE_COMMENT = "comment";

         public string id { get; set; }
         public string comFk { get; set; }
         public DateTime createDate { get; set; }
         public string memberFk { get; set; }
         public string rebateStatus { get; set; }
         public float amount { get; set; }
         public string rebateMethod { get; set; }
         public string remark { get; set; }
         public string userFk { get; set; }
         public string busType { get; set; }


         public override object getPk()
         {
             return this.id;
         }

         public override void setPk(object pk)
         {
             this.id = pk.ToString();
         }
     }



     public class MemberRewardRuleModel : BaseModel {

         public string id { get; set; }
         public double rebateScale { get; set; }
         public int rebateAmount { get; set; }
         public int rebateScaleAmount { get; set; }
         public int comValidityDays { get; set; }
         public int rebateLimit { get; set; }
         public int rebateValidityMonths { get; set; }
         public string name { get; set; }


         public override object getPk()
         {
             return this.id;
         }

         public override void setPk(object pk)
         {
             this.id = pk.ToString();
         }

         public  int getRewardAmount(int price) {
             if (price < this.rebateScaleAmount)
             {
                 return  this.rebateAmount;
             }
             else
             {
                 return Convert.ToInt32(price * this.rebateScale);
             }
         }

     }


     public class MemberRewardLogModel:BaseModel {





         public string id{get;set;}
         public string method{get;set;}
         public string memberFk{get;set;}
         public DateTime createDate{get;set;}
         public string remark{get;set;}
         public string moblie{get;set;}
         public string bank{get;set;}
         public string exchangeStatus{get;set;}
         public string userFk{get;set;}
         public string rewardFkArrays{get;set;}
         public int amount{get;set;}
         

         public override object getPk()
         {
             return this.id;
         }

         public override void setPk(object obj)
         {
             this.id = obj.ToString();
         }
     }


     public class MemberAccountModel : BaseModel {

         public static string METHOD_ACCOUNT_CREATE = "account_create";
         public static string METHOD_COMMENT_REWARD = "comment_reward";
         public static string METHOD_REWARD_REBATE = "reward_rebate";

         private static Dictionary<string, string> DictMethods;

         public static Dictionary<string, string> GetDictMethods()
         {

             if (DictMethods == null)
             {
                 DictMethods = new Dictionary<string, string>();
                 DictMethods.Add( METHOD_ACCOUNT_CREATE,"会员奖励" );
                 DictMethods.Add( METHOD_COMMENT_REWARD,"点评奖励");
                 DictMethods.Add(METHOD_REWARD_REBATE,"返回奖励");
             }
             return DictMethods;
         }

         public MemberAccountModel() { 
         
         }

         public MemberAccountModel(string memberFk) {
             this.memberFk = memberFk;
         }

         public string id{get;set;}
         public string memberFk{get;set;}
         public int oldAmount{get;set;}
         public int newAmount{get;set;}
         public DateTime createDate{get;set;}
         public string remark{get;set;}
         public string method{get;set;}
         public int orderFk{get;set;}
         public string userFk{get;set;}
         public int changeAmount { get; set; }

         public override object getPk()
         {
             return this.id;
         }

         public override void setPk(object obj)
         {
             this.id = obj.ToString();
         }
     }

}
