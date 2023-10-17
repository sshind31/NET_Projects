using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoginRegister.Models
{
    public class LoginUser
    {
        [DisplayName("Login name")]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "This field is compulsary")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Remeber Me")]
        public bool RemeberMe { get; set; }

    }
}