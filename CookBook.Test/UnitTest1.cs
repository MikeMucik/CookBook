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
        //Testy jednostkowe
        [Fact]
        public void CanGetRecipeById() // to jest ok
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
            var manager = new RecipeManager(new MenuActionService(), mock.Object);
            //Act
            var returnedRecipe = manager.GetRecipeById(recipe.Id);
            //Assert
            //Assert.Equal(recipe, returnedRecipe);
            returnedRecipe.Should().BeOfType(typeof(Recipe));
            returnedRecipe.Should().NotBeNull();
            returnedRecipe.Should().BeSameAs(recipe);
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
            var manager = new RecipeManager(new MenuActionService(), mock.Object);
            //Act
            var returnedRecipe = manager.GetRecipeById(recipe.Id);
            //Assert
            Assert.NotEqual(recipe, returnedRecipe);
            returnedRecipe.Should().BeNull();
        }

        [Fact]
        public void CanGetLastId() // to jest ok
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
            var manager = new RecipeManager(new MenuActionService(), mock.Object);
            //Act
            var returnedId = manager.GetLastId();
            //Assert
            Assert.Equal(recipe.Id, returnedId);
            // returnedId.Should().BeSameAs(recipe.Id);  // Czy dobrze rozumiem �e warto�ci(int) s� inaczej zapisywane w pami�ci ni� obiekty
            // i przy por�wnaniu int a klasy trzeba to inaczej zrobi�; warto�� a referencja
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
        public void CanAddRecipe() //to jest ok
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
            var manager = new RecipeManager(new MenuActionService(), mock.Object);
            //Act
            var returnedRecipeId = manager.AddRecipeNew(recipe); // to jest int
            Recipe returnedRecipe = manager.GetRecipeById(returnedRecipeId);
            //Assert
            Assert.Equal(recipe.Id, returnedRecipeId); // czy metoda zwraca to samo id co posiada obiekt mock
            mock.Verify(s => s.AddRecipe(recipe)); //czy metoda zosta�a wywo�ana
          //  Assert.Equal(recipe, returnedRecipe);
        }

        [Fact]

        public void CanGetAllRecipes() // to jest ok
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
            list.Add(recipe1);
            list.Add(recipe);
            //Prepare rest
            var mock = new Mock<IService<Recipe>>();
            mock.Setup(s => s.GetAllRecipes()).Returns(list);
            var manager = new RecipeManager(new MenuActionService(), mock.Object);
            //Act
            var returnedRecipes = manager.GetAllRecipes();
            //Asser
            mock.Verify(s => s.GetAllRecipes()); // rozumiem to �e to czy wywo�a�o funkcje
            returnedRecipes.Should().BeOfType<List<Recipe>>(); // �e lista zwr�cona przez metod� to lista przepis�w
            returnedRecipes.Should().BeEquivalentTo(list); // �e lista wynikowa r�wna jest li�cie po metodzie
        }

        [Fact]
        public void CanEditRecipeById() //  jeszcze co� bym tu zmieni�
        {
            //Arrangr
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
            mock.Setup(s => s.GetRecipeById(1)).Returns(recipe); //proper Id
            mock.Setup(s => s.UpdateRecipe(recipe));
            var manager = new RecipeManager(new MenuActionService(), mock.Object);
            //Act
            //object recipeToEdit =
            var editedRecipe = manager.EditRecipeById(1);
            //Assert
            mock.Verify(s => s.UpdateRecipe(recipe));// czy metoda zosta�a wywo�ana
            editedRecipe.Should().Be(recipe.Id); // czy do edycji zosta� wzi�ty przepis z id = 1 ?
            editedRecipe.Equals(recipe);
        }
    }
}