using Microsoft.EntityFrameworkCore;
using School_Portal.BE.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Portal.BE.Core.Infrastructure.Persistence
{
    public class SchoolContext: DbContext
    {
        //public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<Grade> Grades { get; set; }

        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(b =>
            {
                b.HasKey(s => s.Id);
                b.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(255); 

                b.HasMany(s => s.StudentSubjects)
                    .WithOne(ss => ss.Student)
                    .HasForeignKey(ss => ss.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(s => s.Grades)
                    .WithOne(g => g.Student)
                    .HasForeignKey(g => g.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Subject>(b =>
            {
                b.HasKey(s => s.Id);
                b.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(255);
                b.Property(s => s.IsRequired)
                    .IsRequired();
                b.Property(g => g.MinimumPassMark)
                    .IsRequired();


                b.HasMany(s => s.StudentSubjects)
                    .WithOne(ss => ss.Subject)
                    .HasForeignKey(ss => ss.SubjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(s => s.Grades)
                    .WithOne(g => g.Subject)
                    .HasForeignKey(g => g.SubjectId)
                    .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Grade>(b =>
            {
                b.HasKey(g => g.Id);
                b.Property(g => g.Value)
                    .IsRequired(); 
                b.Property(g => g.Note)
                    .HasMaxLength(500);

                b.HasOne(g => g.Student)
                    .WithMany(s => s.Grades)
                    .HasForeignKey(g => g.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(g => g.Subject)
                    .WithMany(s => s.Grades)
                    .HasForeignKey(g => g.SubjectId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<StudentSubject>(b =>
            {
                b.HasKey(ss => new { ss.StudentId, ss.SubjectId });

                b.HasOne(ss => ss.Student)
                    .WithMany(s => s.StudentSubjects)
                    .HasForeignKey(ss => ss.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(ss => ss.Subject)
                    .WithMany(s => s.StudentSubjects)
                    .HasForeignKey(ss => ss.SubjectId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, Name = "Alice" },
            new Student { Id = 2, Name = "Bob" }
        );

            modelBuilder.Entity<Subject>().HasData(
                new Subject { Id = 1, Name = "Math", MinimumPassMark = 65, IsRequired = true },
                new Subject { Id = 2, Name = "Geography", MinimumPassMark = 80, IsRequired = false }
            );

            modelBuilder.Entity<StudentSubject>().HasData(
                new StudentSubject { StudentId = 1, SubjectId = 1 },
                new StudentSubject { StudentId = 1, SubjectId = 2 },
                new StudentSubject { StudentId = 2, SubjectId = 1 }
            );

            modelBuilder.Entity<Grade>().HasData(
                 new Grade { Id = 1, StudentId = 1, SubjectId = 1, Value = 95, Note = "Excellent" },
                 new Grade { Id = 2, StudentId = 1, SubjectId = 2, Value = 88, Note = "Good" }
            );

        }
    }
}
