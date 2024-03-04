﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.App.Concrete;
using CookBook.Domain.Entity;
using CookBook.App.Abstract;

namespace CookBook.App.Managers
{
    public class RecipeManager
    {
        private readonly MenuActionService _actionService;
        private IService<Recipe> _recipeService;

        public RecipeManager(MenuActionService actionService, IService<Recipe> recipeService)
        {
            _recipeService = recipeService;
            _actionService = actionService;
            _recipeService.ReadDataJsonToList();
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
            int.TryParse(operation.KeyChar.ToString(), out int categoryId);
            Console.WriteLine("\r\nPlease enter name for a new recipe: ");
            var name = Console.ReadLine();
            Console.WriteLine("\r\nPlease enter number of ingredients: ");
            var numberOfIngredient = Console.ReadLine();
            //sprawdzenie czy użytkownik podał liczbę

            int recipeNumberOfIgredient;
            bool isNumeric = int.TryParse(numberOfIngredient, out recipeNumberOfIgredient);
            var ingredients = new List<Ingredient>();
            if (isNumeric)
            {
                for (int i = 0; i < recipeNumberOfIgredient; i++)
                {
                    Console.WriteLine($"\r\nPlease enter name {i + 1} ingredients:");
                    var nameIngredient = Console.ReadLine();
                    Console.WriteLine($"\r\nPlease enter quantity of{nameIngredient}:");
                    var quantityString = Console.ReadLine();
                    if (int.TryParse(quantityString?.ToString(), out int quantity))
                    {
                        Console.WriteLine($"\r\nPlease enter unit of {nameIngredient}:");
                    }
                    else
                    {
                        Console.WriteLine("Your insert is not correct. Please try again");
                        // AddNewRecipe(); 
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
            int.TryParse(dificultOfPreparationString.KeyChar.ToString(), out int dificultOfPreparation);
            Console.WriteLine("\r\nPlease enter number of portions: ");
            var numberOfPortionsString = Console.ReadKey();
            int.TryParse(numberOfPortionsString.KeyChar.ToString(), out int numberOfProportions);
            var lastId = _recipeService.GetLastId();
            var lastIdHere = lastId + 1;
            //czy wprowadzenie lastId dało efekt?
            Recipe recipe = new Recipe(lastIdHere, name, categoryId, ingredients, description, timeOfPreparation, dificultOfPreparation, numberOfProportions);
            _recipeService.AddRecipe(recipe);
            _recipeService.SaveDataFromListToJson(_recipeService.SerializeListToStringInJson());
           // _recipeService.SaveListAsJson();
            return recipe.Id;
        }

        public void DeleteRecipeById()
        {
            Console.WriteLine("\r\nPlease insert id of recipe to delete: ");
            var idToRemove = Console.ReadLine();
            if (int.TryParse(idToRemove, out int id))
            {
                RemoveRecipeById(id);
                Console.WriteLine("\r\nRecipe delete successfully.");
            }
            else
            {
                Console.WriteLine("\r\nInvalid input for recipe id.");
                // RemoveRecipeById();
            }
            _recipeService.SaveDataFromListToJson(_recipeService.SerializeListToStringInJson());
        }

        public void GetByIdRecipe()
        {
            Console.WriteLine("\r\nPlease insert recipe id to show.");
            var idToShow = Console.ReadLine();
            if (int.TryParse(idToShow, out int id))
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
                if (recipe.Ingredients.Any(i => i.NameIngredient == recipesToShowByIngredient))
                {
                    matchingRecipes.Add(recipe);
                }
            }
            if (matchingRecipes.Count == 0)
            {
                Console.WriteLine("There is no recipes with insert ingredient");
                // GetRecipeByIngredient();
            }
            else
            {
                foreach (var recipe in matchingRecipes)
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
            var addNewRecipeMenu = _actionService.GetMenuActionsByMenuName("AddNewRecipeMenu");
            Console.WriteLine("\r\nPlease select category of meal: ");
            for (int i = 0; i < addNewRecipeMenu.Count; i++)
            {
                Console.WriteLine($"{addNewRecipeMenu[i].Id} {addNewRecipeMenu[i].Name}");
            }
            Console.WriteLine("\r\nPlease enter number of category of recipes to show: ");
            var recipeCategory = Console.ReadLine();
            int.TryParse(recipeCategory, out int category);
            var allRecipes = _recipeService.GetAllRecipes();
            List<Recipe> productToShow = new List<Recipe>();            
            foreach (var recipe in allRecipes)
            {
                if (recipe.CategoryId == category)
                {
                    productToShow.Add(recipe);                   
                }
            }
            if (productToShow.Count > 0)
            {
                foreach (var recipe in productToShow)
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
            }
            else
            {
                Console.WriteLine("There is no recipes with insert category");
            }
        }

        public void EditRecipe()
        {
            Console.WriteLine("\r\nPlease insert id to edit:");
            var recipeToEdit = Console.ReadLine();
            if (int.TryParse(recipeToEdit, out int idToChange))
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
                    int.TryParse(selectKindOfData.KeyChar.ToString(), out int categoryDataId);
                    switch (categoryDataId)
                    {
                        case 1:
                            Console.WriteLine("\r\nInsert new name of recipe: ");
                            var newName = Console.ReadLine();
                            recipeToChange.Name = newName; // w tym miejscu jest zmieniana wartość w przepisie a następnie podmieniany cały przepis
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
                            int.TryParse(operation.KeyChar.ToString(), out int categoryId);
                            recipeToChange.CategoryId = categoryId;
                            _recipeService.UpdateRecipe(recipeToChange);
                            break;
                        case 3:
                            Console.WriteLine("\r\nDo you want add or edit ingredients :");
                            Console.WriteLine("1 - add ingredients.");
                            Console.WriteLine("2 - edit ingredients.");
                            var selectOperationWithIngredientsS = Console.ReadKey();
                            int.TryParse(selectOperationWithIngredientsS.KeyChar.ToString(), out int selectOperationWithIngredients);
                            switch (selectOperationWithIngredients)
                            {
                                case 1:
                                    //tu będziemy dodawać składnik
                                    var ingredient = new List<Ingredient>();
                                    Console.WriteLine("\r\nPlease enter name of new ingredient: ");
                                    var nameAddedIngredient = Console.ReadLine();
                                    Console.WriteLine($"\r\nPlease enter quantity of {nameAddedIngredient}:");
                                    var quantityStringAdded = Console.ReadLine();
                                    int.TryParse(quantityStringAdded, out int quantityAdded);
                                    Console.WriteLine($"\r\nPlease enter unit of {nameAddedIngredient}:");
                                    var unitAdded = Console.ReadLine();
                                    ingredient = recipeToChange.Ingredients;
                                    ingredient.Add(new Ingredient(nameAddedIngredient, quantityAdded, unitAdded));
                                    recipeToChange.Ingredients = ingredient;
                                    _recipeService.UpdateRecipe(recipeToChange);
                                    break;
                                case 2:

                                    Console.WriteLine("\r\nPlease enter number of ingredients: ");
                                    var numberOfIngredient = Console.ReadLine();
                                    int recipeNumberOfIgredient;
                                    bool isNumeric = int.TryParse(numberOfIngredient, out recipeNumberOfIgredient);
                                    var ingredients = new List<Ingredient>();
                                    if (isNumeric)
                                    {
                                        for (int i = 0; i < recipeNumberOfIgredient; i++)
                                        {
                                            Console.WriteLine($"\r\nPlease enter {i + 1} ingredients:");
                                            var name = Console.ReadLine();
                                            Console.WriteLine($"\r\nPlease enter quantity of {name}:");
                                            var quantityString = Console.ReadLine();
                                            int.TryParse(quantityString, out int quantity);
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
                            }
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
                            int.TryParse(newDifficultOfRecipeString.KeyChar.ToString(), out int difficult);
                            recipeToChange.Difficulty = difficult;
                            _recipeService.UpdateRecipe(recipeToChange);
                            break;
                        case 7:
                            Console.WriteLine("\r\nInsert new number portions of recipe :");
                            var newNumberOfPortions = Console.ReadKey();
                            int.TryParse(newNumberOfPortions.KeyChar.ToString(), out int portion);
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
                _recipeService.SaveDataFromListToJson(_recipeService.SerializeListToStringInJson()); //po edycji nadpisz plik z danymi
            }
            else
            {
                Console.WriteLine("\r\nInvalid input for recipe id.");
            }
            Console.WriteLine("\r\nDo you want change data in recipes: \r\n1 - yes\r\n2 - no");
            var againEdit = Console.ReadKey();
            int.TryParse(againEdit.KeyChar.ToString(), out int againEditInt);
            if (againEditInt == 1)
            {
                EditRecipe();
            }
        }

        public void RemoveRecipeById(int id)// może to do recipe service 
        {
            var recipe = _recipeService.GetRecipeById(id);
            if (recipe != null)
            {
                _recipeService.RemoveRecipe(recipe);
            }
            else
            {
                Console.WriteLine("You have selected an Id number that does not contain a recipe");
            }
        }
    }
}
