using Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project.EF
{
    public class TrainingContext: DbContext
    {
        public TrainingContext() : base("BwConnection")
        {

        }

        public DbSet<CourseCategory> categories { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<Trainer> trainers { get; set; }
        public DbSet<Trainee> trainees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trainer>().ToTable("Trainer");
            modelBuilder.Entity<Trainer>().HasKey<int>(b => b.id);
            modelBuilder.Entity<Trainer>().Property(b => b.name).HasColumnType("varchar").HasMaxLength(50);
            modelBuilder.Entity<Trainer>().Property(b => b.email).HasColumnType("varchar").HasMaxLength(30);
            modelBuilder.Entity<Trainer>().Property(b => b.phonenumber).HasColumnType("varchar").HasMaxLength(12);
            modelBuilder.Entity<Trainer>().Property(b => b.workplace).HasColumnType("varchar").HasMaxLength(15);

            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Course>().HasKey<int>(r => r.id);
            modelBuilder.Entity<Course>().Property(r => r.name).HasColumnType("varchar").HasMaxLength(50);

            modelBuilder.Entity<CourseCategory>().ToTable("Category");
            modelBuilder.Entity<CourseCategory>().HasKey<int>(r => r.id);
            modelBuilder.Entity<CourseCategory>().Property(r => r.name).HasColumnType("varchar").HasMaxLength(50);

            modelBuilder.Entity<Trainee>().ToTable("Trainee");
            modelBuilder.Entity<Trainee>().HasKey<int>(s => s.id);
            modelBuilder.Entity<Trainee>().Property(s => s.name).HasColumnType("varchar").HasMaxLength(50);
            modelBuilder.Entity<Trainee>().Property(s => s.username).HasColumnType("varchar").HasMaxLength(50);
            modelBuilder.Entity<Trainee>().Property(s => s.edu).HasColumnType("varchar").HasMaxLength(50);
            modelBuilder.Entity<Trainee>().Property(s => s.language).HasColumnType("varchar").HasMaxLength(30);
            modelBuilder.Entity<Trainee>().Property(s => s.location).HasColumnType("varchar").HasMaxLength(50);
            modelBuilder.Entity<Trainee>().Property(s => s.department).HasColumnType("varchar").HasMaxLength(50);
        }
    }
}