using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IOSystem.Models
{
    public class OrgRegister
    {
        [Key]
        public Int64 RegisterID { get; set; }
        public Int64 SrNo { get; set; }
        public string RegisterName { get; set; }
        public string RTypeName { get; set; }
        public Int64 OrgID { get; set; }
        public string DeleteFlag { get; set; }
    }
}
