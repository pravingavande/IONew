using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IOSystem.Models
{
    public class IRegister
    {
        [Key]
        public Int64 IRID { get; set; }
        public Int64 IRNo { get; set; }

        //public DateTime IRDate { get; set; } = DateTime.Now;
        public DateTime? IRDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Letter No is required.")]
        public string LetterNo { get; set; }
        public DateTime LetterDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Receive From  is required.")]
        public string SchoolName { get; set; } = "-";

        public string MobileNo { get; set; } = "0";
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Subject Name is required.")]
        public string SubjectName { get; set; } = "-";
        public Int64 EmpID { get; set; }
        public Int64 SectionID { get; set; }

        [Required(ErrorMessage = "Letter Category is required.")]
        public Int64 LetterCategoryID { get; set; }
        public Int64 Duration { get; set; } = 0;
        public string Remark { get; set; } = "-";
        public Int64 StatusID { get; set; }
        public Int64 ORID { get; set; }
        public DateTime TDate { get; set; } = DateTime.Now;
        public string ORRemark { get; set; } = "-";
        public Int64 FYID { get; set; }
        public Int64 RegisterID { get; set; }
        public Int64 OrgID { get; set; }
        public Int64 LoginEmpID { get; set; }
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;

        [NotMapped]
        [Required(ErrorMessage = "Section & Employee Name is required.")]
        public Int64 ESDID { get; set; }
    }
}
