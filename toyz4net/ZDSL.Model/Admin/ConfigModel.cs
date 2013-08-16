using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Model;

namespace ZDSL.Model.Admin
{
    public  class ConfigModel:BaseModel
    {

        public string id { get; set; }
        public string autoOrderInd { get; set; }
        public string  staticInd{ get;set;}

        public override object getPk()
        {
            return id;
        }

        public override void setPk(object obj)
        {
            this.id = obj.ToString();
        }
    }
}
