using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.Domain.Common;

namespace CookBook.Domain.Entity
{
    public class Recipe : BaseEntity
    {
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public List<string> Ingredients { get; set; }

        public Recipe(int id, string name, int categoryId, List<string> ingredients, string description)
            {

            Name = name;
            CategoryId = categoryId;
            Ingredients = ingredients;
            Description = description;
            }
       
             
    }
}
 //public Recipe()
        //{
        //    Ingredients = new List<string>();
        //}