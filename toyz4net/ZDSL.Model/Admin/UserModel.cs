using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Client;
using Toyz4net.Core.Model;

namespace ZDSL.Model.Admin
{
    public class UserModel : BaseModel
    {

        public string id { get; set; }
        public string pwd { get; set; }
        public string name { get; set; }
        public string roleFk { get; set; }
        public string email { get; set; }


        public override object getPk()
        {
            return this.id;
        }

        public override void setPk(object pk)
        {
            this.id = pk.ToString();
        }
       
    }
}
