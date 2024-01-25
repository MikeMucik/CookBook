using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.App.Abstract;
using CookBook.Domain.Common;

namespace CookBook.App.Common
{
    public class BaseService<T> : IService<T> where T : BaseEntity

    {
        public List<T> Recipes { get ; set ; }
        public BaseService() 
        {
            Recipes = new List<T>();
        }
        public int GetLastId()
        {
            int lastId;
            if(Recipes.Any())
            {
                lastId = Recipes.OrderBy(p => p.Id).LastOrDefault().Id;
            }
            else
            {
                lastId = 0;
            }
            return lastId;
        }
          
        public int AddRecipe(T recipe)
        {
            Recipes.Add(recipe);
            return recipe.Id;
        }

        public List<T> GetAllRecipes()
        {
            return Recipes;
        }

        public void RemoveRecipe(T recipe)
        {
            Recipes.Remove(recipe);
        }

        public int UpdateRecipe(T recipe)
        {
            var entity = Recipes.Find(p => p.Id == recipe.Id);
            if (entity != null)
            {
                entity = recipe;
            }
            return entity.Id;
        }
        public T GetRecipeById(int id)
        {
            var entity = Recipes.FirstOrDefault(p => p.Id == id);
            return entity;
        }
    }
}
