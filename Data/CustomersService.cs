using Cafe.Data;
using System.Text.Json;

namespace Cafe.Data
{
    public class CustomerService
    {

        public static List<Customers> GetAll()
        {
            string appUsersFilePath = Util.GetAppCustomerFilePath();
            if (!File.Exists(appUsersFilePath))
            {
                return new List<Customers>();
            }

            var json = File.ReadAllText(appUsersFilePath);

            return JsonSerializer.Deserialize<List<Customers>>(json);
        }


    }
}