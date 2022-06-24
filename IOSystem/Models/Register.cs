using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IOSystem.Models
{
    public class Register
    {
        [Key]
        public Int64 RegisterID { get; set; }
        public Int64 SrNo { get; set; }
        public string RegisterName { get; set; }
        public string RType { get; set; }
        public string DeleteFlag { get; set; }
    }
}
