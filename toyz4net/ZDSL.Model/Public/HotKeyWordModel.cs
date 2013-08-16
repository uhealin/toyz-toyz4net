using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Model;

namespace ZDSL.Model.Public
{
    public  class HotKeywordModel:BaseModel
    {
        private static char CHAR_SPAR = '|';
        public string id { get; set; }
        public string brandNameArray { get; set; }
        public string clsNameArray { get; set; }
        public string dsNameArray { get; set; }
        public string llsNameArray { get; set; }
        public string trffNameArray { get; set; }
        public string metroNameArray { get; set; }

        public override object getPk()
        {
            return this.id;
        }

        public override void setPk(object obj)
        {
            this.id = obj.ToString();
        }

        public string[] getBrandNames() {
            if (string.IsNullOrEmpty(this.brandNameArray)) {
                return new string[] { };
            }
            return this.brandNameArray.Split(CHAR_SPAR);
        }

        public string[] getTrffNames()
        {
            if (string.IsNullOrEmpty(this.trffNameArray))
            {
                return new string[] { };
            }
            return this.trffNameArray.Split(CHAR_SPAR);
        }

        public string[] getMetroNames()
        {
            if (string.IsNullOrEmpty(this.metroNameArray))
            {
                return new string[] { };
            }
            return this.metroNameArray.Split(CHAR_SPAR);
        }

        public string[] getClNames()
        {
            if (string.IsNullOrEmpty(this.clsNameArray))
            {
                return new string[] { };
            }
            return this.clsNameArray.Split(CHAR_SPAR);
        }

        public string[] getDNames()
        {
            if (string.IsNullOrEmpty(this.dsNameArray))
            {
                return new string[] { };
            }
            return this.dsNameArray.Split(CHAR_SPAR);
        }

        public string[] getLlNames()
        {
            if (string.IsNullOrEmpty(this.llsNameArray))
            {
                return new string[] { };
            }
            return this.llsNameArray.Split(CHAR_SPAR);
        }
    }
}
