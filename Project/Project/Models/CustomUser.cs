using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class CustomUser : IdentityUser
    {
        //public int Id { get; set; }
        //--------------------------------------
        [DisplayName("Name")]
        [Required(ErrorMessage = "Name can not be null!!")]
        public string name { get; set; }
        //-------------------------------------
        //--------------------------------------
        [DisplayName("Age")]
        [Required(ErrorMessage = "Age can not be null!!")]
        public int age { get; set; }
        //--------------------------------------
        [DisplayName("Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date of Birth can not be null!!")]
        public DateTime dob { get; set; }
        //--------------------------------------
        [DisplayName("Education")]
        [Required(ErrorMessage = "Education can not be null!!")]
        public string edu { get; set; }
        //--------------------------------------
        [DisplayName("Progaming Language")]
        [Required(ErrorMessage = "Progaming Language can not be null!!")]
        public string language { get; set; }
        //--------------------------------------
        [DisplayName("TOEIC")]
        [Required(ErrorMessage = "TOEIC can not be null!!")]
        public int toeic { get; set; }
        //--------------------------------------
        [DisplayName("Experience")]
        [Required(ErrorMessage = "Experience can not be null!!")]
        public string exp { get; set; }
        //--------------------------------------
        [DisplayName("Department")]
        [Required(ErrorMessage = "Department can not be null!!")]
        public string department { get; set; }
        //--------------------------------------
        [DisplayName("Location")]
        [Required(ErrorMessage = "Location can not be null!!")]
        public string location { get; set; }
        //--------------------------------------
        [DisplayName("Type")]
        [Required(ErrorMessage = "Type can not be null!!")]
        public string type { get; set; }
        //---------------------
        [DisplayName("Work Place")]
        [Required(ErrorMessage = "Work place can not be null!!")]
        public string workplace { get; set; }

        [DisplayName("Role")]
        [Required(ErrorMessage = "Role can not be null!!")]
        public string Role { get; set; }

        //---------------------

        //---------------------------------------many -> many relationship---------------------------------
        public CustomUser()
        {
            courses = new List<Course>();
        }
        public List<Course> courses { get; set; }


        public string ToSeparated(string r)
        {
            return $"{this.Id}{r}" +
                    $"{this.name}{r}" +
                    $"{this.age}{r}" +
                    $"{this.dob}{r}" +
                    $"{this.edu}{r}" +
                    $"{this.language}{r}" +
                    $"{this.toeic}{r}" +
                    $"{this.exp}{r}" +
                    $"{this.department}{r}" +
                    $"{this.location}";
        }

        /*public string ToSeparatedt(string r)
        {
            return $"{this.Id}{r}" +
                    $"{this.name}{r}" +
                    $"{this.UserName}{r}" +
                    $"{this.type}{r}" +
                    $"{this.workplace}{r}" +
                    $"{this.PhoneNumber}{r}";
                    
        }*/

        public string ToSeparatedt(string r)
        {
            return $"{this.Id}{r}" +
                    $"{this.name}{r}" +
                    $"{this.workplace}{r}" +
                    $"{this.type}{r}" +
                    $"{this.PhoneNumber}{r}" +
                    $"{this.Email}";
        }

        public string ToSeparateds(string r)
        {
            return $"{this.Id}{r}" +
                    $"{this.name}{r}" +
                    $"{this.Email}";
        }



        public override string ToString()
        {
            return string.Format(" name:{0}; username:{1}; password:{2}; type:{3}; workplace: {4}; phonenumber: {5}; email: {6}; age:{7}; dateofbirth:{8}; education:{9}; language:{10}; toeic: {11}; experience:{12}; department:{13}; location:{14}; Role:{15}; Id:{15}",
             this.name, this.UserName, this.PasswordHash, this.type, this.workplace, this.PhoneNumber, this.Email, this.dob, this.edu, this.language, this.toeic, this.exp, this.department, this.location,this.Role, this.Id);
        }
    }
}