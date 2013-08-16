using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Model;

namespace ZDSL.Model.Data
{
    public abstract  class CommentModel : BaseModel
    {


        public string id { get; set; }
        public string title { get; set; }
        public string context { get; set; }
        public DateTime createDate { get; set; }
        public string comStatus { get; set; }

        public int serviceScore { get; set; }
        public int hygieneScore { get; set; }
        public int facilityScore { get; set; }
        public int priceScore { get; set; }
        public string recommendInd { get; set; }

        public override object getPk()
        {
            return this.id;
        }

        public override void setPk(object pk)
        {
            this.id = pk.ToString();
        }
    }



    public  class MemberCommentModel : CommentModel
    {
        public int orderFk { get; set; }
        public string hotelFk { get; set; }
        public string hotelName { get; set; }
        public string memberFk { get; set; }
        public string memberName { get; set; }
    }


    public  class NewsCommentModel : CommentModel
    {
        public string newsFk { get; set; }
 
    }

    public  class HotelCommentModel : CommentModel
    {
        public string hotelFk { get; set; }
        public string hotelName { get; set; }
        public string memberFk { get; set; }
        public string memberNickName { get; set; }

        public MemberModel member { get; set; }

    }

    public  class ExhiCommentModel : CommentModel
    {
        public string exhiFk { get; set; }

    }


}
