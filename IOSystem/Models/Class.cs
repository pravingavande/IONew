using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IOSystem.Models
{
    public class Class
    {
        [Key]
        public Int64 ClassID { get; set; }
        public Int64 SrNo { get; set; }
        public string ClassName { get; set; }
        public string DeleteFlag { get; set; }
    }
}
