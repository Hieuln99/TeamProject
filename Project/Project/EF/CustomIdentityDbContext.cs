using Project.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project.EF
{
    public class CustomIdentityDbContext: IdentityDbContext<CustomUser>
    {
        public CustomIdentityDbContext() : base("BwConnection")
        {

        }
        public DbSet<CourseCategory> categories { get; set; }
        public DbSet<Course> courses { get; set; }
    }
}