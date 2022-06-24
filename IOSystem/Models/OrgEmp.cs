using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IOSystem.Models
{
    public class OrgEmp
    {
        [Key]
        public Int64 EmpID { get; set; }
        public Int64 SrNo { get; set; }
        public string EmpName { get; set; }
        public string EmpUserName { get; set; }
        public string EmpPassword { get; set; }
        public Int64 DesignationID { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string EmailID { get; set; }
        public Int64 EmpTypeID { get; set; }
        public Int64 OrgID { get; set; }
        public string DeleteFlag { get; set; }
    }
}
