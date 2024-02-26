using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using CookBook.App.Concrete;
using CookBook.App.Managers;

namespace CookBook
{
    public class Program
    {
        public const string FILE_NAME = @"C:\CookBookFiles\ImportFile.xlsx";
        static void Main(string[] args)
        {
            //List<Ingredient> ingredients = new List<Ingredient>
            //{
            //    new Ingredient("Egg", 3, "pieces"),
            //    new Ingredient("Butter", 25, "grams"),
            //    new Ingredient("Salt", 2, "pinches")
            //};
            //Recipe recipe = new Recipe(1, "fried eggs", 1, ingredients, " ALe jaja", "15 minut", 1, 1);
            //List<Ingredient> ingredients1 = new List<Ingredient>
            //{
            //    new Ingredient("Bread", 2, "slices"),
            //    new Ingredient("Butter", 25, "grams"),
            //    new Ingredient("ham", 2, "slices")
            //};
            //Recipe recipe1 = new Recipe(2, "sandwich", 1, ingredients1, " Ale kanapka", "5 minut", 1, 1);
            //List<Recipe> list = new List<Recipe>();
            //List<Recipe> recipes = list;
            //list.Add(recipe1);
            //list.Add(recipe);
            //ListService listService = new ListService();
            //var recipes = listService.MethodRead();
            //List<Recipe> myRecipes = listService.MethodRead();
            //RecipeManager recipeManager
            //Recipes.AddRange(myRecipes);

            MenuActionService actionService = new MenuActionService();
            RecipeService recipeService = new RecipeService();
            RecipeManager recipeManager = new RecipeManager(actionService, recipeService);

            Console.WriteLine("Welcome in our CookBook");
            bool programRun = true;
            while (programRun == true)
            {
                Console.WriteLine("\r\nPlease select an option: ");
                var mainMenu = actionService.GetMenuActionsByMenuName("Main");
                for (int i = 0; i < mainMenu.Count; i++)
                {
                    Console.WriteLine($"{mainMenu[i].Id} {mainMenu[i].Name}");
                }
                var operation = Console.ReadKey();
                switch (operation.KeyChar)
                {
                    case '1':
                        recipeManager.AddNewRecipe();
                        break;

                    case '2':
                        recipeManager.DeleteRecipeById();
                        break;

                    case '3':
                        recipeManager.GetByIdRecipe();
                        break;

                    case '4':
                        //Console.WriteLine("\r\nPlease enter one ingredient to show recipes: ");
                        //var recipesToShowByIngredient = Console.ReadLine();
                        recipeManager.GetRecipeByIngredient();
                        break;

                    case '5':
                        //Console.WriteLine("\r\nPlease enter number of category of recipes to show: ");
                        //var categoryIdString = Console.ReadKey(); 
                        //Int32.TryParse(categoryIdString.KeyChar.ToString(), out int categoryId);
                        recipeManager.GetRecipesByCategory();
                        break;

                    case '6':
                        recipeManager.EditRecipe();
                        break;
                    case '7':
                        Console.WriteLine("\r\nYou leave the program.");
                        programRun = false;
                        break;
                    default:
                        Console.WriteLine("\r\n Your choice is incorrect");
                        break;
                }
            }

        }
    }
}
