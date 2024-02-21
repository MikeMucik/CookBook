using System.Collections;
using System.Collections.Generic;
using CookBook.App.Abstract;
using CookBook.App.Concrete;
using CookBook.App.Managers;
using CookBook.Domain.Entity;
using FluentAssertions;
using Moq;

namespace CookBook.Test
{
    public class IntegrationTest
    {
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

        [Fact]
        public void CanAddNewRecipe()
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
            var manager = new RecipeManager(new MenuActionService(), recipeService);
            //Act
            manager.AddRecipeNew(recipe);
            //Assert
            recipeService.GetRecipeById(recipe.Id).Should().NotBeNull();
            recipeService.GetRecipeById(recipe.Id).Should().BeSameAs(recipe);
            //recipeService.Recipes.FirstOrDefault(r => r.Id == recipe.Id).Should().NotBeNull();
            //Clean
            manager.RemoveRecipeById(recipe.Id);
        }

        [Fact]
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
            int lastId = 1;
            Recipe recipe = new Recipe(lastId, "fried eggs", 1, ingredients, " ALe jaja", "15 minut", 1, 1);
            //Prepare rest
            IService<Recipe> recipeService = new RecipeService();
            recipeService.AddRecipe(recipe);
            var manager = new RecipeManager(new MenuActionService(), recipeService);
            //Act
            var returnedId = manager.GetLastId();
            //Assert
            Assert.Equal(recipe.Id, returnedId);
            // returnedId.Should().BeSameAs(recipe.Id);
            //Clean
            recipeService.RemoveRecipe(recipe);
        }


        [Fact]
        public void CanGetAllRecipes() //przy pustej bazie
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
            List<Recipe> list = new List<Recipe>();

            //Prepare rest

            list.Add(recipe1);
            list.Add(recipe);

            IService<Recipe> recipeService = new RecipeService();
            var manager = new RecipeManager(new MenuActionService(), recipeService);

            recipeService.AddRecipe(recipe);
            recipeService.AddRecipe(recipe1);
            //Act
            var returnedRecipes = manager.GetAllRecipes();
            //Asser

            returnedRecipes.Should().BeOfType<List<Recipe>>();
            returnedRecipes.Should().BeEquivalentTo(list);
            //Clean
            recipeService.RemoveRecipe(recipe);
            recipeService.RemoveRecipe(recipe1);
        }
        [Fact]
        public void CanUpdateRecipeById()
        {
            //Arrange
            //Prepare data
            int editedId = 1;
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
            var manager = new RecipeManager(new MenuActionService(), recipeService);
            //Act
            var editedRecipe = manager.EditRecipeById(editedId);
            //Assert
            editedRecipe.Should().Be(recipe.Id);
            editedRecipe.Equals(recipe);
        }
    }
}


