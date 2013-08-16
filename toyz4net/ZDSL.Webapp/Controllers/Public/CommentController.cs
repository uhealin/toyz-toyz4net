using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Criterion;
using ZDSL.Biz;
using ZDSL.Model.Data;
using Toyz4net.Core.Util;
using ZDSL.Model.Public;

namespace ZDSL.Webapp.Controllers.Public
{
    public class CommentController : BaseZdController
    {


        public static string GetPathIndex()
        {
            string pattern = "{0}{1}";
            string filePath = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.Comment.Index"];
            if (IsDebug())
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(), "/Public/Comment/Index");
            }
            else
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(), filePath);
            }
        }


        //
        // GET: /Comment/

        public ActionResult Index()
        {
            ICriteria icr = BaseZdBiz.CreateCriteria<HotelCommentModel>(this.getPager());
            icr.AddOrder(Order.Desc("createDate"));
            IList<HotelCommentModel> hotelComments = icr.List<HotelCommentModel>();
            ViewData[typeof(HotelCommentModel).Name] = hotelComments;
            this.VdHotBookingHotel(10);
            this.VdHotBrand(12);
            this.VdHotCity(15);
            PageSeoModel seo = PublicBiz.getCurPageSeo();
            this.setPageDesc(seo.commentIndexDesc);
            this.setPageKeyWords(seo.commentIndexKeywords);
            return View();
            
        }


        public ActionResult TableHotelCommentList(string hotelId) {
            ICriteria icr = BaseZdBiz.CreateCriteria<HotelCommentModel>(this.getPager());
            icr.Add(Restrictions.Eq("hotelFk",hotelId));
            icr.AddOrder(Order.Desc("createDate"));
            IList<HotelCommentModel> hotelComments = icr.List<HotelCommentModel>();
            ViewData[typeof(HotelCommentModel).Name] = hotelComments;
            return View();
        }

        [ValidateInput(false)]
        public ActionResult DoSubmitHotelComment(string vCode,string context) {

            JsResultObject re = WebUtil.DoValidateCode(vCode);
            if (re.code != JsResultObject.CODE_SUCCESS)
            {
                return JsonText(re, JsonRequestBehavior.AllowGet);
            }

            MemberModel member = this.getAuthMember();
            if (member == null)
            {
                re.msg = "请先使用会员帐号登陆，再进行评论";
                return JsonText(re, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(context)) {
                re.msg = "评论不能为空字符";
                return JsonText(re, JsonRequestBehavior.AllowGet);
            }


            HotelCommentModel hotelComment = new HotelCommentModel();
            hotelComment = WebUtil.Eval<HotelCommentModel>(hotelComment, "", "");
            hotelComment.createDate = DateTime.Now;
            hotelComment.setPk(hotelComment.createPk());
            hotelComment.memberFk = member.id;
            hotelComment.memberNickName = member.nickName;
      
             re = BaseZdBiz.Save(hotelComment);
            return JsonText(re,JsonRequestBehavior.AllowGet);
        }



        public ActionResult TableMemberCommentList(string hotelId)
        {
            ICriteria icr = BaseZdBiz.CreateCriteria<MemberCommentModel>(this.getPager());
            icr.Add(Restrictions.Eq("hotelFk", hotelId));
            IList<MemberCommentModel> comments = icr.List<MemberCommentModel>();
            ViewData[typeof(MemberCommentModel).Name] = comments;
            return View();
        }

        [ValidateInput(false)]
        public ActionResult DoSubmitMemberComment(string vCode)
        {
            JsResultObject re = WebUtil.DoValidateCode(vCode);
            if (re.code != JsResultObject.CODE_SUCCESS) {
                return JsonText(re, JsonRequestBehavior.AllowGet);
            }

            MemberModel model = this.getAuthMember();
            if (model == null) {
                re.msg = "请先使用会员帐号登陆，再进行评论";
                return JsonText(re, JsonRequestBehavior.AllowGet);
            }
            MemberCommentModel comment = new MemberCommentModel();
            comment = WebUtil.Eval<MemberCommentModel>(comment, "", "");
            comment.createDate = DateTime.Now;
            comment.setPk(comment.createPk());
            re = BaseZdBiz.Save(comment);
            return JsonText(re, JsonRequestBehavior.AllowGet);
        }
    }
}
