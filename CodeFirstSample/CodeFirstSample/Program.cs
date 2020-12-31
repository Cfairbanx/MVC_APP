using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class Program
    {
        public static void Main(string[] args)
        {
            using (var db = new SchoolContext())
            {
                Console.Write("Enter a Student's name.");
                var name = Console.ReadLine();

                var stud = new Student { StudentName = name };
                db.Students.Add(stud);
                db.SaveChanges();

                Console.Write("Student saved successfully!");

                var query = from b in db.Students
                            orderby b.StudentName
                            select b;

            Console.WriteLine("Students in the database: ");
            foreach (var item in query)
            {
                Console.WriteLine(item.StudentName);
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            }
        }      
    }

    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public byte[] Photo { get; set; }
        public decimal Height { get; set; }
        public float Weight { get; set; }

        public Grade Grade { get; set; }
    }

    public class Grade
    {
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public string Section { get; set; }

        public ICollection<Student> Students { get; set; }
    }

    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
    }


