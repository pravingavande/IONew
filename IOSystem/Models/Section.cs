using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IOSystem.Models
{
    public class Section
    {
        [Key]
        public Int64 SectionID { get; set; }
        public string SectionName { get; set; }
        public Int64 OrgID { get; set; }
        public string DeleteFlag { get; set; }
    }
}
