using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Util;
using ZDSL.Model.Data;
using NHibernate;
using NHibernate.Criterion;
using ZDSL.Model.Public;
using Toyz4net.Core.Model;

namespace ZDSL.Biz
{
    public  class PublicBiz :BaseZdBiz
    {



        private PublicBiz() { }

        private static PublicBiz Instant;
        private static readonly object lockHelper = new object();


        public static PublicBiz GetInstant()
        {

            if (Instant == null)
            {
                lock (lockHelper)
                {
                    if (Instant == null)
                    {
                        Instant = new PublicBiz();
                    }
                }

            }
            return Instant;
        }

        public JsResultObject login(MemberModel member,string key) {
            JsResultObject re = new JsResultObject();
            this.OpenSession();
            ICriteria icr = this.session.CreateCriteria<MemberModel>();
            icr.Add(Restrictions.Or(Restrictions.Eq("id",key),Restrictions.Or(Restrictions.Eq("email",key) ,Restrictions.Eq("moblie", key))));
            icr.Add(Restrictions.Eq("pwd", member.pwd));
            IList<MemberModel> members = icr.List<MemberModel>();
            if (members.Count == 1) {
                re.code = JsResultObject.CODE_SUCCESS;
                re.msg = string.Format("会员 {0} 登陆成功",member.nickName);
                members[0].pwd = null;
                re.attrs.Add(typeof(MemberModel).Name, members[0]);
            }else{
                re.code = JsResultObject.CODE_ERROR;
                re.msg = string.Format("帐号 {0} 登陆失败",member.nickName);
            }
            return re;
        }




        public IList<BrandModel> getHotBrands() {
            
            ICriteria icr = BaseZdBiz.CreateCriteria<BrandModel>();
            icr.Add(Restrictions.Eq("recInd",BaseModel.IND_Y));
            icr.AddOrder(Order.Desc("recLevel"));
            IList<BrandModel> listHotBrand = icr.List<BrandModel>();
            return listHotBrand;
        }


        public FrontPageModel getCurFrontPage() {
            return BaseZdBiz.Load<FrontPageModel>("default");
        }

        public static  PageSeoModel getCurPageSeo() {
            return BaseZdBiz.Load<PageSeoModel>("default");
        }

        public static IList<OuterLinkModel> getOuterLinks() {
            ICriteria icr = BaseZdBiz.CreateCriteria<OuterLinkModel>();
            icr.Add(Restrictions.Eq("recInd", BaseModel.IND_Y));
            icr.AddOrder(Order.Desc("recLevel"));
            return icr.List<OuterLinkModel>();
        }
    }
}
