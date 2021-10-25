
using Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Course 
    {
        public int id { get; set; }

        //------------------
        [DisplayName("Name")]
        [Required(ErrorMessage = "Name can not be null!!")]
        public string name { get; set; }

        //------------------
        [DisplayName("Description")]
        [Required(ErrorMessage = "Description can not be null!!")]
        public string description { get; set; }

        [DisplayName("Category")]
        public int CourseCategoryID { get; set; }
        public CourseCategory CourseCategory { get; set; }

        //-----------------------------
        public List<CustomUser> people { get; set; }

        //------------------------------
        public string ToSeparatedString(string r)
        {
            return $"{this.id}{r}" +
                    $"{this.name}{r}" +
                    $"{this.description}{r}" +
                    $"{this.CourseCategory.name}";
            


        }

        public override string ToString()
        {
            return string.Format("id:{0}; Name: {1}; Description:{2}; Category:{3}", this.id, this.name, this.description,this.CourseCategory);
        }

    }
}