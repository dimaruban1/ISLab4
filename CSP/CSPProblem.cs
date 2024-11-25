using CSP.Models;

namespace CSPTimetable
{
    public class CSP
    {
        public List<Subject> Subjects { get; set; }
        public List<Classroom> Classrooms { get; set; }
        public List<Teacher> Professors { get; set; }
        public List<Group> Groups { get; set; }
        public List<Timeslot> Timeslots { get; set; }

        public CSP(List<Subject> subjects, List<Classroom> classrooms, List<Teacher> professors, List<Group> groups, List<Timeslot> timeslots)
        {
            Subjects = subjects;
            Classrooms = classrooms;
            Professors = professors;
            Groups = groups;
            Timeslots = timeslots;
        }

        public Timetable GenerateTimetable()
        {
            Timetable timetable = new Timetable();

            foreach(var group in Groups)
            {
                if(!Backtrack(timetable, group))
                {
                    Console.WriteLine($"Unable to generate a complete timetable for group {group.Name}.");
                }
            }

            return timetable;
        }


        private bool Backtrack(Timetable timetable, Group group)
        {
            int maxSubjects = Math.Min(group.Subjects.Count, Timeslots.Count);

            if(timetable.ScheduledClasses.Count(sc => sc.Group.Id == group.Id) == maxSubjects)
            {
                return true;
            }

            foreach(var subject in group.Subjects)
            {
                if(timetable.ScheduledClasses.Any(sc => sc.Group.Id == group.Id && sc.Subject.Id == subject.Id))
                {
                    continue;
                }

                foreach(var professor in Professors.Where(p => p.Subjects.Any(s => s.Id == subject.Id)))
                {
                    foreach(var classroom in Classrooms)
                    {
                        foreach(var timeslot in Timeslots)
                        {
                            var scheduled = new Scheduled
                            {
                                Subject = subject,
                                Professor = professor,
                                Classroom = classroom,
                                Group = group,
                                Timeslot = timeslot
                            };

                            if(IsValidAssignment(timetable, scheduled))
                            {
                                timetable.ScheduledClasses.Add(scheduled);

                                if(Backtrack(timetable, group))
                                {
                                    return true;
                                }

                                timetable.ScheduledClasses.Remove(scheduled);
                            }
                        }
                    }
                }
            }

            return false;
        }


        private bool IsValidAssignment(Timetable timetable, Scheduled scheduledClass)
        {
            if(scheduledClass.Classroom.Capacity < scheduledClass.Group.NumberOfStudents)
            {
                return false;
            }

            foreach(var existingClass in timetable.ScheduledClasses)
            {
                if(existingClass.Timeslot.Id == scheduledClass.Timeslot.Id)
                {
                    if(existingClass.Professor.Id == scheduledClass.Professor.Id)
                    {
                        return false;
                    }

                    if(existingClass.Group.Id == scheduledClass.Group.Id)
                    {
                        return false;
                    }

                    if(existingClass.Classroom.Id == scheduledClass.Classroom.Id)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
