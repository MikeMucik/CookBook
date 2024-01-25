using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.App.Common;
using CookBook.Domain;
using CookBook.Domain.Entity;

namespace CookBook.App.Concrete
{
    public class RecipeService : BaseService<Recipe>
    {

    }
}


//public int RecipeChooseIdView()
//{
//    Console.WriteLine("\r\nPlease enter number id of recipe to show: ");
//    var recipeId = Console.ReadLine();
//    int id;
//    Int32.TryParse(recipeId, out id);
//    return id;
//}

//public void ShowRecipe(int detailId)
//{
//    Recipe productToShow = new Recipe();
//    foreach (var recipe in Recipes)
//    {
//        if (recipe.Id == detailId)
//        {
//            productToShow = recipe;
//            break;
//        }
//    }
//    Console.WriteLine($"Recipe id: {productToShow.Id} \r\nRecipe name: {productToShow.Name} \r\nRecipe category: {productToShow.CategoryId}");
//    //Console.WriteLine($"\r\nRecipe ingredients: {productToShow.Ingredients}\r\nRecipe description: {productToShow.Description}");
//    Console.WriteLine("Recipe ingredients :");
//    foreach (var ingredient in productToShow.Ingredients)
//    {
//        Console.Write(" " + ingredient);
//    }
//    Console.WriteLine($"\r\nRecipe description: {productToShow.Description}");

//}

//public int RecipeChooseCategoryView()
//{
//    Console.WriteLine("\r\nPlease enter number of category of recipes to show: ");
//    var recipeCategory = Console.ReadLine();
//    int category;
//    Int32.TryParse(recipeCategory, out category);
//    return category;
//}

//public void ShowRecipesByCategory(int detailRecipeByCategory)
//{
//    Recipe productToShow = new Recipe();
//    foreach (var recipe in Recipes)
//    {
//        if (recipe.CategoryId == detailRecipeByCategory)
//        {
//            productToShow = recipe;
//        }

//        Console.WriteLine($"\r\nRecipe id: {productToShow.Id} \r\nRecipe name: {productToShow.Name} \r\nRecipe category: {productToShow.CategoryId}");
//        //Console.WriteLine($"\r\nRecipe ingredients: {productToShow.Ingredients}\r\nRecipe description: {productToShow.Description}");
//        Console.WriteLine("Recipe ingredients :");
//        foreach (var ingredient in productToShow.Ingredients)
//        {
//            Console.Write(" " + ingredient);
//        }
//        Console.WriteLine($"\r\nRecipe description: {productToShow.Description}");
//    }
//}

//public string RecipeChooseIngredientView()
//{
//    Console.WriteLine("\r\nPlease enter one ingredient to show recipes: ");
//    var detailRecipeByIngredient = Console.ReadLine();
//    return detailRecipeByIngredient;
//}

//public void ShowRecipesByIngredient(string detailRecipeByIngredient)
//{
//    foreach (var recipe in Recipes)
//    {
//        if (recipe.Ingredients.Contains(detailRecipeByIngredient))
//        {
//            Console.WriteLine($"\r\nRecipe id: {recipe.Id} \r\nRecipe name: {recipe.Name} \r\nRecipe category: {recipe.CategoryId}");

//            Console.WriteLine("Recipe ingredients :");
//            foreach (var ingredient in recipe.Ingredients)
//            {
//                Console.Write(" " + ingredient);
//            }
//            Console.WriteLine($"\r\nRecipe description: {recipe.Description}");
//        }
//    }
//}

