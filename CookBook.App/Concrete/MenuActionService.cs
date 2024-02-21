using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.App.Common;
using CookBook.Domain.Entity;

namespace CookBook.App.Concrete
{
    public class MenuActionService : BaseService<MenuAction>
    {
        public MenuActionService()
        {
            Initialize();
        }

        public List<MenuAction> GetMenuActionsByMenuName(string menuName)
        {
            List<MenuAction> result = new List<MenuAction>();
            foreach (var menuAction in Recipes)
            {
                if (menuAction.MenuName == menuName)
                {
                    result.Add(menuAction);
                }
            }
            return result;
        }
        public void Initialize()
        {
            AddRecipe(new MenuAction(1, "Add recipes", "Main"));
            AddRecipe(new MenuAction(2, "Remove recipe", "Main"));
            AddRecipe(new MenuAction(3, "Show recipe by id", "Main"));
            AddRecipe(new MenuAction(4, "Find recipes by ingredient", "Main"));
            AddRecipe(new MenuAction(5, "Find recipe by category", "Main"));
            AddRecipe(new MenuAction(6, "Edit recipe", "Main"));         
            AddRecipe(new MenuAction(7, "Exit program", "Main"));

            AddRecipe(new MenuAction(1, "Breakfast", "AddNewRecipeMenu"));
            AddRecipe(new MenuAction(2, "Lunch", "AddNewRecipeMenu"));
            AddRecipe(new MenuAction(3, "Dinner", "AddNewRecipeMenu"));
            AddRecipe(new MenuAction(4, "Soup", "AddNewRecipeMenu"));
            AddRecipe(new MenuAction(5, "Appetizer", "AddNewRecipeMenu"));
            AddRecipe(new MenuAction(6, "Drink", "AddNewRecipeMenu"));
            AddRecipe(new MenuAction(7, "Dessert", "AddNewRecipeMenu"));

            AddRecipe(new MenuAction(1, "Name", "KindOfData"));
            AddRecipe(new MenuAction(2, "Category", "KindOfData"));
            AddRecipe(new MenuAction(3, "Ingredients", "KindOfData"));
            AddRecipe(new MenuAction(4, "Description", "KindOfData"));
            AddRecipe(new MenuAction(5, "Preparation time", "KindOfData"));
            AddRecipe(new MenuAction(6, "Difficult", "KindOfData"));
            AddRecipe(new MenuAction(7, "Number of portions", "KindOfData"));
        }
    }
}
