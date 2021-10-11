using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Staff
    {
        public int id { get; set; }

        //--------------------------------------
        [DisplayName("Staff Name")]
        [Required(ErrorMessage = "Name can not be null!!")]
        public string name { get; set; }

        //--------------------------------------
        [DisplayName("Staff User Name")]
        [Required(ErrorMessage = "User Name can not be null!!")]
        public string username { get; set; }

        //--------------------------------------
        [DisplayName("Staff Password")]
        [Required(ErrorMessage = "Password can not be null!!")]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*[0-9])(?=.*[@$!%*#?&])[A-Za-z0-9@$!%*#?&]{8,}$",
        ErrorMessage = "Password must be 8 characters, one letter or non-letter and special character")]
        public string password { get; set; }

        public string ToSeparatedString(string r)
        {
            return $"{this.id}{r}" +
                    $"{this.name}{r}" +
                    $"{this.username}{r}" +
                    $"{this.password}";
        }
        public override string ToString()
        {
            return string.Format("id:{0}; name:{1}; username:{2}; password:{3}",
                this.id, this.name, this.username, this.password);
        }
    }
}