using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Trainee
    {
        public int id { get; set; }
        //--------------------------------------
        [DisplayName("Name")]
        [Required(ErrorMessage = "Name can not be null!!")]
        public String name { get; set; }
        //--------------------------------------
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


        //---------------------------------------many -> many relationship---------------------------------
        public Trainee()
        {
            courses = new List<Course>();
        }
        public List<Course> courses { get; set; }

        //-------------------------------------------------------------------------------



        public string ToSeparatedString(string r)
        {
            return $"{this.id}{r}" +
                    $"{this.name}{r}" +
                    $"{this.username}{r}" +
                    $"{this.password}{r}" +
                    $"{this.age}{r}" +
                    $"{this.dob}{r}" +
                    $"{this.edu}{r}" +
                    $"{this.language}{r}" +
                    $"{this.toeic}{r}" +
                    $"{this.exp}{r}" +
                    $"{this.department}{r}" +
                    $"{this.location}";
        }
        public override string ToString()
        {
            return string.Format("id:{0}; name:{1}; username:{2}; password:{3}; age:{4}; dateofbirth:{5}; education:{6}; language:{7}; toeic: {8}; experience:{9}; department:{10}; location:{11}"
                , this.id, this.name, this.username, this.password,this.age, this.dob, this.edu, this.language, this.toeic, this.exp, this.department, this.location);
        }
    }
}