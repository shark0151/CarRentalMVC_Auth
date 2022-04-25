namespace CarRentalMVC_Auth.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface ICosmosDbService
    {
        Task<IEnumerable<Car>> GetItemsAsync(string query);
        Task<Car> GetItemAsync(string id);
        Task AddItemAsync(Car item);
        Task UpdateItemAsync(string id, Car item);
        Task DeleteItemAsync(string id);
    }
}
