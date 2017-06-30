using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project2016.Models
{
    public class UserModel
    {   
        [Required(ErrorMessage ="Pleas enter Username")]        
        public string Username;

        [Required(ErrorMessage ="Please enter Password")]
        public string Password;
        public int Active;
    }
}