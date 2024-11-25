using CSP.Models;
using System.Runtime.InteropServices;
using System.Text;

namespace CSPTimetable
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;


            var subjects = new List<Subject>
            {
                new Subject { Id = 1, Name = "OOP" },
                new Subject { Id = 2, Name = "DB" },
                new Subject { Id = 3, Name = "Programming Fundamentals" },
                new Subject { Id = 4, Name = "Data Structures and Algorithms" },
                new Subject { Id = 5, Name = "Computer Networks" },
                new Subject { Id = 6, Name = "Operating Systems" },
                new Subject { Id = 7, Name = "Software Engineering" }
            };

            var classrooms = new List<Classroom>
            {
                new Classroom { Id = 1, Name = "30", Capacity = 100 },
                new Classroom { Id = 2, Name = "31", Capacity = 70 }
            };

            var professors = new List<Teacher>
            {
                new Teacher { Id = 1, Name = "Alice", Subjects = new List<Subject> { subjects[0] } },
                new Teacher { Id = 2, Name = "Bob", Subjects = new List<Subject> { subjects[1] } },
                new Teacher { Id = 3, Name = "Charlie", Subjects = new List<Subject> { subjects[4] } },
                new Teacher { Id = 4, Name = "Diana", Subjects = new List<Subject> { subjects[2] } },
                new Teacher { Id = 5, Name = "Edward", Subjects = new List<Subject> { subjects[5] } },
                new Teacher { Id = 6, Name = "Fiona", Subjects = new List<Subject> { subjects[6] } },
                new Teacher { Id = 7, Name = "George", Subjects = new List<Subject> { subjects[3] } },
            };


            var timeslots = new List<Timeslot>
            {
                new Timeslot { Id = 1, TimeSlot = "Mon 09:00-10:30" },
                new Timeslot { Id = 2, TimeSlot = "Mon 10:40-12:10" },
                new Timeslot { Id = 3, TimeSlot = "Mon 12:20-13:50" },
                new Timeslot { Id = 4, TimeSlot = "Tue 09:00-10:30" },
                new Timeslot { Id = 5, TimeSlot = "Tue 10:40-12:10" },
                new Timeslot { Id = 6, TimeSlot = "Tue 12:20-13:50" },
                new Timeslot { Id = 7, TimeSlot = "Sun 09:00-10:30" },
                new Timeslot { Id = 8, TimeSlot = "Sun 10:40-12:10" },
                new Timeslot { Id = 9, TimeSlot = "Sun 12:20-13:50" }
            };


            var groups = new List<Group>
            {
                new Group { Id = 1, Name = "G-1", NumberOfStudents = 27, Subjects = subjects },
                new Group { Id = 2, Name = "G-2", NumberOfStudents = 30, Subjects = subjects }
            };

            var solver = new CSP(subjects, classrooms, professors, groups, timeslots);
            Timetable timetable = solver.GenerateTimetable();

            PrintTimetable(timetable);
        }



        static void PrintTimetable(Timetable timetable)
        {

            Console.WriteLine("Generated timetable:");
            Console.WriteLine(new string('-', 134));
            Console.WriteLine("| {0, -10} | {1, -40} | {2, -23} | {3, -15} | {4, -10} | {5, -17} |", "Group", "Subject", "Teacher", "Classroom", "Capacity", "Timeslot");
            Console.WriteLine(new string('-', 134));

            foreach (var scheduledClass in timetable.ScheduledClasses)
            {
                Console.WriteLine("| {0, -10} | {1, -40} | {2, -23} | {3, -15} | {4, -10} | {5, -17} |",
                    scheduledClass.Group.Name,
                    scheduledClass.Subject.Name,
                    scheduledClass.Professor.Name,
                    scheduledClass.Classroom.Name,
                    scheduledClass.Classroom.Capacity,
                    scheduledClass.Timeslot.TimeSlot);
            }

            Console.WriteLine(new string('-', 134));
        }
    }
}