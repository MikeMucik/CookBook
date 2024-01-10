using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook
{
    public class RecipeService
    {
        public List<Recipe> Recipes { get; set; }
        public RecipeService()
        {
            Recipes = new List<Recipe>();
        }




        public ConsoleKeyInfo AddNewRecipeView(MenuActionService actionService)
        {
            var addNewRecipeMenu = actionService.GetMenuActionsByMenuName("AddNewRecipeMenu");
            Console.WriteLine("\r\nPlease select category of meal: ");
            for (int i = 0; i < addNewRecipeMenu.Count; i++)
            {
                Console.WriteLine($"{addNewRecipeMenu[i].Id} {addNewRecipeMenu[i].Name}");
            }
            var operation = Console.ReadKey();
            return operation;//wybór posiłku np Breakfast
        }

        public int AddNewRecipe(char recipeCategory)
        {
            int recipeCategoryId;
            Int32.TryParse(recipeCategory.ToString(), out recipeCategoryId);
            Recipe recipe = new Recipe();
            recipe.CategoryId = recipeCategoryId;
            Console.WriteLine("\r\nPlease enter id for a new recipe: ");
            var id = Console.ReadLine();
            int recipeId;
            Int32.TryParse(id, out recipeId);
            Console.WriteLine("\r\nPlease enter name for a new recipe: ");
            var name = Console.ReadLine();


            Console.WriteLine("\r\nPlease enter number of ingredients: ");
            var numberOfIngredient = Console.ReadLine();
            int recipeNumberOfIgredient;
            Int32.TryParse(numberOfIngredient, out recipeNumberOfIgredient);
            for (int i = 0; i < recipeNumberOfIgredient; i++)
            {
                Console.WriteLine($"\r\nPlease enter {i + 1} ingredients:");
                var ingredient = Console.ReadLine();
                recipe.Ingredients.Add(ingredient);

            }

            recipe.Id = recipeId;// tu dodajemy id
            recipe.Name = name; //czemu tu jest ostrzeżenie ?(teraz nie ma bo jest"?" w recipe       tu dodajemy nazwę 
            Recipes.Add(recipe);//Tu dodajemy przepis id+nazwa wcześniej wybrana kategoria
            return recipeId;

        }

        //public int AddNewRecipe(char recipeCategory, int id)
        // {
        //     int recipeCategoryId;
        //     Int32.TryParse(recipeCategory.ToString(), out recipeCategoryId);
        //     Recipe recipe = new Recipe();
        //     recipe.CategoryId = recipeCategoryId;
        //     Console.WriteLine("\r\nPlease enter name for new recipe: ");
        //     var name = Console.ReadLine();

        //     recipe.Name = name;

        //     Recipes.Add(recipe);
        //     return id;
        // }
        public int RemoveRecipeView()
        {
            Console.WriteLine("\r\nPlease enter number id of recipe to remove: ");
            var recipeId = Console.ReadLine();
            int id;
            Int32.TryParse(recipeId, out id);
            return id;
        }
        public void RemoveRecipe(int removeId)
        {
            Recipe productToRemove = new Recipe();
            foreach (var recipe in Recipes)
            {
                if (recipe.Id == removeId)
                {
                    productToRemove = recipe;
                    break;
                }
            }
            Recipes.Remove(productToRemove); //tu usuwamy przepis
        }

        public int RecipeChooseIdView()
        {
            Console.WriteLine("\r\nPlease enter number id of recipe to show: ");
            var recipeId = Console.ReadLine();
            int id;
            Int32.TryParse(recipeId, out id);
            return id;
        }

        public void ShowRecipe(int detailId)
        {
            Recipe productToShow = new Recipe();
            foreach (var recipe in Recipes)
            {
                if (recipe.Id == detailId)
                {
                    productToShow = recipe;
                    break;
                }
            }
            Console.WriteLine($"Recipe id: {productToShow.Id} \r\nRecipe name: {productToShow.Name} \r\nRecipe category: {productToShow.CategoryId}");
            //Console.WriteLine($"\r\nRecipe ingredients: {productToShow.Ingredients}\r\nRecipe description: {productToShow.Description}");
            Console.WriteLine("Recipe ingredients :");
            foreach (var ingredient in productToShow.Ingredients)
            {
                Console.Write(" " + ingredient);
            }

        }

        public int RecipeChooseCategoryView()
        {
            Console.WriteLine("\r\nPlease enter number of category of recipes to show: ");
            var recipeCategory = Console.ReadLine();
            int category;
            Int32.TryParse(recipeCategory, out category);
            return category;
        }

        public void ShowRecipesByCategory(int detailRecipeByCategory)
        {
            Recipe productToShow = new Recipe();
            foreach (var recipe in Recipes)
            {
                if (recipe.CategoryId == detailRecipeByCategory)
                {
                    productToShow = recipe;

                }

                Console.WriteLine($"\r\nRecipe id: {productToShow.Id} \r\nRecipe name: {productToShow.Name} \r\nRecipe category: {productToShow.CategoryId}");
                //Console.WriteLine($"\r\nRecipe ingredients: {productToShow.Ingredients}\r\nRecipe description: {productToShow.Description}");
                Console.WriteLine("Recipe ingredients :");
                foreach (var ingredient in productToShow.Ingredients)
                {
                    Console.Write(" " + ingredient);
                }
            }
        }

        public string RecipeChooseIngredientView()
        {
            Console.WriteLine("\r\nPlease enter one ingredient to show recipes: ");
            var detailRecipeByIngredient = Console.ReadLine();
            return detailRecipeByIngredient;
        }

        public void ShowRecipesByIngredient(string detailRecipeByIngredient)
        {
            //  Recipe productToShow = new Recipe();
            foreach (var recipe in Recipes)
            {
                if (recipe.Ingredients.Contains(detailRecipeByIngredient))
                {
                    //  productToShow = recipe;



                    Console.WriteLine($"\r\nRecipe id: {recipe.Id} \r\nRecipe name: {recipe.Name} \r\nRecipe category: {recipe.CategoryId}");
                    Console.WriteLine($"\r\nRecipe description: {recipe.Description}");
                    Console.WriteLine("Recipe ingredients :");
                    foreach (var ingredient in recipe.Ingredients)
                    {
                        Console.Write(" " + ingredient);
                    }
                }
            }
        }
        //public int ShowRecipesByCategoryView()
        //{
        //    var addNewRecipeMenu = actionService.GetMenuActionsByMenuName("AddNewRecipeMenu");
        //    Console.WriteLine("\r\nPlease select category of meal: ");
        //    for (int i = 0; i < addNewRecipeMenu.Count; i++)
        //    {
        //        Console.WriteLine($"{addNewRecipeMenu[i].Id} {addNewRecipeMenu[i].Name}");
        //    }
        //    Console.WriteLine("Choose category of ")
        //    return CategoryId
        //}





    }
}
