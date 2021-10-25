using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class LoginForm
    {
        [Required]
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string Password { get; set; }

    }

    public class RegisterForm
    {
        [Required]
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Confirm password is not match !!!")]
        public string ConfirmPassword { get; set; }
    }

    public class TrainerRegisterForm
    {
        [Required]
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Confirm password is not match !!!")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string type { get; set; }

        [Required]
        [Display(Name = "Work Place")]
        public string workplace { get; set; }

        [Required(ErrorMessage = "Phone number can not be null!!")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class TraineeRegisterForm
    {
        [Required(ErrorMessage = "Username can not be null!!")]
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Password can not be null and more than 5 characters!")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Confirm password is not match !!!")]
        public string ConfirmPassword { get; set; }
        //----------------------------------
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        //-------------------------------------
        [Display(Name = "Age")]
        [Required(ErrorMessage = "Age can not be null!!")]
        public int age { get; set; }
        //--------------------------------------
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date of Birth can not be null!!")]
        public DateTime dob { get; set; }
        //--------------------------------------
        [Display(Name = "Education")]
        [Required(ErrorMessage = "Education can not be null!!")]
        public string edu { get; set; }
        //--------------------------------------
        [Display(Name = "Progaming Language")]
        [Required(ErrorMessage = "Progaming Language can not be null!!")]
        public string language { get; set; }
        //--------------------------------------
        [Display(Name = "TOEIC")]
        [Required(ErrorMessage = "TOEIC can not be null!!")]
        public int toeic { get; set; }
        //--------------------------------------
        [Display(Name = "Experience")]
        [Required(ErrorMessage = "Experience can not be null!!")]
        public string exp { get; set; }
        //--------------------------------------
        [Display(Name = "Department")]
        [Required(ErrorMessage = "Department can not be null!!")]
        public string department { get; set; }
        //--------------------------------------
        [Display(Name = "Location")]
        [Required(ErrorMessage = "Location can not be null!!")]
        public string location { get; set; }
        //--------------------------------------
    }

    public class ChangePass
    {
        [Required(ErrorMessage = "Current password can not be null!")]
        [Display(Name ="Current password")]
        [DataType(DataType.Password)]
        public string currentpass { get; set; }

        [Required(ErrorMessage = "Password can not be null and more than 5 characters!")]
        [DataType(DataType.Password)]
        [Display(Name ="New PassWord")]
        public string newpass { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Confirm password")]
        [Compare("newpass",ErrorMessage ="Confirm password does not match.")]
        public string confirmpass { get; set; }
    }

}