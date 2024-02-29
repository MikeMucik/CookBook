using System.Collections;
using System.Collections.Generic;
using CookBook.App.Abstract;
using CookBook.App.Concrete;
using CookBook.App.Managers;
using CookBook.Domain.Entity;
using FluentAssertions;
//using Moq;

namespace CookBook.Test
{
    public class IntegrationTest
    {
        [Fact] 
        public void CanAddRecipe()
        {
            //Arrange
            //Prepare data
            List<Ingredient> ingredients = new List<Ingredient>
            {
                new Ingredient("Egg", 3, "piece"),
                new Ingredient("Butter", 25, "grams"),
                new Ingredient("Salt", 2, "pinches")
            };
            Recipe recipe = new Recipe(1, "fried eggs", 1, ingredients, " ALe jaja", "15 minut", 1, 1);
            //Prepare rest            
            IService<Recipe> recipeService = new RecipeService();
            //Act
            var returnedRecipeId = recipeService.AddRecipe(recipe); // to jest int
            Recipe returnedRecipe = recipeService.GetRecipeById(returnedRecipeId);
            //Assert
            Assert.Equal(recipe.Id, returnedRecipeId); // czy metoda zwraca to samo id co posiada obiekt mock
            Assert.Equal(recipe, returnedRecipe);
            recipeService.GetRecipeById(recipe.Id).Should().NotBeNull();
            recipeService.GetRecipeById(recipe.Id).Should().BeSameAs(recipe);
            //Clean
            recipeService.RemoveRecipe(recipe);
        }
        [Fact]
        public void CanDeleteRecipeWithProperId()
        {
            //Arrange
            //Prepare data
            List<Ingredient> ingredients = new List<Ingredient>
            {
                new Ingredient("Egg", 3, "piece"),
                new Ingredient("Butter", 25, "grams"),
                new Ingredient("Salt", 2, "pinches")
            };
            Recipe recipe = new Recipe(1, "fried eggs", 1, ingredients, " ALe jaja", "15 minut", 1, 1);
            //Prepare rest
            IService<Recipe> recipeService = new RecipeService();
            recipeService.AddRecipe(recipe);
            var manager = new RecipeManager(new MenuActionService(), recipeService);

            //Act
            manager.RemoveRecipeById(recipe.Id);
            //Assert
            recipeService.GetRecipeById(recipe.Id).Should().BeNull();
            recipeService.Recipes.FirstOrDefault(r => r.Id == recipe.Id).Should().BeNull();
        }

        [Fact]   // Czy ten test ma sens skoro sprawdzono to w teście jednostkowym, a w prawdziwej bazie w ten sposó nie
        // dodam ostatniego, chyba żę baza pusta

        public void CanGetLastId()
        {
            //Arrange
            //Prepare data
            List<Ingredient> ingredients = new List<Ingredient>
                {
                    new Ingredient("Egg", 3, "piece"),
                    new Ingredient("Butter", 25, "grams"),
                    new Ingredient("Salt", 2, "pinches")
                };
            Recipe recipe = new Recipe(1, "fried eggs", 1, ingredients, " ALe jaja", "15 minut", 1, 1);
            //Prepare rest
            IService<Recipe> recipeService = new RecipeService();
            recipeService.AddRecipe(recipe);
            //Act
            var returnedId = recipeService.GetLastId();
            //Assert            
            returnedId.Should().Be(recipe.Id);
            //Clean
            recipeService.RemoveRecipe(recipe);
        }


        [Fact]
        public void CanGetAllRecipes() //przy pustej bazie uważam że jest to dobrze
        {
            //Arrange
            //Prepare data
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
            List<Recipe> expectedlist = new List<Recipe> { recipe, recipe1 };
            IService<Recipe> recipeService = new RecipeService();
            recipeService.AddRecipe(recipe);
            recipeService.AddRecipe(recipe1);
            //Act
            var returnedRecipes = recipeService.GetAllRecipes();
            //Asser
            returnedRecipes.Should().BeOfType<List<Recipe>>();
            returnedRecipes.Should().BeEquivalentTo(expectedlist);
            //Clean
            recipeService.RemoveRecipe(recipe);
            recipeService.RemoveRecipe(recipe1);
        }
        [Fact]
        public void CanUpdateRecipe()
        {
            //Arrange
            //Prepare data           
            List<Ingredient> ingredients = new List<Ingredient>
            {
                new Ingredient("Egg", 3, "piece"),
                new Ingredient("Butter", 25, "grams"),
                new Ingredient("Salt", 2, "pinches")
            };
            Recipe recipe = new Recipe(1, "fried eggs", 1, ingredients, " ALe jaja", "15 minut", 1, 1);
            //prepare rest
            IService<Recipe> recipeService = new RecipeService();
            recipeService.AddRecipe(recipe);
            //Act
            var editedRecipe = recipeService.UpdateRecipe(recipe);
            //Assert
            editedRecipe.Should().Be(recipe.Id);
            editedRecipe.Equals(recipe);
            //Clean
            recipeService.RemoveRecipe(recipe);
        }
        [Fact]
        public void CanGetRecipeById()
        {
            //Arrange
            //Prepare data           
            List<Ingredient> ingredients = new List<Ingredient>
            {
                new Ingredient("Egg", 3, "piece"),
                new Ingredient("Butter", 25, "grams"),
                new Ingredient("Salt", 2, "pinches")
            };
            Recipe recipe = new Recipe(1, "fried eggs", 1, ingredients, "Ale jaja", "15 minut", 1, 1);
            IService<Recipe> recipeServis = new RecipeService();
            recipeServis.AddRecipe(recipe);
            // Act
            var result = recipeServis.GetRecipeById(1);
            //Assert
            result.Should().Be(recipe);
            result.Equals(recipe);
            result.Should().NotBeNull();
            result.Should().BeSameAs(recipe);
        }
    }
}


