using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Toyz4net.Core.Util;
using ZDSL.Model.Data;
using ZDSL.Model.Admin;
using Toyz4net.Core.Client;
using NHibernate;
using NHibernate.Criterion;
using Toyz4net.Core.Service;
using System.Net;
using ZDSL.Model.Public;
using Toyz4net.Core.Model;

namespace ZDSL.Biz
{
    public class DataBiz:BaseZdBiz
    {


        private DataBiz() { }

        private static DataBiz Instant;
        private static readonly object lockHelper = new object();



        public static DataBiz GetInstant()
        {

            if (Instant == null)
            {
                lock (lockHelper)
                {
                    if (Instant == null)
                    {
                        Instant = new DataBiz();
                    }
                }

            }
            return Instant;
        }



        public void ImportHotelList() {        
            IList<ELongHotelAdapter> hotels = ELongStaticClient.GetHotelList();
            this.OpenSession();
            ITransaction tx = this.session.BeginTransaction();
            foreach (ELongHotelAdapter hotel in hotels)
            {
                HotelModel hm = new HotelModel();
                hm.fromYiLong(hotel);
                this.session.SaveOrUpdate(hm);
            }
            tx.Commit();
        }




        public void ImportGeoList() {

            IList<ELongGeoAdapter> Geos = ELongStaticClient.GetGeoList();
            this.OpenSession();
            ITransaction tx = this.session.BeginTransaction();
            foreach (ELongGeoAdapter geo in Geos)
            {
                GeoModel temp = new GeoModel();
                temp.from(geo);
                System.Console.WriteLine(string.Format("{0}:{1}:{2}:{3}", temp.id, temp.provinceName, temp.cityName, temp.url));
                this.session.SaveOrUpdate(temp);
            }
            tx.Commit();
        }

        public void ImportGeoLocationList()
        {

            IList<ELongGeoLocationAdapter> locals = ELongStaticClient.GetGeoLocationList();
            this.OpenSession();
            ITransaction tx = this.session.BeginTransaction();
            foreach (ELongGeoLocationAdapter local in locals)
            {
                
                System.Console.WriteLine(string.Format("{0}:{1}:{2}:{3}", local.exType,local.exGeoId,local.id,local.name));
                if (local.exType == ELongStaticClient.GEO_LOCATION_COMMERCIAL) {
                    
                    GeoCommercialLocationModel temp = new GeoCommercialLocationModel();
                    temp.from(local);
                    this.session.SaveOrUpdate(temp);
                }
                else if (local.exType == ELongStaticClient.GEO_LOCATION_DISTRICTS)
                {
                   
                    GeoDistrictsModel temp = new GeoDistrictsModel();
                    temp.from(local);
                    this.session.SaveOrUpdate(temp);
                }
                else if (local.exType == ELongStaticClient.GEO_LOCATION_LANDMARK)
                {
                    
                    GeoLandmarkLocationModel temp = new GeoLandmarkLocationModel();
                    temp.from(local);
                    this.session.SaveOrUpdate(temp);
                }
                
            }
            tx.Commit();
            this.CloseSession();
        }

        public void ImportBrandList() {
            
            IList<ELongBrandAdapter> brands = ELongStaticClient.GetBrandList();
            this.OpenSession();
            ITransaction tx = this.session.BeginTransaction();
            foreach (ELongBrandAdapter brand in brands)
            {
                BrandModel temp = new BrandModel();
                temp.from(brand);
                //System.Console.WriteLine(string.Format("{0}:{1}:{2}:{3}", temp.brandID, temp.brandNameLong, temp.groupID, temp.lastChangetime.ToString()));
                this.session.SaveOrUpdate(temp);
            }
            tx.Commit();
        }


        public void ImportDictList() {
            IList<ELongBaseCodeAdapter> basecodes = ELongStaticClient.GetBaseCodeList();
            //this.OpenSession();
           // ITransaction tx = this.session.BeginTransaction();
            foreach (ELongBaseCodeAdapter basecode in basecodes)
            {
                DictModel dict = new DictModel();
                dict.from(basecode);
                System.Console.WriteLine(string.Format("{0}:{1}:{2}",dict.type,dict.text,dict.value));
                BaseZdBiz.SaveOrUpdate(dict,"");
            }
            //tx.Commit();
        }

        public void LocalHotelImage(string hotelId,string basePath) {
            ICriteria icr = CreateCriteria<HotelImageModel>();
            icr.Add(Restrictions.Eq("hotelFk", hotelId));
            IList<HotelImageModel> images=icr.List<HotelImageModel>();
            WebClient client = new WebClient();
            foreach(HotelImageModel image in images){
                string folder = string.Format("{0}/{1}/{2}", basePath.TrimEnd('/'), hotelId, image.imgType);
                FileUtil.CreateFolder(folder);
              string filePath = string.Format("{0}/{1}{2}", folder,image.title,FileUtil.GetPostfixStr(image.imgUrl));
               
                  client.DownloadFile(image.imgUrl, filePath);
            }
            
        }

        public void LocalHotelImage(string basePath)
        {
            ICriteria icr = CreateCriteria<HotelImageModel>();
            IList<HotelImageModel> images = icr.List<HotelImageModel>();
            WebClient client = new WebClient();
            foreach (HotelImageModel image in images)
            {
               
                string folder = string.Format("{0}/{1}", basePath.TrimEnd('/'), image.hotelFk);
                FileUtil.CreateFolder(folder);
                string filePattern = "";
                if (image.isThum())
                {
                    filePattern = "{0}/{1}.thum{2}";
                }
                else if (image.isNormal())
                {
                    filePattern = "{0}/{1}.normal{2}";
                }
                else if (image.isAround()) {
                    filePattern = "{0}/{1}.around{2}";
                }
                else
                {
                    continue;
                }
                string filePath = string.Format(filePattern, folder,image.title, FileUtil.GetPostfixStr(image.imgUrl));
                try
                {
                    client.DownloadFile(image.imgUrl, filePath);
                    System.Console.WriteLine("保存图片:"+filePath);
                }
                catch (Exception ex) {
                    System.Console.WriteLine(ex.Message);
                }
            }

        }

        public JsResultObject LocalHotelImage(string basePath, HotelImageModel image,bool isTop) {
            JsResultObject re = new JsResultObject();
            WebClient client = new WebClient();
            string folder = string.Format("{0}/{1}", basePath.TrimEnd('/'), image.hotelFk);
            FileUtil.CreateFolder(folder);
            string filePattern = "";
            string filePath="";
            if (isTop)
            {
                if (image.isThum())
                {
                    filePattern = "{0}/top.thum{1}";
                }
                else if (image.isNormal())
                {
                    filePattern = "{0}/top.normal{1}";
                }
                filePath = string.Format(filePattern, folder, FileUtil.GetPostfixStr(image.imgUrl));
            }
            else
            {
                if (image.isThum())
                {
                    filePattern = "{0}/{1}.thum{2}";
                }
                else if (image.isNormal())
                {
                    filePattern = "{0}/{1}.normal{2}";
                }
                else if (image.isAround())
                {
                    filePattern = "{0}/{1}.around{2}";
                }
                else
                {
                    re.code = JsResultObject.CODE_ERROR;
                    return re;
                }
                filePath = string.Format(filePattern, folder, image.title, FileUtil.GetPostfixStr(image.imgUrl));
            }
            try
            {
                client.DownloadFile(image.imgUrl, filePath);
                re.rowNum = 1;
                re.msg = "保存图片:" + filePath;

            }
            catch (Exception ex)
            {
                re.rowNum = 0;
                re.msg = ex.Message;
                
            }
            return re;
        }

        public void LocalHotelTopImage(string basePath)
        {
            ICriteria icr = CreateCriteria<HotelImageModel>();
            icr.Add((Restrictions.Like("title","%外观%")));
            IList<HotelImageModel> images = icr.List<HotelImageModel>();
            WebClient client = new WebClient();
            foreach (HotelImageModel image in images)
            {
                    string folder = string.Format("{0}/{1}", basePath.TrimEnd('/'), image.hotelFk);
                    FileUtil.CreateFolder(folder);
                    string filePattern = "";
                    if (image.isThum())
                    {
                        filePattern = "{0}/top.thum{1}";
                    }
                    else if (image.isNormal())
                    {
                        filePattern = "{0}/top.normal{1}";
                    }
                    else
                    {
                        continue;
                    }
                    string filePath = string.Format(filePattern, folder, FileUtil.GetPostfixStr(image.imgUrl));
                    client.DownloadFile(image.imgUrl, filePath);
                }

            
        }

        public void LocalHotelTopImage(string hotelId, string basePath) {
            ICriteria icr = CreateCriteria<HotelImageModel>();
            icr.Add(Restrictions.Eq("hotelFk", hotelId));
            IList<HotelImageModel> images = icr.List<HotelImageModel>();
            WebClient client = new WebClient();
            foreach (HotelImageModel image in images)
            {
                if (image.title.Contains("外观"))
                {
                    string folder = string.Format("{0}/{1}", basePath.TrimEnd('/'), hotelId);
                    FileUtil.CreateFolder(folder);
                    string filePattern = "";
                    if (image.isThum()) {
                        filePattern = "{0}/thum{1}";
                    }
                    else if (image.isNormal())
                    {
                        filePattern = "{0}/normal{1}";
                    }
                    else {
                        continue;
                    }
                    string filePath = string.Format(filePattern, folder, FileUtil.GetPostfixStr(image.imgUrl));

                    client.DownloadFile(image.imgUrl, filePath);
                }

            }
        }

        public JsResultObject ImportHotelDetail(string hotelId,bool hasSubInfo) {
            JsResultObject re = new JsResultObject();
            
            ELongHotelDetailAdapter adapter = ELongStaticClient.GetHotelDetail(hotelId);
            HotelDetailModel hotel = new HotelDetailModel();
            hotel.from(adapter);
            System.Console.WriteLine(string.Format("{0}:{1}:{2}", hotel.country, hotel.city, hotel.name));
            re= BaseZdBiz.SaveOrUpdate(hotel,"");
          

            if (!hasSubInfo) {  return re; } 

                IList<ELongHotelImageAdapter> images = ELongStaticClient.GetHotelImageList(hotelId);
                foreach (ELongHotelImageAdapter image in images)
                {
                    HotelImageModel temp = new HotelImageModel();
                    temp.setPk(temp.createPk());
                    temp.from(image);
                    //System.Console.WriteLine(string.Format("{0}:{1}:{2}", temp.hotelFk, temp.title, temp.imgUrl));
                   re.rowNum+= BaseZdBiz.SaveOrUpdate(temp,"").rowNum;
                    
                }
            

            
                IList<ELongHotelRoomAdapter> rooms = ELongStaticClient.GetHotelRoomList(hotelId);

                foreach (ELongHotelRoomAdapter room in rooms)
                {
                    HotelRoomModel temp = new HotelRoomModel();
                    temp.setPk(temp.createPk());
                    temp.from(room);
                    //System.Console.WriteLine(string.Format("{0}:{1}:{2}", temp.hotelFk, temp.roomName, temp.area));
                    re.rowNum += BaseZdBiz.SaveOrUpdate(temp,"").rowNum;
                   
                }

                ELongHotelFeatureInfoAdapter featureInfo = ELongStaticClient.GetHotelFeatureInfo(hotelId);
                HotelFeatrueInfoModel hotelFeatrueInfoModel = new HotelFeatrueInfoModel();
                hotelFeatrueInfoModel.from(featureInfo);
                re.rowNum += BaseZdBiz.SaveOrUpdate(hotelFeatrueInfoModel,"").rowNum;


                IList<ELongHotelLandMarkAdapter> landmarks = ELongStaticClient.GetHotelLandMarkList(hotelId);

                foreach (ELongHotelLandMarkAdapter landmark in landmarks)
                {
                    HotelLandMarkModel temp = new HotelLandMarkModel();
                    temp.setPk(temp.createPk());
                    temp.from(landmark);
                    temp.id = temp.createPk().ToString();
                    //System.Console.WriteLine(string.Format("{0}:{1}:{2}", temp.hotelFk, temp.roomName, temp.area));
                    re.rowNum += BaseZdBiz.SaveOrUpdate(temp, "").rowNum;
                    
                }

                IList<ElongHotelSurroundingAttractionAdapter> attractions = ELongStaticClient.GetHotelSurroundingAttractionList(hotelId);

                foreach (ElongHotelSurroundingAttractionAdapter attraction in attractions)
                {
                    HotelSurroundingAttractionModel temp = new HotelSurroundingAttractionModel();
                    temp.setPk(temp.createPk());
                    temp.from(attraction);
                    //System.Console.WriteLine(string.Format("{0}:{1}:{2}", temp.hotelFk, temp.roomName, temp.area));
                    re.rowNum += BaseZdBiz.SaveOrUpdate(temp, "").rowNum;
                    
                }

                IList<ELongHotelSurroundingCommerceAdapter> commerces = ELongStaticClient.GetHotelSurroundingCommerceList(hotelId);

                foreach (ELongHotelSurroundingCommerceAdapter commerce in commerces)
                {
                    HotelSurroundingCommerceModel temp = new HotelSurroundingCommerceModel();
                    temp.setPk(temp.createPk());
                    temp.from(commerce);
                    //System.Console.WriteLine(string.Format("{0}:{1}:{2}", temp.hotelFk, temp.roomName, temp.area));
                    re.rowNum+= BaseZdBiz.SaveOrUpdate(temp,"").rowNum;
                    
                }

                IList<ElongHotelSurroundingRestaurantAdapter> restaurants = ELongStaticClient.GetHotelSurroundingRestaurantList(hotelId);

                foreach (ElongHotelSurroundingRestaurantAdapter restaurant in restaurants)
                {
                    HotelSurroundingRestaurantModel temp = new HotelSurroundingRestaurantModel();
                    temp.setPk(temp.createPk());
                    temp.from(restaurant);
                    //System.Console.WriteLine(string.Format("{0}:{1}:{2}", temp.hotelFk, temp.roomName, temp.area));
                    re.rowNum+= BaseZdBiz.SaveOrUpdate(temp,"").rowNum;
                    
                }

                IList<ElongHotelSurroundingShopAdapter> shops = ELongStaticClient.GetHotelSurroundingShopList(hotelId);

                foreach (ElongHotelSurroundingShopAdapter shop in shops)
                {
                    HotelSurroundingShopModel temp = new HotelSurroundingShopModel();
                    temp.setPk(temp.createPk());
                    temp.from(shop);
                    //System.Console.WriteLine(string.Format("{0}:{1}:{2}", temp.hotelFk, temp.roomName, temp.area));
                    re.rowNum += BaseZdBiz.SaveOrUpdate(temp, "").rowNum;
                   
                }


                IList<ElongHotelTrafficInfoAdapter> traffics = ELongStaticClient.GetHotelTrafficInfoList(hotelId);

                foreach (ElongHotelTrafficInfoAdapter traffic in traffics)
                {
                    HotelTrafficInfoModel temp = new HotelTrafficInfoModel();

                    temp.from(traffic);
                    temp.id = temp.createPk().ToString();
                    //System.Console.WriteLine(string.Format("{0}:{1}:{2}", temp.hotelFk, temp.roomName, temp.area));
                    re.rowNum+= BaseZdBiz.SaveOrUpdate(temp,"").rowNum;
                   
                }
            
           
            return re;
        }

        public JsResultObject cancelOrder(OrderModel order, string cancelCode, string cancelReason) {

            JsResultObject re = new JsResultObject();
            ELongApiBiz elongBiz = ELongApiBiz.GetInstant();
            
            order.orderStatus = OrderModel.ORDER_STATUS_CANCEL;
            BaseZdBiz.Update(order,"");
            if (order.elongOrderId!=default(int))
            {
                re = elongBiz.cancelHotelOrderById(order.elongOrderId, cancelCode, cancelReason);
            }
            return re;
        }

        public JsResultObject renewOrder(OrderModel order)
        {

            JsResultObject re = new JsResultObject();
            ELongApiBiz elongBiz = ELongApiBiz.GetInstant();

            if (DateTimeUtil.CompareDay(order.checkInDate,DateTime.Now)<0) {
                re.code = JsResultObject.CODE_ERROR;
                re.msg = string.Format( "原订单:{0} 已超出入住日期{1},不能再还原,请重新创建订单",order.id,order.checkInDate.ToShortDateString());
            }else{
                 order.orderStatus = OrderModel.ORDER_STATUS_NEW;
                re= BaseZdBiz.Update(order, "");
                re.msg = string.Format("原订单:{0} 还原成功,请记得在{1}入住", order.id, order.checkInDate.ToShortDateString());
            }


            return re;
        }

        public JsResultObject cancelOrders(IList<OrderModel> orders, string cancelCode, string cancelReason)
        {
            JsResultObject re = new JsResultObject();
            ELongApiBiz elongBiz=ELongApiBiz.GetInstant();
            IList<int> cancelOrderId=new List<int>();
            foreach (OrderModel order in orders) {
                JsResultObject tempRe = elongBiz.cancelHotelOrderById(order.elongOrderId, cancelCode, cancelReason);
                if (tempRe.code == JsResultObject.CODE_SUCCESS) {
                    cancelOrderId.Add(order.elongOrderId);
                }
            }
            re.title = "操作成功";
            if (cancelOrderId.Count == 0)
            {
               
                re.msg = "没有订单被取消";
            }
            else {
                re.msg =string.Format("成功取消 {0} 条订单,订单号:{0}",
                    cancelOrderId.Count
                    ,cancelOrderId.ToArray().ToString()
                );
            }

            return re;
        }


        public JsResultObject submitComment(MemberCommentModel comment) {
            JsResultObject re = new JsResultObject();
            OrderModel order = BaseZdBiz.Load<OrderModel>(comment.orderFk);
            MemberRewardRuleModel rule = GetCurRewardRule();
            if (order.checkOutDate.AddDays(rule.comValidityDays).CompareTo(DateTime.Now) < 0) {
                re.code = JsResultObject.CODE_ERROR;
                re.msg =string.Format( "离店日期超出{0}日",rule.comValidityDays);
                return re;
            }

            comment.createDate = DateTime.Now;
            comment.setPk(comment.createPk());
            re=BaseZdBiz.Save(comment);
            order.orderStatus = OrderModel.ORDER_STATUS_COMMENTED;
            re = BaseZdBiz.Update(order,"");
            MemberModel member = BaseZdBiz.Load<MemberModel>(comment.memberFk);
            MemberAccountModel oldAccount = getCurrMemberAccount(member);
            MemberAccountModel newAccount = new MemberAccountModel(comment.memberFk);
            newAccount.method = MemberAccountModel.METHOD_COMMENT_REWARD;
            newAccount.createDate = DateTime.Now;
            newAccount.method = MemberAccountModel.METHOD_COMMENT_REWARD;
            newAccount.oldAmount = oldAccount.newAmount;
            newAccount.changeAmount=Convert.ToInt32(GetReward(order).amount);
            newAccount.newAmount = newAccount.oldAmount + newAccount.changeAmount;
            newAccount.setPk(newAccount.createPk());
            re = BaseZdBiz.Save(newAccount);
     
            return re;
        }

        public JsResultObject submitComment(ExhiModel comment) {
            return new JsResultObject();
        }

        public JsResultObject submitComment(NewsCommentModel comment) {
            return new JsResultObject();
        }


        public JsResultObject grantMemberCommentReward(MemberRewardModel reward) {
            return new JsResultObject();
        }


        public JsResultObject deductMemberCommentReward(MemberRewardModel reward) {
            return new JsResultObject();
        }

        public JsResultObject cleanMemberComment() {
            return new JsResultObject();
        }


        public JsResultObject submitOrder(OrderModel order, bool isElong) {
            JsResultObject re = new JsResultObject();

            order.lastModifyDate = DateTime.Now;
            

            if (isElong) {
                ELongApiBiz elongBiz = ELongApiBiz.GetInstant();
                re = elongBiz.submitHotelOrder(ref order);
                if (re.code == JsResultObject.CODE_SUCCESS)
                {
                    order.orderStatus = OrderModel.ORDER_STATUS_ELONG_SUCCESS;
                }
                else {
                    order.orderStatus = OrderModel.ORDER_STATUS_FAIL;
                }
            }

            BaseZdBiz.SaveOrUpdate(order,"订单");

            return re;
        }


        public JsResultObject recommendHotel(HotelModel hotel,int priorityLevel,string remark ) {

            JsResultObject re = new JsResultObject();

            HotelRecommendModel recommend = new HotelRecommendModel();
            recommend.priorityLevel = priorityLevel;
            recommend.remark = remark;
            recommend.hotelName = hotel.hotelName;
            recommend.hotelId = hotel.hotelId;
            try
            {
                this.OpenSession();
                ITransaction itx = this.session.BeginTransaction();
                SaveOrUpdate(recommend,"");
                itx.Commit();
                this.CloseSession();
                re.title = "操作成功";
                re.msg =string.Format( "{0} 推荐成功",hotel.hotelName);
            }
            catch (Exception ex) {
                re.title = "操作失败";
                re.code = JsResultObject.CODE_ERROR;
                re.msg = string.Format("失败原因:{0}",ex.Message);
            }
            return re;
        }

        public JsResultObject setHotBrand(BrandModel brand, int priorityLevel, string remark)
        {

            JsResultObject re = new JsResultObject();
            HotBrandModel hotBrand = new HotBrandModel();
            hotBrand.id = brand.brandID;
            hotBrand.name = brand.brandName;
             hotBrand.priorityLevel = priorityLevel;
             hotBrand.remark = remark;

            try
            {
                this.OpenSession();
                ITransaction itx = this.session.BeginTransaction();
                SaveOrUpdate(hotBrand, "");
                itx.Commit();
                this.CloseSession();
                re.title = "操作成功";
                re.msg = string.Format("{0} 推荐成功", brand.brandName);
            }
            catch (Exception ex)
            {
                re.title = "操作失败";
                re.code = JsResultObject.CODE_ERROR;
                re.msg = string.Format("失败原因:{0}", ex.Message);
            }
            return re;
        }

        public JsResultObject checkMemberRegInfo(MemberModel member) {
            JsResultObject re = new JsResultObject();
            JsResultObject tempRe = new JsResultObject();

            MemberModel tempMember = BaseZdBiz.Load<MemberModel>(member.id);
            if (tempMember != null) {
                re.code = JsResultObject.CODE_ERROR;
                re.traceStack.Add("exists",string.Format("用户名{0}已被注册，请使用另外用户名的重新注册",member.id));
                return re;
            }

            ICriteria icr = BaseZdBiz.CreateCriteria<MemberModel>();
            icr.Add(Restrictions.Eq( "email", member.email));
            if (icr.List().Count > 0) {
                re.code = JsResultObject.CODE_ERROR;
                re.traceStack.Add("exists", string.Format("邮箱{0}已被注册，请使用另外邮箱的重新注册", member.email));
                return re;
            }

            icr = BaseZdBiz.CreateCriteria<MemberModel>();
            icr.Add(Restrictions.Eq("moblie", member.email));
            if (icr.List().Count > 0)
            {
                re.code = JsResultObject.CODE_ERROR;
                re.traceStack.Add("moblie", string.Format("手机{0}已被注册，请使用另外手机的重新注册", member.moblie));
                return re;
            }


          //  tempRe = ServiceUtil.ValidateEmail(member.id);
           // re.code += tempRe.code;
         //   if (tempRe.code != JsResultObject.CODE_SUCCESS)
         //   {
         //       re.traceStack.Add("email", tempRe.msg);
         //   }

            tempRe = ServiceUtil.ValidateMoblie(member.moblie);
            re.code += tempRe.code;
            if (tempRe.code != JsResultObject.CODE_SUCCESS)
            {
                re.traceStack.Add("moblie", tempRe.msg);
            }


            return re;  
        }

        public JsResultObject submitMemberRegInfo(MemberModel member)
        {
            JsResultObject re = BaseZdBiz.Save(member);
            if (re.code == JsResultObject.CODE_SUCCESS)
            {
                MemberRewardRuleModel rule=GetCurRewardRule();
                MemberAccountModel account = new MemberAccountModel(member.id);
                account.oldAmount = 0;
                account.newAmount = 10;
                account.changeAmount = 10;
                account.createDate = DateTime.Now;
                account.method = MemberAccountModel.METHOD_ACCOUNT_CREATE;
                account.remark=string.Format("帐号创建成功，获得奖励{0}元",10);
                account.setPk(account.createPk());
                re = BaseZdBiz.Save(account);
            }
            return re;
        }

        public JsResultObject createNewOrder(OrderModel order) {
            JsResultObject re = new JsResultObject();
            this.OpenSession();
            ICriteria icr=this.session.CreateCriteria<OrderModel>();
            DateTime startDate = new DateTime(order.createDate.Year, order.createDate.Month, order.createDate.Day, 0, 0, 0);
            DateTime endDate = startDate.AddDays(1);
            icr.Add(Restrictions.And(
                      Restrictions.And(
                           Restrictions.Eq("hotelId", order.hotelId)
                           , Restrictions.Between("createDate", startDate, endDate)
                      ),
                      Restrictions.Eq("guestsNameArray", order.guestsNameArray)

            ));
            IList<OrderModel> orders = icr.List<OrderModel>();
            if (orders.Count > 0)
            {
                re.code = JsResultObject.CODE_ERROR;
                re.msg= "同名住客不能在同一天预订同一间酒店的房间";
                return re;
            }
            string[] guestsName = order.guestsNameArray.Split(',');
            if (string.IsNullOrEmpty(order.guestsNameArray))
            {
                re.code = JsResultObject.CODE_ERROR;
                re.msg= "必须至少填写一个住客的名字";
                return re;  
            }
            Array.Sort(guestsName);
            order.guestsNameArray = ObjectUtil.ToUnionString(guestsName,',');

            re = ServiceUtil.ValidateMoblie(order.mobile);
            if (re.code != JsResultObject.CODE_SUCCESS) { return re; }

           // re = ServiceUtil.ValidateEmail(order.email);
           // if (re.code != JsResultObject.CODE_SUCCESS) { return re; }



            re = BaseZdBiz.Save(order);
            ConfigModel config = AdminBiz.GetCurrConfig();
            if (config.autoOrderInd == BaseModel.IND_Y) {
                re = submitOrder(order, true);
            }
            return re;
        }

        public int getMemberCurAmount(MemberModel member) {

            int curAmount = 0;

            IList<MemberRewardModel> rewards = getMemberRewards(member,new string[] { MemberRewardModel.REBATE_STATUS_UNREBATED });
            foreach (MemberRewardModel reward in rewards) {
                
                curAmount += Convert.ToInt32(reward.amount);
            }
            return curAmount;
        }

        public JsResultObject requireMemberRewards(MemberModel member) {
            JsResultObject re = new JsResultObject();
            IList<MemberRewardModel> rewards = getMemberRewards(member, new string[] { MemberRewardModel.REBATE_STATUS_UNREBATED });
            foreach (MemberRewardModel reward in rewards)
            {
                reward.rebateStatus = MemberRewardModel.REBATE_STATUS_REQUIRE;
                re.code += BaseZdBiz.Update(reward,"").code;
            }
            return re;
        }

        public JsResultObject rebateReward(MemberModel member, int rebateAmount, string remark) {
            JsResultObject re = new JsResultObject();
            MemberAccountModel account = getCurrMemberAccount(member);
            if (member.rebateInd != BaseModel.IND_Y) {
                re.code = JsResultObject.CODE_ERROR;
                re.msg = string.Format("当前用户尚未申请返还", account.newAmount, rebateAmount);
                return re;
            }
            else if (account.newAmount < rebateAmount)
            {
                re.code = JsResultObject.CODE_ERROR;
                re.msg = string.Format("当前用户帐户奖励{0},小于{1},返还无效", account.newAmount, rebateAmount);
                return re;
            }
            MemberAccountModel newAccount = new MemberAccountModel();
            newAccount.createDate = DateTime.Now;
            newAccount.oldAmount = account.newAmount;
            newAccount.newAmount = account.newAmount - rebateAmount;
            newAccount.remark = remark;
            newAccount.memberFk = member.id;
            newAccount.method = MemberAccountModel.METHOD_REWARD_REBATE;
            newAccount.setPk(newAccount.createPk());
            newAccount.remark = string.Format("成功为手机号码{0}充值{1}元",member.rebateMoblie, rebateAmount);
            re = BaseZdBiz.Save(newAccount);
            if (re.code == JsResultObject.CODE_SUCCESS)
            {
                MemberRewardLogModel rewardLog = new MemberRewardLogModel();
                rewardLog.createDate = DateTime.Now;
                rewardLog.amount = rebateAmount;
                rewardLog.moblie = member.rebateMoblie;
                rewardLog.method = MemberAccountModel.METHOD_REWARD_REBATE;
                rewardLog.remark = string.Format("成功为手机号码{0}充值{1}元", member.rebateMoblie, rebateAmount);
                rewardLog.status = BaseModel.STATUS_ACTIVATE;
                rewardLog.memberFk = member.id;
                rewardLog.setPk(rewardLog.createPk());
                re = BaseZdBiz.Save(rewardLog);
            }
            return re;
        }

        private IList<MemberRewardModel> getMemberRewards(MemberModel member, string[] rebateStatus) {
            ICriteria icr = CreateCriteria<MemberRewardModel>();
            icr.Add(Restrictions.Eq("memberFk", member.id));
            icr.Add(Restrictions.In("rebateStatuts", rebateStatus));
            IList<MemberRewardModel> rewards = icr.List<MemberRewardModel>();
            return rewards;
        }

        public JsResultObject exchangeReward(MemberRewardLogModel log) {
            JsResultObject re = new JsResultObject();
            return re;
        }

        private MemberRewardModel GetReward(OrderModel order) {
            MemberRewardRuleModel rule = GetCurRewardRule();
            MemberRewardModel reward = new MemberRewardModel();
            reward.setPk(reward.createPk());
            reward.createDate = DateTime.Now;
            if (order.totalPrice < rule.rebateScaleAmount)
            {
                reward.amount = rule.rebateAmount;
            }
            else {
                reward.amount = (float)(order.totalPrice * rule.rebateScale);
            }
            reward.memberFk = order.memberFk;
            return reward;
        }

        public  MemberRewardRuleModel GetCurRewardRule() {
            MemberRewardRuleModel rule=BaseZdBiz.Load<MemberRewardRuleModel>("default");
            if (rule == null) {
                ICriteria icr = BaseZdBiz.CreateCriteria<MemberRewardRuleModel>();
                rule = icr.List<MemberRewardRuleModel>()[0];
            }
            return rule;
        }


        public IList<HotelDetailModel> getHotHotelDetail() {
            IList<string> ids = new List<string>();
            ICriteria icr = BaseZdBiz.CreateCriteria<HotelModel>();
            icr.Add(Restrictions.Eq("recInd", BaseModel.IND_Y));
            icr.AddOrder(Order.Desc("recLevel"));
            IList<HotelModel> hotels = icr.List<HotelModel>();
            foreach(HotelModel hotel in hotels){
              ids.Add(hotel.hotelId);
            }
            icr = BaseZdBiz.CreateCriteria<HotelDetailModel>();
            icr.Add(Restrictions.In("id",ids.ToArray()));
            return icr.List<HotelDetailModel>();
        }


        public MemberAccountModel getCurrMemberAccount(MemberModel member) {
            ICriteria icr = BaseZdBiz.CreateCriteria<MemberAccountModel>();
            icr.Add(Restrictions.Eq("memberFk",member.id));
            icr.AddOrder(Order.Desc("createDate"));
            IList<MemberAccountModel> accounts = icr.List<MemberAccountModel>();
            if (accounts.Count > 0)
            {
                return accounts[0];
            }
            else {
                return new MemberAccountModel(member.id);
            }
        }

    }
}
