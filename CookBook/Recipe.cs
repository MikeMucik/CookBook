using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook
{
    public class Recipe
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public List<string> Ingredients { get; set; }
        public Recipe()
        {
            Ingredients = new List<string>();
        }
             
    }
}
//public Recipe (int id, string name, int CategoryId)
//{
//    Id = id;
//    Name = name;
//    TypeId = CategoryId;
//}