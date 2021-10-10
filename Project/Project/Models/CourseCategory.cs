using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class CourseCategory
    {
        public int id { get; set; }

        //---------------------
        [DisplayName("Name")]
        [Required(ErrorMessage = "Name can not be null!!")]
        public string name { get; set; }

        //---------------------
        [DisplayName("Description")]
        [Required(ErrorMessage = "Description can not be null!!")]
        public string description { get; set; }


        //----------------------many -> one relationship------------------------------------
        public List<Course> courseowned { get; set; }
        public CourseCategory()
        {
            courseowned = new List<Course>();
        }

        public string ToSeparatedString(string r)
        {
            return $"{this.id}{r}" +
                    $"{this.name}{r}" +
                    $"{this.description}";
        }
        public override string ToString()
        {
            return string.Format("id:{0}; name:{1}; describe:{2}", this.id, this.name, this.description);
        }
    }
}
