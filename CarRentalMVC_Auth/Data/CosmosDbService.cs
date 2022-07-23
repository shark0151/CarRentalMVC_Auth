namespace CarRentalMVC_Auth.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Fluent;
    using Microsoft.Extensions.Configuration;
    using CarRentalMVC_Auth.Models.Document;

    public class CosmosDbService : ICosmosDbService<Vehicle_Doc>, ICosmosDbService<Rental_Doc>, ICosmosDbService<User_Doc>, ICosmosDbService<Location_Doc>, ICosmosDbService<Insurance_Doc>
    {
        private Container _container;
        private Container _containerVeh;
        private Container _containerIns;
        private Container _containerUsr;
        private Container _containerLoc;
        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
            this._containerVeh = dbClient.GetContainer(databaseName, "Cars");
            this._containerIns = dbClient.GetContainer(databaseName, "Insurances");
            this._containerUsr = dbClient.GetContainer(databaseName, "Users");
            this._containerLoc = dbClient.GetContainer(databaseName, "Locations");
        }
        
        #region Vehicle

        async Task ICosmosDbService<Vehicle_Doc>.AddItemAsync(Vehicle_Doc item)
        {
            await this._container.CreateItemAsync<Vehicle_Doc>(item, new PartitionKey(item.Id));
        }

        async Task ICosmosDbService<Vehicle_Doc>.DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<Vehicle_Doc>(id, new PartitionKey(id));
            
        }

        async Task<Vehicle_Doc> ICosmosDbService<Vehicle_Doc>.GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Vehicle_Doc> response = await this._container.ReadItemAsync<Vehicle_Doc>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        async Task<IEnumerable<Vehicle_Doc>> ICosmosDbService<Vehicle_Doc>.GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Vehicle_Doc>(new QueryDefinition(queryString));
            List<Vehicle_Doc> results = new List<Vehicle_Doc>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        async Task ICosmosDbService<Vehicle_Doc>.UpdateItemAsync(string id, Vehicle_Doc item)
        {
            await this._container.UpsertItemAsync<Vehicle_Doc>(item, new PartitionKey(id));
        }

        #endregion

        #region Rental

        public async Task<string> RunProcedure()
        {
            var x = 
                await _container.Scripts.ExecuteStoredProcedureAsync<string>("GetVehiclesOvertime",
                    new PartitionKey("rental"), Array.Empty<dynamic>());
            return x.Resource;
        }

        async Task ICosmosDbService<Rental_Doc>.AddItemAsync(Rental_Doc item)
        {   
            //move this check to a trigger
            Vehicle_Doc veh = await this._containerVeh.ReadItemAsync<Vehicle_Doc>(item.vehicle.Id, new PartitionKey(item.vehicle.Id));
            Insurance_Doc ins = await this._containerIns.ReadItemAsync<Insurance_Doc>(item.insurance.Id, new PartitionKey(item.insurance.Id));
            User_Doc usr = await this._containerUsr.ReadItemAsync<User_Doc>(item.user.Id, new PartitionKey(item.user.Id));
            Location_Doc loc = await this._containerLoc.ReadItemAsync<Location_Doc>(item.drop_location.Id, new PartitionKey(item.drop_location.Id));
            Location_Doc loc2 = await this._containerLoc.ReadItemAsync<Location_Doc>(item.pick_location.Id, new PartitionKey(item.pick_location.Id));
            if (veh != null && ins != null && usr != null && loc != null && loc2 != null)
                await this._container.CreateItemAsync<Rental_Doc>(item, new PartitionKey(item.type), new ItemRequestOptions { PreTriggers = new List<string> { "TimeCheck" } });
            
        }

        async Task ICosmosDbService<Rental_Doc>.DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<Rental_Doc>(id, new PartitionKey("rental"));

        }

        async Task<Rental_Doc> ICosmosDbService<Rental_Doc>.GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Rental_Doc> response = await this._container.ReadItemAsync<Rental_Doc>(id, new PartitionKey("rental"));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        async Task<IEnumerable<Rental_Doc>> ICosmosDbService<Rental_Doc>.GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Rental_Doc>(new QueryDefinition(queryString));
            List<Rental_Doc> results = new List<Rental_Doc>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        async Task ICosmosDbService<Rental_Doc>.UpdateItemAsync(string id, Rental_Doc item)
        {
            await this._container.UpsertItemAsync<Rental_Doc>(item, new PartitionKey("rental"));
        }

        #endregion

        #region User

        async Task ICosmosDbService<User_Doc>.AddItemAsync(User_Doc item)
        {
            await this._container.CreateItemAsync<User_Doc>(item, new PartitionKey(item.Id));
        }

        async Task ICosmosDbService<User_Doc>.DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<User_Doc>(id, new PartitionKey(id));

        }

        async Task<User_Doc> ICosmosDbService<User_Doc>.GetItemAsync(string id)
        {
            try
            {
                ItemResponse<User_Doc> response = await this._container.ReadItemAsync<User_Doc>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        async Task<IEnumerable<User_Doc>> ICosmosDbService<User_Doc>.GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<User_Doc>(new QueryDefinition(queryString));
            List<User_Doc> results = new List<User_Doc>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        async Task ICosmosDbService<User_Doc>.UpdateItemAsync(string id, User_Doc item)
        {
            await this._container.UpsertItemAsync<User_Doc>(item, new PartitionKey(id));
        }

        #endregion

        #region Location

        async Task ICosmosDbService<Location_Doc>.AddItemAsync(Location_Doc item)
        {
            await this._container.CreateItemAsync<Location_Doc>(item, new PartitionKey(item.Id));
        }

        async Task ICosmosDbService<Location_Doc>.DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<Location_Doc>(id, new PartitionKey(id));

        }

        async Task<Location_Doc> ICosmosDbService<Location_Doc>.GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Location_Doc> response = await this._container.ReadItemAsync<Location_Doc>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        async Task<IEnumerable<Location_Doc>> ICosmosDbService<Location_Doc>.GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Location_Doc>(new QueryDefinition(queryString));
            List<Location_Doc> results = new List<Location_Doc>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        async Task ICosmosDbService<Location_Doc>.UpdateItemAsync(string id, Location_Doc item)
        {
            await this._container.UpsertItemAsync<Location_Doc>(item, new PartitionKey(id));
        }

        #endregion

        #region Insurance

        async Task ICosmosDbService<Insurance_Doc>.AddItemAsync(Insurance_Doc item)
        {
            await this._container.CreateItemAsync<Insurance_Doc>(item, new PartitionKey(item.Id));
        }

        async Task ICosmosDbService<Insurance_Doc>.DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<Insurance_Doc>(id, new PartitionKey(id));

        }

        async Task<Insurance_Doc> ICosmosDbService<Insurance_Doc>.GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Insurance_Doc> response = await this._container.ReadItemAsync<Insurance_Doc>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        async Task<IEnumerable<Insurance_Doc>> ICosmosDbService<Insurance_Doc>.GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Insurance_Doc>(new QueryDefinition(queryString));
            List<Insurance_Doc> results = new List<Insurance_Doc>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        async Task ICosmosDbService<Insurance_Doc>.UpdateItemAsync(string id, Insurance_Doc item)
        {
            await this._container.UpsertItemAsync<Insurance_Doc>(item, new PartitionKey(id));
        }

        #endregion
    }
}
