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
            List<LecturerCourse> LecturerCourses = new List<LecturerCourse>();

            Students.Add(new Student() { Id = 1101, Name = "Bill Collins", BirthDate = DateTime.Parse("08.08.1999"), PhoneNumber = "06733450123", Email = "billcol@gmail.com.", GitHubLink = "link1",StudCourses=new List<Course>() });
            Students.Add(new Student() { Id = 1102, Name = "Ivan Vitrenko", BirthDate = DateTime.Parse("08.08.1998"), PhoneNumber = "0673345044", Email = "ivanvl@gmail.com.", GitHubLink = "link2", StudCourses = new List<Course>() });
            Students.Add(new Student() { Id = 1103, Name = "Andriy Tymchuk", BirthDate = DateTime.Parse("08.08.1998"), PhoneNumber = "0673345044", Email = "ivanvl@gmail.com.", GitHubLink = "link2", StudCourses = new List<Course>() });


            Courses.Add(new Course() { Id = 1, Name = "Math", StartDate = DateTime.Parse("04.05.2019"), EndDate = DateTime.Parse("24.08.2019"), PassCredits = 95, CourseLecturers = new List < Lecturer > () });
            Courses.Add(new Course() { Id = 2, Name = "Phisics", StartDate = DateTime.Parse("07.05.2019"), EndDate = DateTime.Parse("30.10.2019"), PassCredits = 90, CourseLecturers = new List<Lecturer>() });
            Courses.Add(new Course() { Id = 3, Name = "Mechanics", StartDate = DateTime.Parse("07.05.2019"), EndDate = DateTime.Parse("30.10.2019"), PassCredits = 90, CourseLecturers = new List<Lecturer>() });
            Courses.Add(new Course() { Id = 4, Name = "Chemistry", StartDate = DateTime.Parse("07.05.2019"), EndDate = DateTime.Parse("30.10.2019"), PassCredits = 90, CourseLecturers = new List<Lecturer>() });
            Courses.Add(new Course() { Id = 5, Name = "Programming", StartDate = DateTime.Parse("07.05.2019"), EndDate = DateTime.Parse("30.10.2019"), PassCredits = 90, CourseLecturers = new List<Lecturer>() });


            Lecturers.Add(new Lecturer() { Id = 101, Name = "John", BirthDate = DateTime.Parse("08.08.1975") });
            Lecturers.Add(new Lecturer() { Id = 102, Name = "Robin", BirthDate = DateTime.Parse("07.07.1995") });
            Lecturers.Add(new Lecturer() { Id = 103, Name = "Ivan", BirthDate = DateTime.Parse("07.07.1995") });

            LecturerCourses.Add(new LecturerCourse() { CourseId=1, LecturerId=102});
            LecturerCourses.Add(new LecturerCourse() { CourseId = 2, LecturerId = 103 });
            LecturerCourses.Add(new LecturerCourse() { CourseId = 3, LecturerId = 101 });
            LecturerCourses.Add(new LecturerCourse() { CourseId = 4, LecturerId = 102 });
            LecturerCourses.Add(new LecturerCourse() { CourseId = 5, LecturerId = 102 });
            LecturerCourses.Add(new LecturerCourse() { CourseId = 1, LecturerId = 103 });
            LecturerCourses.Add(new LecturerCourse() { CourseId = 1, LecturerId = 101 });

            HomeTasks.Add(new HomeTask() { Id = 22001, CourseId = 1, Date = DateTime.Parse("14.05.2019"), Title = "Intro", Description = "Intro", Number = 1 });
            HomeTasks.Add(new HomeTask() { Id = 22002, CourseId = 1, Date = DateTime.Parse("15.05.2019"), Title = "Intro", Description = "Intro", Number = 2 });
            HomeTasks.Add(new HomeTask() { Id = 22003, CourseId = 2, Date = DateTime.Parse("17.05.2019"), Title = "Intro", Description = "Intro", Number = 1 });
            HomeTasks.Add(new HomeTask() { Id = 22004, CourseId = 3, Date = DateTime.Parse("17.05.2019"), Title = "Intro", Description = "Intro", Number = 1 });


            HomeTaskAssessments.Add(new HomeTaskAssessment { Id = 222001, StudentId = 1101, HomeTaskId = 22001, Date = DateTime.Parse("18.05.2019"), IsComplete = true });
            HomeTaskAssessments.Add(new HomeTaskAssessment { Id = 222002, StudentId = 1101, HomeTaskId = 22002, Date = DateTime.Parse("18.05.2019"), IsComplete = true });
            HomeTaskAssessments.Add(new HomeTaskAssessment { Id = 222003, StudentId = 1101, HomeTaskId = 22003, Date = DateTime.Parse("18.05.2019"), IsComplete = false });
            HomeTaskAssessments.Add(new HomeTaskAssessment { Id = 222004, StudentId = 1102, HomeTaskId = 22003, Date = DateTime.Parse("18.05.2019"), IsComplete = true });
            HomeTaskAssessments.Add(new HomeTaskAssessment { Id = 222005, StudentId = 1102, HomeTaskId = 22003, Date = DateTime.Parse("18.05.2019"), IsComplete = true });
            HomeTaskAssessments.Add(new HomeTaskAssessment { Id = 222006, StudentId = 1102, HomeTaskId = 22004, Date = DateTime.Parse("18.05.2019"), IsComplete = true });
            HomeTaskAssessments.Add(new HomeTaskAssessment { Id = 222007, StudentId = 1103, HomeTaskId = 22003, Date = DateTime.Parse("18.05.2019"), IsComplete = true });
            HomeTaskAssessments.Add(new HomeTaskAssessment { Id = 222008, StudentId = 1103, HomeTaskId = 22004, Date = DateTime.Parse("18.05.2019"), IsComplete = true });



            // InitializeEntities(Students);
            Console.WriteLine("List of existing students:");
            foreach (var student in Students)
            {   
                Console.WriteLine(student.Name);
            }
            Console.ReadKey();

            Console.WriteLine();

            var studHWRate = Students.Join(
                HomeTaskAssessments,
                stud => stud.Id,
                hwrate => hwrate.StudentId,
                    (stud, hwrate) => new
                    {
                        StudentID = stud.Id,
                        StudentName = stud.Name,
                        HomeTaskRateID = hwrate.Id,
                        HomeTaskMark = hwrate.IsComplete,
                        HomeTaskID = hwrate.HomeTaskId

                    });
          /*
            foreach (var item in studHWRate)
            {
                Console.WriteLine("Students-HomeworkAssessments: StudID {0}, StudName {1}, HWRateID {2}, TAsk IsComplete {3}", item.StudentID, item.StudentName, item.HomeTaskRateID, item.HomeTaskMark); 
            }
            Console.ReadLine();
            */

            var stdtask = studHWRate.Join(
                HomeTasks,
                hwr => hwr.HomeTaskID,
                hts => hts.Id,
                (hwr, hts) => new
                {
                    StudentID = hwr.StudentID,
                    StudentName = hwr.StudentName,
                    HomeTaskRateID = hwr.HomeTaskRateID,
                    HomeTaskMark = hwr.HomeTaskMark,
                    HomeTaskID = hts.Id,
                    CourseID=hts.CourseId
                }
                );


            var stdcourseList = stdtask.Join(
                Courses,
                st => st.CourseID,
                cr => cr.Id,
                (st, cr) => new
                {
                    StudentID = st.StudentID,
                    StudentName = st.StudentName,
                    HomeTaskID = st.HomeTaskID,
                    HomeTaskMark = st.HomeTaskMark,
                    CourseID = cr.Id,
                    CourseName = cr.Name,
                    StartDate=cr.StartDate,
                    EndDate=cr.EndDate,
                    PassCred=cr.PassCredits

                }).ToList();          

      

    

            foreach (Student student in Students)
            {
             
                foreach (var item in stdcourseList)
                {
                    if (student.Id == item.StudentID)
                    {
                        student.StudCourses.Add(new Course {Id=item.CourseID,Name=item.CourseName,StartDate=item.StartDate,EndDate=item.EndDate,PassCredits=item.PassCred });
                    }
                }

            }


            var courseList1 = Courses.Join(
                LecturerCourses,
                c=>c.Id,
                ct=>ct.CourseId,
                (c,ct)=> new
                {
                    CourseID=c.Id,
                    LecturerID=ct.LecturerId

                }
                );

            var courseList2 = courseList1.Join(
                Lecturers,
                c=>c.LecturerID,
                ct=>ct.Id,

                (c, ct) => new
                {
                    CourseID = c.CourseID,
                   LecturerID= ct.Id,
                   LecturerName= ct.Name,
                   LEcturerBirthDate= ct.BirthDate
                }
                );




            foreach(Course course in Courses)
            {
                foreach (var item in courseList2)
                {
                    if (course.Id == item.CourseID)
                    {
                        course.CourseLecturers.Add(new Lecturer { Id = item.LecturerID, Name = item.LecturerName, BirthDate=item.LEcturerBirthDate });
                    }
                }
            }


         /*
            var gruppedResult = stdcourseList.GroupBy(s => new { s.StudentName, s.CourseName }).GroupBy(m => m.Key.StudentName);

            
             foreach (var stud in gruppedResult)
             {
                 Console.WriteLine("Students:{0}", stud.Key);

                 foreach(var cr in stud)
                 {
                     Console.WriteLine("Courses:{0}", cr.Key);

                    foreach (var hw in cr)
                    {
                        Console.WriteLine("HomeTasks: HtaskID: {0} HtaskMark: {1}", hw.HomeTaskID, hw.HomeTaskMark);


                    }   
                 }
               //  Console.WriteLine("Students-Courses: StudID {0}, StudName {1},  CourseID {2}, Course Title {3} ", item.StudentID, item.StudentName, item.CourseID, item.CourseName);
             }
             Console.ReadLine();
           
            */
            
               //Add new Student to List from XML file:

               XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Student>));

            /*
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

            */
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
            
            public List<Course> StudCourses { get; set; }
            [XmlIgnore]
            public List<HomeTaskAssessment> HomeTaskAssessments { get; set; }
        }


        [Serializable]
        public class Course
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int PassCredits { get; set; }

            public List<Lecturer> CourseLecturers { get; set; }

            [XmlIgnore]
            public ICollection<Student> Students { get; set; }
            [XmlIgnore]
            public ICollection<HomeTask> HomeTasks { get; set; }

           
        }





        [Serializable]
        public class Lecturer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime BirthDate { get; set; }
            public List<Course> Courses { get; set; }
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
           // public int HWorkID { get; set; }

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

       

       

    }
}
