﻿namespace WindowsFormsApp
{
    public class Task
    {
        public int TaskID { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Stage { get; set; }
        public int CategoryID { get; set; }
        public int UserID { get; set; }
        

        public Task(int userId, string name, string date, string description, string priority, int categoryId)
        {
            UserID = userId;
            Name = name;
            Date = date;
            Description = description;
            Priority = priority;
            CategoryID = categoryId;
        }
        public Task()
        {
            
        }
    }
}