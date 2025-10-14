using GymSystemDAL.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymSystemDAL.Data.DataSeed
{
    public class GymDBContextSeeding
    {
        public static bool SeedData(GymSystemDBContext context)
        {
			try
			{
                var HasPlans = context.Plans.Any();
                var HasCategories = context.Categories.Any();
                if (HasPlans && HasCategories) return false;
                if (!HasPlans)
                {
                    var Plans = LoadDataFromJsonFile<Entities.Plan>("Plans.json");
                    if (Plans.Any())
                    {
                        context.Plans.AddRange(Plans);
                       
                    }
                }
                if (!HasCategories)
                {
                    var Categories = LoadDataFromJsonFile<Entities.Category>("Categories.json");
                    if (Categories.Any())
                    {
                        context.Categories.AddRange(Categories);
                      
                    }
                }
                return context.SaveChanges() > 0;
              
            }
			catch (Exception)
			{

				return false;
			
            }
        }
        private static List<T> LoadDataFromJsonFile<T>(string FileName)
        {
          var filePath=Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FileName);
                
            if (!File.Exists(filePath)) throw new FileNotFoundException() ;
            string data = File.ReadAllText(filePath);
            var Options= new JsonSerializerOptions() { PropertyNameCaseInsensitive=true}; 
            return JsonSerializer.Deserialize<List<T>>(data, Options) ?? new List<T>();


        }


    }
}
