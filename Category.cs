using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }

        public Category(int categoryId, string name)
        {
            CategoryID = categoryId;
            Name = name;
        }
    }
}
