using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IOSystem.Models
{
    public class ORegister
    {
        [Key]
        public Int64 ORID { get; set; }
        public Int64 ORNo { get; set; }

        //public DateTime ORDate { get; set; } = DateTime.Now;
        public DateTime? ORDate { get; set; }
        public string SubjectName { get; set; }
        public string SchoolName { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public Int64 EmpID { get; set; }
        public Int64 SectionID { get; set; }
        public Int64 LetterCategoryID { get; set; }
        public string Remark { get; set; }
        public Int64 IRNo { get; set; }
        public Int64 IRID { get; set; }
        public Int64 FYID { get; set; }
        public Int64 RegisterID { get; set; }
        public Int64 OrgID { get; set; }
        public Int64 LoginEmpID { get; set; }
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;
    }
}
