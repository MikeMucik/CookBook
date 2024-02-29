using System.Collections.Generic;
using CookBook.App.Abstract;
using CookBook.App.Concrete;
using CookBook.App.Managers;
using CookBook.Domain.Entity;
using FluentAssertions;
using Moq;

namespace CookBook.Test

{
    public class UnitTest1
    {
   
        [Fact]
        public void CanGetRecipeByCorrectId() // to jest ok
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
            var mock = new Mock<IService<Recipe>>();
            mock.Setup(s => s.GetRecipeById(1)).Returns(recipe);           
            var recipeService = mock.Object;
            //Act           
            var returnedRecipe = recipeService.GetRecipeById(recipe.Id);
            //Assert            
            returnedRecipe.Should().BeOfType(typeof(Recipe));
            returnedRecipe.Should().NotBeNull();
            returnedRecipe.Should().BeSameAs(recipe);
            mock.Verify(s => s.GetRecipeById(recipe.Id));
        }

        [Fact]
        public void CanGetRecipeByIdReturnedNullWhenRecipeNotExist() //to jest ok
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
            var mock = new Mock<IService<Recipe>>();
            mock.Setup(s => s.GetRecipeById(1000)).Returns(recipe); //not existing recipe
            var recipeServis = mock.Object;
            //Act
            var returnedRecipe = recipeServis.GetRecipeById(recipe.Id);
            //Assert
            Assert.NotEqual(recipe, returnedRecipe);
            returnedRecipe.Should().BeNull();
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
            Recipe recipe = new Recipe(1, "fried eggs", 1, ingredients, " ALe jaja", "15 minut", 1, 1);
            //Prepare rest
            var mock = new Mock<IService<Recipe>>();
            mock.Setup(s => s.GetLastId()).Returns(recipe.Id);
            var recipeService = new RecipeService();
            recipeService.AddRecipe(recipe);
            //Act            
            var returnedId = recipeService.GetLastId();
            //Assert
            Assert.Equal(recipe.Id, returnedId);
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
            var mock = new Mock<IService<Recipe>>();
            mock.Setup(s => s.GetRecipeById(1)).Returns(recipe); //proper Id
            mock.Setup(s => s.RemoveRecipe(It.IsAny<Recipe>()));
            var manager = new RecipeManager(new MenuActionService(), mock.Object);
            //Act
            manager.RemoveRecipeById(recipe.Id);
            //Assert
            mock.Verify(s => s.RemoveRecipe(recipe));

        }

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
            var mock = new Mock<IService<Recipe>>();
            mock.Setup(s => s.AddRecipe(recipe)).Returns(recipe.Id);
            var recipeService = mock.Object;
            //Act
            var returnedRecipeId = recipeService.AddRecipe(recipe);
            //Assert
            Assert.Equal(recipe.Id, returnedRecipeId); // czy metoda zwraca to samo id co posiada obiekt mock
            mock.Verify(s => s.AddRecipe(recipe)); //czy metoda zosta³a wywo³ana
                                                   //                    Assert.Equal(recipe, returnedRecipe);
        }


        [Fact]
        public void CanEditRecipe()
        {
            //Arrange
            //Prepare Data
            List<Ingredient> ingredients = new List<Ingredient>
            {
                new Ingredient("Egg", 3, "pieces"),
                new Ingredient("Butter", 25, "grams"),
                new Ingredient("Salt", 2, "pinches")
            };
            Recipe recipe = new Recipe(1, "fried eggs", 1, ingredients, " ALe jaja", "15 minut", 1, 1);
            //Prepare rest
            var mock = new Mock<IService<Recipe>>();
            mock.Setup(s => s.UpdateRecipe(recipe));           
            var recipeService = mock.Object;
            //Act           
            var editedRecipe = recipeService.UpdateRecipe(recipe);            
            //Assert
            mock.Verify(s => s.UpdateRecipe(recipe));// czy metoda zosta³a wywo³ana            
            editedRecipe.Equals(recipe);
        }
    }
}