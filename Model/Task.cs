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
        

        public Task( int userId, string name, string date, string description, string priority, int categoryId)
        {
            
            UserID = userId;
            Name = name;
            Date = date;
            Description = description;
            Priority = priority;
            CategoryId = categoryId;
        }
        public Task(int taskId, int userId, string name, string date, string description, string priority, int categoryId)
        {
            TaskID = taskId;
            UserID = userId;
            Name = name;
            Date = date;
            Description = description;
            Priority = priority;
            CategoryId = categoryId;
        }
        public Task(int taskId, int userId, string name, string date, string description, string priority, string stage, int categoryId)
        {
            TaskID = taskId;
            UserID = userId;
            Name = name;
            Date = date;
            Description = description;
            Priority = priority;
            Stage = Stage;
            CategoryId = categoryId;
        }
    }
}
