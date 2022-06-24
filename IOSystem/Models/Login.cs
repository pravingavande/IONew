using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IOSystem.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Please Enter UserName")]
        public string EmpUserName { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        public string EmpPassword { get; set; }
    }
}
