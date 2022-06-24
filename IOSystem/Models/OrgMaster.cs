using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IOSystem.Models
{
    public class OrgMaster
    {
        [Key]
        public Int64 OrgID { get; set; }
        public string OrgName { get; set; }
        public string OrgNameShort { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNo { get; set; }
        public string Website { get; set; }
        public string EmailID { get; set; }
        public Int64 OrgTypeID { get; set; }
        public Int64 OrgIDMain { get; set; }
        public string DeleteFlag { get; set; }
        public string IRNoManual { get; set; }
        public string ORNoManual { get; set; }
    }
}
