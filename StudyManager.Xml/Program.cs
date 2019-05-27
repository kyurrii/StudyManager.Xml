using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Collections;

namespace StudyManager.Xml
{
    public class Program
    {
       public static void Main(string[] args)
       {
            List<Student> Students = new List<Student>();
            List<Course> Courses = new List<Course>();
            List<Lecturer> Lecturers = new List<Lecturer>();
            List<HomeTask> HomeTasks = new List<HomeTask>();
            List<HomeTaskAssessment> HomeTaskAssessments = new List<HomeTaskAssessment>();

            Students.Add(new Student() { Id = 1101, Name = "Bill Collins", BirthDate = DateTime.Parse("08.08.1999"), PhoneNumber = "06733450123", Email = "billcol@gmail.com.", GitHubLink = "link1" });
            Students.Add(new Student() { Id = 1102, Name = "Ivan Vitrenko", BirthDate = DateTime.Parse("08.08.1998"), PhoneNumber = "0673345044", Email = "ivanvl@gmail.com.", GitHubLink = "link2" });

            Courses.Add(new Course() { Id = 1, Name = "Math", StartDate = DateTime.Parse("04.05.2019"), EndDate = DateTime.Parse("24.08.2019"), PassCredits = 95 });
            Courses.Add(new Course() { Id = 2, Name = "Phisics", StartDate = DateTime.Parse("07.05.2019"), EndDate = DateTime.Parse("30.10.2019"), PassCredits = 90 });

            Lecturers.Add(new Lecturer() { Id = 101, Name = "John", BirthDate = DateTime.Parse("08.08.1975") });
            Lecturers.Add(new Lecturer() { Id = 102, Name = "Robin", BirthDate = DateTime.Parse("07.07.1995") });

            HomeTasks.Add(new HomeTask() { Id = 22001, CourseId = 1, Date = DateTime.Parse("14.05.2019"), Title = "Intro", Description = "Intro", Number = 1 });
            HomeTasks.Add(new HomeTask() { Id = 22002, CourseId = 1, Date = DateTime.Parse("15.05.2019"), Title = "Intro", Description = "Intro", Number = 2 });
            HomeTasks.Add(new HomeTask() { Id = 22003, CourseId = 2, Date = DateTime.Parse("17.05.2019"), Title = "Intro", Description = "Intro", Number = 1 });

            HomeTaskAssessments.Add(new HomeTaskAssessment { Id = 222001, StudentId = 1101, HomeTaskId = 22001, Date = DateTime.Parse("18.05.2019"), IsComplete = true });
            HomeTaskAssessments.Add(new HomeTaskAssessment { Id = 222002, StudentId = 1101, HomeTaskId = 22002, Date = DateTime.Parse("18.05.2019"), IsComplete = true });
            HomeTaskAssessments.Add(new HomeTaskAssessment { Id = 222003, StudentId = 1101, HomeTaskId = 22003, Date = DateTime.Parse("18.05.2019"), IsComplete = false });
            HomeTaskAssessments.Add(new HomeTaskAssessment { Id = 222004, StudentId = 1102, HomeTaskId = 22003, Date = DateTime.Parse("18.05.2019"), IsComplete = true });

            // InitializeEntities(Students);
            Console.WriteLine("List of existing students:");
            foreach (var student in Students)
            {
                
                Console.WriteLine(student.Name);
              
            }
            Console.ReadKey();

            Console.WriteLine();
            //Add new Student to List from XML file:

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Student>));

            List<Student> newStudents = new List<Student>();

            string FileName = "newStudent1.xml";

          //  newStudents = AddNewObject(Students, FileName);

           
           using (var fileStream = File.OpenRead(FileName))
            {
                
                newStudents = (List<Student>)xmlSerializer.Deserialize(fileStream);

            }

            Console.WriteLine("Updated List of students from XML file:");
            foreach (var student in newStudents)
            {
                if (Students.Exists(x=> x.Id==student.Id)==false)    //.Contains(new Student{Id= student.Id})
                {
                    Students.Add(student);
                  }
                   
               }
             //   Console.ReadKey();   

            foreach (var student in Students)
            {
                Console.WriteLine(student.Name);
               
            }
            Console.ReadKey();
                                                            
           
            using (var fileStream = File.Create("serializedStudent.xml"))
            {
                xmlSerializer.Serialize(fileStream, Students);
            }

        }


        [Serializable]
        public class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime BirthDate { get; set; }
          //  public string LastName { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string GitHubLink { get; set; }
            [XmlIgnore]
            public ICollection<Course> Courses { get; set; }
            [XmlIgnore]
            public ICollection<HomeTaskAssessment> HomeTaskAssessments { get; set; }
        }
        [Serializable]
        public class Course
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int PassCredits { get; set; }
            [XmlIgnore]
            public ICollection<Student> Students { get; set; }
            [XmlIgnore]
            public ICollection<HomeTask> HomeTasks { get; set; }
            [XmlIgnore]
            public ICollection<Lecturer> Lecturers { get; set; }
        }

        public class Lecturer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime BirthDate { get; set; }
            public ICollection<Course> Courses { get; set; }
        }

        public class HomeTask
        {
           // public int HWorkID { get; set; }
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime Date { get; set; }
            public int Number { get; set; }

            public int  CourseId { get; set; }

          //  public Course Course { get; set; }
            public ICollection<HomeTaskAssessment> HomeTaskAssessments { get; set; }

        }


        public class HomeTaskAssessment
        {
           // public int Id { get; set; }
            public int Id { get; set; }
            public Boolean IsComplete { get; set; }
            public DateTime Date { get; set; }
            public int HWorkID { get; set; }

            public int StudentId { get; set; }
            public int HomeTaskId { get; set; }

         //   public Student Student { get; set; }
          //  public HomeTask HomeTask { get; set; }
        }


        public class LecturerCourse
        {
            public int LecturerId { get; set; }
            public int CourseId { get; set; }
        }

        public class StudentCourse
        {
            public int CourseId { get; set; }
            public int StudentId { get; set; }
        }

        public static void InitializeEntities(List<Student> studlist)
        {

            studlist.Add(new Student() { Id = 1101, Name = "Bill Collins", BirthDate = DateTime.Parse("08.08.1999"), PhoneNumber = "06733450123", Email = "billcol@gmail.com.", GitHubLink = "link1" });
            studlist.Add(new Student() { Id = 1102, Name = "Ivan Vitrenko", BirthDate = DateTime.Parse("08.08.1998"), PhoneNumber = "0673345044", Email = "ivanvl@gmail.com.", GitHubLink = "link2" });

        }
        /*
          public static List<Object> AddNewObject (List<Object> obj, String filename)
          {
              XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Object>));

              List<Object> newObjects = new List<Object>();

              using (var fileStream = File.OpenRead(filename)) 
              {
                   newObjects = (List<Object>)xmlSerializer.Deserialize(fileStream);

              }

              return newObjects;


          }   */



    }
}
