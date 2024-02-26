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
        //[Fact]
        //public void CanGetRecipeById() // 
        //{
        //    //Arrange
        //    //Prepare data
        //    List<Ingredient> ingredients = new List<Ingredient>
        //    {
        //        new Ingredient("Egg", 3, "piece"),
        //        new Ingredient("Butter", 25, "grams"),
        //        new Ingredient("Salt", 2, "pinches")
        //    };
        //    Recipe recipe = new Recipe(1, "fried eggs", 1, ingredients, " ALe jaja", "15 minut", 1, 1);
        //    //Prepare rest
        //    var mock = new Mock<IService<Recipe>>();
        //    mock.Setup(s => s.GetRecipeById(1)).Returns(recipe);
        //    var manager = new RecipeManager(new MenuActionService(), mock.Object);
        //    //Act
        //    var returnedRecipe = manager.GetRecipeById(recipe.Id);
        //    //Assert
        //    //Assert.Equal(recipe, returnedRecipe);
        //    returnedRecipe.Should().BeOfType(typeof(Recipe));
        //    returnedRecipe.Should().NotBeNull();
        //    returnedRecipe.Should().BeSameAs(recipe);
        //}


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
            //var manager = new RecipeManager(new MenuActionService(), mock.Object);
            var recipeService = mock.Object;
            //Act
            //var returnedRecipe = manager.GetRecipeById(recipe.Id);
            var returnedRecipe = recipeService.GetRecipeById(recipe.Id);
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
            // var  baseService = new BaseService<Recipe>();
            // baseService.AddRecipe(recipe);
            var recipeService = new RecipeService();
            recipeService.AddRecipe(recipe);
            //Act
            // var returnedId = baseService.GetLastId();
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



        //[Fact]
        //public void CanDeleteRecipe()
        //{
        //    //Arrange
        //    //Prepare data
        //    List<Ingredient> ingredients = new List<Ingredient>
        //    {
        //        new Ingredient("Egg", 3, "piece"),
        //        new Ingredient("Butter", 25, "grams"),
        //        new Ingredient("Salt", 2, "pinches")
        //    };
        //    Recipe recipe = new Recipe(1, "fried eggs", 1, ingredients, " ALe jaja", "15 minut", 1, 1);
        //    //Prepare rest
        //    List<Recipe> list = new List<Recipe>();
        //    list.Add(recipe);
        //    var mock = new Mock<IService<Recipe>>();
        //    //mock.Setup(s => s.GetRecipeById(1)).Returns(recipe); //proper Id
        //    mock.Setup(s => s.RemoveRecipe(It.IsAny<Recipe>()));

        //    //var recipeService = new RecipeService();
        //    var recipeService = mock.Object;
        //    //Act
        //    recipeService.RemoveRecipe(recipe);
        //    //Assert
        //    mock.Verify(s => s.RemoveRecipe(recipe), Times.Once);
        //    list.Should().BeEmpty();
        //}

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
            // var manager = new RecipeManager(new MenuActionService(), mock.Object);
            var recipeService = mock.Object;
            //Act
            var returnedRecipeId = recipeService.AddRecipe(recipe);
            //var returnedRecipeId = manager.AddRecipeNew(recipe); // to jest int
            //Recipe returnedRecipe = recipeService.GetRecipeById(returnedRecipeId);
            //Assert
            Assert.Equal(recipe.Id, returnedRecipeId); // czy metoda zwraca to samo id co posiada obiekt mock
            mock.Verify(s => s.AddRecipe(recipe)); //czy metoda zosta³a wywo³ana
                                                   //                    Assert.Equal(recipe, returnedRecipe);
        }



        //[Fact]

        //public void CanGetAllRecipes() // to jest ok
        //{
        //    //Arrange
        //    //Prepare data
        //    List<Ingredient> ingredients = new List<Ingredient>
        //    {
        //        new Ingredient("Egg", 3, "pieces"),
        //        new Ingredient("Butter", 25, "grams"),
        //        new Ingredient("Salt", 2, "pinches")
        //    };
        //    Recipe recipe = new Recipe(1, "fried eggs", 1, ingredients, " ALe jaja", "15 minut", 1, 1);
        //    List<Ingredient> ingredients1 = new List<Ingredient>
        //    {
        //        new Ingredient("Bread", 2, "slices"),
        //        new Ingredient("Butter", 25, "grams"),
        //        new Ingredient("ham", 2, "slices")
        //    };
        //    Recipe recipe1 = new Recipe(2, "sandwich", 1, ingredients1, " Ale kanapka", "5 minut", 1, 1);
        //    List<Recipe> list = new List<Recipe>();
        //    list.Add(recipe1);
        //    list.Add(recipe);
        //    //Prepare rest
        //    var mock = new Mock<IService<Recipe>>();
        //    mock.Setup(s => s.GetAllRecipes()).Returns(list);
        //    var manager = new RecipeManager(new MenuActionService(), mock.Object);
        //    var recipeServis = new RecipeService();
        //    //recipeServis.AddRecipe(mock.Object);
        //    //Act
        //    var returnedRecipes = manager.GetAllRecipes();
        //   // var returnedRecipes = manager.GetAllRecipes();
        //    //Asser
        //    mock.Verify(s => s.GetAllRecipes()); // rozumiem to ¿e to czy wywo³a³o funkcje
        //    returnedRecipes.Should().BeOfType<List<Recipe>>(); // ¿e lista zwrócona przez metodê to lista przepisów
        //    returnedRecipes.Should().BeEquivalentTo(list); // ¿e lista wynikowa równa jest liœcie po metodzie
        //}

        //[Fact]
        //public void CanEditRecipeById() //  jeszcze coœ bym tu zmieni³
        //{
        //    //Arrangr
        //    //Prepare Data
        //    List<Ingredient> ingredients = new List<Ingredient>
        //    {
        //        new Ingredient("Egg", 3, "pieces"),
        //        new Ingredient("Butter", 25, "grams"),
        //        new Ingredient("Salt", 2, "pinches")
        //    };
        //    Recipe recipe = new Recipe(1, "fried eggs", 1, ingredients, " ALe jaja", "15 minut", 1, 1);
        //    //Prepare rest
        //    var mock = new Mock<IService<Recipe>>();
        //    mock.Setup(s => s.GetRecipeById(1)).Returns(recipe); //proper Id
        //    mock.Setup(s => s.UpdateRecipe(recipe));
        //    var manager = new RecipeManager(new MenuActionService(), mock.Object);
        //    //Act
        //    //object recipeToEdit =
        //    var editedRecipe = manager.EditRecipeById(1);
        //    //Assert
        //    mock.Verify(s => s.UpdateRecipe(recipe));// czy metoda zosta³a wywo³ana
        //    editedRecipe.Should().Be(recipe.Id); // czy do edycji zosta³ wziêty przepis z id = 1 ?
        //    editedRecipe.Equals(recipe);
        //}

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
            //var manager = new RecipeManager(new MenuActionService(), mock.Object);
            //var recipeServis = new RecipeService();
            var recipeService = mock.Object;
            //Act
            //object recipeToEdit =
            var editedRecipe = recipeService.UpdateRecipe(recipe);
            //var editedRecipe = manager.EditRecipe(recipe);
            //Assert
            mock.Verify(s => s.UpdateRecipe(recipe));// czy metoda zosta³a wywo³ana
            //editedRecipe.Should().Be(recipe.Id); // czy do edycji zosta³ wziêty przepis z id = 1 ?
            editedRecipe.Equals(recipe);
        }
    }
}