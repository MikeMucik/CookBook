using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using CookBook.Domain.Entity;
using Newtonsoft.Json;
using CookBook.App.Concrete;
using CookBook.App.Abstract;
using System.Collections;
using CookBook.App.Common;


namespace CookBook.App.Concrete
{
    public class ListService
    {
        public void Method()
        {
            List<Recipe> list = new List<Recipe>();
            List<Ingredient> ingredients = new List<Ingredient>
            {
                new Ingredient("Egg", 3, "pieces"),
                new Ingredient("Butter", 25, "grams"),
                new Ingredient("Salt", 2, "pinches")
            };
            Recipe recipe = new Recipe(1, "fried eggs", 1, ingredients, " ALe jaja", "15 minut", 1, 1);
            List<Ingredient> ingredients1 = new List<Ingredient>
            {
                new Ingredient("Bread", 2, "slices"),
                new Ingredient("Butter", 25, "grams"),
                new Ingredient("ham", 2, "slices")
            };
            Recipe recipe1 = new Recipe(2, "sandwich", 1, ingredients1, " Ale kanapka", "5 minut", 1, 1);
            list.Add(recipe1);
            list.Add(recipe);

            string output = JsonConvert.SerializeObject(list);

            var recipes = JsonConvert.DeserializeObject<List<Recipe>>(output);

            using StreamWriter sw = new StreamWriter(@"C:\Temp\recipes.txt");
            using JsonWriter writer = new JsonTextWriter(sw);

            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(writer, list);
        }
        public List<Recipe> MethodRead()
        {
            string input;
            using (StreamReader sr = new StreamReader(@"C:\Temp\recipes.txt"))
            {
                input = sr.ReadToEnd();
            }
             List<Recipe> recipes = JsonConvert.DeserializeObject<List<Recipe>>(input);
            // Recipe recipeList;
            //recipes.Add(input)
            return recipes;

        }
        public void MethodWrite()
        {
            List<Recipe> list = new List<Recipe>();
            string output = JsonConvert.SerializeObject(list);

            var recipes = JsonConvert.DeserializeObject<List<Recipe>>(output);

            using StreamWriter sw = new StreamWriter(@"C:\Temp\recipes.txt");
            using JsonWriter writer = new JsonTextWriter(sw);

            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(writer, list);         
        }
        public static void MethodReadJson()
        {
            string input;
            using (StreamReader sr = new StreamReader(@"C:\Temp\recipes.txt"))
            {
                input = sr.ReadToEnd();

            }

        }
    //    [Fact]
    //    public void CanGetRecipeByCorrectId1()
    //    {
    //        //Arrange
    //        //Prepare data
    //        List<Ingredient> ingredients = new List<Ingredient>
    //{
    //    new Ingredient("Egg", 3, "piece"),
    //    new Ingredient("Butter", 25, "grams"),
    //    new Ingredient("Salt", 2, "pinches")
    //};
    //        Recipe recipe = new Recipe(1, "fried eggs", 1, ingredients, " ALe jaja", "15 minut", 1, 1);

    //        //Prepare mock service
    //        var mock = new Mock<IService<Recipe>>();
    //        mock.Setup(s => s.GetRecipeById(1)).Returns(recipe);
    //        var recipeService = mock.Object;

    //        //Act
    //        var returnedRecipe = recipeService.GetRecipeById(recipe.Id);

    //        //Assert
    //        mock.Verify(s => s.GetRecipeById(recipe.Id), Times.Once); // Sprawdzenie, czy metoda GetRecipeById została wywołana dokładnie raz
    //        returnedRecipe.Should().Be(recipe); // Sprawdzenie, czy zwrócony przepis jest poprawny
    //    }
    }
}
