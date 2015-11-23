using Contoso_Uni.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Configuration;


namespace Contoso_Uni.Models {
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class Contoso_UniContext : DbContext {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
        public class MyConfiguration : DbMigrationsConfiguration<Contoso_UniContext> {
            public MyConfiguration() {
                this.AutomaticMigrationsEnabled = true;
            }

            // This code below is used to create and Populate a Database. //
            protected override void Seed(Contoso_UniContext context) {
                var students = new List<Student>
                    {
                    new Student { FirstMidName = "Carson",   LastName = "Alexander",
                        EnrollmentDate = DateTime.Parse("2010-09-01") },
                    new Student { FirstMidName = "Meredith", LastName = "Alonso",
                        EnrollmentDate = DateTime.Parse("2012-09-01") },
                    new Student { FirstMidName = "Arturo",   LastName = "Anand",
                        EnrollmentDate = DateTime.Parse("2013-09-01") },
                    new Student { FirstMidName = "Gytis",    LastName = "Barzdukas",
                        EnrollmentDate = DateTime.Parse("2012-09-01") },
                    new Student { FirstMidName = "Yan",      LastName = "Li",
                        EnrollmentDate = DateTime.Parse("2012-09-01") },
                    new Student { FirstMidName = "Peggy",    LastName = "Justice",
                        EnrollmentDate = DateTime.Parse("2011-09-01") },
                    new Student { FirstMidName = "Laura",    LastName = "Norman",
                        EnrollmentDate = DateTime.Parse("2013-09-01") },
                    new Student { FirstMidName = "Nino",     LastName = "Olivetto",
                        EnrollmentDate = DateTime.Parse("2005-08-11")}
                };
                students.ForEach(s => context.Student.AddOrUpdate(p => p.LastName, s));
                context.SaveChanges();

                var courses = new List<Course>
                    {
                    new Course {CourseID = 1050, Title = "Chemistry",      Credits = 3, },
                    new Course {CourseID = 4022, Title = "Microeconomics", Credits = 3, },
                    new Course {CourseID = 4041, Title = "Macroeconomics", Credits = 3, },
                    new Course {CourseID = 1045, Title = "Calculus",       Credits = 4, },
                    new Course {CourseID = 3141, Title = "Trigonometry",   Credits = 4, },
                    new Course {CourseID = 2021, Title = "Composition",    Credits = 3, },
                    new Course {CourseID = 2042, Title = "Literature",     Credits = 4, }
                };
                courses.ForEach(s => context.Course.AddOrUpdate(p => p.Title, s));
                context.SaveChanges();

                var enrollments = new List<Enrollment>
                    {
                    new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Alexander").ID,
                        CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                        Grade = Grade.A
                    },
                     new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Alexander").ID,
                        CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                        Grade = Grade.C
                     },
                     new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Alexander").ID,
                        CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                        Grade = Grade.B
                     },
                     new Enrollment {
                         StudentID = students.Single(s => s.LastName == "Alonso").ID,
                        CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                        Grade = Grade.B
                     },
                     new Enrollment {
                         StudentID = students.Single(s => s.LastName == "Alonso").ID,
                        CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                        Grade = Grade.B
                     },
                     new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Alonso").ID,
                        CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                        Grade = Grade.B
                     },
                     new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Anand").ID,
                        CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID
                     },
                     new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Anand").ID,
                        CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
                        Grade = Grade.B
                     },
                    new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Barzdukas").ID,
                        CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                        Grade = Grade.B
                     },
                     new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Li").ID,
                        CourseID = courses.Single(c => c.Title == "Composition").CourseID,
                        Grade = Grade.B
                     },
                     new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Justice").ID,
                        CourseID = courses.Single(c => c.Title == "Literature").CourseID,
                        Grade = Grade.B
                     }
                };

                foreach (Enrollment e in enrollments) {
                    var enrollmentInDataBase = context.Enrollment.Where(
                        s =>
                             s.Student.ID == e.StudentID &&
                             s.Course.CourseID == e.CourseID).SingleOrDefault();
                    if (enrollmentInDataBase == null) {
                        context.Enrollment.Add(e);
                    }
                }
                context.SaveChanges();
            }
        }
        public System.Data.Entity.DbSet<Contoso_Uni.Models.Enrollment> Enrollment { get; set; }
        public System.Data.Entity.DbSet<Contoso_Uni.Models.Student> Student { get; set; }
        public System.Data.Entity.DbSet<Contoso_Uni.Models.Course> Course { get; set; }

        public Contoso_UniContext() : base("name=Contoso_UniContext") {

            if (!Database.Exists("Contoso_UniContext")) {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<Contoso_UniContext, MyConfiguration>());
            }
        }
    }
}
