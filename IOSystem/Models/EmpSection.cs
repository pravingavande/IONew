using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IOSystem.Models
{
    public class EmpSection
    {
        [Key]
        public string EmpSectionName { get; set; }
    }
}
