using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IOSystem.Models
{
    public class Category
    {
        [Key]
        public Int64 CategoryID { get; set; }
        public Int64 SrNo { get; set; }
        public string CategoryName { get; set; }
        public string DeleteFlag { get; set; }
    }
}
