using Microsoft.EntityFrameworkCore;
using Students_portal.Models;

namespace Students_portal.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Student> StudentsTable { get; set; }
        public DbSet<Faculty> FacultiesTable { get; set; }
        public DbSet<Department> DepartmentsTable { get; set; }
        public DbSet<Course> CoursesTable { get; set; }
        public DbSet<Result> ResultsTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Faculty -> Departments
            modelBuilder.Entity<Department>()
                .HasOne(d => d.Faculty)
                .WithMany(f => f.Departments)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Department -> Students
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Department -> Courses
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Student -> Results
            modelBuilder.Entity<Result>()
                .HasOne(r => r.Student)
                .WithMany(s => s.Results)
                .HasForeignKey(r => r.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Course -> Results
            modelBuilder.Entity<Result>()
                .HasOne(r => r.Course)
                .WithMany(c => c.Results)
                .HasForeignKey(r => r.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Store enums as readable strings instead of ints (optional)
            modelBuilder.Entity<Student>()
                .Property(s => s.Gender).HasConversion<string>().HasMaxLength(10);
            modelBuilder.Entity<Student>()
                .Property(s => s.Status).HasConversion<string>().HasMaxLength(15);
            modelBuilder.Entity<Student>()
                .Property(s => s.MatriculationNumber).IsRequired(false);
            modelBuilder.Entity<Course>()
                .Property(c => c.Semester).HasConversion<string>().HasMaxLength(10);
            modelBuilder.Entity<Result>()
                .Property(r => r.Semester).HasConversion<string>().HasMaxLength(10);
        }
    }
}
