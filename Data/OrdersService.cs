using System.Text.Json;

namespace Cafe.Data
{
    public class OrderService
    {

        public static List<Orders> GetAll()
        {
            string appUsersFilePath = Util.GetAppOrderFilePath();
            if (!File.Exists(appUsersFilePath))
            {
                return new List<Orders>();
            }

            var json = File.ReadAllText(appUsersFilePath);

            return JsonSerializer.Deserialize<List<Orders>>(json);
        }



    }
}
