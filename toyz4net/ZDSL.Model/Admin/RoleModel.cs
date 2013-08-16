using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Model;

namespace ZDSL.Model.Admin
{
    public  class RoleModel :BaseModel
    {

        public string id { get; set; }
        public string name { get; set; }

        public string menuFkArray { get; set; }


        public override object getPk()
        {
            return this.id;
        }

        public override void setPk(object pk)
        {
            this.id = pk.ToString();
        
        }


        public string[] getArrayMenuFk() {
            if (string.IsNullOrEmpty(this.menuFkArray)) {
                return new string[] { };
            }
            return this.menuFkArray.Split(',');
        }
    }
}
