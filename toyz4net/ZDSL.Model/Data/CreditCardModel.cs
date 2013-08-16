using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Model;
using Toyz4net.Core.NorthBoundAPIService;


namespace ZDSL.Model.Data
{
     public class CreditCardModel :BaseModel
    {

         public string number { get; set; }
         public string veryfyCode { get; set; }
         public int veryfyYear { get; set; }
         public int veryfyMonth { get; set; }
         public string holderName { get; set; }
         public string holderIdType { get; set; }
         public string holderIdNo { get; set; }


         public override object getPk()
         {
             return this.number;
         }

         public override void setPk(object pk)
         {
             this.number = pk.ToString();
         }

         public CreditCardForSubmitHotelOrder toCreditCardForSubmitHotelOrder()
         {
             CreditCardForSubmitHotelOrder creditCard = new CreditCardForSubmitHotelOrder();
             creditCard.Number = this.number;
             creditCard.VeryfyCode = this.veryfyCode;
             creditCard.ValidYear = this.veryfyYear;
             creditCard.ValidMonth = this.veryfyMonth;
             creditCard.CardHolderName = this.holderName;
             creditCard.IdNumber = this.holderIdNo;
             creditCard.IdTypeCode = this.holderIdType;
             creditCard.Number = this.number;
             return creditCard;
         }


    }
}
