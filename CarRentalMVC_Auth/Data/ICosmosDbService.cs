namespace CarRentalMVC_Auth.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CarRentalMVC_Auth.Models.Document;
    using Models;

    public interface ICosmosDbService<T>
    {
        //Vehicle
        Task<IEnumerable<T>> GetItemsAsync(string query);
        Task<T> GetItemAsync(string id);
        Task AddItemAsync(T item);
        Task UpdateItemAsync(string id, T item);
        Task DeleteItemAsync(string id);
        
    }
}
