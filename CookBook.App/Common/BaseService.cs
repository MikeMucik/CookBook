using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.App.Abstract;
using CookBook.Domain.Common;
using Newtonsoft.Json;


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
            string serializeList = JsonConvert.SerializeObject(Recipes, Formatting.Indented);// to drugie by plik był łatwy do odczytu w txt
            return serializeList;
        }

        public void SaveDataFromListToJson(string serializedFormatJson)
        {
            string fileNamePath = @"C:\Temp\recipes.txt";
            if (File.Exists(fileNamePath))
            {
                File.WriteAllText(fileNamePath, serializedFormatJson);
            }
            else
            {
                Console.WriteLine("\n\rAn error occurred while trying to save");
            }            
        }

        public void ReadDataJsonToList()
        {
           string fileNamePath = @"C:\Temp\recipes.txt";
            string jsonString = File.ReadAllText(fileNamePath);
            List<T> deserializedRecipes = JsonConvert.DeserializeObject<List<T>>(jsonString);
            if (deserializedRecipes != null && deserializedRecipes.Count > 0)
            {
                Recipes = deserializedRecipes;
            }
            else
            {
                Console.WriteLine("Database not found !!");
                return;
            }            
        }       
    }   
}
