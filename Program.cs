
using System;
using System.Linq;
using System.Collections.Generic;
namespace WorkingWithLinqMethodSyntax
{
    public class Program
    {
        public static void Main()
        {
            // Student collection
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "Frank Furter", Age = 55, Major="Hospitality", Tuition=3500.00} ,
                new Student() { StudentID = 2, StudentName = "Gina Host", Age = 21, Major="Hospitality", Tuition=4500.00 } ,
                new Student() { StudentID = 3, StudentName = "Cookie Crumb",  Age = 21, Major="CIT", Tuition=2500.00 } ,
                new Student() { StudentID = 4, StudentName = "Ima Script",  Age = 48, Major="CIT", Tuition=5500.00 } ,
                new Student() { StudentID = 5, StudentName = "Cora Coder",  Age = 35, Major="CIT", Tuition=1500.00 } ,
                new Student() { StudentID = 6, StudentName = "Ura Goodchild" , Age = 40, Major="Marketing", Tuition=500.00} ,
                new Student() { StudentID = 7, StudentName = "Take Mewith" , Age = 29, Major="Aerospace Engineering", Tuition=5500.00 }
            };

            // Student GPA Collection
            IList<StudentGPA> studentGPAList = new List<StudentGPA>() {
                new StudentGPA() { StudentID = 1,  GPA=4.0} ,
                new StudentGPA() { StudentID = 2,  GPA=3.5} ,
                new StudentGPA() { StudentID = 3,  GPA=2.0 } ,
                new StudentGPA() { StudentID = 4,  GPA=1.5 } ,
                new StudentGPA() { StudentID = 5,  GPA=4.0 } ,
                new StudentGPA() { StudentID = 6,  GPA=2.5} ,
                new StudentGPA() { StudentID = 7,  GPA=1.0 }
            };

            // Club collection
            IList<StudentClubs> studentClubList = new List<StudentClubs>() {
            new StudentClubs() {StudentID=1, ClubName="Photography" },
            new StudentClubs() {StudentID=1, ClubName="Game" },
            new StudentClubs() {StudentID=2, ClubName="Game" },
            new StudentClubs() {StudentID=5, ClubName="Photography" },
            new StudentClubs() {StudentID=6, ClubName="Game" },
            new StudentClubs() {StudentID=7, ClubName="Photography" },
            new StudentClubs() {StudentID=3, ClubName="PTK" },
            };

            //a) Group by GPA and display the students' IDs using LINQ query Method Syntax and display the results
            var groupedByGPA = studentGPAList.GroupBy(s => s.GPA)
                .OrderBy(g => g.Key);

            Console.WriteLine();
            Console.WriteLine("Students Grouped by GPA: ");
            Console.WriteLine();
            foreach (var group in groupedByGPA)
            {
                Console.WriteLine($"GPA: {group.Key} ");
                foreach (var s in group)
                {
                    Console.WriteLine($"Student ID: {s.StudentID}");
                }
                Console.WriteLine("----------------------------------------------");
            }

            //b) Sort by Club, then group by Club and display the student's IDs using LINQ query Method Syntax and display the results
            var sortedAndGroupedByClub = studentClubList.OrderBy(c => c.ClubName)
                .GroupBy(c => c.ClubName);

            Console.WriteLine();
            Console.WriteLine("Students Grouped by Club: ");
            Console.WriteLine();
            foreach (var group in sortedAndGroupedByClub)
            {
                Console.WriteLine($"Club: {group.Key}");
                foreach (var c in group)
                {
                    Console.WriteLine($"Student ID: {c.StudentID}");
                }
                Console.WriteLine("----------------------------------------------");
            }

            //c) Count the number of students with a GPA between 2.5 and 4.0 using LINQ query Method Syntax and display the results
            int count = studentGPAList.Count(s => s.GPA >= 2.5 && s.GPA <= 4.0);
            Console.WriteLine();
            Console.WriteLine("Number of students with GPA between 2.5 and 4.0: " + count);
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------");

            //d) Average all student's tuition using LINQ query Method Syntax and display the results
            double averageTuition = studentList.Average(s => s.Tuition);
            Console.WriteLine();
            Console.WriteLine("Average Tuition: " + averageTuition);
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------");

            //e) Using LINQ query Method Syntax, find the student paying the most tuition and display their name, major and tuition.
            Student studentWithMaxTuition = studentList.OrderByDescending(s => s.Tuition).FirstOrDefault();
            if (studentWithMaxTuition != null)
            {
                Console.WriteLine();
                Console.WriteLine("Student with the highest tuition:");
                Console.WriteLine();
                Console.WriteLine("Name: " + studentWithMaxTuition.StudentName);
                Console.WriteLine("Major: " + studentWithMaxTuition.Major);
                Console.WriteLine("Tuition: " + studentWithMaxTuition.Tuition);
                Console.WriteLine("----------------------------------------------");
            }

            //f) Using LINQ query Method Syntax, join the student list and student GPA list on student ID and display the student's name, major and gpa
            IEnumerable<string> studentInfo = studentList.Join(studentGPAList, s => s.StudentID, g => g.StudentID, (s, g) => "Student ID: " + s.StudentID + " - Name: " + s.StudentName + " - Major: " + s.Major + " - GPA: " + g.GPA);

            Console.WriteLine();
            Console.WriteLine("Student Information: ");
            Console.WriteLine();
            foreach (string s in studentInfo)
            {
                Console.WriteLine(s);
                Console.WriteLine("----------------------------------------------");
            }

            //g) Using LINQ query Method Syntax, join the student list and student club list. Display the names of only those students who are in the Game club.
            IEnumerable<string> gameClubStudents = studentList.Join(studentClubList, s => s.StudentID, c => c.StudentID, (s, c) => new { Student = s, Club = c })
                .Where(sc => sc.Club.ClubName == "Game")
                .Select(sc => "Student ID: " + sc.Student.StudentID + " - Name: " + sc.Student.StudentName);

            Console.WriteLine();
            Console.WriteLine("Students in the Game Club: ");
            Console.WriteLine();
            foreach (string s in gameClubStudents)
            {
                Console.WriteLine(s);
                Console.WriteLine("----------------------------------------------");
            }
        }
    }
 
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
        public string Major { get; set; }
	    public double Tuition {get;set;}
    }

    public class StudentClubs
    {
        public int StudentID { get; set; }
        public string ClubName { get; set; }
    }

    public class StudentGPA
    {
        public int StudentID { get; set;}
        public double GPA    { get; set;}
    }      
}