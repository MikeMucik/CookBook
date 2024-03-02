using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CookBook.App.Abstract;
using CookBook.Domain.Common;


namespace CookBook.App.Common
{
    public class BaseService<T> : IService<T> where T : BaseEntity
    {
        public List<T> Recipes { get; set; }
        public BaseService()
        {
            Recipes = new List<T>();
        }

        public int GetLastId()
        {
            int lastId;
            if (Recipes.Any())
            {
                lastId = Recipes.OrderBy(r => r.Id).LastOrDefault().Id;
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

        public T GetRecipeById(int id)
        {
            var entity = Recipes.FirstOrDefault(r => r.Id == id);

            return entity;
        }

        public int UpdateRecipe(T recipe)
        {
            var entity = Recipes.FirstOrDefault(r => r.Id == recipe.Id);
            if (entity != null)
            {
                entity = recipe;
            }
            return recipe.Id;
            //return entity != null ? entity.Id : 0;
        }

        public string SerializeListToStringInJson()
        {
            string serializeList = JsonSerializer.Serialize(Recipes , new JsonSerializerOptions { WriteIndented = true });
            return serializeList;
        }

        public void SaveDataFromListToJson(string serializedFormatJson)
        {            
            try
            {
                string fileName = @"C:\Temp\recipes.txt";              
                File.WriteAllText($@"{fileName}", serializedFormatJson);
            }
            catch (Exception e)
            {
                Console.WriteLine("Nastąpił błąd w zapisywaniu pliku");
            }
        }

        public void ReadDataJsonToList()
        {
            string fileName = @"C:\Temp\recipes.txt";
            try
            {
                string jsonString = File.ReadAllText(fileName);
                Recipes = JsonSerializer.Deserialize<List<T>>(jsonString)!;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Warning : Database file not found");
                return;
            }
        }       
    }   
}
