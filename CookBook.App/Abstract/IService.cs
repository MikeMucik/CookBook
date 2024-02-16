using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.App.Abstract
{
    public interface IService<T>
    {
        List<T> Recipes { get; set; }

        List<T> GetAllRecipes();
        T GetRecipeById(int id);
        int GetLastId();
        int AddRecipe(T recipe);
        int UpdateRecipe(T recipe);
        void RemoveRecipe(T recipe);
    }
}
