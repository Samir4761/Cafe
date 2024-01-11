using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cafe.Data
{
    internal class CoffeeService
    {
        private static List<CoffeeType> _coffeeTypesCache;

        private static string GetFilePath()
        {
            string appDataDirectoryPath = Util.GetAppDirectoryPath();
            return Path.Combine(appDataDirectoryPath, "CoffeeTypes.json");
        }

        private static void SaveAll()
        {
            var json = JsonSerializer.Serialize(_coffeeTypesCache);
            File.WriteAllText(GetFilePath(), json);
        }

        public static List<CoffeeType> GetAll()
        {
            if (_coffeeTypesCache != null)
            {
                return _coffeeTypesCache;
            }

            string appUsersfilePath = Util.GetAppCoffeeFilePath();
            if (!File.Exists(appUsersfilePath))
            {
                _coffeeTypesCache = new List<CoffeeType>();
                return _coffeeTypesCache;
            }

            var json = File.ReadAllText(appUsersfilePath);
            _coffeeTypesCache = JsonSerializer.Deserialize<List<CoffeeType>>(json) ?? new List<CoffeeType>();
            return _coffeeTypesCache;
        }

        public static void Add(CoffeeType coffeeType)
        {
            GetAll();
            _coffeeTypesCache.Add(coffeeType);
            SaveAll();
        }

        public static void Update(CoffeeType coffeeType)
        {
            GetAll();
            var index = _coffeeTypesCache.FindIndex(ct => ct.Id == coffeeType.Id);
            if (index != -1)
            {
                _coffeeTypesCache[index] = coffeeType;
                SaveAll();
            }
        }

        public static void Delete(Guid coffeeTypeId)
        {
            GetAll();
            var coffeeType = _coffeeTypesCache.FirstOrDefault(ct => ct.Id == coffeeTypeId);
            if (coffeeType != null)
            {
                _coffeeTypesCache.Remove(coffeeType);
                SaveAll();
            }
        }
    }

    public class CoffeeType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
