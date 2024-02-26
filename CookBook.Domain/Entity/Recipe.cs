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

        public List<Ingredient> Ingredients { get; set; }
        public string? Description { get; set; }
        public string? TimeOfPreparation { get; set; }
        public int? Difficulty { get; set; }
        public int? Portions { get; set; }
        public Recipe(int id, string? name, int categoryId, List<Ingredient> ingredients, string? description, string? timeOfPreparation, int? difficulty, int? portions)
        {
            Id = id;//czy to potrzebne??
            Name = name;
            CategoryId = categoryId;
            Ingredients = ingredients;
            Description = description;
            TimeOfPreparation = timeOfPreparation;
            Difficulty = difficulty;
            Portions = portions;
        }
        public Recipe() 
        {
            Ingredients = new List<Ingredient>();
        }

    }
    public class Ingredient// : BaseEntity
    {
        public string? NameIngredient { get; set; }
        public int Quantity { get; set; }
        public string? Unit { get; set; }
        public Ingredient(string? name, int quantity, string? unit)
        {
            NameIngredient = name;
            Quantity = quantity;
            Unit = unit;
        }
        public Ingredient() 
        {

        }
    }
}
