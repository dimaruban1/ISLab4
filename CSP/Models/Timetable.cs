namespace CSP.Models
{
    public class Timetable
    {
        public List<Scheduled> ScheduledClasses { get; set; }

        public Timetable()
        {
            ScheduledClasses = new List<Scheduled>();
        }
    }
}
