using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class UserEntity
    {
        
            public int id { get; set; }
            public string name { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public string type { get; set; }
            public string workplace { get; set; }
            public String email { set; get; }
            public String phonenumber { set; get; }
            public int age { get; set; }
            public DateTime dob { get; set; }
            public string edu { get; set; }
            public string language { get; set; }
            public int toeic { get; set; }
            public string exp { get; set; }
            public string department { get; set; }
            public string location { get; set; }

        
    }
}