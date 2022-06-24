using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IOSystem.Models
{
    public class OrgFY
    {
        [Key]
        public Int64 FYID { get; set; }
        public Int64 SrNo { get; set; }
        public string FyName { get; set; }
        public DateTime FromDate { get; set; } = DateTime.Now;
        public DateTime ToDate { get; set; } = DateTime.Now;
        public Int64 OrgID { get; set; }
        public string DeleteFlag { get; set; }
    }
}
