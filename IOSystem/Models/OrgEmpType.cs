using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IOSystem.Models
{
    public class OrgEmpType
    {
        [Key]
        public Int64 EmpTypeID { get; set; }
        public string EmpTypeName { get; set; }
        public string DeleteFlag { get; set; }

    }
}
