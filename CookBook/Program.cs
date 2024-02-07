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
            RecipeManager recipeManager = new RecipeManager(actionService);
            //RecipeService recipeService = new RecipeService();
            //IService<MenuAction> actionService = new MenuActionService();
            //IService<Recipe> recipeService = new RecipeService();
            //MenuActionService actionService = new();
            //RecipeService recipeService = new();

            //actionService = Initialize(actionService);

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
                        //var keyInfo = recipeService.AddNewRecipeView(actionService); //wybierz kategorię posiłku
                        //recipeService.AddNewRecipe(keyInfo.KeyChar);
                        break;

                    case '2':
                        recipeManager.RemoveRecipeById();
                        //var removeId = recipeService.RemoveRecipeView();
                        //recipeService.RemoveRecipe(removeId);
                        break;

                    case '3':
                        recipeManager.GetByIdRecipe();
                        //var detailId = recipeService.RecipeChooseIdView();
                        //recipeService.ShowRecipe(detailId);
                        break;

                    case '4':
                        recipeManager.GetRecipeByIngredient();
                        //var detailRecipeByIngredient = recipeService.RecipeChooseIngredientView();
                        //recipeService.ShowRecipesByIngredient(detailRecipeByIngredient);
                        break;

                    case '5':
                        recipeManager.GetRecipesByCategory();
                        //var detailRecipeByCategory = recipeService.RecipeChooseCategoryView();
                        //recipeService.ShowRecipesByCategory(detailRecipeByCategory);
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

        //private static MenuActionService Initialize(MenuActionService actionService)
        //{
        //    actionService.AddNewAction(1, "Add recipes", "Main");
        //    actionService.AddNewAction(2, "Remove recipe", "Main");
        //    actionService.AddNewAction(3, "Show recipe by id", "Main");
        //    actionService.AddNewAction(4, "Find recipes by ingredient", "Main");
        //    actionService.AddNewAction(5, "Find recipe by category", "Main");
        //    /*actionService.AddNewAction(6, "Edit recipe", "Main")*/;

        //    actionService.AddNewAction(1, "Breakfast", "AddNewRecipeMenu");
        //    actionService.AddNewAction(2, "Lunch", "AddNewRecipeMenu");
        //    actionService.AddNewAction(3, "Dinner", "AddNewRecipeMenu");
        //    actionService.AddNewAction(4, "Soup", "AddNewRecipeMenu");
        //    actionService.AddNewAction(5, "Appetizer", "AddNewRecipeMenu");
        //    actionService.AddNewAction(6, "Drink", "AddNewRecipeMenu");
        //    actionService.AddNewAction(7, "Dessert", "AddNewRecipeMenu");

        //    return actionService;

        //}
    }
}
