using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Trainer
    {
        public int id { get; set; }
        //---------------------
        [DisplayName("Trainer Name")]
        [Required(ErrorMessage = "Name can not be null!!")]
        public string name { get; set; }
        //---------------------
        [DisplayName("User Name")]
        [Required(ErrorMessage = "User Name can not be null!!")]
        public string username { get; set; }
        //--------------------------------------
        [DisplayName("Password")]
        [Required(ErrorMessage = "Password can not be null!!")]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*[0-9])(?=.*[@$!%*#?&])[A-Za-z0-9@$!%*#?&]{8,}$",
        ErrorMessage = "Password must be 8 characters, one letter or non-letter and special character")]

        public string password { get; set; }
        //--------------------------------------
        [DisplayName("Trainer Type")]
        [Required(ErrorMessage = "Type can not be null!!")]
        public string type { get; set; }
        //---------------------
        [DisplayName("Work Place")]
        [Required(ErrorMessage = "Work place can not be null!!")]
        public string workplace { get; set; }
        //---------------------
        [DisplayName("Email")]
        [Required(ErrorMessage = "Email can not be null!!")]
        [EmailAddress]
        public String email { set; get; }
        //---------------------
        [DisplayName("Telephone")]
        [Required(ErrorMessage = "Contact can not be null!!")]
        [RegularExpression(@"^([0-9]{10})$",
        ErrorMessage = "Phone number must be numbers and it has to have 10 numbers!!!!!!")]
        public String phonenumber { set; get; }

        //-----------------------------many - many -----------------\
        public Trainer()
        {
            courses = new List<Course>();
        }
        public List<Course> courses { get; set; }

        public string ToSeparatedString(string r)
        {
            return  $"{this.id}{r}" +
                    $"{this.name}{r}" +
                    $"{this.username}{r}" +
                    $"{this.password}{r}" +
                    $"{this.type}{r}" +
                    $"{this.workplace}{r}" +
                    $"{this.phonenumber}{r}" +
                    $"{this.email}";
        }
        public override string ToString()
        {
            return string.Format("id:{0}; name:{1}; username:{2}; password:{3}; type:{4}; workplace: {5}; phonenumber: {6}; email: {7}",
                this.id, this.name, this.username, this.password, this.type, this.workplace, this.phonenumber, this.email);
        }
    }
}