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
        [DisplayName("Name")]
        [Required(ErrorMessage = "Name can not be null!!")]
        public string name { get; set; }
        //---------------------
        [DisplayName("Type")]
        [Required(ErrorMessage = "Type can not be null!!")]
        public bool type { get; set; }
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
            return $"{this.id}{r}" +
                    $"{this.name}{r}" +
                    $"{this.type}{r}" +
                    $"{this.workplace}{r}" +
                    $"{this.phonenumber}{r}" +
                    $"{this.email}";
        }
        public override string ToString()
        {
            return string.Format("id:{0}; name: {1}; type:{2}; workplace: {3}; phonenumber: {4}; email: {5}",
                this.id, this.name, this.type, this.workplace, this.phonenumber, this.email);
        }
    }
}