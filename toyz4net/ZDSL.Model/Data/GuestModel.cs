using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Model;
using Toyz4net.Core.NorthBoundAPIService;
using Toyz4net.Core.Util;

namespace ZDSL.Model.Data
{
    public class GuestModel :BaseModel
    {
       
        public static string  TYPE_BLACKLIST ="blacklist";
        public static string  TYPT_VIP = "vip";
        public static string TYPE_NORMAL = "normal";

        public string id { get; set; }
        public string idNumber { get; set; }
        public string idTypeCode { get; set; }
        public string genderCode { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string nationality { get; set; }
        public int phoneInterCode { get; set; }
        public int phoneAreaCode { get; set; }
        public int phoneNumber { get; set; }
        public int phoneExt { get; set; }
        public int faxInterCode { get; set; }
        public int faxAreaCode { get; set; }
        public int faxNumber { get; set; }
        public int faxExt { get; set; }
        public string parameterString1 { get; set; }
        public string parameterString2 { get; set; }
        public string parameterString3 { get; set; }
        public int parameterInt1 { get; set; }
        public int parameterInt2 { get; set; }
        public int parameterInt3 { get; set; }
        public string nameCn { get; set; }
        public string nameEn { get; set; }
        public string type { get; set; }



        public override object getPk()
        {
            return this.id;
        }

        public override void setPk(object pk)
        {
            this.id = pk.ToString();
        }

        public ContacterForSubmitHotelOrder toContacterForSubmitHotelOrder() {
            ContacterForSubmitHotelOrder contacter = new ContacterForSubmitHotelOrder();
            contacter.IdNumber = this.idNumber;
            contacter.IdTypeCode = this.idTypeCode;
            contacter.GenderCode = this.genderCode;
            contacter.Email = this.email;
            contacter.Mobile = this.mobile;
            contacter.Name = ObjectUtil.Parse(this.nameCn, this.nameEn);
            PhoneForSubmitHotelOrder phone = new PhoneForSubmitHotelOrder();
            phone.AreaCode =  this.phoneAreaCode;
            phone.InterCode = this.phoneInterCode;
            phone.Ext = this.phoneExt;
            phone.Nmber =this.phoneNumber;
            FaxForSubmitHotelOrder fax = new FaxForSubmitHotelOrder();
            fax.AreaCode = this.faxAreaCode;
            fax.InterCode = this.faxInterCode;
            fax.Ext = this.faxExt;
            fax.Nmber = this.phoneNumber;
            contacter.Phone = phone;
            contacter.Fax = fax;
            return contacter;
        }


        public GuestForSubmitHotelOrder toGuestForSubmitHotelOrder() {
            GuestForSubmitHotelOrder guest = new GuestForSubmitHotelOrder();
            guest.Email = this.email;
            guest.GenderCode = this.genderCode;
            guest.IdNumber = this.idNumber;
            guest.IdTypeCode = this.idTypeCode;
            guest.Mobile = this.mobile;
            guest.Name = ObjectUtil.Parse(this.nameCn, this.nameEn);
            guest.Nationality = this.nationality;
             PhoneForSubmitHotelOrder phone = new PhoneForSubmitHotelOrder();
            phone.AreaCode =  this.phoneAreaCode;
            phone.InterCode = this.phoneInterCode;
            phone.Ext = this.phoneExt;
            phone.Nmber =this.phoneNumber;
            FaxForSubmitHotelOrder fax = new FaxForSubmitHotelOrder();
            fax.AreaCode = this.faxAreaCode;
            fax.InterCode = this.faxInterCode;
            fax.Ext = this.faxExt;
            fax.Nmber = this.phoneNumber;
            guest.Fax = fax;
            guest.Phone=phone;
   
            return guest;
        
        }

       
    }
}
