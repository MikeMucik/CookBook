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
                        recipeManager.GetRecipeByIngredient();
                        break;
                    case '5':
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
