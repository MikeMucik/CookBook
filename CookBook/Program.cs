using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace CookBook
{
    public class Program
    {
        static void Main(string[] args)
        {
            //GenericService<Recipe> genericRecipeService = new GenericService<Recipe>();

            //Recipe itemForGeneric = new Recipe(1, "Apple");
            //Recipe item2ForGeneric = new Recipe(2, "Strawberry");
            //genericRecipeService.Add(itemForGeneric);
            //genericRecipeService.Add(item2ForGeneric);

            //var items = genericRecipeService.GetAll();


            //genericRecipeService.Remove(recipe2ForGeneric);

            //GenericService<MenuAction> genericActionService = new GenericService<MenuAction>();
            //MenuAction menuAction = new MenuAction(1);
            //genericActionService.Add(menuAction);
            // Przywitanie użytkownika
            //// Wybierz opcję co chcesz zrobić zrobione !!!
            //// 1 Dodaj nowy przepis zrobione !!!
            //// 2 Usuń przepis zrobione !!!
            ///  3 Pozkaż szczegóły przepisu zrobione !!!
            //// 4 Znajdź przepis po składniku(filtr)
            //// 5 Pokaż przepisy z danej kategorii

            ////// 1a. Wybierz kategorię( śniadanie, obiad etc.)
            ////// 1b. Wpisanie szczegółów( składniki(name) i ich ilość(quantity), czas przygotowania, na ile osób

            ////// 2a. Wybierz przepis po id lub nazwie
            ////// 2b. Usuń przepis z listy
            
            ////// 3a. Wybierz id przepisu
            ////// 3b. Wypisz szczegóły przepisu

            ////// 4a. Wprowadź składnik
            ////// 4b. Wypisz przepisy z zadanym składnikiem
            ////// 4c. Wybierz przepis z powyżeszej listy

            ////// 5a. Wybierz kategorie
            ////// 5b. Wypisz przepisy z danej kategorii
            ////// 5c. Wybierz przepis z powyżeszej listy
            ///
            MenuActionService actionService = new MenuActionService();
            RecipeService recipeService = new RecipeService();
            actionService = Initialize(actionService);
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

                        var keyInfo = recipeService.AddNewRecipeView(actionService); //wybierz kategorię posiłku
                        /*var id = */recipeService.AddNewRecipe(keyInfo.KeyChar); //czy potrzebne to id ?
                        break;

                    case '2':
                        var removeId = recipeService.RemoveRecipeView();
                        recipeService.RemoveRecipe(removeId);
                        break;

                    case '3':
                        var detailId = recipeService.RecipeChooseIdView();
                        recipeService.ShowRecipe(detailId);
                        break;

                    case '4':
                        var detailRecipeByIngredient = recipeService.RecipeChooseIngredientView();
                        recipeService.ShowRecipesByIngredient(detailRecipeByIngredient);
                        break;

                    case '5':
                        var detailRecipeByCategory = recipeService.RecipeChooseCategoryView();
                        recipeService.ShowRecipesByCategory(detailRecipeByCategory);
                        break;

                    default:
                        Console.WriteLine("\r\n Your choice is incorrect");
                        break;
                }
            }

        }

        private static MenuActionService Initialize(MenuActionService actionService)
        {
            actionService.AddNewAction(1, "Add recipes", "Main");
            actionService.AddNewAction(2, "Remove recipe", "Main");
            actionService.AddNewAction(3, "Show recipe by id", "Main");
            actionService.AddNewAction(4, "Find recipes by ingredient", "Main");
            actionService.AddNewAction(5, "Find recipe by category", "Main");

            actionService.AddNewAction(1, "Breakfast", "AddNewRecipeMenu");
            actionService.AddNewAction(2, "Lunch", "AddNewRecipeMenu");
            actionService.AddNewAction(3, "Dinner", "AddNewRecipeMenu");
            actionService.AddNewAction(4, "Soup", "AddNewRecipeMenu");
            actionService.AddNewAction(5, "Appetizer", "AddNewRecipeMenu");
            actionService.AddNewAction(6, "Drink", "AddNewRecipeMenu");
            actionService.AddNewAction(7, "Dessert", "AddNewRecipeMenu");

            return actionService;

        }
    }
}
