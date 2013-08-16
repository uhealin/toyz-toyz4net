using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Client;
using Toyz4net.Core.Model;

namespace ZDSL.Model.Admin
{
    public class DictModel: BaseModel
    {
       
        public string id { get; set; }
        public string text { get; set; }
        public string value { get; set; }
        public string type { get; set; }
        //public int status { get; set; }

        public override object getPk()
        {
            return this.id;
        }

        public override void setPk(object pk)
        {
            this.id = pk.ToString();
        }


        public void from(ELongBaseCodeAdapter basecode)
        {

            this.text = basecode.text;
            this.type = basecode.type;
            this.value = basecode.value;
            this.id = this.createPk().ToString();
        }


        public   object createPk() {
            return "dict::"+base.createPk();
        }


    }



    public class DictTypeModel : BaseModel
    {
        public string id
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }

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
