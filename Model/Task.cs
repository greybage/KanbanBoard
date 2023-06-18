using System;
namespace WindowsFormsApp
{
    public class Task
    {
        public int TaskID { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Stage { get; set; }
        public int CategoryId { get; set; }
        public int UserID { get; set; }

        public int GetDaysRemaining()
        {
            DateTime currentDate = DateTime.Today;
            DateTime taskDate = DateTime.Parse(Date); // Analizujesz string jako DateTime
            TimeSpan remainingTime = taskDate - currentDate;
            return remainingTime.Days;
        }

        public int GetDaysOverdue()
        {
            DateTime currentDate = DateTime.Today;
            DateTime taskDate = DateTime.Parse(Date); // Analizujesz string jako DateTime
            TimeSpan overdueTime = currentDate - taskDate;
            return overdueTime.Days;



        }

    }
}
