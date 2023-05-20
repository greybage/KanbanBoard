using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;


namespace WindowsFormsApp
{
    public class Task
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Stage { get; set; }
        public string Category { get; set; }

        public Task(int id, string name, DateTime date, string description, string priority, string stage, string category)
        {
            ID = id;
            Name = name;

            Date = date;
            Description = description;
            Priority = priority;
            Stage = stage;
            Category = category;
        }
    }
    
}
