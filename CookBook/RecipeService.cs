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
        private static int recipeId = 0; // próba nadawania id




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
            //Console.WriteLine("\r\nPlease enter id for a new recipe: ");
            //var id = Console.ReadLine();
            recipeId++;
            recipe.Id = recipeId;

            //Int32.TryParse(id, out recipeId);

            Console.WriteLine("\r\nPlease enter name for a new recipe: ");
            var name = Console.ReadLine();


            Console.WriteLine("\r\nPlease enter number of ingredients: ");
            var numberOfIngredient = Console.ReadLine();
            //sprawdzenie czy użytkownik podał liczbę

            int recipeNumberOfIgredient;
            bool isNumeric = Int32.TryParse(numberOfIngredient, out recipeNumberOfIgredient);

            if (isNumeric)
            {
                for (int i = 0; i < recipeNumberOfIgredient; i++)
                {
                    Console.WriteLine($"\r\nPlease enter {i + 1} ingredients:");
                    var ingredient = Console.ReadLine();
                    recipe.Ingredients.Add(ingredient);

                }
            }
            else
            {
                Console.WriteLine("You didn't enter number of ingredients");
            }
            Console.WriteLine("\r\nPlease enter description of recipe: ");

            var description = Console.ReadLine();
            recipe.Description = description;

            recipe.Id = recipeId;// tu dodajemy id

            recipe.Name = name; //czemu tu jest ostrzeżenie ?(teraz nie ma bo jest"?" w recipe, tu dodajemy nazwę 
            Recipes.Add(recipe);//Tu dodajemy przepis id+nazwa wcześniej wybrana kategoria
            return recipeId;
        }


        public int RemoveRecipeView()
        {
            Console.WriteLine("\r\nPlease enter number id of recipe to remove: ");
            var recipeId = Console.ReadLine();
            int id;
            Int32.TryParse(recipeId, out id);
            return id; // które poniżej będzie równe removeId
        }
        public void RemoveRecipe(int removeId)
        {
            Recipe productToRemove = null; // jak daje "Recipe productToRemove ;" mam błąd kompilacji dlatego = null
            foreach (var recipe in Recipes)
            {
                if (recipe.Id == removeId)
                {
                    productToRemove = recipe;
                    break;
                }
            }
            if (productToRemove != null)
            {
                Recipes.Remove(productToRemove); //tu usuwamy przepis
            }
            else
            {
                Console.WriteLine("You didn't choose existing recipe");
            }
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
            Recipe productToShow = null; // jak daje "Recipe productToShow ;" mam błąd kompilacji dlatego = null
            foreach (var recipe in Recipes)
            {
                if (recipe.Id == detailId)
                {
                    productToShow = recipe;
                    break;
                }
            }
            if (productToShow != null)
            {
                Console.WriteLine($"Recipe id: {productToShow.Id} \r\nRecipe name: {productToShow.Name} \r\nRecipe category: {productToShow.CategoryId}");
                //Console.WriteLine($"\r\nRecipe ingredients: {productToShow.Ingredients}\r\nRecipe description: {productToShow.Description}");
                Console.WriteLine("Recipe ingredients :");
                foreach (var ingredient in productToShow.Ingredients)
                {
                    Console.Write(" " + ingredient);
                }
                Console.WriteLine($"\r\nRecipe description: {productToShow.Description}");
            }
            else
            {
                Console.WriteLine("There is no recipe for a given id ");
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
            Recipe productToShow = null; // jak daje "Recipe productToShow ;" mam błąd kompilacji dlatego = null
            foreach (var recipe in Recipes)
            {
                if (recipe.CategoryId == detailRecipeByCategory)
                {
                    productToShow = recipe;
                }
            }
            if (productToShow != null)
            {
                Console.WriteLine($"\r\nRecipe id: {productToShow.Id} \r\nRecipe name: {productToShow.Name} \r\nRecipe category: {productToShow.CategoryId}");
                Console.WriteLine("Recipe ingredients :");
                foreach (var ingredient in productToShow.Ingredients)
                {
                    Console.Write(" " + ingredient);
                }
                Console.WriteLine($"\r\nRecipe description: {productToShow.Description}");
            }
            else
            {
                Console.WriteLine("There is no recipes to show");
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
            foreach (var recipe in Recipes)
            {
                if (recipe.Ingredients.Contains(detailRecipeByIngredient))
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
                    Console.WriteLine("There is no recipes with insert ingredient");
                }
            }
        }
    }
}
