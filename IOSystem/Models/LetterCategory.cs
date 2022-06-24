using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IOSystem.Models
{
    public class LetterCategory
    {
        [Key]
        public Int64 LetterCategoryID { get; set; }
        public Int64 SrNo { get; set; }
        public string LetterCategoryName { get; set; }
        public Int64 OrgID { get; set; }
        public string DeleteFlag { get; set; }
    }
}
