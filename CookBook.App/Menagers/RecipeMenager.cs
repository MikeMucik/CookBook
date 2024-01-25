using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.App.Concrete;
using CookBook.Domain.Entity;

namespace CookBook.App.Menager
{
    public class RecipeMenager
    {
        private readonly MenuActionService _actionService;
        private RecipeService _recipeService;
        
        public RecipeMenager(MenuActionService actionService)
        {
            _recipeService = new RecipeService();
            _actionService = actionService;

        }

        public int AddNewRecipe()
        {
            var addNewRecipeMenu = _actionService.GetMenuActionsByMenuName("AddNewRecipeMenu");
            Console.WriteLine("\r\nPlease select category of meal: ");
            for (int i = 0; i < addNewRecipeMenu.Count; i++)
            {
                Console.WriteLine($"{addNewRecipeMenu[i].Id} {addNewRecipeMenu[i].Name}");
            }
            var operation = Console.ReadKey();
            int categoryId;
            Int32.TryParse(operation.KeyChar.ToString(), out categoryId);

            Console.WriteLine("\r\nPlease enter name of recipe:");
            var name = Console.ReadLine();

            var lastId = _recipeService.GetLastId();

            Console.WriteLine("\r\nPlease enter number of ingredients: ");
            var numberOfIngredient = Console.ReadLine();
            int recipeNumberOfIgredient;
            Int32.TryParse(numberOfIngredient, out recipeNumberOfIgredient);
            var ingredients = new List<string>();
           
            for (int i = 0; i < recipeNumberOfIgredient; i++)
            {
                Console.WriteLine($"\r\nPlease enter {i + 1} ingredients:");
                var ingredient = Console.ReadLine();
                ingredients.Add(ingredient);
            }

            Console.WriteLine("\r\nPlease enter description of recipe: ");
            var description = Console.ReadLine();
            Recipe recipe = new Recipe(lastId + 1, name, categoryId, ingredients, description);

            _recipeService.AddRecipe(recipe);
            return recipe.Id;
        }
        public void RemoveRecipeById(int id)
        {
            var recipe = _recipeService.GetRecipeById(id);
            _recipeService.RemoveRecipe(recipe);
        }
        public Recipe GetRecipeById(int id)
        {
            var recipe = _recipeService.GetRecipeById(id);
            return recipe;
        }
    }
}
