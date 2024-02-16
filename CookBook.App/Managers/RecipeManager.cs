using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.App.Concrete;
using CookBook.Domain.Entity;
using CookBook.App.Abstract;
using CookBook.App.Common; // to dodałem bo AddRecipeNew nie używa AddRecipe tj nie zwraca recipe.Id daje zero zaniast 1 ???
using System.Xml.Linq;

namespace CookBook.App.Managers
{
    public class RecipeManager
    {
        private readonly MenuActionService _actionService;
        private IService<Recipe> _recipeService;
        //private RecipeService _recipeService;
        public RecipeManager(MenuActionService actionService, IService<Recipe> recipeService)
        {
            _recipeService = recipeService;
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
            var ingredients = new List<Ingredient>();
            if (isNumeric)
            {
                for (int i = 0; i < recipeNumberOfIgredient; i++)
                {
                    Console.WriteLine($"\r\nPlease enter name {i + 1} ingredients:");
                    var nameIngredient = Console.ReadLine();
                    Console.WriteLine($"\r\nPlease enter quantity of{nameIngredient}:");
                    var quantityString = Console.ReadLine();
                    if (Int32.TryParse(quantityString?.ToString(), out int quantity))
                    {
                        Console.WriteLine($"\r\nPlease enter unit of{nameIngredient}:");

                    }
                    else
                    {
                        Console.WriteLine("Your insert is not correct. Please try again");
                        AddNewRecipe();
                    }
                    var unit = Console.ReadLine();
                    ingredients.Add(new Ingredient(nameIngredient, quantity, unit));
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
            Console.WriteLine("\r\nPlease enter dificult of recipe: ");
            var dificultOfPreparationString = Console.ReadKey();
            Int32.TryParse(dificultOfPreparationString.KeyChar.ToString(), out int dificultOfPreparation);
            var numberOfPortionsString = Console.ReadKey();
            Int32.TryParse(numberOfPortionsString.KeyChar.ToString(), out int numberOfProportions);
            var lastId = _recipeService.GetLastId();
            var lastIdHere = lastId + 1;
            //czy wprowadzenie lastId dało efekt?
            Recipe recipe = new Recipe(lastIdHere, name, categoryId, ingredients, description, timeOfPreparation, dificultOfPreparation, numberOfProportions);
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
                    foreach (var ingredient in recipeToShow.Ingredients)
                    {
                        Console.WriteLine((" " + ingredient.NameIngredient + " " + ingredient.Quantity + " " + ingredient.Unit));
                    }
                    Console.WriteLine($"Description: {recipeToShow.Description}");
                    Console.WriteLine($"Time to preparation: {recipeToShow.TimeOfPreparation}");
                    Console.WriteLine($"Difficult: {recipeToShow.Difficulty}");
                    Console.WriteLine($"Portions: {recipeToShow.Portions}");
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
                if (recipe.Ingredients.Any(i => i.NameIngredient == recipesToShowByIngredient)) ;
                // tu zacznij dalej poprawiać ?
                {
                    matchingRecipes.Add(recipe);
                }

                if (matchingRecipes.Count == 0)
                {
                    Console.WriteLine("There is no recipes with insert ingredient");
                   // GetRecipeByIngredient();
                }
                else
                {
                    Console.WriteLine($"\r\nRecipe id: {recipe.Id} \r\nRecipe name: {recipe.Name} \r\nRecipe category: {recipe.CategoryId}");

                    Console.WriteLine("Recipe ingredients :");
                    foreach (var ingredient in recipe.Ingredients)
                    {
                        Console.Write((" " + ingredient.NameIngredient + " " + ingredient.Quantity + " " + ingredient.Unit));
                    }
                    Console.WriteLine($"Recipe description: {recipe.Description}");
                    Console.WriteLine($"Time to preparation: {recipe.TimeOfPreparation}");
                    Console.WriteLine($"Difficult: {recipe.Difficulty}");
                    Console.WriteLine($"Portions: {recipe.Portions}");
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
                        Console.Write(" " + ingredient.NameIngredient + " " + ingredient.Quantity + " " + ingredient.Unit);
                    }
                    Console.WriteLine($"\r\nTime to preparation: {recipe.TimeOfPreparation}");
                    Console.WriteLine($"Difficult: {recipe.Difficulty}");
                    Console.WriteLine($"Portions: {recipe.Portions}");
                }
                else
                {
                    Console.WriteLine("There is no recipes with insert category");
                }
                //return recipe;
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
                            var ingredients = new List<Ingredient>();
                            if (isNumeric)
                            {
                                for (int i = 0; i < recipeNumberOfIgredient; i++)
                                {
                                    Console.WriteLine($"\r\nPlease enter {i + 1} ingredients:");
                                    var name = Console.ReadLine();
                                    Console.WriteLine($"\r\nPlease enter quantity of {name}:");
                                    var quantityString = Console.ReadLine();
                                    Int32.TryParse(quantityString.ToString(), out int quantity);
                                    Console.WriteLine($"\r\nPlease enter unit of {name}:");
                                    var unit = Console.ReadLine();
                                    ingredients.Add(new Ingredient(name, quantity, unit));
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
                        case 6:
                            Console.WriteLine("\r\nInsert new diffuclt of recipe :");
                            var newDifficultOfRecipeString = Console.ReadKey();
                            Int32.TryParse(newDifficultOfRecipeString.KeyChar.ToString(), out int difficult);
                            recipeToChange.Difficulty = difficult;
                            _recipeService.UpdateRecipe(recipeToChange);
                            break;
                        case 7:
                            Console.WriteLine("\r\nInsert new number portions of recipe :");
                            var newNumberOfPortions = Console.ReadKey();
                            Int32.TryParse(newNumberOfPortions.KeyChar.ToString(), out int portion);
                            recipeToChange.Portions = portion;
                            _recipeService.UpdateRecipe(recipeToChange);
                            break;
                        default:
                            Console.WriteLine("\r\n Your choice is incorrect");
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
            var againEdit = Console.ReadKey();
            Int32.TryParse(againEdit.KeyChar.ToString(), out int againEditInt);
            if (againEditInt == 1)
            {
                EditRecipe();
            }
            //koniec edit
        }





        public Recipe GetRecipeById(int id)
        {
            var recipe = _recipeService.GetRecipeById(id);
            return recipe;
        }

        public void RemoveRecipeById(int id)
        {
            var recipe = _recipeService.GetRecipeById(id);
            _recipeService.RemoveRecipe(recipe);
        }

        public int GetLastId()
        {
            var recipeLastId = _recipeService.GetLastId();
            return recipeLastId;
        }

        public int AddRecipeNew(Recipe recipe)
        {
            int recipeId = _recipeService.AddRecipe(recipe);

            return recipeId;
        }

        public object GetAllRecipes()
        {
            return _recipeService.GetAllRecipes();

        }
        public int EditRecipeById(int id)
        {
            var recipe = _recipeService.GetRecipeById(id);
            _recipeService.UpdateRecipe(recipe);
            return recipe.Id;
        }
    }
}
