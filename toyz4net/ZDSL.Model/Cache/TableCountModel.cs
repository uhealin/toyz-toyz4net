using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Model;

namespace ZDSL.Model.Cache
{
    public class TableCountModel:BaseCacheModel
    {

        public int hotelAll { get; set; }
        public int hotelOk { get; set; }
        public int hotelClosed { get; set; }
        public int hotelDel { get; set; }
        public string id { get; set; }
        public int geoAll { get; set; }
        public int brandAll { get; set; }
        public int userAll { get; set; }
        public int memberAll { get; set; }
        public int newsAll { get; set; }
        public int exhibitionAll { get; set; }


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
