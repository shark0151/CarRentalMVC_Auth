namespace CarRentalMVC_Auth.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Fluent;
    using Microsoft.Extensions.Configuration;

    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(Car item)
        {
            await this._container.CreateItemAsync<Car>(item, new PartitionKey(item.Id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<Car>(id, new PartitionKey(id));
        }

        public async Task<Car> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Car> response = await this._container.ReadItemAsync<Car>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<Car>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Car>(new QueryDefinition(queryString));
            List<Car> results = new List<Car>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, Car item)
        {
            await this._container.UpsertItemAsync<Car>(item, new PartitionKey(id));
        }
    }
}
