using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.App.Concrete;
using CookBook.Domain.Entity;

namespace CookBook.App.Managers
{
    public class RecipeManager
    {
        private readonly MenuActionService _actionService;
        private RecipeService _recipeService;
        public RecipeManager(MenuActionService actionService)
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
            //int categoryId;
            Int32.TryParse(operation.KeyChar.ToString(), out int categoryId);
            Console.WriteLine("\r\nPlease enter name for a new recipe: ");
            var name = Console.ReadLine();
            Console.WriteLine("\r\nPlease enter number of ingredients: ");
            var numberOfIngredient = Console.ReadLine();
            //sprawdzenie czy użytkownik podał liczbę

            int recipeNumberOfIgredient;
            bool isNumeric = Int32.TryParse(numberOfIngredient, out recipeNumberOfIgredient);
            var ingredients = new List<string?>();
            if (isNumeric)
            {
                for (int i = 0; i < recipeNumberOfIgredient; i++)
                {
                    Console.WriteLine($"\r\nPlease enter {i + 1} ingredients:");
                    var ingredient = Console.ReadLine();
                    ingredients.Add(ingredient);
                }
            }
            else
            {
                Console.WriteLine("You didn't enter number of ingredients");
            }
            Console.WriteLine("\r\nPlease enter description of recipe: ");

            var description = Console.ReadLine();
            Console.WriteLine("\r\nPlease enter time preparation of recipe: ");
            var timeOfPreparation = Console.ReadLine();
            var lastId = _recipeService.GetLastId();
            var lastIdHere = lastId + 1;
            //czy wprowadzenie lastId dało efekt?
            Recipe recipe = new Recipe(lastIdHere, name, categoryId, ingredients, description, timeOfPreparation);
            _recipeService.AddRecipe(recipe);
            return recipe.Id;
        }

        public void RemoveRecipeById()
        {
            Console.WriteLine("\r\nPlease insert id of recipe to delete: ");
            var idToRemove = Console.ReadLine();
            if (Int32.TryParse(idToRemove, out int id))
            {
                Recipe recipeToRemove = _recipeService.GetRecipeById(id);
                if (recipeToRemove != null)
                {
                    _recipeService.RemoveRecipe(recipeToRemove);
                    Console.WriteLine("\r\nRecipe delete successfully.");
                }
                else
                {
                    Console.WriteLine("\r\nRecipe with this id is not exist.");
                }
            }
            else
            {
                Console.WriteLine("\r\nInvalid input for recipe id.");
               // RemoveRecipeById();
            }
        }

        public void GetByIdRecipe()
        {
            Console.WriteLine("\r\nPlease insert recipe id to show.");
            var idToShow = Console.ReadLine();
            if (Int32.TryParse(idToShow, out int id))
            {
                Recipe recipeToShow = _recipeService.GetRecipeById(id);
                if (recipeToShow != null)
                {
                    Console.WriteLine($"There is details for id: {recipeToShow.Id}");
                    Console.WriteLine($"Name: {recipeToShow.Name}");
                    Console.WriteLine($"Category: {recipeToShow.CategoryId}");
                    Console.WriteLine("Ingredients: ");
                    foreach (var ingrdient in recipeToShow.Ingredients)
                    {
                        Console.Write($" {ingrdient}");
                    }
                    Console.WriteLine($"\r\nDescription: {recipeToShow.Description}");
                    Console.WriteLine($"\r\nRecipe description: {recipeToShow.TimeOfPreparation}");
                }
                else
                {
                    Console.WriteLine("\r\nRecipe with this id is not exist.");
                }
            }
            else
            {
                Console.WriteLine("\r\nInvalid input for recipe id.");
               // GetByIdRecipe();
            }
        }

        public void GetRecipeByIngredient()
        {
                      Console.WriteLine("\r\nPlease enter one ingredient to show recipes: ");
            var recipesToShowByIngredient = Console.ReadLine();

            List<Recipe> matchingRecipes = new List<Recipe>();
            var allRecipes = _recipeService.GetAllRecipes();
            foreach (var recipe in allRecipes)
            {
                if (recipe.Ingredients.Contains(recipesToShowByIngredient))
                {
                    matchingRecipes.Add(recipe);
                }

                if (matchingRecipes.Count == 0)
                {
                    Console.WriteLine("There is no recipes with insert ingredient");
                }
                else
                {
                    Console.WriteLine($"\r\nRecipe id: {recipe.Id} \r\nRecipe name: {recipe.Name} \r\nRecipe category: {recipe.CategoryId}");

                    Console.WriteLine("Recipe ingredients :");
                    foreach (var ingredient in recipe.Ingredients)
                    {
                        Console.Write(" " + ingredient);
                    }
                    Console.WriteLine($"\r\nRecipe description: {recipe.Description}");
                    Console.WriteLine($"\r\nRecipe description: {recipe.TimeOfPreparation}");

                }

            }
        }

        public void GetRecipesByCategory()
        {
            Console.WriteLine("\r\nPlease enter number of category of recipes to show: ");
            var recipeCategory = Console.ReadLine();
            int category;
            Int32.TryParse(recipeCategory, out category);
            var allRecipes = _recipeService.GetAllRecipes();
            List<Recipe> productToShow = new List<Recipe>();
            foreach (var recipe in allRecipes)
            {
                if (recipe.CategoryId == category)
                {
                    productToShow.Add(recipe);
                }
                if (productToShow.Count > 0)
                {
                    Console.WriteLine($"\r\nRecipe id: {recipe.Id} \r\nRecipe name: {recipe.Name} \r\nRecipe category: {recipe.CategoryId}");
                    Console.WriteLine("Recipe ingredients :");
                    foreach (var ingredient in recipe.Ingredients)
                    {
                        Console.Write(" " + ingredient);
                    }
                    Console.WriteLine($"\r\nRecipe description: {recipe.Description}");
                }
                else
                {
                    Console.WriteLine("There is no recipes with insert category");
                }
            }
        }

        public void EditRecipe()
        {
            Console.WriteLine("\r\nPlease insert id to edit:");
            var recipeToEdit = Console.ReadLine();
            if (Int32.TryParse(recipeToEdit, out int idToChange))
            {
                Recipe recipeToChange = _recipeService.GetRecipeById(idToChange);
                if (recipeToChange != null)
                {
                    var kindOfData = _actionService.GetMenuActionsByMenuName("KindOfData");
                    Console.WriteLine("\r\nSelect which data you want to change from the list");
                    for (int i = 0; i < kindOfData.Count; i++)
                    {
                        Console.WriteLine($"{kindOfData[i].Id} {kindOfData[i].Name}");
                    }
                    var selectKindOfData = Console.ReadKey();
                    Int32.TryParse(selectKindOfData.KeyChar.ToString(), out int categoryDataId);
                    switch (categoryDataId)
                    {
                        case 1:
                            Console.WriteLine("\r\nInsert new name of recipe: ");
                            var newName = Console.ReadLine();
                            recipeToChange.Name = newName;
                            _recipeService.UpdateRecipe(recipeToChange);
                            break;
                        case 2:
                            var addNewRecipeMenu = _actionService.GetMenuActionsByMenuName("AddNewRecipeMenu");
                            Console.WriteLine("\r\nPlease select new category of meal: ");
                            for (int i = 0; i < addNewRecipeMenu.Count; i++)
                            {
                                Console.WriteLine($"{addNewRecipeMenu[i].Id} {addNewRecipeMenu[i].Name}");
                            }
                            var operation = Console.ReadKey();
                            //int categoryId;
                            Int32.TryParse(operation.KeyChar.ToString(), out int categoryId);
                            recipeToChange.CategoryId = categoryId;
                            _recipeService.UpdateRecipe(recipeToChange);
                            break;
                        case 3:
                            Console.WriteLine("\r\nPlease enter number of ingredients: ");
                            var numberOfIngredient = Console.ReadLine();
                            int recipeNumberOfIgredient;
                            bool isNumeric = Int32.TryParse(numberOfIngredient, out recipeNumberOfIgredient);
                            var ingredients = new List<string?>();
                            if (isNumeric)
                            {
                                for (int i = 0; i < recipeNumberOfIgredient; i++)
                                {
                                    Console.WriteLine($"\r\nPlease enter {i + 1} ingredient:");
                                    var newIngredient = Console.ReadLine();
                                    ingredients.Add(newIngredient);
                                }
                            }
                            else
                            {
                                Console.WriteLine("You didn't enter number of ingredients");
                            }
                            recipeToChange.Ingredients = ingredients;
                            _recipeService.UpdateRecipe(recipeToChange);
                            break;
                        case 4:
                            Console.WriteLine("\r\nInsert new description of recipe: ");
                            var newDescription = Console.ReadLine();
                            recipeToChange.Description = newDescription;
                            _recipeService.UpdateRecipe(recipeToChange);
                            break;
                        case 5:
                            Console.WriteLine("\r\nInsert new preparation time of recipe: ");
                            var newTimeOfPreparation = Console.ReadLine();
                            recipeToChange.TimeOfPreparation = newTimeOfPreparation;
                            _recipeService.UpdateRecipe(recipeToChange);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("You have selected a non-existent recipe");
                }
            }
            else
            {
                Console.WriteLine("\r\nInvalid input for recipe id.");
            }
            Console.WriteLine("\r\nDo you want change data in this recipe: \r\n1 - yes\r\n2 - no");
            var againEdit = Console.ReadLine();
            Int32.TryParse(againEdit.ToString(), out int againEditInt);
            if (againEditInt == 1)
            {
                EditRecipe();
            }
        }
    }
}
