using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Model;

namespace ZDSL.Model.Public
{
    public abstract  class BaseHotModel:BaseModel
    {
        public int priorityLevel { get; set; }
        public string remark { get; set; }
    }
}
