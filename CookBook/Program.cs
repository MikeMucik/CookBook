using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using CookBook.App.Abstract;
using CookBook.App.Concrete;
using CookBook.App.Managers;
using CookBook.Domain.Entity;

namespace CookBook
{
    public class Program
    {
        static void Main(string[] args)
        {
            MenuActionService actionService = new MenuActionService();
            RecipeService recipeService = new RecipeService();
            RecipeManager recipeManager = new RecipeManager(actionService, recipeService);

            Console.WriteLine("Welcome in our CookBook");
            while (true)
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
                        recipeManager.RemoveRecipeById();
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

                    default:
                        Console.WriteLine("\r\n Your choice is incorrect");
                        break;
                }
            }

        }
    }
}
