using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IOSystem.Models
{
    public class Designation
    {
        [Key]
        public Int64 DesignationID { get; set; }
        public Int64 SrNo { get; set; }
        public string DesignationName { get; set; }
        public Int64 OrgID { get; set; }
        public string DeleteFlag { get; set; }
    }
}
