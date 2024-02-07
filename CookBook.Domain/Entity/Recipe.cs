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
        // public int Id { get; set; }
        public string? Name { get; set; }
        public int CategoryId { get; set; }

        public List<string> Ingredients { get; set; }
        public string? Description { get; set; }
        public Recipe(int id, string? name, int categoryId, List<string> ingredients, string? description)
        {
            Id = id;//czy to dało efekt?
            Name = name;
            CategoryId = categoryId;
            Ingredients = ingredients;
            Description = description;
        }
        public Recipe(int id)
        {
            Id = Id;
        }
        //public Recipe()
        //{
        //    Ingredients = new List<string>();
        //}
        //public Recipe(int id, string? name, int categoryId, List<string> ingredients, string? description)
        //{

        //}
    }
}
