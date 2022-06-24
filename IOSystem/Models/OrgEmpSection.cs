using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IOSystem.Models
{
    public class OrgEmpSection
    {
        [Key]
        public Int64 ESDID { get; set; }
        public Int64 EmpID { get; set; }
        public Int64 SectionID { get; set; }

        [NotMapped]
        public String EmpSectionName { get; set; }
    }
}
