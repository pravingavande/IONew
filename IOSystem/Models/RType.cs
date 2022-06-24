using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOSystem.Models
{
    public class RType
    {
        public string RTypeID { get; set; }
        public string RTypeName { get; set; }
        public List<SelectListItem> RTypeList { set; get; }
    }
}
